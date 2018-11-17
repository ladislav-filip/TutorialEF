using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorTerm.Migrations
{
    public partial class AddOrderItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    ItemNumber = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.ItemNumber);
                    table.ForeignKey(
                        name: "FK_OrderItem_MyOrders_Prefix_Year_Number",
                        columns: x => new { x.Prefix, x.Year, x.Number },
                        principalTable: "MyOrders",
                        principalColumns: new[] { "Prefix", "Year", "Number" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Prefix_Year_Number",
                table: "OrderItem",
                columns: new[] { "Prefix", "Year", "Number" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");
        }
    }
}
