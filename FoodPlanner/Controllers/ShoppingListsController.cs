using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Data;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Controllers
{
    public class ShoppingListsController : Controller
    {
        private readonly FoodPlannerContext _context;

        public ShoppingListsController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: FoodPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodPlan.ToListAsync());
        }

        // GET: FoodPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodPlan = await _context.FoodPlan
                .Include(fp => fp.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(fp => fp.Recipes)
                    .ThenInclude(r => r.Recipe)
                        .ThenInclude(r => r.Ingredients)
                            .ThenInclude(i => i.Product)
                                .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodPlan == null)
            {
                return NotFound();
            }

            // Initialise Shop Items
            List<ShopItem> shopItems = new List<ShopItem>();

            // Add food plan products
            foreach(var product in foodPlan.Products)
            {
                shopItems.Add((ShopItem)product);
            }

            // Add recipe ingredients
            foreach(var recipe in foodPlan.Recipes)
            {
                foreach(var ingredient in recipe.Recipe.Ingredients)
                {
                    shopItems.Add((ShopItem)ingredient);
                }
            }

            return View(shopItems);
        }
    }
}
