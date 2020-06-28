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
    public class ConsumablesController : Controller
    {
        private readonly FoodPlannerContext _context;

        public ConsumablesController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: Consumables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consumable.ToListAsync());
        }

        // GET: Consumables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        // GET: Consumables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consumables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consumable);
        }

        // GET: Consumables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumable.FindAsync(id);
            if (consumable == null)
            {
                return NotFound();
            }
            return View(consumable);
        }

        // POST: Consumables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Consumable consumable)
        {
            if (id != consumable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumableExists(consumable.Id))
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
            return View(consumable);
        }

        // GET: Consumables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        // POST: Consumables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumable = await _context.Consumable.FindAsync(id);
            _context.Consumable.Remove(consumable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumableExists(int id)
        {
            return _context.Consumable.Any(e => e.Id == id);
        }
    }
}
