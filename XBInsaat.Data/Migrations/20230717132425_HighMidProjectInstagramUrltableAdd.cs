using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class HighMidProjectInstagramUrltableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "MidProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "HighProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "MidProjects");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "HighProjects");
        }
    }
}
