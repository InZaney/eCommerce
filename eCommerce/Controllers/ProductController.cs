using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            // Configuration: Products per page (easy to change)
            int productsPerPage = 3;

            // Default to page 1 if not provided
            int pageNumber = page ?? 1;

            // Get total count of products
            int totalProducts = await _context.Products.CountAsync();

            // Get the products for the current page
            List<Product> paginatedProducts = await _context.Products
                .OrderBy(p => p.Title)
                .Skip((pageNumber - 1) * productsPerPage)
                .Take(productsPerPage)
                .ToListAsync();

            // Create paginated list with metadata
            PaginatedList<Product> pagedProducts = new PaginatedList<Product>(
                paginatedProducts,
                totalProducts,
                pageNumber,
                productsPerPage
            );

            return View(pagedProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(p);           // Add the product to the context
                await _context.SaveChangesAsync(); // Save changes to the database

                TempData["Message"] = $"{p.Title} created successfully!"; // Set a success message in TempData

                return RedirectToAction(nameof(Index));
            }
            return View(p); // If the model state is invalid, return the view with the product data and validation errors
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound(); // Return a 404 Not Found response if the product is not found
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(p); // Update the product in the context
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{p.Title} updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [ActionName(nameof(Delete))]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if ( product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"{product.Title} deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
