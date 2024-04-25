using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcollumnstodatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DateBirth",
                table: "User",
                newName: "dateBirth");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "User",
                newName: "cnpj");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CnhType",
                table: "User",
                newName: "chnType");

            migrationBuilder.RenameColumn(
                name: "CnhImage",
                table: "User",
                newName: "chnImage");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "User",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "dateBirth",
                table: "User",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "User",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "chnType",
                table: "User",
                type: "VARCHAR(3)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "chnImage",
                table: "User",
                type: "VARCHAR(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dateBirth",
                table: "User",
                newName: "DateBirth");

            migrationBuilder.RenameColumn(
                name: "cnpj",
                table: "User",
                newName: "Cnpj");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "chnType",
                table: "User",
                newName: "CnhType");

            migrationBuilder.RenameColumn(
                name: "chnImage",
                table: "User",
                newName: "CnhImage");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBirth",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CnhType",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "CnhImage",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldMaxLength: 1000);
        }
    }
}
