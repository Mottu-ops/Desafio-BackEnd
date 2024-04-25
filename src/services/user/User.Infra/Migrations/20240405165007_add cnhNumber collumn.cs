using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcnhNumbercollumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CnhNumber",
                table: "User",
                newName: "cnhNumber");

            migrationBuilder.AlterColumn<string>(
                name: "cnhNumber",
                table: "User",
                type: "VARCHAR(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cnhNumber",
                table: "User",
                newName: "CnhNumber");

            migrationBuilder.AlterColumn<string>(
                name: "CnhNumber",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)",
                oldMaxLength: 15);
        }
    }
}
