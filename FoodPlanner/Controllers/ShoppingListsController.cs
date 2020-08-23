using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Classes;
using FoodPlanner.Data;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task<IActionResult> Details()
        {
            // DEPRECATED - Only regenerate now on command
            // Check if correct list already generated, if not then re-generate
            //if (Days.Value != ShoppingList.ShoppingListSize)
            //{
            //    GenerateShoppingList(Days.Value);
            //}

            return View(ShoppingList.ShopItems);
        }

        public IActionResult RegenerateList(int? Days)
        {
            if (Days != null && GenerateShoppingList(Days.Value))
            {
                return RedirectToAction(nameof(Details));
            }

            return NotFound();
        }

        public bool GenerateShoppingList(int days)
        {
            try
            {
                // Set expected shop day
                var shopDay = DayOfWeek.Sunday;
                int start = (int)DateTime.Now.DayOfWeek;
                int target = (int)shopDay;
                if (target <= start)
                    target += days;
                var shopDate = DateTime.Now.AddDays(target - start).Date;

                // Get all future foodplans which haven't been shopped yet
                var foodPlans = _context.FoodPlan
                    .Include(fp => fp.ShopTrip)
                    .Where(fp => fp.Date.Date >= DateTime.Now.Date && fp.Date.Date <= shopDate.AddDays(days).Date && fp.ShopTrip == null)
                    .Include(fp => fp.Products)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.ProductType)
                    .Include(fp => fp.Products)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Category)
                    .Include(fp => fp.Recipes)
                        .ThenInclude(r => r.Recipe)
                            .ThenInclude(r => r.Ingredients)
                                .ThenInclude(i => i.Product)
                                    .ThenInclude(p => p.ProductType)
                    .Include(fp => fp.Recipes)
                        .ThenInclude(r => r.Recipe)
                            .ThenInclude(r => r.Ingredients)
                                .ThenInclude(i => i.Product)
                                    .ThenInclude(p => p.Category)
                    .ToList();

                // Initialise Shop Items
                List<ShopItem> shopItems = new List<ShopItem>();

                // Add shop items for each unshopped day
                foreach (var foodPlan in foodPlans)
                {
                    // Add food plan products
                    foreach (var product in foodPlan.Products)
                    {
                        // Convert to standard units and add item
                        shopItems.Add(MeasurementUnit.ConvertToStandardUnits(product));
                    }

                    // Add recipe ingredients
                    foreach (var recipe in foodPlan.Recipes)
                    {
                        // Add product for each ingredient
                        foreach (var ingredient in recipe.Recipe.Ingredients)
                        {
                            // Check portions and adjust quantity for 2 people
                            if (recipe.Recipe.Portions > 0)
                            {
                                float portions = recipe.Recipe.Portions;
                                float householdSize = 2;
                                float portionRatio = (householdSize / portions);
                                ingredient.Quantity = ingredient.Quantity * portionRatio;
                            }

                            // Convert to standard units and add item
                            shopItems.Add(MeasurementUnit.ConvertToStandardUnits(ingredient));
                        }
                    }
                }

                // Combine same products
                shopItems = MeasurementUnit.CombineShopItems(shopItems);

                // Check old shopping list marked items and then set new shopping list marked items
                if (ShoppingList.ShopItems != null)
                {
                    shopItems = ShoppingList.CopyMarkedItems(shopItems, ShoppingList.ShopItems);
                }

                ShoppingList.ShopItems = shopItems;
                ShoppingList.ShoppingListSize = days;
                ShoppingList.ExpectedShopDate = shopDate;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult ToggleShopItem(int? product_id, string unitString)
        {
            if (product_id == null)
            {
                return NotFound();
            }

            var shopItems = ShoppingList.ShopItems.Where(si => si.ProductId == product_id && si.Unit.ToString() == unitString).ToList();

            foreach (var shopItem in shopItems)
            {
                shopItem.Bought = !shopItem.Bought;
            }

            return Ok();
        }

        public IActionResult ResetShoppingListItems()
        {
            if (ShoppingList.ResetShopItems())
            {
                return RedirectToAction(nameof(Details));
            }

            return BadRequest();
        }
    }
}
