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
            return View();
        }

        // GET: FoodPlans/Details/5
        public async Task<IActionResult> Details(int? Days)
        {
            if (Days == null)
            {
                return NotFound();
            }

            // Set expected shop day
            var shopDay = DayOfWeek.Sunday;
            int start = (int)DateTime.Now.DayOfWeek;
            int target = (int)shopDay;
            if (target <= start)
                target += 7;
            var shopDate = DateTime.Now.AddDays(target - start);

            // Get all future foodplans which haven't been shopped yet
            var foodPlans = _context.FoodPlan
                .Include(fp => fp.ShopTrip)
                .Where(fp => fp.Date >= DateTime.Now && fp.Date <= shopDate.AddDays(Days.Value) && fp.ShopTrip == null)
                .Include(fp => fp.Products)
                    .ThenInclude(p => p.Product)
                .Include(fp => fp.Recipes)
                    .ThenInclude(r => r.Recipe)
                        .ThenInclude(r => r.Ingredients)
                .ToList();

            // Initialise Shop Items
            List<ShopItem> shopItems = new List<ShopItem>();

            foreach (var foodPlan in foodPlans)
            {
                // Add food plan products
                foreach (var product in foodPlan.Products)
                {
                    shopItems.Add((ShopItem)product);
                }

                // Add recipe ingredients
                foreach (var recipe in foodPlan.Recipes)
                {
                    foreach (var ingredient in recipe.Recipe.Ingredients)
                    {
                        shopItems.Add((ShopItem)ingredient);
                    }
                }
            }

            return View(shopItems);
        }
    }
}
