using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerPersistence.Migrations
{
    public partial class AddDefaultValueToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserTokens",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 162, DateTimeKind.Local).AddTicks(8412),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 400, DateTimeKind.Local).AddTicks(8302));

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UserTokens",
                maxLength: 50,
                nullable: true,
                defaultValue: "39a53bd6-2a89-4426-899e-ddc0f6b9d865",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "89e4a1b6-2212-4a9d-beef-0ebeccc10111");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTokens",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 162, DateTimeKind.Local).AddTicks(7258),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 400, DateTimeKind.Local).AddTicks(7218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 153, DateTimeKind.Local).AddTicks(9514),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 392, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 149, DateTimeKind.Local).AddTicks(7529),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 388, DateTimeKind.Local).AddTicks(254));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserTokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 400, DateTimeKind.Local).AddTicks(8302),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 162, DateTimeKind.Local).AddTicks(8412));

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UserTokens",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "89e4a1b6-2212-4a9d-beef-0ebeccc10111",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "39a53bd6-2a89-4426-899e-ddc0f6b9d865");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 400, DateTimeKind.Local).AddTicks(7218),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 162, DateTimeKind.Local).AddTicks(7258));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 392, DateTimeKind.Local).AddTicks(2014),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 153, DateTimeKind.Local).AddTicks(9514));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 27, 14, 27, 22, 388, DateTimeKind.Local).AddTicks(254),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 27, 18, 27, 45, 149, DateTimeKind.Local).AddTicks(7529));
        }
    }
}
