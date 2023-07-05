using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class ProjectDescribceTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Describe",
                table: "Projects",
                newName: "DescribeRu");

            migrationBuilder.AddColumn<string>(
                name: "DescribeAz",
                table: "Projects",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescribeEn",
                table: "Projects",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescribeAz",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DescribeEn",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "DescribeRu",
                table: "Projects",
                newName: "Describe");
        }
    }
}
