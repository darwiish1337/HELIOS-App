using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class AddJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job",
                schema: "AppData",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "AppData",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "AppData",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "AppData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "AppData",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                schema: "AppData",
                table: "Jobs",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "AppData");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "AppData",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "AppData",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Job",
                schema: "AppData",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
