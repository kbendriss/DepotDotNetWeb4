using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    /// <inheritdoc />
    public partial class CHANGEMODEL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStaff_Games_BasketballGameId",
                table: "GameStaff");

            migrationBuilder.DropIndex(
                name: "IX_GameStaff_BasketballGameId",
                table: "GameStaff");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GameStaff_BasketballGameId",
                table: "GameStaff",
                column: "BasketballGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStaff_Games_BasketballGameId",
                table: "GameStaff",
                column: "BasketballGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
