using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FM.TreasureHunt.Web.Data.Migrations
{
    public partial class TreasureFinds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreasureFinds",
                columns: table => new
                {
                    TreasureFindId = table.Column<Guid>(nullable: false),
                    TreasureId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    DateFound = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreasureFinds", x => x.TreasureFindId);
                    table.ForeignKey(
                        name: "FK_TreasureFinds_Treasures_TreasureId",
                        column: x => x.TreasureId,
                        principalTable: "Treasures",
                        principalColumn: "TreasureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreasureFinds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreasureFinds_TreasureId",
                table: "TreasureFinds",
                column: "TreasureId");

            migrationBuilder.CreateIndex(
                name: "IX_TreasureFinds_UserId",
                table: "TreasureFinds",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreasureFinds");
        }
    }
}
