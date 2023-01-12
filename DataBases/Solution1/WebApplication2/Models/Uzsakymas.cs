using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Uzsakymas
    {
        [DisplayName("Užsakymo numeris")]
        [Required]
        public int Number { get; set; }

        [DisplayName("Data")]
        [Required]
        public DateTime Date { get; set; }

        [DisplayName("Būsena")]
        [Required]
        public string? Status { get; set; }

        [DisplayName("Darbuotojo id")]
        [Required]
        public int WorkerId { get; set; }

        [DisplayName("Pirkėjo kodas")]
        [Required]
        public int BuyerId { get; set; }

        public Pirkejas Buyer { get; set; } = new Pirkejas();

        public List<UzsakymoPreke> Items { get; set; } = new List<UzsakymoPreke>();

        public Saskaita Check { get; set; } = new Saskaita();

        public ListsM Lists { get; set; } = new ListsM();

        public IList<UzsakymoPreke> UzsakymoPrekeList { get; set; } = new List<UzsakymoPreke>();

        public class ListsM
        {
            public IList<SelectListItem>? Workers { get; set; }

            public IList<SelectListItem>? Statuses { get; set; }

            public IList<SelectListItem>? Items { get; set; }

        }

    }
}
