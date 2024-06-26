using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApimyServices.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoverNameAR",
                schema: "address",
                table: "Governorates");

            migrationBuilder.DropColumn(
                name: "CityNameAR",
                schema: "address",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NameAR",
                schema: "AppData",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "GoverNameEN",
                schema: "address",
                table: "Governorates",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CityNameEN",
                schema: "address",
                table: "Cities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEN",
                schema: "AppData",
                table: "Categories",
                newName: "Name");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "السباكة");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "كهرباء");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "نجارة");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "تكييف");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "دهان");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "نظافه");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "لياسة");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "نقل اثاث");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "تبليط");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "مكافحة حشرات");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "تصليح سيارات");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "القاهرة");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "الجيزة");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "الأسكندرية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "الدقهلية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "البحر الأحمر");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "البحيرة");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "الفيوم");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "الغربية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "الإسماعيلية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "المنوفية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "المنيا");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "القليوبية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "الوادي الجديد");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "السويس");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "اسوان");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "اسيوط");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "بني سويف");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "بورسعيد");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "دمياط");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "الشرقية");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "جنوب سيناء");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "كفر الشيخ");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "مطروح");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "الأقصر");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 25,
                column: "Name",
                value: "قنا");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "شمال سيناء");

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 27,
                column: "Name",
                value: "سوهاج");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "address",
                table: "Governorates",
                newName: "GoverNameEN");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "address",
                table: "Cities",
                newName: "CityNameEN");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "AppData",
                table: "Categories",
                newName: "NameEN");

            migrationBuilder.AddColumn<string>(
                name: "GoverNameAR",
                schema: "address",
                table: "Governorates",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CityNameAR",
                schema: "address",
                table: "Cities",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                schema: "AppData",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "السباكة", "Plumbing" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "كهرباء", "Electricity" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "نجارة", "Carpentry" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "تكييف", "HVAC" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "دهان", "Painting" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "نظافه", "cleanliness" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "لياسة", "Plastering" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "نقل اثاث", "Moving furniture" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "تبليط", "flooring" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "مكافحة حشرات", "Anti Bugs" });

            migrationBuilder.UpdateData(
                schema: "AppData",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "NameAR", "NameEN" },
                values: new object[] { "تصليح سيارات", "Fixing Cars" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "القاهرة", "Cairo" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الجيزة", "Giza" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الأسكندرية", "Alexandria" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الدقهلية", "Dakahlia" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "البحر الأحمر", "Red Sea" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "البحيرة", "Beheira" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الفيوم", "Fayoum" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الغربية", "Gharbiya" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الإسماعيلية", "Ismailia" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "المنوفية", "Menofia" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "المنيا", "Minya" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "القليوبية", "Qaliubiya" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الوادي الجديد", "New Valley" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "السويس", "Suez" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "اسوان", "Aswan" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "اسيوط", "Assiut" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "بني سويف", "Beni Suef" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "بورسعيد", "Port Said" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "دمياط", "Damietta" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الشرقية", "Sharkia" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "جنوب سيناء", "South Sinai" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "كفر الشيخ", "Kafr Al sheikh" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "مطروح", "Matrouh" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "الأقصر", "Luxor" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "قنا", "Qena" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "شمال سيناء", "North Sinai" });

            migrationBuilder.UpdateData(
                schema: "address",
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "GoverNameAR", "GoverNameEN" },
                values: new object[] { "سوهاج", "Sohag" });
        }
    }
}
