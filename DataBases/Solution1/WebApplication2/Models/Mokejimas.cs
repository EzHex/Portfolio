using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Mokejimas
    {
        [DisplayName("Data")]
        [Required]
        public DateTime Data { get; set; }

        [DisplayName("Suma")]
        [Required]
        public decimal Sum { get; set; }

        [DisplayName("Id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Saskaitos id")]
        [Required]
        public int FkSaskaita { get; set; }

        [DisplayName("Pirkejo kodas")]
        [Required]
        public int FkPirkejoKodas  { get; set; }

    }
}
