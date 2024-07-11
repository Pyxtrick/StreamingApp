using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class StreamHighlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneUTC",
                table: "Status");

            migrationBuilder.AddColumn<int>(
                name: "AppAuthEnum",
                table: "UserDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMod",
                table: "Status",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "Status",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StreamHighlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreamHistoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOfDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StreamTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamHighlights", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreamHighlights");

            migrationBuilder.DropColumn(
                name: "AppAuthEnum",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "IsMod",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "Status");

            migrationBuilder.AddColumn<int>(
                name: "TimeZoneUTC",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
