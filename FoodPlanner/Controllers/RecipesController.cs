using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Data;
using FoodPlanner.Models;
using FoodPlanner.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FoodPlanner.Interfaces;

namespace FoodPlanner.Controllers
{
    public class RecipesController : Controller
    {
        private readonly FoodPlannerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RecipesController(FoodPlannerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.Include(r => r.Cuisine).OrderBy(r => r.Cuisine.Name).ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await GetDatabaseRecipeAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductType)
                .ToList();
            ViewData["Products"] = products;

            var cuisines = _context.Cuisines
                .ToList();
            ViewData["Cuisines"] = cuisines;

            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Portions,InstructionText,RecipeSource")] Recipe recipe, List<int> ProductIds, List<double> Quantities, List<Unit> Units, int CuisineId, string add_ingredient)
        {
            if (ModelState.IsValid)
            {
                // Get the current user
                var user = GetCurrentUserAsync().Result;
                if (user != null)
                {
                    recipe.AddedBy = user.UserName;
                }

                // Get Cuisine
                var cuisine = _context.Cuisines.Find(CuisineId);
                if (cuisine != null)
                {
                    recipe.Cuisine = cuisine;
                }

                // Initialise ingredients
                recipe.Ingredients = new List<Ingredient>();

                // Add ingredients
                //foreach(int productId in ProductIds)
                for (int i = 0; i < ProductIds.Count; i++)
                {
                    if (ProductIds[i] != 0)
                    {
                        var product = _context.Products.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
                        var ingredient = new Ingredient()
                        {
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
                var newRecipe = _context.Recipes.Where(r => r.Name == recipe.Name).FirstOrDefault();

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

            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var recipe = await GetDatabaseRecipeAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Check if this user is allowed to edit this recipe.
            // Only recipes created by the user or household can be edited by the user.
            if (recipe.AddedBy != user.UserName)
            {
                if (!_userManager.IsInRoleAsync(user, "Administrator").Result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductType)
                .ToList();
            ViewData["Products"] = products;

            var cuisines = _context.Cuisines
                .ToList();
            ViewData["Cuisines"] = cuisines;
            ViewData["Ingredients"] = recipe.Ingredients.ToList();

            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Portions,AddedBy,InstructionText,RecipeSource")] Recipe recipe, List<int> ProductIds, List<double> Quantities, List<Unit> Units, int CuisineId, string add_ingredient)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Check if this user is allowed to edit this recipe.
            // Only recipes created by the user or administrator can be edited by the user.
            // TODO Add household as editing ability on recipes
            if (recipe.AddedBy != user.UserName)
            {
                if (!_userManager.IsInRoleAsync(user, "Administrator").Result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get actual recipe from database
                    var storedRecipe = _context.Recipes.Where(r => r.Id == recipe.Id)
                        .Include(r => r.Ingredients)
                        .FirstOrDefault();

                    // Set values to stored recipe
                    storedRecipe.Name = recipe.Name;
                    storedRecipe.Portions = recipe.Portions;
                    storedRecipe.AddedBy = recipe.AddedBy;
                    storedRecipe.InstructionText = recipe.InstructionText;
                    storedRecipe.RecipeSource = recipe.RecipeSource;

                    // Get Cuisine
                    var cuisine = _context.Cuisines.Find(CuisineId);
                    if (cuisine != null)
                    {
                        storedRecipe.Cuisine = cuisine;
                    }

                    // Add ingredients
                    //foreach(int productId in ProductIds)
                    for (int i = 0; i < ProductIds.Count; i++)
                    {
                        if (ProductIds[i] != 0)
                        {
                            var product = _context.Products.Where(p => p.Id == ProductIds[i]).FirstOrDefault();
                            var ingredient = new Ingredient()
                            {
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

        // GET: Recipes/AddInstructions/5
        public IActionResult AddInstructions(int? recipe_id)
        {
            if (recipe_id == null)
            {
                return NotFound();
            }

            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Get actual recipe from database
            var recipe = _context.Recipes.Where(r => r.Id == recipe_id)
                .Include(r => r.Instructions)
                .FirstOrDefault();

            if (recipe.Instructions == null)
            {
                recipe.Instructions = new RecipeInstructions();
                _context.Update(recipe);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Edit), new { id = recipe_id });
        }

        // GET: Recipes/EditInstructions/5
        public async Task<IActionResult> EditInstructions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var recipe = await GetDatabaseRecipeAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Check if this user is allowed to edit this recipe.
            // Only recipes created by the user or household can be edited by the user.
            if (recipe.AddedBy != user.UserName)
            {
                if (!_userManager.IsInRoleAsync(user, "Administrator").Result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var recipeTargets = new List<IRecipeStepTarget>();
            recipeTargets.AddRange(recipe.Ingredients.Cast<IRecipeStepTarget>());
            recipeTargets.AddRange(_context.RecipeStepTargetItems.Cast<IRecipeStepTarget>());
            ViewData["Targets"] = recipeTargets;

            ViewData["Actions"] = _context.RecipeStepActions.ToList();
            ViewData["Ingredients"] = recipe.Ingredients.ToList();

            return View(recipe);
        }

        // POST: Recipes/EditInstrutions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInstructions(int id, string SaveAction, List<int> StepIds, List<int> Orders, List<string> Texts)
        {
            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            bool returnToRecipe = true;

            switch (SaveAction)
            {
                case "Save":
                    returnToRecipe = false;
                    break;
                case "SaveAndReturn":
                    returnToRecipe = true;
                    break;
                case "Return":
                    return RedirectToAction(nameof(Edit), new { id = id });
                case "AddInstructionStep":
                    return RedirectToAction(nameof(AddInstructionStep), new { recipe_id = id });

            }

            // Get actual recipe from database
            var recipe = _context.Recipes.Where(r => r.Id == id)
                .Include(r => r.Instructions)
                    .ThenInclude(i => i.Steps)
                .FirstOrDefault();

            // Check if this user is allowed to edit this recipe.
            // Only recipes created by the user or administrator can be edited by the user.
            // TODO Add household as editing ability on recipes
            if (recipe.AddedBy != user.UserName)
            {
                if (!_userManager.IsInRoleAsync(user, "Administrator").Result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    for(int i = 0; i < Orders.Count; i++)
                    {
                        var recipeStep = recipe.Instructions.Steps.First(s => s.Id == StepIds[i]);

                        recipeStep.Order = Orders[i];
                        recipeStep.Text = Texts[i];

                        _context.Update(recipeStep);
                    }

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
            }

            if (returnToRecipe)
            {
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            else
            {
                return RedirectToAction(nameof(EditInstructions), new { id = id });
            }
        }

        // GET: Recipes/AddInstructionStep/5
        public IActionResult AddInstructionStep(int? recipe_id)
        {
            if (recipe_id == null)
            {
                return NotFound();
            }

            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Get actual recipe from database
            var recipe = _context.Recipes.Where(r => r.Id == recipe_id)
                .Include(r => r.Instructions)
                    .ThenInclude(i => i.Steps)
                .FirstOrDefault();

            var order = recipe.Instructions.Steps.Count > 0 ? recipe.Instructions.Steps.Max(s => s.Order) + 1 : 1;
            recipe.Instructions.Steps.Add(new RecipeStep() { Order = order });
            _context.Update(recipe);
            _context.SaveChanges();

            return RedirectToAction(nameof(EditInstructions), new { id = recipe_id });
        }

        // GET: Recipes/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }

        private Recipe GetDatabaseRecipe(int? id)
        {
            return _context.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(r => r.Instructions)
                    .ThenInclude(i => i.Steps)
                .FirstOrDefault(m => m.Id == id);
        }
        
        private async Task<Recipe> GetDatabaseRecipeAsync(int? id)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(r => r.Instructions)
                    .ThenInclude(i => i.Steps)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // GET: FoodPlans/DeleteIngredient/5
        public IActionResult DeleteIngredient(int? recipe_id, int? recipe_ingredient_id, bool? edit)
        {
            if (recipe_ingredient_id != null)
            {
                var recipe_ingredient = (Ingredient)_context.ShopItems.Find(recipe_ingredient_id);

                if (recipe_ingredient.RecipeId == recipe_id)
                {
                    _context.ShopItems.Remove(recipe_ingredient);
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

        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            return user;
        }
    }
}
