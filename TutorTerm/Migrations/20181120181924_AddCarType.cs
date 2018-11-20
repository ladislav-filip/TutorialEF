using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorTerm.Migrations
{
    public partial class AddCarType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarType",
                table: "Car",
                type: "text",
                maxLength: 20,
                nullable: true);
            
            migrationBuilder.UpdateData("Car", "CarId", 1, "CarType", "SKODA");
            migrationBuilder.UpdateData("Car", "CarId", 2, "CarType", "FIAT");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Active",
                value: "T");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Active",
                value: "T");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Active",
                value: "T");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Car");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Active",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Active",
                value: null);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Active",
                value: null);
        }
    }
}
