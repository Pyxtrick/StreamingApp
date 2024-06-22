using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class updatemany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMessageBaned",
                table: "Ban",
                newName: "IsExcludeQueue");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "SpecialWords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "CommandAndResponse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CommandAndResponse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ExcludePole",
                table: "Ban",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ExcludeReason",
                table: "Ban",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsExcludeChat",
                table: "Ban",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CommandAndResponse");

            migrationBuilder.DropColumn(
                name: "ExcludePole",
                table: "Ban");

            migrationBuilder.DropColumn(
                name: "ExcludeReason",
                table: "Ban");

            migrationBuilder.DropColumn(
                name: "IsExcludeChat",
                table: "Ban");

            migrationBuilder.RenameColumn(
                name: "IsExcludeQueue",
                table: "Ban",
                newName: "IsMessageBaned");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "SpecialWords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "CommandAndResponse",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
