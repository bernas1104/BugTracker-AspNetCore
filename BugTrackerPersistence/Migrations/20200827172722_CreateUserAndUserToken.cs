using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerPersistence.Migrations
{
    public partial class CreateUserAndUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: true, defaultValue: false),
                    GithubUsername = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 388, DateTimeKind.Local).AddTicks(254)),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 392, DateTimeKind.Local).AddTicks(2014)),
                    DeletedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(maxLength: 50, nullable: true, defaultValue: "89e4a1b6-2212-4a9d-beef-0ebeccc10111"),
                    UserId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 400, DateTimeKind.Local).AddTicks(7218)),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 400, DateTimeKind.Local).AddTicks(8302)),
                    DeletedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
