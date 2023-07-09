using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class XBServicesDescribeAzEnRuTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Describe",
                table: "XBServices",
                newName: "DescribeRu");

            migrationBuilder.AddColumn<string>(
                name: "DescribeAz",
                table: "XBServices",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescribeEn",
                table: "XBServices",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescribeAz",
                table: "XBServices");

            migrationBuilder.DropColumn(
                name: "DescribeEn",
                table: "XBServices");

            migrationBuilder.RenameColumn(
                name: "DescribeRu",
                table: "XBServices",
                newName: "Describe");
        }
    }
}
