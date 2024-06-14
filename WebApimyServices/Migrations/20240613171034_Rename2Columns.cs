using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class Rename2Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastUserTypeUpdateDate",
                schema: "security",
                table: "Users",
                newName: "LastUserTypeUpdateDate");

            migrationBuilder.RenameColumn(
                name: "lastLastnameUpdateDate",
                schema: "security",
                table: "Users",
                newName: "LastLastnameUpdateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUserTypeUpdateDate",
                schema: "security",
                table: "Users",
                newName: "lastUserTypeUpdateDate");

            migrationBuilder.RenameColumn(
                name: "LastLastnameUpdateDate",
                schema: "security",
                table: "Users",
                newName: "lastLastnameUpdateDate");
        }
    }
}
