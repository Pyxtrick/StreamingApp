using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations;

/// <inheritdoc />
public partial class initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Achievements",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GiftedSubsCount = table.Column<int>(type: "int", nullable: false),
                GiftedBitsCount = table.Column<int>(type: "int", nullable: false),
                GiftedDonationCount = table.Column<int>(type: "int", nullable: false),
                WachedStreams = table.Column<int>(type: "int", nullable: false),
                LastStreamSeen = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Achievements", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Ban",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IsBaned = table.Column<bool>(type: "bit", nullable: false),
                TimeOutAmount = table.Column<int>(type: "int", nullable: false),
                MessagesDeletedAmount = table.Column<int>(type: "int", nullable: false),
                BanedAmount = table.Column<int>(type: "int", nullable: false),
                BanedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastMessage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ban", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CommandAndResponse",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Command = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Response = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false),
                Auth = table.Column<int>(type: "int", nullable: false),
                Category = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CommandAndResponse", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Emotes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                Sound = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                Video = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                TimesUsed = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Emotes", x => x.Id);
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
                GameCategory = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GameInfo", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "GameStream",
            columns: table => new
            {
                GameCategoryId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StreamId = table.Column<int>(type: "int", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GameStream", x => x.GameCategoryId);
            });

        migrationBuilder.CreateTable(
            name: "SpecialWords",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                TimesUsed = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SpecialWords", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "StreamHistory",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StreamTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                GameStreamId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StreamHistory", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Sub",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SubGiffted = table.Column<bool>(type: "bit", nullable: false),
                CurrentySubscribed = table.Column<bool>(type: "bit", nullable: false),
                SubscribedTime = table.Column<int>(type: "int", nullable: false),
                CurrentTier = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sub", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TwitchDetailId = table.Column<int>(type: "int", nullable: false),
                StatusId = table.Column<int>(type: "int", nullable: false),
                TwitchAchievementsId = table.Column<int>(type: "int", nullable: false),
                BanId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserDetail",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserDetail", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Status",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TwitchSubId = table.Column<int>(type: "int", nullable: false),
                FirstChatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                FallowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsVIP = table.Column<bool>(type: "bit", nullable: false),
                IsMod = table.Column<bool>(type: "bit", nullable: false),
                IsVerified = table.Column<bool>(type: "bit", nullable: false),
                IsFriend = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Status", x => x.Id);
                table.ForeignKey(
                    name: "FK_Status_Sub_TwitchSubId",
                    column: x => x.TwitchSubId,
                    principalTable: "Sub",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Status_TwitchSubId",
            table: "Status",
            column: "TwitchSubId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Achievements");

        migrationBuilder.DropTable(
            name: "Ban");

        migrationBuilder.DropTable(
            name: "CommandAndResponse");

        migrationBuilder.DropTable(
            name: "Emotes");

        migrationBuilder.DropTable(
            name: "GameInfo");

        migrationBuilder.DropTable(
            name: "GameStream");

        migrationBuilder.DropTable(
            name: "SpecialWords");

        migrationBuilder.DropTable(
            name: "Status");

        migrationBuilder.DropTable(
            name: "StreamHistory");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "UserDetail");

        migrationBuilder.DropTable(
            name: "Sub");
    }
}
