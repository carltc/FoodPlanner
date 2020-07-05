using System;
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

namespace FoodPlanner.Controllers
{
    public class FoodPlansController : Controller
    {
        private readonly FoodPlannerContext _context;

        public FoodPlansController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: FoodPlans
        public async Task<IActionResult> Index(int? Days)
        {
            // Set days to 14 if not set
            if (Days == null)
            {
                Days = 14;
            }    

            // get current date
            var dateNow = DateTime.Now;
            var endDate = dateNow.AddDays(14);

            // Create a list of empty foodplans for this date range
            var foodPlans = new List<FoodPlan>();
            for (int i = 0; i < Days; i++)
            {
                foodPlans.Add(new FoodPlan(dateNow.AddDays(i)));
            }

            // Get upcoming foodplans
            var currentFoodPlans = _context.FoodPlan
                .Where(fp => fp.Date >= dateNow && fp.Date <= endDate)
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodPlan == null)
            {
                return NotFound();
            }

            return View(foodPlan);
        }

        // GET: FoodPlans/Create
        public IActionResult Create(long? foodplandate)
        {
            if (foodplandate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Reconstruct long date
            DateTime dateTime = DateTime.FromBinary(foodplandate.Value);

            // Get foodplan to add to
            var foodplan = new FoodPlan(dateTime);

            if (foodplan == null)
            {
                return RedirectToAction(nameof(Create));
            }

            PopulateRecipeAndProductLists();

            return View(foodplan);
        }

        // GET: FoodPlans/Add
        public IActionResult Add(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Create));
            }

            // Get foodplan to add to
            var foodplan = _context.FoodPlan.Where(fp => fp.Id == id).FirstOrDefault();

            if (foodplan == null)
            {
                return RedirectToAction(nameof(Create));
            }

            PopulateRecipeAndProductLists();

            return View(foodplan);
        }

        // POST: FoodPlans/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,Date")] FoodPlan foodPlan, List<int> RecipeIds, List<Meal> RecipeMeals, List<int> ProductIds, List<Meal> ProductMeals, List<double> Quantities, List<Unit> Units)
        {
            if (ModelState.IsValid)
            {
                // Get full foodplan that is being added or use new if it doesn't exist yet
                FoodPlan fullFoodPlan;
                bool newFoodPlan;
                if (_context.FoodPlan.Find(foodPlan.Id) == null)
                {
                    fullFoodPlan = foodPlan;
                    fullFoodPlan.Recipes = new List<FoodPlanRecipe>(); // initiate new list of recipes
                    fullFoodPlan.Products = new List<FoodPlanProduct>(); // initiate new list of products
                    newFoodPlan = true;
                }
                else
                {
                    fullFoodPlan = _context.FoodPlan.Where(fp => fp.Id == foodPlan.Id)
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
                        var product = _context.Product.Where(p => p.Id == ProductIds[i]).Include(p => p.ProductType).FirstOrDefault();
                        var foodPlanProduct = new FoodPlanProduct()
                        {
                            Name = $"{product.Name} {product.ProductType.Name}",
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
                        var recipe = _context.Recipe.Where(r => r.Id == RecipeIds[i]).FirstOrDefault();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodPlan = await _context.FoodPlan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodPlan == null)
            {
                return NotFound();
            }

            return View(foodPlan);
        }

        // POST: FoodPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodPlan = await _context.FoodPlan.FindAsync(id);
            _context.FoodPlan.Remove(foodPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodPlanExists(int id)
        {
            return _context.FoodPlan.Any(e => e.Id == id);
        }

        private void PopulateRecipeAndProductLists()
        {
            // Get products
            var products = _context.Product
                .Include(p => p.ProductType)
                .Select(p => new
                {
                    Id = p.Id,
                    FullName = $"{p.Name} {p.ProductType.Name}"
                }).ToList(); // Create product names as combination of product and product type
            // Add a blank one for none
            products.Add(new
            {
                Id = 0,
                FullName = "None"
            });
            var productList = new SelectList(products, "Id", "FullName"); // Create a select list from products
            productList.Where(pl => pl.Value == "0").FirstOrDefault().Selected = true; // Set the blank one as default value
            ViewData["ProductId"] = productList; // Send it to client viewbag

            // Get recipes
            var recipes = _context.Recipe.ToList();
            // Add a blank one for none
            recipes.Add(new Recipe() { Name = "None", Id = 0 });
            var recipeList = new SelectList(recipes, "Id", "Name"); // Create a select list from recipes
            recipeList.Where(pl => pl.Value == "0").FirstOrDefault().Selected = true; // Set the blank one as default value
            ViewData["RecipeId"] = recipeList; // Send it to client viewbag
        }
    }
}
