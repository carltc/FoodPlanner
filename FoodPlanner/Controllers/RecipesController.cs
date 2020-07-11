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
    public class RecipesController : Controller
    {
        private readonly FoodPlannerContext _context;

        public RecipesController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipe.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            var products = _context.Product
                .Include(p => p.Category)
                .Include(p => p.ProductType)
                .ToList();
            ViewData["Products"] = products;

            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Recipe recipe, List<int> ProductIds, List<double> Quantities, List<Unit> Units, string add_ingredient)
        {
            if (ModelState.IsValid)
            {
                // Initialise ingredients
                recipe.Ingredients = new List<Ingredient>();

                // Add ingredients
                //foreach(int productId in ProductIds)
                for (int i = 0; i < ProductIds.Count; i++)
                {
                    if (ProductIds[i] != 0)
                    {
                        var product = _context.Product.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
                        var ingredient = new Ingredient()
                        {
                            Name = product.Name,
                            ProductId = product.Id,
                            Quantity = Quantities[i],
                            Unit = Units[i]
                        };
                        recipe.Ingredients.Add(ingredient);
                    }
                }

                // Add recipe
                _context.Add(recipe);

                _context.SaveChanges();

                // Get newly created recipe
                var newRecipe = _context.Recipe.Where(r => r.Name == recipe.Name).FirstOrDefault();

                if (add_ingredient != null)
                {
                    return RedirectToAction(nameof(Edit), new { id = newRecipe.Id });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            var products = _context.Product
                .Include(p => p.Category)
                .Include(p => p.ProductType)
                .ToList();
            ViewData["Products"] = products;

            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Recipe recipe, List<int> ProductIds, List<double> Quantities, List<Unit> Units, string add_ingredient)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get actual recipe from database
                    var storedRecipe = _context.Recipe.Where(r => r.Id == recipe.Id)
                        .Include(r => r.Ingredients)
                        .FirstOrDefault();

                    // Add ingredients
                    //foreach(int productId in ProductIds)
                    for (int i = 0; i < ProductIds.Count; i++)
                    {
                        if (ProductIds[i] != 0)
                        {
                            var product = _context.Product.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
                            var ingredient = new Ingredient()
                            {
                                Name = product.Name,
                                ProductId = product.Id,
                                Quantity = Quantities[i],
                                Unit = Units[i]
                            };
                            storedRecipe.Ingredients.Add(ingredient);
                        }
                    }

                    _context.Update(storedRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (add_ingredient != null)
                {
                    return RedirectToAction(nameof(Edit), new { id = recipe.Id });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }


        // GET: FoodPlans/DeleteIngredient/5
        public IActionResult DeleteIngredient(int? recipe_id, int? recipe_ingredient_id, bool? edit)
        {
            if (recipe_ingredient_id != null)
            {
                var recipe_ingredient = (Ingredient)_context.ShopItem.Find(recipe_ingredient_id);

                if (recipe_ingredient.RecipeId == recipe_id)
                {
                    _context.ShopItem.Remove(recipe_ingredient);
                    _context.SaveChanges();
                }

                if (edit != null && edit.Value)
                {
                    return RedirectToAction(nameof(Edit), new { id = recipe_id });
                }
                return RedirectToAction(nameof(Details), new { id = recipe_id });
            }

            return NotFound();
        }
    }
}
