using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.spoonacular;
using FoodPlanner.Models;
using FoodPlanner.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.OpenAPITools.Client;
using FoodPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FoodPlanner.Controllers
{
    public class SpoontacularController : Controller
    {
        private readonly FoodPlannerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SpoontacularController(FoodPlannerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string query, string cuisine, string diet, string excludeIngredients, string intolerances)
        {
            ViewData["Recipes"] = new List<OnlineRecipe>();
            ViewData["SearchQuery"] = query;

            if (!String.IsNullOrWhiteSpace(query))
            {
                try
                {
                    // Add to Meal Plan
                    var searchResults = Spoontacular.apiInstance.SearchRecipes(query, cuisine, diet, excludeIngredients, intolerances, null, null, null, false);
                    var results = JsonConvert.DeserializeObject<RecipeResults>(searchResults.ToString());
                    ViewData["Recipes"] = results.results;
                }
                catch (Exception e)
                {
                    Console.Write("Exception when calling DefaultApi.AddToMealPlan: " + e.Message);
                }
            }

            return View();
        }

        public IActionResult Recipe(string id, string previousSearchQuery = null)
        {
            if (!String.IsNullOrWhiteSpace(id) && Int32.TryParse(id, out int Id))
            {
                try
                {
                    // Add to Meal Plan
                    var searchResults = Spoontacular.apiInstance.GetRecipeInformation(Id, false);
                    var recipe = JsonConvert.DeserializeObject<OnlineRecipe>(searchResults.ToString());
                    ViewData["Recipe"] = recipe;

                    if (previousSearchQuery != null)
                    {
                        ViewData["PreviousSearchQuery"] = previousSearchQuery;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Exception when calling DefaultApi.AddToMealPlan: " + e.Message);
                }
            }

            // Add product types to viewbag
            ViewData["ProductTypes"] = _context.ProductTypes.ToList();
            // Add categories to viewbag
            ViewData["Categories"] = _context.Categorys.ToList();

            return View();
        }

        public IActionResult AddRecipe(string Name, int Servings, string Cuisine, List<string> Names, List<string> Aisles, List<float> Amounts, List<string> Units, List<int> StepNumbers, List<string> StepTexts, string previousSearchQuery = null)
        {
            if (!String.IsNullOrWhiteSpace(Name))
            {
                var newRecipe = new Recipe();

                newRecipe.Name = Name;
                newRecipe.Portions = Servings;
                newRecipe.Ingredients = new List<Ingredient>();

                // See if cuisine already exists
                if (!_context.Cuisines.Where(c => c.Name == Cuisine).Any())
                {
                    // Create a new cuisine and add to DB
                    var cuisine = new Cuisine()
                    {
                        Name = Cuisine
                    };
                    _context.Cuisines.Add(cuisine);
                    _context.SaveChanges();
                }

                // Find newly added Cuisine and add to recipe if it exists
                if (_context.Cuisines.Where(c => c.Name == Cuisine).Any())
                {
                    newRecipe.Cuisine = _context.Cuisines.Where(c => c.Name == Cuisine).FirstOrDefault();
                }

                // Get the current user
                var user = GetCurrentUserAsync().Result;
                if (user != null)
                {
                    newRecipe.AddedBy = user.UserName;
                }

                // Add the new ingredients
                if (Names!= null && Units != null && Aisles != null)
                {
                    if (Names.Count == Units.Count && Names.Count == Aisles.Count && Names.Count == Amounts.Count)
                    {
                        for (int i = 0; i < Names.Count; i++)
                        {
                            Ingredient newIngredient;
                            Product product;

                            // Check if product already exists
                            var existingProducts = _context.Products
                                .Include(p => p.ProductType)
                                .Include(p => p.Category)
                                .Where(p => p.ProductType.Name == Names[i] && p.Category.Name == Aisles[i])
                                .ToList();

                            // If product(s) found
                            if (existingProducts.Count > 0)
                            {
                                product = existingProducts.FirstOrDefault();
                            }
                            else
                            {
                                // Create product then add as ingredient
                                product = new Product("", Aisles[i], Names[i]);
                            }

                            newIngredient = new Ingredient(Names[i], Amounts[i], Units[i], product);
                            newRecipe.Ingredients.Add(newIngredient);
                        }
                    }
                }

                // Add the instructions (must be done after ingredients as it links step to ingredients)
                if (StepNumbers != null && StepTexts != null && StepNumbers.Count == StepTexts.Count)
                {
                    RecipeInstructions recipeInstructions = new RecipeInstructions();

                    for (int i = 0; i < StepNumbers.Count; i++)
                    {
                        RecipeStep newStep = new RecipeStep()
                        {
                            Order = StepNumbers[i],
                            Text = StepTexts[i]
                        };

                        newStep.PopulateIngredients(newRecipe.Ingredients);
                        recipeInstructions.Steps.Add(newStep);
                    }

                    newRecipe.Instructions = recipeInstructions;
                }

                _context.Add(newRecipe);

                _context.SaveChanges();
            }

            return RedirectToAction("Index", new { Query = previousSearchQuery });
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            return user;
        }
    }
}
