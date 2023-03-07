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
    public class RodzajeController : Controller
    {
        private readonly SalonFryzjerskiContext _context;

        public RodzajeController(SalonFryzjerskiContext context)
        {
            _context = context;
        }

        // GET: Rodzaje
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rodzaje.ToListAsync());
        }

        // GET: Rodzaje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rodzaje == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaj == null)
            {
                return NotFound();
            }

            return View(rodzaj);
        }

        // GET: Rodzaje/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rodzaje/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Cena")] Rodzaj rodzaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rodzaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rodzaj);
        }

        // GET: Rodzaje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rodzaje == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaje.FindAsync(id);
            if (rodzaj == null)
            {
                return NotFound();
            }
            return View(rodzaj);
        }

        // POST: Rodzaje/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Cena")] Rodzaj rodzaj)
        {
            if (id != rodzaj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rodzaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RodzajExists(rodzaj.Id))
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
            return View(rodzaj);
        }

        // GET: Rodzaje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rodzaje == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaj == null)
            {
                return NotFound();
            }

            return View(rodzaj);
        }

        // POST: Rodzaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rodzaje == null)
            {
                return Problem("Entity set 'SalonFryzjerskiContext.Rodzaje'  is null.");
            }
            var rodzaj = await _context.Rodzaje.FindAsync(id);
            if (rodzaj != null)
            {
                _context.Rodzaje.Remove(rodzaj);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RodzajExists(int id)
        {
          return _context.Rodzaje.Any(e => e.Id == id);
        }
    }
}
