using System.ComponentModel.DataAnnotations;

namespace CukierniaAdamMus.Models
{
    public class Klient
    {
        [Key]
        public int KlientId { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imię nie może przekraczać 50 znaków.")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(50, ErrorMessage = "Nazwisko nie może przekraczać 50 znaków.")]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [Phone(ErrorMessage = "Nieprawidłowy format numeru telefonu.")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email.")]
        public string Email { get; set; }
        public virtual ICollection<Zamowienie>? Zamowienia { get; set; }
    }
}
