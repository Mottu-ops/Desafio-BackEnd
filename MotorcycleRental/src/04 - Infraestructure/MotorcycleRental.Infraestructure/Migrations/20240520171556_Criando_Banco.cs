using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRental.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Criando_Banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deliverymen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "varchar", maxLength: 18, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    DriverLicenseNumber = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    DriverLicenseType = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    CNHImageUrl = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverymen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Model = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Plate = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descrition = table.Column<string>(type: "varchar", maxLength: 120, nullable: false),
                    Days = table.Column<decimal>(type: "numeric(5,0)", nullable: false),
                    DayValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PercentageFine = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AdditionalValueDaily = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliverymanId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentanPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    WasReturned = table.Column<bool>(type: "boolean", nullable: false),
                    RentalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AdditionalFineValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AdditionalDailyValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalRentalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Deliverymen_DeliverymanId",
                        column: x => x.DeliverymanId,
                        principalTable: "Deliverymen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalContracts_RentalPlans_RentanPlanId",
                        column: x => x.RentanPlanId,
                        principalTable: "RentalPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymen_CNPJ",
                table: "Deliverymen",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymen_DriverLicenseNumber",
                table: "Deliverymen",
                column: "DriverLicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverymen_Email",
                table: "Deliverymen",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Plate",
                table: "Motorcycles",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_DeliverymanId_MotorcycleId_RentanPlanId_Sta~",
                table: "RentalContracts",
                columns: new[] { "DeliverymanId", "MotorcycleId", "RentanPlanId", "StartDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_MotorcycleId",
                table: "RentalContracts",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_RentanPlanId",
                table: "RentalContracts",
                column: "RentanPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalPlans_Days",
                table: "RentalPlans",
                column: "Days",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalContracts");

            migrationBuilder.DropTable(
                name: "Deliverymen");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "RentalPlans");
        }
    }
}
