using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Ataskaita
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateTo { get; set; }

        public string? Status { get; set; }

        public decimal SumFrom { get; set; }
        public decimal SumTo { get; set; }

        public List<Uzsakymas> Orders { get; set; } = new List<Uzsakymas>();
        public List<Tuple<int, DateTime, string, string, string, decimal, decimal, Tuple<int>>> OrdersTuple =
            new List<Tuple<int, DateTime, string, string, string, decimal, decimal, Tuple<int>>>();

        public List<string> Workers = new List<string>();
        public decimal Sum { get; set; }
        public int OrdersCount { get; set; }

        public ListsM Lists { get; set; } = new ListsM();


        public class ListsM
        {
            public IList<SelectListItem>? Statuses { get; set; }
        }
    }
}
