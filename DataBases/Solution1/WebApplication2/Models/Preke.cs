using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Preke
    {
        [DisplayName("Kodas")]
        [Required]
        public int Code { get; set; }

        [DisplayName("Pavadinimas")]
        [MaxLength(120)]
        [Required]
        public string? Title { get; set; }

        [DisplayName("Kaina")]
        [Required]
        public decimal Price { get; set; }

        [DisplayName("Kiekis")]
        [Required]
        public int Count { get; set; }

        [DisplayName("Gamintojas")]
        [MaxLength(120)]
        [Required]
        public string? Manufacturer { get; set; }

        [DisplayName("Platintojas")]
        [MaxLength(120)]
        [Required]
        public string? Distributor { get; set; }

        [DisplayName("Rušis")]
        [MaxLength(120)]
        [Required]
        public string? Type { get; set; }


        public ListsM Lists { get; set; } = new ListsM();

        public IList<Specifikacija> Specifikacijos { get; set; } = new List<Specifikacija>();


        public class ListsM
        {
            public IList<SelectListItem>? Types { get; set; }

        }

    }
}
