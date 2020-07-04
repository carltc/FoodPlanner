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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodPlan == null)
            {
                return NotFound();
            }

            return View(foodPlan);
        }

        // GET: FoodPlans/Create
        public IActionResult Create()
        {// Get products and categories
            var products = _context.Product
                .Include(p => p.Category)
                .Select(p => new
                {
                    Id = p.Id,
                    FullName = p.Category.Name + " (" + p.Name + ")"
                });

            ViewData["RecipeId"] = new SelectList(_context.Recipe, "Id", "Name");
            ViewData["ProductId"] = new SelectList(products, "Id", "FullName");

            return View();
        }

        // POST: FoodPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PlanStart")] FoodPlan foodPlan, List<int> RecipeIds, List<int> ProductIds, List<double> Quantities, List<Unit> Units)
        {
            if (ModelState.IsValid)
            {
                // Initialise ingredients
                foodPlan.Products = new List<FoodPlanProduct>();
                foodPlan.Recipes = new List<FoodPlanRecipe>();

                // Add ingredients
                //foreach(int productId in ProductIds)
                for (int i = 0; i < ProductIds.Count; i++)
                {
                    var product = _context.Product.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
                    var foodPlanProduct = new FoodPlanProduct()
                    {
                        Name = product.Name,
                        ProductId = product.Id,
                        Product = product,
                        FoodPlanId = foodPlan.Id,
                        FoodPlan = foodPlan,
                        Quantity = Quantities[i],
                        Unit = Units[i]
                    };
                    foodPlan.Products.Add(foodPlanProduct);
                }

                // Add Recipes
                for (int i = 0; i < RecipeIds.Count; i++)
                {
                    var recipe = _context.Recipe.Where(r => r.Id == RecipeIds[i]).FirstOrDefault();
                    var foodPlanRecipe = new FoodPlanRecipe()
                    {
                        RecipeId = recipe.Id,
                        Recipe = recipe,
                        FoodPlanId = foodPlan.Id,
                        FoodPlan = foodPlan
                    };
                    foodPlan.Recipes.Add(foodPlanRecipe);
                }

                _context.Add(foodPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodPlan);
        }

        // GET: FoodPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _context.Product
                .Include(p => p.Category)
                .Select(p => new
                {
                    Id = p.Id,
                    FullName = p.Category.Name + " (" + p.Name + ")"
                });

            ViewData["RecipeId"] = new SelectList(_context.Recipe, "Id", "Name");
            ViewData["ProductId"] = new SelectList(products, "Id", "FullName");

            var foodPlan = _context.FoodPlan
                .Where(fp => fp.Id == id)
                .Include(fp => fp.Recipes)
                    .ThenInclude(r => r.Recipe)
                .Include(fp => fp.Products)
                    .ThenInclude(p => p.Product)
                .FirstOrDefault();
            if (foodPlan == null)
            {
                return NotFound();
            }
            return View(foodPlan);
        }

        // POST: FoodPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PlanStart")] FoodPlan foodPlan, List<int> RecipeIds, List<int> ProductIds, List<double> Quantities, List<Unit> Units)
        {
            if (id != foodPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Initialise ingredients
                    foodPlan.Products = new List<FoodPlanProduct>();
                    foodPlan.Recipes = new List<FoodPlanRecipe>();

                    // Add ingredients
                    //foreach(int productId in ProductIds)
                    for (int i = 0; i < ProductIds.Count; i++)
                    {
                        var product = _context.Product.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
                        var foodPlanProduct = new FoodPlanProduct()
                        {
                            Name = product.Name,
                            ProductId = product.Id,
                            Product = product,
                            FoodPlanId = foodPlan.Id,
                            FoodPlan = foodPlan,
                            Quantity = Quantities[i],
                            Unit = Units[i]
                        };
                        foodPlan.Products.Add(foodPlanProduct);
                    }

                    // Add Recipes
                    for (int i = 0; i < RecipeIds.Count; i++)
                    {
                        var recipe = _context.Recipe.Where(r => r.Id == RecipeIds[i]).FirstOrDefault();
                        var foodPlanRecipe = new FoodPlanRecipe()
                        {
                            RecipeId = recipe.Id,
                            Recipe = recipe,
                            FoodPlanId = foodPlan.Id,
                            FoodPlan = foodPlan
                        };
                        foodPlan.Recipes.Add(foodPlanRecipe);
                    }

                    _context.Update(foodPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodPlanExists(foodPlan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodPlan);
        }

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
    }
}
