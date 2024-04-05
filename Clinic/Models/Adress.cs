using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Models;

[Table("adresses", Schema = "main")]
public class Adress
{
	public const int MaxStreetNameLength = 64;
	public const int MaxCityLength = 32;

	[DisplayName("ID")]
	public int Id { get; set; }

	[DisplayName("Ulica")]
	[Required(ErrorMessage = "Ulica jest wymagana!")]
	[MaxLength(MaxStreetNameLength, ErrorMessage = "Nazwa ulicy nie może być dłuższa niż 64 znaki!")]
	public string StreetName { get; set; }

	[DisplayName("Numer Budynku")]
	[Required(ErrorMessage = "Numer budynku jest wymagany!")]
	[Range(0, int.MaxValue, ErrorMessage = "Numer budynku nie może być ujemny!")]
	public int StreetNumber { get; set; }

	[DisplayName("Numer lokalu")]
	[Range(0, int.MaxValue, ErrorMessage = "Numer lokalu nie może być ujemny!")]
	public int? ApartmentNumber { get; set; }

	[DisplayName("Nazwa Miasta")]
	[Required(ErrorMessage = "Nazwa Miasta jest wymagana!")]
	[MaxLength(MaxCityLength, ErrorMessage = "Nazwa miasta nie może być dłuższa niż 32 znaki!")]
	public string City { get; set; }

	[DisplayName("Kod pocztowy")]
	[Required(ErrorMessage = "Kod pocztowy jest wymagany!")]
	[RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Kod pocztowy musi być w formacie XX-XXX!")]
	public string ZipCode { get; set; }


	public ICollection<Patient>? Patients { get; set; }

}