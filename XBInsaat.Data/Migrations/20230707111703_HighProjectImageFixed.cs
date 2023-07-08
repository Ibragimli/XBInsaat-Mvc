using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class HighProjectImageFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighProjectImages_News_NewsId",
                table: "HighProjectImages");

            migrationBuilder.DropIndex(
                name: "IX_HighProjectImages_NewsId",
                table: "HighProjectImages");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "HighProjectImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "HighProjectImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HighProjectImages_NewsId",
                table: "HighProjectImages",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighProjectImages_News_NewsId",
                table: "HighProjectImages",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id");
        }
    }
}
