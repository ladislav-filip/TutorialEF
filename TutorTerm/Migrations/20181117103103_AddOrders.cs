using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorTerm.Migrations
{
    public partial class AddOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyOrders",
                columns: table => new
                {
                    Prefix = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyOrders", x => new { x.Prefix, x.Year, x.Number });
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)2, "female" });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "LanguageId", "Code", "Name" },
                values: new object[] { (short)4, "en", "english" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "GenderId", "LanguageId", "Name", "Surname" },
                values: new object[] { 3, (byte)1, (short)1, "Karel", "Čtrvtý" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "GenderId", "LanguageId", "Name", "Surname" },
                values: new object[] { 2, (byte)2, (short)4, "Alžběta", "Druhá" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyOrders");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "LanguageId",
                keyValue: (short)4);
        }
    }
}
