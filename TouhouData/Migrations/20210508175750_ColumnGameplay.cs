using Microsoft.EntityFrameworkCore.Migrations;

namespace TouhouData.Migrations
{
    public partial class ColumnGameplay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gameplay",
                table: "Cards",
                type: "nchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gameplay",
                table: "Cards");
        }
    }
}
