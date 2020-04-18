using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatBookApp.Migrations
{
    public partial class UpdateBookApiWhiteList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Book_ApiWhiteList",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Book_ApiWhiteList",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Book_ApiWhiteList",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Book_ApiWhiteList");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Book_ApiWhiteList");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Book_ApiWhiteList");
        }
    }
}
