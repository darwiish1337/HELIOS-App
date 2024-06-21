using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataToJobAndEditInTableJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_UserId",
                schema: "AppData",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_UserId",
                schema: "AppData",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "AppData",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                schema: "AppData",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                schema: "AppData",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "AppData",
                table: "Jobs",
                columns: new[] { "Id", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 1, "assets/images/categories/plumbing.png", "سباك" },
                    { 2, "assets/images/categories/electricity.png", "فني كهرباء" },
                    { 3, "assets/images/categories/carpentry.png", "نجار" },
                    { 4, "assets/images/categories/hvac.png", "فني التدفئة والتكييف والتبريد" },
                    { 5, "assets/images/categories/painting.png", "حرفي دهان" },
                    { 6, "assets/images/categories/cleanliness.png", "عامل نظافه" },
                    { 7, "assets/images/categories/plastering.png", "عامل بنا" },
                    { 8, "assets/images/categories/oyster_worker.png", "عامل محاره" },
                    { 9, "assets/images/categories/moving_furniture.png", "ناقل اثاث" },
                    { 10, "assets/images/categories/flooring.png", "مبلط" },
                    { 11, "assets/images/categories/anti_bugs.png", "مكافح حشرات" },
                    { 12, "assets/images/categories/fixing_cars.png", "مصلح سيارات" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_JobId",
                schema: "AppData",
                table: "Users",
                column: "JobId",
                unique: true,
                filter: "[JobId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Jobs_JobId",
                schema: "AppData",
                table: "Users",
                column: "JobId",
                principalSchema: "AppData",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Jobs_JobId",
                schema: "AppData",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_JobId",
                schema: "AppData",
                table: "Users");

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "AppData",
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "JobId",
                schema: "AppData",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                schema: "AppData",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "AppData",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                schema: "AppData",
                table: "Jobs",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_UserId",
                schema: "AppData",
                table: "Jobs",
                column: "UserId",
                principalSchema: "AppData",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
