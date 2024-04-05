using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Models;

[Table("patients", Schema = "main"), Index(nameof(Pesel), IsUnique = true)]
public class Patient
{
	public const int MaxNameLength = 32;

	[DisplayName("ID")]
	public int Id { get; set; }


	[DisplayName("Imię")]
	[Required(ErrorMessage = "Imię jest wymagane!")]
	[MaxLength(MaxNameLength, ErrorMessage = "Imię nie może być dłuższe niż 32 znaki!")]
	public string FirstName { get; set; }

	[DisplayName("Nazwisko")]
	[Required(ErrorMessage = "Nazwisko jest wymagane!")]
	[MaxLength(MaxNameLength, ErrorMessage = "Nazwisko nie może być dłuższe niż 32 znaki!")]
	public string LastName { get; set; }

	[DisplayName("Numer PESEL")]
	[Required(ErrorMessage = "Numer PESEL jest wymagany!")]
	[RegularExpression(@"\d{11}", ErrorMessage = "Numer PESEL musi składać się z 11 cyfr!")]
	public string Pesel { get; set; }

	public int AddressId { get; set; }
	public Adress Address { get; set; }

}