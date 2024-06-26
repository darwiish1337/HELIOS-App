using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNameFromTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "AppData",
                table: "Problems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "AppData",
                table: "Problems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
