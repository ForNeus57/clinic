using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Models.Migrations
{
    /// <inheritdoc />
    public partial class Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "adresses",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    street_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    street_number = table.Column<int>(type: "integer", nullable: false),
                    apartment_number = table.Column<int>(type: "integer", nullable: true),
                    city = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    first_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    last_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    pesel = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_patients", x => x.id);
                    table.ForeignKey(
                        name: "fk_patients_adresses_address_id",
                        column: x => x.address_id,
                        principalSchema: "main",
                        principalTable: "adresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "adresses",
                columns: new[] { "id", "apartment_number", "city", "street_name", "street_number", "zip_code" },
                values: new object[,]
                {
                    { 1, 1, "Kraków", "Kwiatowa", 1, "11-001" },
                    { 2, null, "Warszawa", "Ogrodnicza", 333, "22-021" },
                    { 3, null, "Wrocław", "Tulipanowa", 32, "33-031" },
                    { 4, 7, "Poznań", "Nawozowa", 12, "44-401" },
                    { 5, 2, "Gdańsk", "Sosnowa", 5, "55-501" },
                    { 6, null, "Gdynia", "Brzozowa", 6, "66-601" },
                    { 7, null, "Sopot", "Klonowa", 7, "77-701" },
                    { 8, 3, "Szczecin", "Dębowa", 8, "88-801" },
                    { 9, 4, "Bydgoszcz", "Świerkowa", 9, "99-901" },
                    { 10, null, "Toruń", "Modrzewiowa", 10, "10-101" },
                    { 11, null, "Olsztyn", "Topolowa", 11, "11-111" },
                    { 12, 5, "Elbląg", "Wiśniowa", 12, "12-121" },
                    { 13, 6, "Białystok", "Jabłoniowa", 13, "13-131" },
                    { 14, null, "Lublin", "Gruszkowa", 14, "14-141" }
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "patients",
                columns: new[] { "id", "address_id", "first_name", "last_name", "pesel" },
                values: new object[,]
                {
                    { 1, 1, "Dominik", "Breksa", "92345678901" },
                    { 2, 2, "Ania", "Nowak", "82345678902" },
                    { 3, 3, "Kamil", "Kowal", "72345678903" },
                    { 4, 4, "Jan", "Kowalski", "62345678904" },
                    { 5, 5, "Krzysztof", "Krawczyk", "52345678905" },
                    { 6, 6, "Ewa", "Błaszczyk", "42345678906" },
                    { 7, 7, "Tomasz", "Karolak", "32345678907" },
                    { 8, 8, "Anna", "Dereszowska", "12345678908" },
                    { 9, 9, "Piotr", "Adamczyk", "12345678909" },
                    { 10, 10, "Katarzyna", "Figura", "12345678910" },
                    { 11, 11, "Maciej", "Stuhr", "12345678911" },
                    { 12, 12, "Agnieszka", "Dygant", "12345678912" },
                    { 13, 13, "Robert", "Więckiewicz", "12345678913" },
                    { 14, 14, "Maja", "Ostaszewska", "12345678914" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_patients_address_id",
                schema: "main",
                table: "patients",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_patients_pesel",
                schema: "main",
                table: "patients",
                column: "pesel",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patients",
                schema: "main");

            migrationBuilder.DropTable(
                name: "adresses",
                schema: "main");
        }
    }
}
