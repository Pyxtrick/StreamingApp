using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class updateSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpamAmmount",
                table: "Settings",
                newName: "TTSSpamAmmount");

            migrationBuilder.RenameColumn(
                name: "MuteChatMessages",
                table: "Settings",
                newName: "PauseChatMessages");

            migrationBuilder.AddColumn<bool>(
                name: "PauseAllerts",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TTSLenghtAmmount",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PauseAllerts",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TTSLenghtAmmount",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "TTSSpamAmmount",
                table: "Settings",
                newName: "SpamAmmount");

            migrationBuilder.RenameColumn(
                name: "PauseChatMessages",
                table: "Settings",
                newName: "MuteChatMessages");
        }
    }
}
