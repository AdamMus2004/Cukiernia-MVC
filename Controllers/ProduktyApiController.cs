using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CukierniaAdamMus.Models;

namespace CukierniaAdamMus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduktyApiController : ControllerBase
    {
        private readonly CukierniaContext _context;

        public ProduktyApiController(CukierniaContext context)
        {
            _context = context;
        }

        // GET: api/ProduktyApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produkt>>> GetProdukty()
        {
            return await _context.Produkty.ToListAsync();
        }

        // GET: api/ProduktyApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produkt>> GetProdukt(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);

            if (produkt == null)
            {
                return NotFound();
            }

            return produkt;
        }

        // PUT: api/ProduktyApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdukt(int id, Produkt produkt)
        {
            if (id != produkt.ProduktId)
            {
                return BadRequest();
            }

            _context.Entry(produkt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduktExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProduktyApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produkt>> PostProdukt(Produkt produkt)
        {
            _context.Produkty.Add(produkt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdukt", new { id = produkt.ProduktId }, produkt);
        }

        // DELETE: api/ProduktyApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdukt(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }

            _context.Produkty.Remove(produkt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduktExists(int id)
        {
            return _context.Produkty.Any(e => e.ProduktId == id);
        }
    }
}
