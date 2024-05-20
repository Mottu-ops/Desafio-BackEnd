using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace MotorcycleRental.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMotorcycleReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_Deliverymen_DeliverymanId",
                table: "RentalContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_RentalPlans_RentanPlanId",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "RentalContracts");

            migrationBuilder.AddColumn<Guid>(
                name: "MotorcycleId",
                table: "RentalContracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_MotorcycleId",
                table: "RentalContracts",
                column: "MotorcycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Deliverymen_DeliverymanId",
                table: "RentalContracts",
                column: "DeliverymanId",
                principalTable: "Deliverymen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Motorcycles_MotorcycleId",
                table: "RentalContracts",
                column: "MotorcycleId",
                principalTable: "Motorcycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_RentalPlans_RentanPlanId",
                table: "RentalContracts",
                column: "RentanPlanId",
                principalTable: "RentalPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_Deliverymen_DeliverymanId",
                table: "RentalContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_Motorcycles_MotorcycleId",
                table: "RentalContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalContracts_RentalPlans_RentanPlanId",
                table: "RentalContracts");

            migrationBuilder.DropIndex(
                name: "IX_RentalContracts_MotorcycleId",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "MotorcycleId",
                table: "RentalContracts");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "RentalContracts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_Deliverymen_DeliverymanId",
                table: "RentalContracts",
                column: "DeliverymanId",
                principalTable: "Deliverymen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalContracts_RentalPlans_RentanPlanId",
                table: "RentalContracts",
                column: "RentanPlanId",
                principalTable: "RentalPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
