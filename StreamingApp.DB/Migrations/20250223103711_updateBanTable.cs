using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class updateBanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWatchList",
                table: "Ban",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWatchList",
                table: "Ban");
        }
    }
}
