using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CukierniaAdamMus.Models
{
    public class Produkt
    {
        [Key]
        public int ProduktId { get; set; }
        [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
        [Display (Name = "Nazwa Produktu")]
        public string Nazwa { get; set; }
        [Display (Name = "Opis Produktu")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Cena produktu jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0.")]
        [DataType(DataType.Currency)]
        public decimal Cena { get; set; }
        [Display (Name = "Kategoria")]
        public int KategoriaId { get; set; }
        [ForeignKey("KategoriaId")]
        public virtual Kategoria Kategoria { get; set; }
        public virtual ICollection<PozycjaZamowienia> PozycjaZamowienias { get; set; }
    }
}
