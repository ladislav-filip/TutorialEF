using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorTerm.Migrations
{
    public partial class AddLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "LanguageId",
                table: "User",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageId = table.Column<short>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_LanguageId",
                table: "User",
                column: "LanguageId");

//            migrationBuilder.AddForeignKey(
//                name: "FK_User_Language_LanguageId",
//                table: "User",
//                column: "LanguageId",
//                principalTable: "Language",
//                principalColumn: "LanguageId",
//                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_User_Language_LanguageId",
//                table: "User");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_User_LanguageId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "User");
        }
    }
}
