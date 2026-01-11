using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CukierniaAdamMus.Models
{
    public class PozycjaZamowienia
    {
        [Key]
        public int PozycjaZamowieniaId { get; set; }
        [Required(ErrorMessage = "Ilość jest wymagana.")]
        public int Ilosc { get; set; }
        public int ZamowienieId { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
        public int ProduktId { get; set; }
        [ForeignKey("ProduktId")]
        public virtual Produkt? Produkt { get; set; }
        
    }
}
