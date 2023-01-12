using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Saskaita
    {
        [DisplayName("Data")]
        [Required]
        public DateTime Date { get; set; }

        [DisplayName("Suma")]
        [Required]
        public decimal Sum { get; set; }

        [DisplayName("Saskaitos id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Užsakymo numeris")]
        [Required]
        public int FkOrderNumber { get; set; }
    }
}
