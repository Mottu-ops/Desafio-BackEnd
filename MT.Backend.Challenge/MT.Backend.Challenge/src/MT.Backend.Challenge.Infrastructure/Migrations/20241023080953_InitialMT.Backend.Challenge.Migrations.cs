using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MT.Backend.Challenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMTBackendChallengeMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryDrivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Document = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DriversLicenseNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DriversLicenseCategory = table.Column<int>(type: "integer", nullable: false),
                    DriversLicenseValidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DriversLicenseImageUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RentalCategoryDays = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentualFine = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DeliveryDriverId = table.Column<string>(type: "character varying(50)", nullable: false),
                    MotorcycleId = table.Column<string>(type: "character varying(50)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstimatedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RentalCategoryId = table.Column<string>(type: "character varying(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_DeliveryDrivers_DeliveryDriverId",
                        column: x => x.DeliveryDriverId,
                        principalTable: "DeliveryDrivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_RentalCategories_RentalCategoryId",
                        column: x => x.RentalCategoryId,
                        principalTable: "RentalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RentalCategories",
                columns: new[] { "Id", "Active", "CreatedAt", "Name", "PercentualFine", "Price", "RentalCategoryDays", "UpdatedAt" },
                values: new object[,]
                {
                    { "19346023-b90b-4d0a-b254-58fae6ff0018", true, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1185), "50 dias", 0m, 18m, 50, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1185) },
                    { "2a11f7df-1f7f-4de7-a510-c895b69d3bad", true, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1179), "45 dias", 0m, 20m, 45, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1180) },
                    { "2a281a76-84f5-4664-a8fb-2cf3629caa8d", true, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1173), "30 dias", 0m, 22m, 30, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1173) },
                    { "433a46c6-4da7-4d93-912a-d8ce496c8977", true, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1083), "7 dias", 0.2m, 30m, 7, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1087) },
                    { "b81b0a3a-f90e-4e77-afe1-b214b6ac137d", true, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1166), "15 dias", 0.4m, 28m, 15, new DateTime(2024, 10, 23, 8, 9, 50, 271, DateTimeKind.Utc).AddTicks(1166) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_Document",
                table: "DeliveryDrivers",
                column: "Document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_DriversLicenseNumber",
                table: "DeliveryDrivers",
                column: "DriversLicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_Id",
                table: "DeliveryDrivers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Id",
                table: "Motorcycles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_LicensePlate",
                table: "Motorcycles",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalCategories_Id",
                table: "RentalCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalCategories_Name",
                table: "RentalCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalCategories_RentalCategoryDays",
                table: "RentalCategories",
                column: "RentalCategoryDays",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DeliveryDriverId",
                table: "Rentals",
                column: "DeliveryDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Id",
                table: "Rentals",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalCategoryId",
                table: "Rentals",
                column: "RentalCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "DeliveryDrivers");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "RentalCategories");
        }
    }
}
