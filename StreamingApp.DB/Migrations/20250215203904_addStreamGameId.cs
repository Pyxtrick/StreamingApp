using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class addStreamGameId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamGame",
                table: "StreamGame");

            migrationBuilder.AddColumn<int>(
                name: "StreamGameId",
                table: "StreamGame",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamGame",
                table: "StreamGame",
                column: "StreamGameId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamGame_GameCategoryId",
                table: "StreamGame",
                column: "GameCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamGame",
                table: "StreamGame");

            migrationBuilder.DropIndex(
                name: "IX_StreamGame_GameCategoryId",
                table: "StreamGame");

            migrationBuilder.DropColumn(
                name: "StreamGameId",
                table: "StreamGame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamGame",
                table: "StreamGame",
                column: "GameCategoryId");
        }
    }
}
