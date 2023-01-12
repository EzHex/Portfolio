using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Padalinys
    {
        [DisplayName("Miestas")]
        [MaxLength(40)]
        [Required]
        public string? City { get; set; }

        [DisplayName("Adresas")]
        [MaxLength(40)]
        [Required]
        public string? Address { get; set; }

        [DisplayName("Kontaktinis numeris")]
        [MaxLength(40)]
        [Required]
        public string? ContactNumber { get; set; }

        [DisplayName("Padalinio id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Imonės id")]
        [Required]
        public int FkCompany { get; set; }

        public ListsM Lists { get; set; } = new ListsM();


        public class ListsM
        {
            public IList<SelectListItem>? Companies { get; set; }

        }
        
    }
}
