using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorTerm.Migrations
{
    public partial class AddHexValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HexValue",
                table: "Color",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "ColorId", "HexValue", "Name" },
                values: new object[] { 1, "#0000FF", "Blue" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "ColorId", "HexValue", "Name" },
                values: new object[] { 2, "#FF0000", "Red" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "ColorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "ColorId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "HexValue",
                table: "Color");
        }
    }
}
