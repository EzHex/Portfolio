using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Imone
    {
		[DisplayName("Pavadinimas")]
		[MaxLength(40)]
		[Required]
		public string? Title { get; set; }

		[DisplayName("Miestas")]
		[MaxLength(40)]
		[Required]
		public string? City { get; set; }

		[DisplayName("Adresas")]
		[MaxLength(40)]
		[Required]
		public string? Address { get; set; }

		[DisplayName("Telefono nr.")]
		[MaxLength(40)]
		[Required]
		public string? Phone { get; set; }

		[DisplayName("Darbuotoju skaičius")]
		[Required]
		public int NumberOfEmployees { get; set; }

		[DisplayName("Id")]
		[Required]
		public int Id { get; set; }

	}
}
