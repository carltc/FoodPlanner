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
    public class ProductsController : Controller
    {
        private readonly FoodPlannerContext _context;

        public ProductsController(FoodPlannerContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.Category)
                .ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            // Add product types to viewbag
            ViewData["ProductTypes"] = _context.ProductType.ToList();
            // Add categories to viewbag
            ViewData["Categories"] = _context.Category.ToList();

            //ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name");
            //ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProductTypeId,CategoryId")] Product product, string Category, string ProductType)
        {
            // Check if NewProductType has been entered
            if (ProductType != null)
            {
                // Check if product type exists
                if (_context.ProductType.Where(pt => pt.Name.ToLower() == ProductType.ToLower()).Count() > 0)
                {
                    product.ProductType = _context.ProductType.Where(pt => pt.Name.ToLower() == ProductType.ToLower()).FirstOrDefault();
                }
                else
                {
                    product.ProductType = new ProductType(ProductType);
                }

            }

            // Check if NewCategory has been entered
            if (Category != null)
            {
                // Check if category exists
                if (_context.Category.Where(c => c.Name.ToLower() == Category.ToLower()).Count() > 0)
                {
                    product.Category = _context.Category.Where(c => c.Name.ToLower() == Category.ToLower()).FirstOrDefault();
                }
                else
                {
                    product.Category = new Category(Category);
                }
            }


            if (product.Name != null && 
                (product.Category != null || product.CategoryId != null) && 
                (product.ProductType != null || product.ProductTypeId > 0))
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            // Add product types to viewbag
            ViewData["ProductTypes"] = _context.ProductType.ToList();
            // Add categories to viewbag
            ViewData["Categories"] = _context.Category.ToList();

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public ActionResult GetProductTypes(string term)
        {
            return Json(_context.ProductType.Where(pt => pt.Name.ToLower().StartsWith(term.ToLower())).Select(a => new { label = a.Name }));
        }

        public ActionResult GetCategories(string term)
        {
            return Json(_context.Category.Where(c => c.Name.ToLower().StartsWith(term.ToLower())).Select(a => new { label = a.Name }));
        }
    }
}
