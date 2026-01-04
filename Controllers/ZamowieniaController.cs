using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CukierniaAdamMus.Models;

namespace CukierniaAdamMus.Controllers
{
    public class ZamowieniaController : Controller
    {
        private readonly CukierniaContext _context;

        public ZamowieniaController(CukierniaContext context)
        {
            _context = context;
        }

        // GET: Zamowienia
        public async Task<IActionResult> Index()
        {
            var cukierniaContext = _context.Zamowienia.Include(z => z.Klient);
            return View(await cukierniaContext.ToListAsync());
        }

        // GET: Zamowienia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia
                .Include(z => z.Klient)
                .FirstOrDefaultAsync(m => m.ZamowienieId == id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        // GET: Zamowienia/Create
        public IActionResult Create()
        {
            ViewData["KlientId"] = new SelectList(_context.Klienci, "KlientId", "Email");
            return View();
        }

        // POST: Zamowienia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZamowienieId,DataZamowienia,WartoscZamowienia,KlientId")] Zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zamowienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlientId"] = new SelectList(_context.Klienci, "KlientId", "Email", zamowienie.KlientId);
            return View(zamowienie);
        }

        // GET: Zamowienia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia.FindAsync(id);
            if (zamowienie == null)
            {
                return NotFound();
            }
            ViewData["KlientId"] = new SelectList(_context.Klienci, "KlientId", "Email", zamowienie.KlientId);
            return View(zamowienie);
        }

        // POST: Zamowienia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZamowienieId,DataZamowienia,WartoscZamowienia,KlientId")] Zamowienie zamowienie)
        {
            if (id != zamowienie.ZamowienieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zamowienie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamowienieExists(zamowienie.ZamowienieId))
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
            ViewData["KlientId"] = new SelectList(_context.Klienci, "KlientId", "Email", zamowienie.KlientId);
            return View(zamowienie);
        }

        // GET: Zamowienia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia
                .Include(z => z.Klient)
                .FirstOrDefaultAsync(m => m.ZamowienieId == id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        // POST: Zamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zamowienie = await _context.Zamowienia.FindAsync(id);
            if (zamowienie != null)
            {
                _context.Zamowienia.Remove(zamowienie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZamowienieExists(int id)
        {
            return _context.Zamowienia.Any(e => e.ZamowienieId == id);
        }
    }
}
