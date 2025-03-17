using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class updateTriggerWithMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleTime",
                table: "Trigger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TargetDataId",
                table: "Target",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CommandAndResponseId",
                table: "Target",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "CommandAndResponse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommandAndResponse_TargetId",
                table: "CommandAndResponse",
                column: "TargetId",
                unique: true,
                filter: "[TargetId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CommandAndResponse_Target_TargetId",
                table: "CommandAndResponse",
                column: "TargetId",
                principalTable: "Target",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandAndResponse_Target_TargetId",
                table: "CommandAndResponse");

            migrationBuilder.DropIndex(
                name: "IX_CommandAndResponse_TargetId",
                table: "CommandAndResponse");

            migrationBuilder.DropColumn(
                name: "ScheduleTime",
                table: "Trigger");

            migrationBuilder.DropColumn(
                name: "CommandAndResponseId",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "CommandAndResponse");

            migrationBuilder.AlterColumn<string>(
                name: "TargetDataId",
                table: "Target",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
