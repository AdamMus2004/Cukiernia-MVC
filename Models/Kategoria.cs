using System.ComponentModel.DataAnnotations;

namespace CukierniaAdamMus.Models
{
    public class Kategoria
    {
        [Key]
        public int KategoriaId { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa kategorii nie może przekraczać 100 znaków.")]
        public string Nazwa { get; set; }

        public virtual ICollection<Produkt> Produkty { get; set; }
    }
}
