using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DarbuotojasList
    {
        [DisplayName("Vardas")]
        public string? Name { get; set; }

        [DisplayName("Pareigos")]
        public string? Duties { get; set; }

        [DisplayName("Darbuotojo id")]
        public string? Id { get; set; }

        [DisplayName("Padalinys")]
        public string? FkDepartment { get; set; }


    }
}
