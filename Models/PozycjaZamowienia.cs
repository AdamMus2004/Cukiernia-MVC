using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CukierniaAdamMus.Models
{
    public class PozycjaZamowienia
    {
        [Key]
        public int PozycjaZamowieniaId { get; set; }

        public int ZamowienieId { get; set; }
        [ForeignKey("ZamowienieId")]
        public virtual Zamowienie? Zamowienie { get; set; }

        public int ProduktId { get; set; }
        [ForeignKey("ProduktId")]
        public virtual Produkt? Produkt { get; set; }

        public int Ilosc { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CenaJednostkowa { get; set; }
    }
}