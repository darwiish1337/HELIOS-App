using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class AddNewSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AppData");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "security",
                newName: "Users",
                newSchema: "AppData");

            migrationBuilder.RenameTable(
                name: "RevokedTokens",
                newName: "RevokedTokens",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                schema: "security",
                newName: "RefreshToken",
                newSchema: "AppData");

            migrationBuilder.RenameTable(
                name: "Rates",
                newName: "Rates",
                newSchema: "AppData");

            migrationBuilder.RenameTable(
                name: "Problems",
                newName: "Problems",
                newSchema: "AppData");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "AppData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "AppData",
                newName: "Users",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "RevokedTokens",
                schema: "security",
                newName: "RevokedTokens");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                schema: "AppData",
                newName: "RefreshToken",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "Rates",
                schema: "AppData",
                newName: "Rates");

            migrationBuilder.RenameTable(
                name: "Problems",
                schema: "AppData",
                newName: "Problems");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "AppData",
                newName: "Categories");
        }
    }
}
