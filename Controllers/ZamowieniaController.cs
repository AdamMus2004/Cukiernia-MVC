using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CukierniaAdamMus.Models;
using Microsoft.AspNetCore.Authorization;

namespace CukierniaAdamMus.Controllers
{
    [Authorize]
    public class ZamowieniaController : Controller
    {
        private readonly CukierniaContext _context;

        public ZamowieniaController(CukierniaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            var query = _context.Zamowienia.Include(z => z.Klient);

            if (User.IsInRole("Admin"))
                return View(await query.ToListAsync());

            return View(await query.Where(z => z.Klient.Email == userEmail).ToListAsync());
        }

        public async Task<IActionResult> Create(int? produktId)
        {
            if (produktId == null) return RedirectToAction("Index", "Produkty");

            var produkt = await _context.Produkty.FindAsync(produktId);
            if (produkt == null) return NotFound();

            
            var email = User.Identity.Name;
            var klient = await _context.Klienci.FirstOrDefaultAsync(k => k.Email == email);
            if (klient == null) return RedirectToAction("Create", "Klienci");

            ViewBag.Produkt = produkt;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int produktId)
        {
            var produkt = await _context.Produkty.FindAsync(produktId);
            var email = User.Identity.Name;
            var klient = await _context.Klienci.FirstOrDefaultAsync(k => k.Email == email);

            if (produkt == null || klient == null) return NotFound();

            var zamowienie = new Zamowienie
            {
                DataZamowienia = DateTime.Now, 
                KlientId = klient.KlientId,    
                WartoscZamowienia = produkt.Cena,
                PozycjaZamowienias = new List<PozycjaZamowienia>
                {
                    new PozycjaZamowienia {
                        ProduktId = produkt.ProduktId,
                        Ilosc = 1,
                        CenaJednostkowa = produkt.Cena
                    }
                }
            };

            _context.Add(zamowienie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}