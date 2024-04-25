using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plan.Infrastructure.Migrations
{
    public partial class AddControllers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_plans",
                table: "plans");

            migrationBuilder.RenameTable(
                name: "plans",
                newName: "Plans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plans",
                table: "Plans",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Plans",
                table: "Plans");

            migrationBuilder.RenameTable(
                name: "Plans",
                newName: "plans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plans",
                table: "plans",
                column: "id");
        }
    }
}
