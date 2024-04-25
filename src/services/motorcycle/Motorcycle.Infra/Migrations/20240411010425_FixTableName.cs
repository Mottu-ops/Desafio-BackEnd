using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motorcycle.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycle",
                table: "Motorcycle");

            migrationBuilder.RenameTable(
                name: "Motorcycle",
                newName: "Motorcycles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles");

            migrationBuilder.RenameTable(
                name: "Motorcycles",
                newName: "Motorcycle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycle",
                table: "Motorcycle",
                column: "id");
        }
    }
}
