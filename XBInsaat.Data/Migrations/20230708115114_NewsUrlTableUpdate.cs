using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class NewsUrlTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "News",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "News",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "News");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "News");
        }
    }
}
