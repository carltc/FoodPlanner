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

namespace FoodPlanner.Controllers
{
    public class SpoontacularController : Controller
    {
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

            return View();
        }
    }
}
