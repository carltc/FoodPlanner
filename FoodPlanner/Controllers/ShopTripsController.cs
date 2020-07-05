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
    public class ShopTripsController : Controller
    {
        private readonly FoodPlannerContext _context;

        public ShopTripsController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: ShopTrips
        public async Task<IActionResult> Index()
        {
            var foodPlannerContext = _context.ShopTrip.Include(s => s.FoodPlan);
            return View(await foodPlannerContext.ToListAsync());
        }

        // GET: ShopTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopTrip = await _context.ShopTrip
                .Include(s => s.FoodPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopTrip == null)
            {
                return NotFound();
            }

            return View(shopTrip);
        }

        // GET: ShopTrips/Create
        public IActionResult Create()
        {
            ViewData["FoodPlanId"] = new SelectList(_context.FoodPlan, "Id", "Id");
            return View();
        }

        // POST: ShopTrips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShopTripCompleted,FoodPlanId")] ShopTrip shopTrip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodPlanId"] = new SelectList(_context.FoodPlan, "Id", "Id", shopTrip.FoodPlanId);
            return View(shopTrip);
        }

        // GET: ShopTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopTrip = await _context.ShopTrip.FindAsync(id);
            if (shopTrip == null)
            {
                return NotFound();
            }
            ViewData["FoodPlanId"] = new SelectList(_context.FoodPlan, "Id", "Id", shopTrip.FoodPlanId);
            return View(shopTrip);
        }

        // POST: ShopTrips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShopTripCompleted,FoodPlanId")] ShopTrip shopTrip)
        {
            if (id != shopTrip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopTripExists(shopTrip.Id))
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
            ViewData["FoodPlanId"] = new SelectList(_context.FoodPlan, "Id", "Id", shopTrip.FoodPlanId);
            return View(shopTrip);
        }

        // GET: ShopTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopTrip = await _context.ShopTrip
                .Include(s => s.FoodPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopTrip == null)
            {
                return NotFound();
            }

            return View(shopTrip);
        }

        // POST: ShopTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopTrip = await _context.ShopTrip.FindAsync(id);
            _context.ShopTrip.Remove(shopTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopTripExists(int id)
        {
            return _context.ShopTrip.Any(e => e.Id == id);
        }
    }
}
