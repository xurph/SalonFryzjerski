using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalonFryzjerski.Data;

namespace SalonFryzjerski.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class WizytyController : Controller
    {
        private readonly SalonFryzjerskiContext _context;

        public WizytyController(SalonFryzjerskiContext context)
        {
            _context = context;
        }

        // GET: Wizyty
        public async Task<IActionResult> Index()
        {
            var salonFryzjerskiContext = _context.Wizyty.Include(w => w.Rodzaj);
            return View(await salonFryzjerskiContext.ToListAsync());
        }

        // GET: Wizyty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wizyty == null)
            {
                return NotFound();
            }

            var wizyta = await _context.Wizyty
                .Include(w => w.Rodzaj)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wizyta == null)
            {
                return NotFound();
            }

            return View(wizyta);
        }

        // GET: Wizyty/Create
        public IActionResult Create()
        {
            ViewData["RodzajId"] = new SelectList(_context.Rodzaje, "Id", "Nazwa");
            return View();
        }

        // POST: Wizyty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,RodzajId,UserId,Ocena")] Wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wizyta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RodzajId"] = new SelectList(_context.Rodzaje, "Id", "Nazwa", wizyta.RodzajId);
            return View(wizyta);
        }

        // GET: Wizyty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wizyty == null)
            {
                return NotFound();
            }

            var wizyta = await _context.Wizyty.FindAsync(id);
            if (wizyta == null)
            {
                return NotFound();
            }
            ViewData["RodzajId"] = new SelectList(_context.Rodzaje, "Id", "Nazwa", wizyta.RodzajId);
            return View(wizyta);
        }

        // POST: Wizyty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,RodzajId,UserId,Ocena")] Wizyta wizyta)
        {
            if (id != wizyta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wizyta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WizytaExists(wizyta.Id))
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
            ViewData["RodzajId"] = new SelectList(_context.Rodzaje, "Id", "Nazwa", wizyta.RodzajId);
            return View(wizyta);
        }

        // GET: Wizyty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wizyty == null)
            {
                return NotFound();
            }

            var wizyta = await _context.Wizyty
                .Include(w => w.Rodzaj)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wizyta == null)
            {
                return NotFound();
            }

            return View(wizyta);
        }

        // POST: Wizyty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wizyty == null)
            {
                return Problem("Entity set 'SalonFryzjerskiContext.Wizyty'  is null.");
            }
            var wizyta = await _context.Wizyty.FindAsync(id);
            if (wizyta != null)
            {
                _context.Wizyty.Remove(wizyta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WizytaExists(int id)
        {
          return _context.Wizyty.Any(e => e.Id == id);
        }
    }
}
