using Microsoft.EntityFrameworkCore.Migrations;

namespace MHMSystem.Migrations.ApplicationDbContextForMarriageHallMigrations
{
    public partial class addMarriageHallToDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookedFromDate",
                table: "MarriageHall");

            migrationBuilder.DropColumn(
                name: "bookedToDate",
                table: "MarriageHall");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bookedFromDate",
                table: "MarriageHall",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bookedToDate",
                table: "MarriageHall",
                type: "TEXT",
                nullable: true);
        }
    }
}
