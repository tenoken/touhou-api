using Microsoft.EntityFrameworkCore.Migrations;

namespace TouhouData.Migrations
{
    public partial class base64EncodedImageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64EncodedImage",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64EncodedImage",
                table: "Photos");
        }
    }
}
