using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CukierniaAdamMus.Models
{
    public class Zamowienie
    {
        [Key]
        public int ZamowienieId { get; set; }

        [Required(ErrorMessage = "Data zamówienia jest wymagana.")]
        [Display(Name = "Data Zamówienia")]
        public DateTime DataZamowienia { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal WartoscZamowienia { get; set; }

        [Display(Name = "Klient")]
        public int KlientId { get; set; }

        [ForeignKey("KlientId")]
        public virtual Klient? Klient { get; set; }

        public virtual ICollection<PozycjaZamowienia>? PozycjaZamowienias { get; set; }
    }
}