using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Specifikacija
    {
        [DisplayName("Specifikacijos pavadinimas")]
        [MaxLength(40)]
        [Required]
        public string? Feature { get; set; }

        [DisplayName("Reikšmė")]
        [MaxLength(40)]
        [Required]
        public string? Value { get; set; }

        [DisplayName("Specifikacijos id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Prekės kodas")]
        [Required]
        public int ItemId { get; set; }

        public ListsM Lists { get; set; } = new ListsM();

        public class ListsM
        {
            public IList<SelectListItem>? Items { get; set; }

        }
    }

}
