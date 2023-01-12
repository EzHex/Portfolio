using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class PadalinysList
    {
        [DisplayName("Miestas")]
        public string? City { get; set; }

        [DisplayName("Adresas")]
        public string? Address { get; set; }

        [DisplayName("Kontaktinis numeris")]
        public string? ContactNumber { get; set; }

        [DisplayName("Padalinio id")]
        public string? Id { get; set; }

        [DisplayName("Imonės id")]
        public string? FkCompany { get; set; }

        public ListsM Lists { get; set; } = new ListsM();

        public class ListsM
        {
            public IList<SelectListItem> Companies { get; set; }

        }
    }
}
