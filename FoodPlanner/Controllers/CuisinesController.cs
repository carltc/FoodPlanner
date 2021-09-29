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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuisines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuisine);
                await _context.SaveChangesAsync();
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
