using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodPlanner.Models;
using FoodPlanner.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FoodPlannerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, FoodPlannerContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Get the current user
            var user = GetCurrentUserAsync().Result;
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // get current date
            var dateNow = DateTime.Now;

            // Set default foodplans for today and tomorrow
            ViewData["TodaysFoodPlan"] = new FoodPlan(DateTime.Now.Date, user.ActiveHouseholdId);
            ViewData["TomorrowsFoodPlan"] = new FoodPlan(DateTime.Now.AddDays(1).Date, user.ActiveHouseholdId);

            // Get today and tomorrows food plans
            var currentFoodPlans = _context.FoodPlans
                .Where(
                    fp => fp.Date.Date >= dateNow.Date &&
                    fp.Date.Date <= dateNow.AddDays(1).Date &&
                    fp.HouseholdId == user.ActiveHouseholdId
                )
                .Include(fp => fp.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.ProductType)
                .Include(fp => fp.Recipes)
                    .ThenInclude(r => r.Recipe)
                .OrderByDescending(fp => fp.Date)
                .ToList();

            if (currentFoodPlans.Where(fp => fp.Date.Date == dateNow.Date).Any())
            {
                var foodplan = currentFoodPlans.Where(fp => fp.Date.Date == dateNow.Date).First();
                ViewData["TodaysFoodPlan"] = foodplan;
            }
            if (currentFoodPlans.Where(fp => fp.Date.Date == dateNow.AddDays(1).Date).Any())
            {
                var foodplan = currentFoodPlans.Where(fp => fp.Date.Date == dateNow.AddDays(1).Date).First();
                ViewData["TomorrowsFoodPlan"] = foodplan;
            }

            // Get latest 6 recipes
            var latestRecipes = _context.Recipes.OrderByDescending(r => r.Id).Take(6).ToList();
            ViewData["LatestRecipes"] = latestRecipes;

            // Get shopping list
            if (ShoppingLists.HasHouseholdList(user.ActiveHouseholdId))
            {
                ViewData["ShoppingList"] = ShoppingLists.Household_ShoppingLists[user.ActiveHouseholdId];
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            return user;
        }
    }
}
