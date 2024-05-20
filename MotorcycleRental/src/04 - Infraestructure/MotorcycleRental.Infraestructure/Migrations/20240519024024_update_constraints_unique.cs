using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace MotorcycleRental.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class update_constraints_unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalContracts_DeliverymanId",
                table: "RentalContracts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "RentalContracts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedEndDate",
                table: "RentalContracts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Plate",
                table: "Motorcycles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);

            migrationBuilder.CreateIndex(
                name: "IX_RentalPlans_Days",
                table: "RentalPlans",
                column: "Days",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_DeliverymanId_MotorcycleId_RentanPlanId_Sta~",
                table: "RentalContracts",
                columns: new[] { "DeliverymanId", "MotorcycleId", "RentanPlanId", "StartDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Plate",
                table: "Motorcycles",
                column: "Plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentalPlans_Days",
                table: "RentalPlans");

            migrationBuilder.DropIndex(
                name: "IX_RentalContracts_DeliverymanId_MotorcycleId_RentanPlanId_Sta~",
                table: "RentalContracts");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_Plate",
                table: "Motorcycles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "RentalContracts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedEndDate",
                table: "RentalContracts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Plate",
                table: "Motorcycles",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_DeliverymanId",
                table: "RentalContracts",
                column: "DeliverymanId");
        }
    }
}
