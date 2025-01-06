using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStartingFive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_BasketballGameId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_BasketballGameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BasketballGameId",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasketballGameId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_BasketballGameId",
                table: "Players",
                column: "BasketballGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_BasketballGameId",
                table: "Players",
                column: "BasketballGameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
