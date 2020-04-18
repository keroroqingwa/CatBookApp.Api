using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatBookApp.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book_ReadRecord",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Openid = table.Column<string>(maxLength: 100, nullable: false),
                    Rule = table.Column<int>(nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    BookName = table.Column<string>(maxLength: 50, nullable: false),
                    BookClassify = table.Column<string>(maxLength: 50, nullable: true),
                    BookLink = table.Column<string>(maxLength: 200, nullable: true),
                    CoverImage = table.Column<string>(maxLength: 500, nullable: true),
                    ChapterName = table.Column<string>(maxLength: 50, nullable: false),
                    ChapterLink = table.Column<string>(maxLength: 500, nullable: false),
                    NumberOfWords = table.Column<int>(nullable: false),
                    ReadSeconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_ReadRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book_ReadRecordReport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Openid = table.Column<string>(maxLength: 100, nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    BookName = table.Column<string>(maxLength: 50, nullable: false),
                    LastBookReadRecordId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_ReadRecordReport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book_User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Appid = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(maxLength: 10, nullable: false),
                    Openid = table.Column<string>(maxLength: 100, nullable: false),
                    NickName = table.Column<string>(maxLength: 50, nullable: false),
                    AvatarUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Gender = table.Column<string>(maxLength: 10, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Province = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Language = table.Column<string>(maxLength: 50, nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    ReadMinutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book_UserPreference",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Openid = table.Column<string>(maxLength: 100, nullable: false),
                    FontSize = table.Column<int>(nullable: false),
                    FontColor = table.Column<string>(maxLength: 10, nullable: true),
                    BackgroundColor = table.Column<string>(maxLength: 10, nullable: true),
                    FontFamily = table.Column<string>(maxLength: 50, nullable: true),
                    KeepScreenOn = table.Column<ulong>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_UserPreference", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book_ReadRecord");

            migrationBuilder.DropTable(
                name: "Book_ReadRecordReport");

            migrationBuilder.DropTable(
                name: "Book_User");

            migrationBuilder.DropTable(
                name: "Book_UserPreference");
        }
    }
}
