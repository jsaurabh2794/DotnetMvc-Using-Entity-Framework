using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MHMSystem.Migrations.ApplicationDbContextForBookingsMigrations
{
    public partial class addBookingsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    bookingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(nullable: false),
                    hallId = table.Column<int>(nullable: false),
                    bookingHallname = table.Column<string>(nullable: true),
                    fromDate = table.Column<string>(nullable: false),
                    toDate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.bookingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
