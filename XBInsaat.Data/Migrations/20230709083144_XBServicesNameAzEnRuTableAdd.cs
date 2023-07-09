using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class XBServicesNameAzEnRuTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "XBServices",
                newName: "NameRu");

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "XBServices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "XBServices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "XBServices");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "XBServices");

            migrationBuilder.RenameColumn(
                name: "NameRu",
                table: "XBServices",
                newName: "Name");
        }
    }
}
