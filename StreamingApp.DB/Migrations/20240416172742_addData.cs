using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations;

/// <inheritdoc />
public partial class addData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "TimeZoneUTC",
            table: "Status",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "EmotesCondition",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                MoneyAmmount = table.Column<int>(type: "int", nullable: false),
                BitAmmount = table.Column<int>(type: "int", nullable: false),
                ChannelPoints = table.Column<int>(type: "int", nullable: false),
                SubAmmount = table.Column<int>(type: "int", nullable: false),
                ExactAmmount = table.Column<bool>(type: "bit", nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false),
                UseTTS = table.Column<bool>(type: "bit", nullable: false),
                EmoteId = table.Column<int>(type: "int", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmotesCondition", x => x.Id);
                table.ForeignKey(
                    name: "FK_EmotesCondition_Emotes_EmoteId",
                    column: x => x.EmoteId,
                    principalTable: "Emotes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EmotesCondition_EmoteId",
            table: "EmotesCondition",
            column: "EmoteId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EmotesCondition");

        migrationBuilder.DropColumn(
            name: "TimeZoneUTC",
            table: "Status");
    }
}
