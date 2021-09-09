using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Classes;
using FoodPlanner.Data;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Controllers
{
    public class ShoppingListsController : Controller
    {
        private readonly FoodPlannerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ShoppingListsController(FoodPlannerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FoodPlans
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: FoodPlans/Details/5
        public async Task<IActionResult> Details(string sortType)
        {
            // Get the current user and therefore the household
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var shoppingList = new ShoppingList();
            // Check if a pre-generated list exists
            if (ShoppingLists.HasHouseholdList(user.ActiveHouseholdId))
            {
                shoppingList = ShoppingLists.Household_ShoppingLists[user.ActiveHouseholdId];
            }

            // Set the sort type
            if (!String.IsNullOrWhiteSpace(sortType))
            {
                if (Enum.TryParse(sortType, true, out ShoppingListSortType shoppingListSortType))
                {
                    shoppingList.SortType = shoppingListSortType;
                }
            }

            return View(shoppingList);
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
                // Get the current user and therefore the household
                var user = GetCurrentUserAsync().Result;
                if (user == null)
                {
                    return false;
                }

                // Set expected shop day
                var shopDate = DateTime.Now.Date;

                // Get all future foodplans which haven't been shopped yet
                var foodPlans = _context.FoodPlans
                    .Where(fp => fp.HouseholdId == user.ActiveHouseholdId)
                    .Include(fp => fp.ShopTrip)
                    .Where(fp => fp.Date.Date >= DateTime.Now.Date && fp.Date.Date <= shopDate.AddDays(days - 1).Date && fp.ShopTrip == null)
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
                        // Add the foodplanid for this product temporarily
                        product.TempFoodPlan = foodPlan;
                        // Convert to standard units and add item
                        shopItems.Add(MeasurementUnit.ConvertToStandardUnits(product));
                    }

                    // Add recipe ingredients
                    foreach (var recipe in foodPlan.Recipes)
                    {
                        // Add product for each ingredient
                        for (int i = 0; i < recipe.Recipe.Ingredients.Count(); i ++)//foreach (var ingredient in recipe.Recipe.Ingredients)
                        {
                            // get ingredient
                            var ingredient = new Ingredient(recipe.Recipe.Ingredients[i]);

                            // Check portions and adjust quantity for 2 people
                            if (recipe.Recipe.Portions > 0)
                            {
                                float portions = recipe.Recipe.Portions;
                                float householdSize = 2;
                                float portionRatio = (householdSize / portions);
                                ingredient.Quantity = ingredient.Quantity * portionRatio;
                            }

                            // Add the foodplanid for this ingredient temporarily
                            ingredient.TempFoodPlan = foodPlan;
                            // Convert to standard units and add item
                            shopItems.Add(MeasurementUnit.ConvertToStandardUnits(ingredient));
                        }
                    }
                }

                // Combine same products
                shopItems = MeasurementUnit.CombineShopItems(shopItems);

                var shoppingList = new ShoppingList();
                // Check if list has already been made before
                if (ShoppingLists.HasHouseholdList(user.ActiveHouseholdId))
                {
                    shoppingList = ShoppingLists.Household_ShoppingLists[user.ActiveHouseholdId];
                }
                // Check old shopping list marked items and then set new shopping list marked items
                if (shoppingList.ShopItems != null)
                {
                    shopItems = shoppingList.CopyMarkedItems(shopItems, shoppingList.ShopItems);
                }

                shoppingList.ShopItems = shopItems;
                shoppingList.ShoppingListSize = days;
                shoppingList.ExpectedShopDate = shopDate;

                // Add shopping list to temporary storage class ShoppingLists
                ShoppingLists.Household_ShoppingLists[user.ActiveHouseholdId] = shoppingList;

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

            // Get the current user and therefore the household
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return NotFound();
            }
            if (ShoppingLists.HasHouseholdList(user.ActiveHouseholdId))
            {
                var shoppingList = ShoppingLists.Household_ShoppingLists[user.ActiveHouseholdId];

                var shopItems = shoppingList.ShopItems.Where(si => si.ProductId == product_id && si.Unit.ToString() == unitString).ToList();

                foreach (var shopItem in shopItems)
                {
                    shopItem.Bought = !shopItem.Bought;
                }

                return Ok();
            }

            return NotFound();
        }

        public IActionResult ResetShoppingListItems()
        {
            // Get the current user and therefore the household
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return NotFound();
            }
            if (ShoppingLists.HasHouseholdList(user.ActiveHouseholdId))
            {
                var shoppingList = ShoppingLists.Household_ShoppingLists[user.ActiveHouseholdId];

                if (shoppingList.ResetShopItems())
                {
                    return RedirectToAction(nameof(Details));
                }
            }

            return BadRequest();
        }
        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            return user;
        }
    }
}
