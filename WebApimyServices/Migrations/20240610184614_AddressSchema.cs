using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class AddressSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "address");

            migrationBuilder.RenameTable(
                name: "Governorates",
                newName: "Governorates",
                newSchema: "address");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "Cities",
                newSchema: "address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Governorates",
                schema: "address",
                newName: "Governorates");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "address",
                newName: "Cities");
        }
    }
}
