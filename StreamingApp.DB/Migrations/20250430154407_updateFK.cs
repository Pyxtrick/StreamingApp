using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class updateFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Status_StatusId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StatusId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Status_UserId",
                table: "Status",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_User_UserId",
                table: "Status",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Status_User_UserId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Status_UserId",
                table: "Status");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_StatusId",
                table: "User",
                column: "StatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Status_StatusId",
                table: "User",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
