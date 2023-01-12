using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UzsakymoPreke
    {
        [DisplayName("Kiekis")]
        [Required]
        public int Count { get; set; }

        [DisplayName("Užsakomos prekės id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Prekės kodas")]
        [Required]
        public int ItemId { get; set; }

        [DisplayName("Užsakymo numeris")]
        [Required]
        public int OrderId { get; set; }
    }
}
