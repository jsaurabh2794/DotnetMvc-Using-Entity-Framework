using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MHMSystem.Migrations.ApplicationDbContextForMarriageHallMigrations
{
    public partial class addMarriageHallToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarriageHall",
                columns: table => new
                {
                    hallId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hallName = table.Column<string>(nullable: true),
                    hallAddress = table.Column<string>(nullable: true),
                    bookedFromDate = table.Column<DateTime>(nullable: false),
                    bookedToDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageHall", x => x.hallId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarriageHall");
        }
    }
}
