using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motorcycle.Infra.Migrations
{
    public partial class FixBusConn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner",
                table: "Motorcycles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "owner",
                table: "Motorcycles",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
