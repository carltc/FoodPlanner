using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Data;
using FoodPlanner.Models;

namespace FoodPlanner.Controllers
{
    public class CuisinesController : Controller
    {
        private readonly FoodPlannerContext _context;

        public CuisinesController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: Cuisines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cuisines.ToListAsync());
        }

        // GET: Cuisines/Create
        public IActionResult Create(string returnUrl = null)
        {
            if (returnUrl != null)
            {
                ViewData["returnUrl"] = returnUrl;
            }

            return View();
        }

        // POST: Cuisines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Cuisine cuisine, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Check that this Cuisine doesn't already exist
                if (_context.Cuisines.Where(c => c.Name.ToLower() == cuisine.Name.ToLower()).Any())
                {
                    // Cuisine already exists, therefore send back an invalid response
                    ViewData["errorMessage"] = $"Cuisine with name '{cuisine.Name}' already exists.";

                    if (returnUrl != null)
                    {
                        ViewData["returnUrl"] = returnUrl;
                    }

                    return View(cuisine);
                }
                _context.Add(cuisine);
                await _context.SaveChangesAsync();

                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(cuisine);
        }

        private bool CuisineExists(int id)
        {
            return _context.Cuisines.Any(e => e.Id == id);
        }
    }
}
