using Microsoft.EntityFrameworkCore.Migrations;

namespace CatBookApp.Migrations
{
    public partial class UpdateFiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Book_User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Book_User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
