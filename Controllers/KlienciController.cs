using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CukierniaAdamMus.Models;
using Microsoft.AspNetCore.Authorization;

namespace CukierniaAdamMus.Controllers
{
    public class KlienciController : Controller
    {
        private readonly CukierniaContext _context;

        public KlienciController(CukierniaContext context)
        {
            _context = context;
        }

        // GET: Klienci
        [Authorize(Roles = "Admin")] // Tylko Admin widzi listę wszystkich klientów
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klienci.ToListAsync());
        }

        // GET: Klienci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var klient = await _context.Klienci.FirstOrDefaultAsync(m => m.KlientId == id);
            if (klient == null) return NotFound();
            return View(klient);
        }

        // GET: Klienci/Create
        // To jest dostępne dla każdego - żebyś mógł stworzyć swój profil
        public IActionResult Create()
        {
            // Jeśli użytkownik jest zalogowany, podpowiadamy mu jego email
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserEmail = User.Identity.Name;
            }
            return View();
        }

        // POST: Klienci/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlientId,Imie,Nazwisko,Telefon,Email")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();

                // Jeśli to Admin, wraca na listę. Jeśli zwykły user, idzie do Zamówień
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Create", "Zamowienia");
                }
            }
            return View(klient);
        }

        // GET: Klienci/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var klient = await _context.Klienci.FindAsync(id);
            if (klient == null) return NotFound();
            return View(klient);
        }

        // POST: Klienci/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("KlientId,Imie,Nazwisko,Telefon,Email")] Klient klient)
        {
            if (id != klient.KlientId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientExists(klient.KlientId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(klient);
        }

        // GET: Klienci/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var klient = await _context.Klienci.FirstOrDefaultAsync(m => m.KlientId == id);
            if (klient == null) return NotFound();
            return View(klient);
        }

        // POST: Klienci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klient = await _context.Klienci.FindAsync(id);
            if (klient != null) _context.Klienci.Remove(klient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlientExists(int id)
        {
            return _context.Klienci.Any(e => e.KlientId == id);
        }
    }
}