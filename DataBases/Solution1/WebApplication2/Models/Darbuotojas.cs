using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Darbuotojas
    {
        [DisplayName("Vardas")]
        [Required]
        public string? Name { get; set; }

        [DisplayName("Pareigos")]
        [Required]
        public string? Duties { get; set; }

        [DisplayName("Darbuotojo id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Padalinys")]
        [Required]
        public int FkDepartment { get; set; }

        public ListsM Lists { get; set; } = new ListsM();

        public class ListsM
        {
            public IList<SelectListItem>? Departaments { get; set; }
            public IList<SelectListItem>? Duties { get; set; }

        }
    }

    
}
