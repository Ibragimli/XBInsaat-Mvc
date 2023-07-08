using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class MidHighProjectTableDeleteUpdateMidProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighMidProjectIds");

            migrationBuilder.AddColumn<int>(
                name: "HighProjectId",
                table: "MidProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MidProjects_HighProjectId",
                table: "MidProjects",
                column: "HighProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MidProjects_HighProjects_HighProjectId",
                table: "MidProjects",
                column: "HighProjectId",
                principalTable: "HighProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MidProjects_HighProjects_HighProjectId",
                table: "MidProjects");

            migrationBuilder.DropIndex(
                name: "IX_MidProjects_HighProjectId",
                table: "MidProjects");

            migrationBuilder.DropColumn(
                name: "HighProjectId",
                table: "MidProjects");

            migrationBuilder.CreateTable(
                name: "HighMidProjectIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighProjectId = table.Column<int>(type: "int", nullable: false),
                    MidProjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighMidProjectIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighMidProjectIds_HighProjects_HighProjectId",
                        column: x => x.HighProjectId,
                        principalTable: "HighProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HighMidProjectIds_MidProjects_MidProjectId",
                        column: x => x.MidProjectId,
                        principalTable: "MidProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HighMidProjectIds_HighProjectId",
                table: "HighMidProjectIds",
                column: "HighProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HighMidProjectIds_MidProjectId",
                table: "HighMidProjectIds",
                column: "MidProjectId");
        }
    }
}
