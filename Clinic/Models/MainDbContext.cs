using Microsoft.EntityFrameworkCore;

namespace Clinic.Models;

public class MainDbContext: DbContext
{
	public virtual DbSet<Patient> Patients { get; set; }
	public virtual DbSet<Adress> Adresses { get; set; }

	public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.UseSerialColumns();

		#region Entity configuration

		modelBuilder.Entity<Patient>(patient =>
		{
			patient
				.Property(p => p.Id)
				.ValueGeneratedOnAdd();
			patient
				.HasKey(p => p.Id);

			patient
				.HasOne(p => p.Address)
				.WithMany(a => a.Patients)
				.HasForeignKey(p => p.AddressId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

		});

		modelBuilder.Entity<Adress>(address =>
		{
			address
				.Property(a => a.Id)
				.ValueGeneratedOnAdd();
			address
				.HasKey(a => a.Id);

		});

		#endregion

		#region Data seeding

		var addresses = new Adress[]
		{
			new() {Id = 1, StreetName = "Kwiatowa", StreetNumber = 1, ApartmentNumber = 1, City = "Kraków", ZipCode = "11-001"},
			new() {Id = 2, StreetName = "Ogrodnicza", StreetNumber = 333, ApartmentNumber = null, City = "Warszawa", ZipCode = "22-021"},
			new() {Id = 3, StreetName = "Tulipanowa", StreetNumber = 32, ApartmentNumber = null, City = "Wrocław", ZipCode = "33-031"},
			new() {Id = 4, StreetName = "Nawozowa", StreetNumber = 12, ApartmentNumber = 7, City = "Poznań", ZipCode = "44-401"},
			new() {Id = 5, StreetName = "Sosnowa", StreetNumber = 5, ApartmentNumber = 2, City = "Gdańsk", ZipCode = "55-501"},
			new() {Id = 6, StreetName = "Brzozowa", StreetNumber = 6, ApartmentNumber = null, City = "Gdynia", ZipCode = "66-601"},
			new() {Id = 7, StreetName = "Klonowa", StreetNumber = 7, ApartmentNumber = null, City = "Sopot", ZipCode = "77-701"},
			new() {Id = 8, StreetName = "Dębowa", StreetNumber = 8, ApartmentNumber = 3, City = "Szczecin", ZipCode = "88-801"},
			new() {Id = 9, StreetName = "Świerkowa", StreetNumber = 9, ApartmentNumber = 4, City = "Bydgoszcz", ZipCode = "99-901"},
			new() {Id = 10, StreetName = "Modrzewiowa", StreetNumber = 10, ApartmentNumber = null, City = "Toruń", ZipCode = "10-101"},
			new() {Id = 11, StreetName = "Topolowa", StreetNumber = 11, ApartmentNumber = null, City = "Olsztyn", ZipCode = "11-111"},
			new() {Id = 12, StreetName = "Wiśniowa", StreetNumber = 12, ApartmentNumber = 5, City = "Elbląg", ZipCode = "12-121"},
			new() {Id = 13, StreetName = "Jabłoniowa", StreetNumber = 13, ApartmentNumber = 6, City = "Białystok", ZipCode = "13-131"},
			new() {Id = 14, StreetName = "Gruszkowa", StreetNumber = 14, ApartmentNumber = null, City = "Lublin", ZipCode = "14-141"}
		};

		var patients = new Patient[]
		{
			new() {Id = 1, FirstName = "Dominik", LastName = "Breksa", Pesel = "92345678901", AddressId = 1},
			new() {Id = 2, FirstName = "Ania", LastName = "Nowak", Pesel = "82345678902", AddressId = 2},
			new() {Id = 3, FirstName = "Kamil", LastName = "Kowal", Pesel = "72345678903", AddressId = 3},
			new() {Id = 4, FirstName = "Jan", LastName = "Kowalski", Pesel = "62345678904", AddressId = 4},
			new() {Id = 5, FirstName = "Krzysztof", LastName = "Krawczyk", Pesel = "52345678905", AddressId = 5},
			new() {Id = 6, FirstName = "Ewa", LastName = "Błaszczyk", Pesel = "42345678906", AddressId = 6},
			new() {Id = 7, FirstName = "Tomasz", LastName = "Karolak", Pesel = "32345678907", AddressId = 7},
			new() {Id = 8, FirstName = "Anna", LastName = "Dereszowska", Pesel = "12345678908", AddressId = 8},
			new() {Id = 9, FirstName = "Piotr", LastName = "Adamczyk", Pesel = "12345678909", AddressId = 9},
			new() {Id = 10, FirstName = "Katarzyna", LastName = "Figura", Pesel = "12345678910", AddressId = 10},
			new() {Id = 11, FirstName = "Maciej", LastName = "Stuhr", Pesel = "12345678911", AddressId = 11},
			new() {Id = 12, FirstName = "Agnieszka", LastName = "Dygant", Pesel = "12345678912", AddressId = 12},
			new() {Id = 13, FirstName = "Robert", LastName = "Więckiewicz", Pesel = "12345678913", AddressId = 13},
			new() {Id = 14, FirstName = "Maja", LastName = "Ostaszewska", Pesel = "12345678914", AddressId = 14}
		};

		modelBuilder.Entity<Adress>().HasData(addresses);
		modelBuilder.Entity<Patient>().HasData(patients);

		#endregion

	}
}