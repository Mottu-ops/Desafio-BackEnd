using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motorcycle.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "plateNumber",
                table: "Motorcycle",
                newName: "plateCode");

            migrationBuilder.RenameColumn(
                name: "manufacturer",
                table: "Motorcycle",
                newName: "model");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "plateCode",
                table: "Motorcycle",
                newName: "plateNumber");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "Motorcycle",
                newName: "manufacturer");
        }
    }
}
