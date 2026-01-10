using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CukierniaAdamMus.Models;
using Microsoft.AspNetCore.Authorization; // WAŻNE: Dodano do obsługi ról

namespace CukierniaAdamMus.Controllers
{
    public class KategorieController : Controller
    {
        private readonly CukierniaContext _context;

        public KategorieController(CukierniaContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        // To widzi każdy (nawet niezalogowany), żeby mógł zobaczyć ofertę
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategorie.ToListAsync());
        }

        // GET: Kategorie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.KategoriaId == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // GET: Kategorie/Create
        [Authorize(Roles = "Admin")] // TYLKO ADMIN
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategorie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")] // Odkomentuj jak już zadziała
        public async Task<IActionResult> Create(Kategoria kategoria)
        {
            // --- TO JEST TA POPRAWKA ---
            // Mówimy systemowi: "Nie sprawdzaj listy produktów, wiem że jest pusta"
            ModelState.Remove("Produkty");
            // ---------------------------

            if (ModelState.IsValid)
            {
                _context.Add(kategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoria);
        }

        // GET: Kategorie/Edit/5
        [Authorize(Roles = "Admin")] // TYLKO ADMIN
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }
            return View(kategoria);
        }

        // POST: Kategorie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // TYLKO ADMIN
        public async Task<IActionResult> Edit(int id, Kategoria kategoria) // Usunięto [Bind]
        {
            if (id != kategoria.KategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriaExists(kategoria.KategoriaId))
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
            return View(kategoria);
        }

        // GET: Kategorie/Delete/5
        [Authorize(Roles = "Admin")] // TYLKO ADMIN
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.KategoriaId == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // POST: Kategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // TYLKO ADMIN
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoria = await _context.Kategorie.FindAsync(id);
            if (kategoria != null)
            {
                _context.Kategorie.Remove(kategoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriaExists(int id)
        {
            return _context.Kategorie.Any(e => e.KategoriaId == id);
        }
    }
}