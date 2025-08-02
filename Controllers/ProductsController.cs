using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext context)
        {
            _db = context;
        }

        // READ: List all products
        public async Task<IActionResult> Index()
        {
            var list = await _db.Products.AsNoTracking().ToListAsync();
            return View(list);
        }

        // READ: Details
        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // CREATE: GET
        public IActionResult Create()
            => View();

        // CREATE: POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            if (!ModelState.IsValid) return View(p);
            _db.Products.Add(p);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // UPDATE: GET
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // UPDATE: POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product p)
        {
            if (id != p.Id) return BadRequest();
            if (!ModelState.IsValid) return View(p);

            _db.Entry(p).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DELETE: GET
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // DELETE: POST
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
