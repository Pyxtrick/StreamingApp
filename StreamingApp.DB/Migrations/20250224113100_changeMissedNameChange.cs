using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class changeMissedNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetData_Alert_EmoteId",
                table: "TargetData");

            migrationBuilder.RenameColumn(
                name: "EmoteId",
                table: "TargetData",
                newName: "AlertId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetData_EmoteId",
                table: "TargetData",
                newName: "IX_TargetData_AlertId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetData_Alert_AlertId",
                table: "TargetData",
                column: "AlertId",
                principalTable: "Alert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetData_Alert_AlertId",
                table: "TargetData");

            migrationBuilder.RenameColumn(
                name: "AlertId",
                table: "TargetData",
                newName: "EmoteId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetData_AlertId",
                table: "TargetData",
                newName: "IX_TargetData_EmoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetData_Alert_EmoteId",
                table: "TargetData",
                column: "EmoteId",
                principalTable: "Alert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
