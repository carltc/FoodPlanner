using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodPlanner.Models;
using FoodPlanner.Data;

namespace FoodPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FoodPlannerContext _context;

        public HomeController(ILogger<HomeController> logger, FoodPlannerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get datetime now and and in 1 week
            var dateTimeNow = DateTime.Now;
            var dateTime1WeekFuture = dateTimeNow.AddDays(7);
            var dateTime1WeekPast = dateTimeNow.AddDays(-7);

            // Get latest foodplan (food plan starting within a week)
            if (_context.FoodPlan.Where(fp => fp.Date <= dateTimeNow && fp.Date >= dateTime1WeekPast).Count() > 0)
            {
                ViewData["CurrentFoodPlanId"] = _context.FoodPlan.Where(fp => fp.Date <= dateTimeNow && fp.Date >= dateTime1WeekPast).FirstOrDefault().Id;
                ViewData["CurrentFoodDate"] = _context.FoodPlan.Where(fp => fp.Date <= dateTimeNow && fp.Date >= dateTime1WeekPast).FirstOrDefault().Date.ToShortDateString();
            }
            else
            {
                ViewData["CurrentFoodPlanId"] = 0;
            }

            // Get current shopping list
            if (_context.FoodPlan.Where(fp => fp.Date >= dateTimeNow && fp.Date <= dateTime1WeekFuture).Count() > 0)
            {
                ViewData["UpcomingFoodPlanId"] = _context.FoodPlan.Where(fp => fp.Date >= dateTimeNow && fp.Date <= dateTime1WeekFuture).FirstOrDefault().Id;
                ViewData["UpcomingFoodDate"] = _context.FoodPlan.Where(fp => fp.Date >= dateTimeNow && fp.Date <= dateTime1WeekFuture).FirstOrDefault().Date.ToShortDateString();
            }
            else
            {
                ViewData["UpcomingFoodPlanId"] = 0;
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
    }
}
