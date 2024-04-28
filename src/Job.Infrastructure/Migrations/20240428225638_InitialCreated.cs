using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoboys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false),
                    DateBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Cnh = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CnhImage = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoboys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Plate = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DatePreview = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdMoto = table.Column<Guid>(type: "uuid", nullable: false),
                    IdMotoboy = table.Column<Guid>(type: "uuid", nullable: false),
                    Plan = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Fine = table.Column<decimal>(type: "numeric", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Created", "Email", "Password", "Updated" },
                values: new object[] { new Guid("500923f0-1ffb-4f59-8c4b-fc28f2dc6339"), new DateTime(2024, 4, 28, 19, 56, 37, 591, DateTimeKind.Local).AddTicks(544), "job@job.com", "mudar@123", null });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_Email",
                table: "Managers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Motoboys");

            migrationBuilder.DropTable(
                name: "Motos");

            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
