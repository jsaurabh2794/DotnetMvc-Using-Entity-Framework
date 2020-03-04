using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MHMSystem.Migrations.ApplicationDbContextForMarriageHallMigrations
{
    public partial class addMarriageHallToDb_new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "bookedToDate",
                table: "MarriageHall",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "bookedFromDate",
                table: "MarriageHall",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "bookedToDate",
                table: "MarriageHall",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "bookedFromDate",
                table: "MarriageHall",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
