using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Pirkejas
    {
        [DisplayName("Kodas")]
        [Required]
        public int Code { get; set; }

        [DisplayName("Vardas")]
        [MaxLength(40)]
        [Required]
        public string? Name { get; set; }

        [DisplayName("Pavardė")]
        [MaxLength(40)]
        [Required]
        public string? Surname { get; set; }

        [DisplayName("Adresas")]
        [MaxLength(40)]
        [Required]
        public string? Address { get; set; }

        [DisplayName("Telefono numeris")]
        [MaxLength(40)]
        [Required]
        public string? Phone { get; set; }

        [DisplayName("Atsiskaitymo būdas")]
        [MaxLength(120)]
        [Required]
        public string? MethodOfPayment { get; set; }

        [DisplayName("Pristatymo būdas")]
        [MaxLength(120)]
        [Required]
        public string? DeliveryMethod { get; set; }

        public ListsM Lists { get; set; } = new ListsM();

        public class ListsM
        {
            public IList<SelectListItem>? Payments { get; set; }

            public IList<SelectListItem>? DeliveryMethods { get; set; }

        }

    }
}
