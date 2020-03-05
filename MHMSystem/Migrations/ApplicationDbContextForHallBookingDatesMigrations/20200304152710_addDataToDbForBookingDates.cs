using Microsoft.EntityFrameworkCore.Migrations;

namespace MHMSystem.Migrations.ApplicationDbContextForHallBookingDatesMigrations
{
    public partial class addDataToDbForBookingDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HallBookingDates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hallId = table.Column<int>(nullable: false),
                    hallName = table.Column<string>(nullable: true),
                    fromDate = table.Column<string>(nullable: true),
                    toDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallBookingDates", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HallBookingDates");
        }
    }
}
