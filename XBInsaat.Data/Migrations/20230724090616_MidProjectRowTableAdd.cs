using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class MidProjectRowTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "MidProjects",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Row",
                table: "MidProjects");
        }
    }
}
