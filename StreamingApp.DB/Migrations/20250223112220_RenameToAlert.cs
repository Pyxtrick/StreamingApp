using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class RenameToAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetData_Emotes_EmoteId",
                table: "TargetData");

            migrationBuilder.DropTable(
                name: "Emotes");

            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Sound = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Video = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    videoLeght = table.Column<int>(type: "int", nullable: false),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMute = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TargetData_Alert_EmoteId",
                table: "TargetData",
                column: "EmoteId",
                principalTable: "Alert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetData_Alert_EmoteId",
                table: "TargetData");

            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.CreateTable(
                name: "Emotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMute = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sound = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    videoLeght = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TargetData_Emotes_EmoteId",
                table: "TargetData",
                column: "EmoteId",
                principalTable: "Emotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
