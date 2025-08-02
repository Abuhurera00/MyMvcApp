using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext context)
        {
            _db = context;
        }

        // READ: List all Users
        public async Task<IActionResult> Index()
        {
            var list = await _db.Users.AsNoTracking().ToListAsync();
            return View(list);
        }

        // READ: Details
        public async Task<IActionResult> Details(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // CREATE: GET
        public IActionResult Create()
            => View();

        // CREATE: POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User u)
        {
            if (!ModelState.IsValid) return View(u);
            _db.Users.Add(u);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // UPDATE: GET
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // UPDATE: POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User u)
        {
            if (id != u.Id) return BadRequest();
            if (!ModelState.IsValid) return View(u);

            _db.Entry(u).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DELETE: GET
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // DELETE: POST
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
