﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Data;
using FoodPlanner.Models;
using FoodPlanner.Classes;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FoodPlanner.Controllers
{
    public class FoodPlansController : Controller
    {
        private readonly FoodPlannerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FoodPlansController(FoodPlannerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FoodPlans
        public async Task<IActionResult> Index(int? Days)
        {
            // Set days to 14 if not set
            if (Days == null)
            {
                Days = 14;
            }

            ViewData["Days"] = Days;

            // get current date
            var dateNow = DateTime.Now;
            var endDate = dateNow.AddDays(Days.Value);

            // Get the current user
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Check if user has a household selected
            if (user.ActiveHouseholdId == 0)
            {
                // Set ActiveHousehold Id
                user.SetActiveHouseholdId(_context);

                 // Check if user still doesn't have a household
                if (user.ActiveHouseholdId == 0)
                {
                    return View();
                }
            }

            // Create a list of empty foodplans for this date range
            var foodPlans = new List<FoodPlan>();
            for (int i = 0; i < Days; i++)
            {
                foodPlans.Add(new FoodPlan(dateNow.AddDays(i), user.ActiveHouseholdId));
            }

            // Get upcoming foodplans for this users household
            var currentFoodPlans = _context.FoodPlans
                .Where(
                    fp => fp.Date.Date >= dateNow.Date && 
                    fp.Date.Date <= endDate.Date &&
                    fp.HouseholdId == user.ActiveHouseholdId
                )
                .Include(fp => fp.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(fp => fp.Recipes)
                    .ThenInclude(r => r.Recipe)
                .ToList();

            // Add all foodplans to food plan
            foreach (var foodplan in currentFoodPlans)
            {
                var matchIndex = foodPlans.IndexOf(foodPlans.Where(fp => fp.Date.Date == foodplan.Date.Date).FirstOrDefault());
                if (matchIndex != -1)
                {
                    foodPlans[matchIndex] = foodplan;
                }
            }

            return View(foodPlans);
        }

        // GET: FoodPlans
        public async Task<IActionResult> List()
        {
            return View(await _context.FoodPlans.ToListAsync());
        }

        // GET: FoodPlans/Details/5
        public async Task<IActionResult> Details(int? id, long? foodplandate, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Check if this foodplan id == 0, if so then this foodplan doesn't exist yet and needs to be made
            if (id == 0)
            {
                return RedirectToAction(nameof(Create), new { foodplandate = foodplandate, returnUrl = returnUrl });
            }

            var foodPlan = _context.FoodPlans
                .Where(fp => fp.Id == id)
                .Include(fp => fp.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(fp => fp.Recipes)
                    .ThenInclude(r => r.Recipe)
                .FirstOrDefault();

            if (foodPlan == null)
            {
                return NotFound();
            }

            if (returnUrl != null)
            {
                ViewData["returnUrl"] = returnUrl;
            }

            return View(foodPlan);
        }

        // GET: FoodPlans/Create
        public IActionResult Create(long? foodplandate, string mealType, bool menu = false, string returnUrl = null)
        {
            if (foodplandate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Reconstruct long date
            DateTime dateTime = DateTime.FromBinary(foodplandate.Value);

            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Get foodplan to add to
            var foodplan = new FoodPlan(dateTime, user.ActiveHouseholdId);

            if (foodplan == null)
            {
                return RedirectToAction(nameof(Create));
            }

            _context.Add(foodplan);
            _context.SaveChanges();

            // Get newly created foodplan
            var newFoodPlan = _context.FoodPlans.Where(fp => fp.Name == foodplan.Name && fp.Date == foodplan.Date && fp.HouseholdId == user.ActiveHouseholdId).FirstOrDefault();

            if (menu)
            {
                return RedirectToAction(nameof(Menu), new { id = newFoodPlan.Id, mealType = mealType, returnUrl = returnUrl });
            }
            return RedirectToAction(nameof(Details), new { id = newFoodPlan.Id, returnUrl = returnUrl });
        }

        // GET: FoodPlans/Add
        //public IActionResult Add(int? id, long? foodplandate, string mealType, string returnUrl = null)
        //{
        //    if (id == 0)
        //    {
        //        return RedirectToAction(nameof(Create), new { foodplandate = foodplandate, mealType = mealType, returnUrl = returnUrl });
        //    }

        //    // Get foodplan to add to
        //    var foodplan = _context.FoodPlans.Where(fp => fp.Id == id).FirstOrDefault();

        //    if (foodplan == null)
        //    {
        //        return RedirectToAction(nameof(Create));
        //    }

        //    if (mealType != null)
        //    {
        //        Meal selectedMeal;
        //        if (Enum.TryParse(mealType, true, out selectedMeal))
        //        {
        //            ViewData["MealType"] = selectedMeal;
        //        }
        //    }

        //    PopulateRecipeAndProductLists();

        //    if (returnUrl != null)
        //    {
        //        ViewData["returnUrl"] = returnUrl;
        //    }
        //    return View(foodplan);
        //}

        // GET: FoodPlans/Menu
        public IActionResult Menu(int? id, long? foodplandate, string mealType, string returnUrl = null)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Create), new { foodplandate = foodplandate, mealType = mealType, menu = true, returnUrl = returnUrl });
            }

            // Get foodplan to add to
            var foodplan = _context.FoodPlans
                .Where(fp => fp.Id == id)
                .FirstOrDefault();

            if (foodplan == null || mealType == null)
            {
                return BadRequest();
            }

            // Set meal type
            Meal selectedMeal;
            if (Enum.TryParse(mealType, true, out selectedMeal))
            {
                ViewData["MealType"] = selectedMeal;
            }

            PopulateRecipeAndProductLists();

            if (returnUrl != null)
            {
                ViewData["returnUrl"] = returnUrl;
            }
            return View(foodplan);
        }

        // POST: FoodPlans/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,Date")] FoodPlan foodPlan, List<int> RecipeIds, List<Meal> RecipeMeals, List<int> ProductIds, List<Meal> ProductMeals, List<double> Quantities, List<Unit> Units, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get full foodplan that is being added or use new if it doesn't exist yet
                FoodPlan fullFoodPlan;
                bool newFoodPlan;
                if (_context.FoodPlans.Find(foodPlan.Id) == null)
                {
                    fullFoodPlan = foodPlan;
                    fullFoodPlan.Recipes = new List<FoodPlanRecipe>(); // initiate new list of recipes
                    fullFoodPlan.Products = new List<FoodPlanProduct>(); // initiate new list of products
                    newFoodPlan = true;
                }
                else
                {
                    fullFoodPlan = _context.FoodPlans.Where(fp => fp.Id == foodPlan.Id)
                        .Include(fp => fp.Products)
                        .Include(fp => fp.Recipes)
                        .FirstOrDefault();
                    newFoodPlan = false;
                }

                // Add ingredients
                //foreach(int productId in ProductIds)
                for (int i = 0; i < ProductIds.Count; i++)
                {
                    if (ProductIds[i] != 0) // Check if product Id is 0, which is the None type
                    {
                        var product = _context.Products.Where(p => p.Id == ProductIds[i]).Include(p => p.ProductType).FirstOrDefault();
                        var foodPlanProduct = new FoodPlanProduct()
                        {
                            ProductId = product.Id,
                            Product = product,
                            FoodPlanId = fullFoodPlan.Id,
                            FoodPlan = fullFoodPlan,
                            Quantity = Quantities[i],
                            Unit = Units[i],
                            Meal = ProductMeals[i]
                        };
                        fullFoodPlan.Products.Add(foodPlanProduct);
                    }
                }

                // Add Recipes
                for (int i = 0; i < RecipeIds.Count; i++)
                {
                    if (RecipeIds[i] != 0) // Check if recipe Id is 0, which is the None type
                    {
                        var recipe = _context.Recipes.Where(r => r.Id == RecipeIds[i]).FirstOrDefault();
                        var foodPlanRecipe = new FoodPlanRecipe()
                        {
                            RecipeId = recipe.Id,
                            Recipe = recipe,
                            FoodPlanId = fullFoodPlan.Id,
                            FoodPlan = fullFoodPlan,
                            Meal = RecipeMeals[i]
                        };
                        fullFoodPlan.Recipes.Add(foodPlanRecipe);
                    }
                }

                // Check if food plan is new and add if it is otherwise update existing
                if (newFoodPlan)
                {
                    _context.Add(fullFoodPlan);
                }
                else
                {
                    _context.Update(fullFoodPlan);
                }

                await _context.SaveChangesAsync();

                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(foodPlan);
        }

        //// GET: FoodPlans/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var products = _context.Product
        //        .Include(p => p.Category)
        //        .Select(p => new
        //        {
        //            Id = p.Id,
        //            FullName = p.Category.Name + " (" + p.Name + ")"
        //        });

        //    ViewData["RecipeId"] = new SelectList(_context.Recipe, "Id", "Name");
        //    ViewData["ProductId"] = new SelectList(products, "Id", "FullName");

        //    var foodPlan = _context.FoodPlan
        //        .Where(fp => fp.Id == id)
        //        .Include(fp => fp.Recipes)
        //            .ThenInclude(r => r.Recipe)
        //        .Include(fp => fp.Products)
        //            .ThenInclude(p => p.Product)
        //        .FirstOrDefault();
        //    if (foodPlan == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(foodPlan);
        //}

        //// POST: FoodPlans/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date")] FoodPlan foodPlan, List<int> RecipeIds, List<int> ProductIds, List<double> Quantities, List<Unit> Units)
        //{
        //    if (id != foodPlan.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Initialise ingredients
        //            foodPlan.Products = new List<FoodPlanProduct>();
        //            foodPlan.Recipes = new List<FoodPlanRecipe>();

        //            // Add ingredients
        //            //foreach(int productId in ProductIds)
        //            for (int i = 0; i < ProductIds.Count; i++)
        //            {
        //                var product = _context.Product.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
        //                var foodPlanProduct = new FoodPlanProduct()
        //                {
        //                    Name = product.Name,
        //                    ProductId = product.Id,
        //                    Product = product,
        //                    FoodPlanId = foodPlan.Id,
        //                    FoodPlan = foodPlan,
        //                    Quantity = Quantities[i],
        //                    Unit = Units[i]
        //                };
        //                foodPlan.Products.Add(foodPlanProduct);
        //            }

        //            // Add Recipes
        //            for (int i = 0; i < RecipeIds.Count; i++)
        //            {
        //                var recipe = _context.Recipe.Where(r => r.Id == RecipeIds[i]).FirstOrDefault();
        //                var foodPlanRecipe = new FoodPlanRecipe()
        //                {
        //                    RecipeId = recipe.Id,
        //                    Recipe = recipe,
        //                    FoodPlanId = foodPlan.Id,
        //                    FoodPlan = foodPlan
        //                };
        //                foodPlan.Recipes.Add(foodPlanRecipe);
        //            }

        //            _context.Update(foodPlan);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FoodPlanExists(foodPlan.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(foodPlan);
        //}

        // GET: FoodPlans/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodPlan = await _context.FoodPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodPlan == null)
            {
                return NotFound();
            }

            return View(foodPlan);
        }

        // POST: FoodPlans/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodPlan = await _context.FoodPlans.FindAsync(id);
            _context.FoodPlans.Remove(foodPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: FoodPlans/DeleteItem/5
        public async Task<IActionResult> DeleteItem(int? foodplan_product_id, int? foodplan_recipe_id, string returnUrl = null)
        {
            if (foodplan_product_id != null)
            {
                var foodplan_product = await _context.ShopItems.FindAsync(foodplan_product_id);
                _context.ShopItems.Remove(foodplan_product);
                await _context.SaveChangesAsync();

                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }
            else if (foodplan_recipe_id != null)
            {
                var foodplan_recipe = await _context.FoodPlanRecipes.FindAsync(foodplan_recipe_id);
                _context.FoodPlanRecipes.Remove(foodplan_recipe);
                await _context.SaveChangesAsync();

                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool FoodPlanExists(int id)
        {
            return _context.FoodPlans.Any(e => e.Id == id);
        }

        private void PopulateRecipeAndProductLists()
        {
            // Get recipes
            var recipes = _context.Recipes
                .Include(r => r.Cuisine)
                .OrderBy(r => r.Cuisine.Name)
                .ToList();
            ViewData["Recipes"] = recipes;

            // Add a blank one for none
            recipes.Add(new Recipe() { Name = "None", Id = 0 });
            var recipeList = new SelectList(recipes, "Id", "Name"); // Create a select list from recipes
            recipeList.Where(pl => pl.Value == "0").FirstOrDefault().Selected = true; // Set the blank one as default value
            ViewData["RecipeId"] = recipeList; // Send it to client viewbag

            // Get Products
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductType)
                .OrderBy(p => p.Category.Name)
                .ToList();
            ViewData["Products"] = products;
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            return user;
        }
    }
}
