using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FM.TreasureHunt.Web.Data.Migrations
{
    public partial class Treasures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treasures",
                columns: table => new
                {
                    TreasureId = table.Column<Guid>(nullable: false),
                    FriendlyName = table.Column<string>(nullable: true),
                    FoundCount = table.Column<int>(nullable: false),
                    LastFound = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treasures", x => x.TreasureId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treasures");
        }
    }
}
