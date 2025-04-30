using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Sound = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Video = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Html = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    videoLeght = table.Column<int>(type: "int", nullable: false),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMute = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ban",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsBaned = table.Column<bool>(type: "bit", nullable: false),
                    IsWatchList = table.Column<bool>(type: "bit", nullable: false),
                    IsExcludeQueue = table.Column<bool>(type: "bit", nullable: false),
                    ExcludePole = table.Column<bool>(type: "bit", nullable: false),
                    IsExcludeChat = table.Column<bool>(type: "bit", nullable: false),
                    ExcludeReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOutAmount = table.Column<int>(type: "int", nullable: false),
                    MessagesDeletedAmount = table.Column<int>(type: "int", nullable: false),
                    BanedAmount = table.Column<int>(type: "int", nullable: false),
                    BanedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastMessage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ban", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Game = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInfo", x => x.Id);
                });

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
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllChat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuteAllerts = table.Column<bool>(type: "bit", nullable: false),
                    MuteChatMessages = table.Column<bool>(type: "bit", nullable: false),
                    ComunityDayActive = table.Column<bool>(type: "bit", nullable: false),
                    Delay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllertDelayS = table.Column<int>(type: "int", nullable: false),
                    TimeOutSeconds = table.Column<int>(type: "int", nullable: false),
                    SpamAmmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstChatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FallowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVIP = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsRaider = table.Column<bool>(type: "bit", nullable: false),
                    IsMod = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreamHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreamTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VodUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreamStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StreamEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamHistory", x => x.Id);
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
                    ScheduleTime = table.Column<int>(type: "int", nullable: false),
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
                name: "Sub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    SubGiffted = table.Column<bool>(type: "bit", nullable: false),
                    CurrentySubscribed = table.Column<bool>(type: "bit", nullable: false),
                    SubscribedTime = table.Column<int>(type: "int", nullable: false),
                    CurrentTier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sub_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    BanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Ban_BanId",
                        column: x => x.BanId,
                        principalTable: "Ban",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamGame",
                columns: table => new
                {
                    StreamGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameCategoryId = table.Column<int>(type: "int", nullable: false),
                    StreamId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamGame", x => x.StreamGameId);
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
                name: "StreamHighlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreamId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighlightUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighlighteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamHighlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamHighlights_StreamHistory_StreamId",
                        column: x => x.StreamId,
                        principalTable: "StreamHistory",
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
                    TargetDataId = table.Column<int>(type: "int", nullable: true),
                    CommandAndResponseId = table.Column<int>(type: "int", nullable: true),
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
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GiftedSubsCount = table.Column<int>(type: "int", nullable: false),
                    GiftedBitsCount = table.Column<int>(type: "int", nullable: false),
                    GiftedDonationCount = table.Column<int>(type: "int", nullable: false),
                    WachedStreams = table.Column<int>(type: "int", nullable: false),
                    LastStreamSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstStreamSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExternalUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppAuthEnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDetail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandAndResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Command = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Response = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Auth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasLogic = table.Column<bool>(type: "bit", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandAndResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandAndResponse_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "Id");
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
                    AlertId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetData_Alert_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alert",
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
                name: "IX_Achievements_UserId",
                table: "Achievements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_PoleId",
                table: "Choice",
                column: "PoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandAndResponse_TargetId",
                table: "CommandAndResponse",
                column: "TargetId",
                unique: true,
                filter: "[TargetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StreamGame_GameCategoryId",
                table: "StreamGame",
                column: "GameCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamGame_StreamId",
                table: "StreamGame",
                column: "StreamId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamHighlights_StreamId",
                table: "StreamHighlights",
                column: "StreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Sub_StatusId",
                table: "Sub",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Target_TriggerId",
                table: "Target",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetData_AlertId",
                table: "TargetData",
                column: "AlertId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetData_TargetId",
                table: "TargetData",
                column: "TargetId",
                unique: true);

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
                name: "IX_UserDetail_UserId",
                table: "UserDetail",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropTable(
                name: "CommandAndResponse");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SpecialWords");

            migrationBuilder.DropTable(
                name: "StreamGame");

            migrationBuilder.DropTable(
                name: "StreamHighlights");

            migrationBuilder.DropTable(
                name: "Sub");

            migrationBuilder.DropTable(
                name: "TargetData");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "Pole");

            migrationBuilder.DropTable(
                name: "GameInfo");

            migrationBuilder.DropTable(
                name: "StreamHistory");

            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Trigger");

            migrationBuilder.DropTable(
                name: "Ban");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
