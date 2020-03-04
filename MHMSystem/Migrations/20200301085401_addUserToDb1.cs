using Microsoft.EntityFrameworkCore.Migrations;

namespace MHMSystem.Migrations
{
    public partial class addUserToDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "MarriageHallId",
                table: "Users",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarriageHallId",
                table: "Users");
        }
    }
}
