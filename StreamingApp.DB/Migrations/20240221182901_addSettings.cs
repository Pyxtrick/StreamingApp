using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations;

/// <inheritdoc />
public partial class addSettings : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Comment",
            table: "SpecialWords",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "Settings",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AllChat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MuteAllerts = table.Column<bool>(type: "bit", nullable: false),
                ComunityDayActive = table.Column<bool>(type: "bit", nullable: false),
                Delay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AllertDelayS = table.Column<int>(type: "int", nullable: false),
                TimeOutSeconds = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Settings", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Settings");

        migrationBuilder.DropColumn(
            name: "Comment",
            table: "SpecialWords");
    }
}
