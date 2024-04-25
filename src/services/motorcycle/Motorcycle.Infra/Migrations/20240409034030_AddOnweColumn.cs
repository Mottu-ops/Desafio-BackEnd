using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motorcycle.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddOnweColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "owner",
                table: "Motorcycle",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner",
                table: "Motorcycle");
        }
    }
}
