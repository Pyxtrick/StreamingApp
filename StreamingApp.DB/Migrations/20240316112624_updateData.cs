using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations;

/// <inheritdoc />
public partial class updateData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsFriend",
            table: "Status");

        migrationBuilder.DropColumn(
            name: "IsMod",
            table: "Status");

        migrationBuilder.AddColumn<string>(
            name: "UserText",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "UserType",
            table: "Status",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<bool>(
            name: "IsMessageBaned",
            table: "Ban",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "UserText",
            table: "User");

        migrationBuilder.DropColumn(
            name: "UserType",
            table: "Status");

        migrationBuilder.DropColumn(
            name: "IsMessageBaned",
            table: "Ban");

        migrationBuilder.AddColumn<bool>(
            name: "IsFriend",
            table: "Status",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsMod",
            table: "Status",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }
}
