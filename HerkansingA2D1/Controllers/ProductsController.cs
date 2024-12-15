using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HerkansingA2D1.Data;
using HerkansingA2D1.Models;

namespace HerkansingA2D1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HerkansingA2D1Context _context;

        public ProductsController(HerkansingA2D1Context context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                if (product.PromotionStart <= DateTime.Now && product.PromotionEnd >= DateTime.Now)
                {
                    product.Price = product.PromotionalPrice ?? product.Price;
                }
            }
            return View(products);
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
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,PromotionalPrice,PromotionStart,PromotionEnd,ImageUrl")] Product product, IFormFile? ImageFile)
        {
            // Validate external URL if provided
            if (!string.IsNullOrWhiteSpace(product.ImageUrl) && !Uri.IsWellFormedUriString(product.ImageUrl, UriKind.Absolute))
            {
                ModelState.AddModelError("ImageUrl", "The provided image URL is not valid.");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid. Reloading form.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return View(product); // Reload the form if validation fails
            }

            // Handle file upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.ImageUrl = "/images/" + fileName; // Use the uploaded image path
            }

            // Save product to database
            _context.Add(product);
            await _context.SaveChangesAsync();
            Console.WriteLine("Product saved successfully.");
            return RedirectToAction(nameof(Index));
        }







        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,PromotionalPrice,PromotionStart,PromotionEnd,ImageUrl")] Product product, IFormFile? ImageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            // Validate external URL if provided
            if (!string.IsNullOrWhiteSpace(product.ImageUrl) && !Uri.IsWellFormedUriString(product.ImageUrl, UriKind.Absolute))
            {
                ModelState.AddModelError("ImageUrl", "The provided image URL is not valid.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                        }

                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var filePath = Path.Combine(imagePath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        product.ImageUrl = "/images/" + fileName; // Use the uploaded image path
                    }

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
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
