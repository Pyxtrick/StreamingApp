using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations;

/// <inheritdoc />
public partial class addChangeHistory : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "UserText",
            table: "User",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "UpdatedAt",
            table: "SpecialWords",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "SpecialWords",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "VodUrl",
            table: "GameStream",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<byte[]>(
            name: "Video",
            table: "Emotes",
            type: "varbinary(max)",
            nullable: true,
            oldClrType: typeof(byte[]),
            oldType: "varbinary(max)");

        migrationBuilder.AlterColumn<byte[]>(
            name: "Sound",
            table: "Emotes",
            type: "varbinary(max)",
            nullable: true,
            oldClrType: typeof(byte[]),
            oldType: "varbinary(max)");

        migrationBuilder.AlterColumn<byte[]>(
            name: "Image",
            table: "Emotes",
            type: "varbinary(max)",
            nullable: true,
            oldClrType: typeof(byte[]),
            oldType: "varbinary(max)");

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "UpdatedAt",
            table: "Emotes",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Emotes",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "Volume",
            table: "Emotes",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "UpdatedAt",
            table: "CommandAndResponse",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "CommandAndResponse",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "SpecialWords");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "SpecialWords");

        migrationBuilder.DropColumn(
            name: "VodUrl",
            table: "GameStream");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "Emotes");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Emotes");

        migrationBuilder.DropColumn(
            name: "Volume",
            table: "Emotes");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "CommandAndResponse");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "CommandAndResponse");

        migrationBuilder.AlterColumn<string>(
            name: "UserText",
            table: "User",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<byte[]>(
            name: "Video",
            table: "Emotes",
            type: "varbinary(max)",
            nullable: false,
            defaultValue: new byte[0],
            oldClrType: typeof(byte[]),
            oldType: "varbinary(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<byte[]>(
            name: "Sound",
            table: "Emotes",
            type: "varbinary(max)",
            nullable: false,
            defaultValue: new byte[0],
            oldClrType: typeof(byte[]),
            oldType: "varbinary(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<byte[]>(
            name: "Image",
            table: "Emotes",
            type: "varbinary(max)",
            nullable: false,
            defaultValue: new byte[0],
            oldClrType: typeof(byte[]),
            oldType: "varbinary(max)",
            oldNullable: true);
    }
}
