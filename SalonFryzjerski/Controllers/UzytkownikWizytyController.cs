using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalonFryzjerski.Data;
using SalonFryzjerski.Models;

namespace SalonFryzjerski.Controllers
{
    [Authorize]
    public class UzytkownikWizytyController : Controller
    {
        private readonly SalonFryzjerskiContext _context;

        public UzytkownikWizytyController(SalonFryzjerskiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salonFryzjerskiContext = _context.Wizyty
                .Where(w=>w.UserId == User.GetId())
                .Include(w => w.Rodzaj);

            return View(await salonFryzjerskiContext.ToListAsync());
        }

        public IActionResult Rezerwacja()
        {
            ViewData["RodzajId"] = new SelectList(_context.Rodzaje, "Id", "Nazwa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezerwacja([Bind("Id,Data,RodzajId,UserId")] Wizyta wizyta)
        {
            if (_context.Wizyty.Any(a => a.UserId == User.GetId() && wizyta.Data >= a.Data && wizyta.Data < a.Data.AddHours(1)))
            {
                ModelState.AddModelError(String.Empty, "Masz już wizytę o wybranej godzinie");
            }

            if (ModelState.IsValid)
            {
                _context.Add(wizyta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RodzajId"] = new SelectList(_context.Rodzaje, "Id", "Nazwa", wizyta.RodzajId);
            return View(wizyta);
        }

        public async Task<IActionResult> Odwolaj(int id)
        {
            var wizyta = await _context.Wizyty.FindAsync(id);
            if (wizyta != null)
            {
                _context.Wizyty.Remove(wizyta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Ocen(int id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> Ocen(int? wid, int? ocena)
        {
            var wizyta = await _context.Wizyty.FirstOrDefaultAsync(m => m.Id == wid);

            if (wizyta == null)
            {
                return NotFound();
            }

            wizyta.Ocena = ocena.GetValueOrDefault();
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
