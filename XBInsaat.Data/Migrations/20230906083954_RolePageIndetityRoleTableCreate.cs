using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XBInsaat.Data.Migrations
{
    public partial class RolePageIndetityRoleTableCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "RolePages");

            migrationBuilder.CreateTable(
                name: "RolePageIdentityRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolePageId = table.Column<int>(type: "int", nullable: false),
                    IdentityRoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePageIdentityRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePageIdentityRoles_AspNetRoles_IdentityRoleId",
                        column: x => x.IdentityRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePageIdentityRoles_RolePages_RolePageId",
                        column: x => x.RolePageId,
                        principalTable: "RolePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePageIdentityRoles_IdentityRoleId",
                table: "RolePageIdentityRoles",
                column: "IdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePageIdentityRoles_RolePageId",
                table: "RolePageIdentityRoles",
                column: "RolePageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePageIdentityRoles");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "RolePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
