using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class Add3ColumnsLastUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastProfileUpdateDate",
                schema: "security",
                table: "Users",
                newName: "lastUserTypeUpdateDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFirstnameUpdateDate",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastLastnameUpdateDate",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFirstnameUpdateDate",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "lastLastnameUpdateDate",
                schema: "security",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "lastUserTypeUpdateDate",
                schema: "security",
                table: "Users",
                newName: "LastProfileUpdateDate");
        }
    }
}
