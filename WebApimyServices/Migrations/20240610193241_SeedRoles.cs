using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bd61d0e-8cc0-432f-9cdf-2d8a63395a3c", "b5aaca31-0f5d-4cdf-9626-3ac5395bc4d3", "Admin", "ADMIN" },
                    { "7e741780-782b-4c5b-8dbf-2c334be7774e", "279107d9-a0d4-49f0-b808-6e07a07df5cf", "Customer", "CUSTOMER" },
                    { "d0141182-e292-4669-96bb-e1106256fe8f", "746609cc-6443-4855-b807-97b707bdea3d", "Factor", "FACTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2bd61d0e-8cc0-432f-9cdf-2d8a63395a3c");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7e741780-782b-4c5b-8dbf-2c334be7774e");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0141182-e292-4669-96bb-e1106256fe8f");
        }
    }
}
