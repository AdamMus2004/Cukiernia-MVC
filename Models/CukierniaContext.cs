namespace CukierniaAdamMus.Models
{
    public class CukierniaContext : DbContext
    {
        public CukierniaContext(DbContextOptions<CukierniaContext> options) : base(options)
        {
        }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowien { get; set; }
    }
}
