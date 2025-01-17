using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmotesCondition");

            migrationBuilder.DropTable(
                name: "GameStream");

            migrationBuilder.DropIndex(
                name: "IX_Status_TwitchSubId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "GameStreamId",
                table: "StreamHistory");

            migrationBuilder.DropColumn(
                name: "StreamTime",
                table: "StreamHighlights");

            migrationBuilder.RenameColumn(
                name: "TimeOfDay",
                table: "StreamHighlights",
                newName: "HighlighteTime");

            migrationBuilder.RenameColumn(
                name: "StreamHistoryId",
                table: "StreamHighlights",
                newName: "StreamId");

            migrationBuilder.AddColumn<DateTime>(
                name: "StreamEnd",
                table: "StreamHistory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StreamStart",
                table: "StreamHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VodUrl",
                table: "StreamHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HighlightUrl",
                table: "StreamHighlights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRaider",
                table: "Status",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Pole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPole = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreamGame",
                columns: table => new
                {
                    GameCategoryId = table.Column<int>(type: "int", nullable: false),
                    StreamId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamGame", x => x.GameCategoryId);
                    table.ForeignKey(
                        name: "FK_StreamGame_GameInfo_GameCategoryId",
                        column: x => x.GameCategoryId,
                        principalTable: "GameInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StreamGame_StreamHistory_StreamId",
                        column: x => x.StreamId,
                        principalTable: "StreamHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trigger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TriggerCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ammount = table.Column<int>(type: "int", nullable: false),
                    AmmountCloser = table.Column<bool>(type: "bit", nullable: false),
                    ExactAmmount = table.Column<bool>(type: "bit", nullable: false),
                    Auth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trigger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoleId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    VotesPoints = table.Column<int>(type: "int", nullable: false),
                    ChannelPointsVotes = table.Column<int>(type: "int", nullable: false),
                    BitsVotes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choice_Pole_PoleId",
                        column: x => x.PoleId,
                        principalTable: "Pole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Target",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggerId = table.Column<int>(type: "int", nullable: false),
                    TargetDataId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSameTime = table.Column<bool>(type: "bit", nullable: false),
                    Chance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Target", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Target_Trigger_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Trigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    EmoteId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetData_Emotes_EmoteId",
                        column: x => x.EmoteId,
                        principalTable: "Emotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetData_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_BanId",
                table: "User",
                column: "BanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_StatusId",
                table: "User",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_TwitchAchievementsId",
                table: "User",
                column: "TwitchAchievementsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_TwitchDetailId",
                table: "User",
                column: "TwitchDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StreamHighlights_StreamId",
                table: "StreamHighlights",
                column: "StreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_TwitchSubId",
                table: "Status",
                column: "TwitchSubId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Choice_PoleId",
                table: "Choice",
                column: "PoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamGame_StreamId",
                table: "StreamGame",
                column: "StreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Target_TriggerId",
                table: "Target",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetData_EmoteId",
                table: "TargetData",
                column: "EmoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetData_TargetId",
                table: "TargetData",
                column: "TargetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StreamHighlights_StreamHistory_StreamId",
                table: "StreamHighlights",
                column: "StreamId",
                principalTable: "StreamHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Achievements_TwitchAchievementsId",
                table: "User",
                column: "TwitchAchievementsId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Ban_BanId",
                table: "User",
                column: "BanId",
                principalTable: "Ban",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Status_StatusId",
                table: "User",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserDetail_TwitchDetailId",
                table: "User",
                column: "TwitchDetailId",
                principalTable: "UserDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamHighlights_StreamHistory_StreamId",
                table: "StreamHighlights");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Achievements_TwitchAchievementsId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Ban_BanId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Status_StatusId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserDetail_TwitchDetailId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropTable(
                name: "StreamGame");

            migrationBuilder.DropTable(
                name: "TargetData");

            migrationBuilder.DropTable(
                name: "Pole");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.DropTable(
                name: "Trigger");

            migrationBuilder.DropIndex(
                name: "IX_User_BanId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StatusId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_TwitchAchievementsId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_TwitchDetailId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_StreamHighlights_StreamId",
                table: "StreamHighlights");

            migrationBuilder.DropIndex(
                name: "IX_Status_TwitchSubId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "StreamEnd",
                table: "StreamHistory");

            migrationBuilder.DropColumn(
                name: "StreamStart",
                table: "StreamHistory");

            migrationBuilder.DropColumn(
                name: "VodUrl",
                table: "StreamHistory");

            migrationBuilder.DropColumn(
                name: "HighlightUrl",
                table: "StreamHighlights");

            migrationBuilder.DropColumn(
                name: "IsRaider",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "StreamId",
                table: "StreamHighlights",
                newName: "StreamHistoryId");

            migrationBuilder.RenameColumn(
                name: "HighlighteTime",
                table: "StreamHighlights",
                newName: "TimeOfDay");

            migrationBuilder.AddColumn<int>(
                name: "GameStreamId",
                table: "StreamHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StreamTime",
                table: "StreamHighlights",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "EmotesCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmoteId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BitAmmount = table.Column<int>(type: "int", nullable: false),
                    ChannelPoints = table.Column<int>(type: "int", nullable: false),
                    ExactAmmount = table.Column<bool>(type: "bit", nullable: false),
                    MoneyAmmount = table.Column<int>(type: "int", nullable: false),
                    SubAmmount = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UseTTS = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "GameStream",
                columns: table => new
                {
                    GameCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StreamId = table.Column<int>(type: "int", nullable: false),
                    VodUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStream", x => x.GameCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_TwitchSubId",
                table: "Status",
                column: "TwitchSubId");

            migrationBuilder.CreateIndex(
                name: "IX_EmotesCondition_EmoteId",
                table: "EmotesCondition",
                column: "EmoteId");
        }
    }
}
