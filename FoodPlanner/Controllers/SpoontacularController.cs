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

        public IActionResult Recipe(string id)
        {
            if (!String.IsNullOrWhiteSpace(id) && Int32.TryParse(id, out int Id))
            {
                try
                {
                    // Add to Meal Plan
                    var searchResults = Spoontacular.apiInstance.GetRecipeInformation(Id, false);
                    var recipe = JsonConvert.DeserializeObject<OnlineRecipe>(searchResults.ToString());
                    ViewData["Recipe"] = recipe;
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

        public IActionResult AddRecipe(string Name, int Servings, List<string> Names, List<string> Aisles, List<float> Amounts, List<string> Units)
        {
            if (!String.IsNullOrWhiteSpace(Name))
            {
                var newRecipe = new Recipe();

                newRecipe.Name = Name;
                newRecipe.Portions = Servings;
                newRecipe.Ingredients = new List<Ingredient>();

                // Get the current user
                var user = GetCurrentUserAsync().Result;
                if (user != null)
                {
                    newRecipe.AddedBy = user.UserName;
                }

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

                _context.Add(newRecipe);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            return user;
        }
    }
}
