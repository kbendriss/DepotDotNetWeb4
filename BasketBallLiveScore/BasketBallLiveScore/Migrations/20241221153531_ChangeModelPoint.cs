using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsScored_Teams_TeamId",
                table: "PointsScored");

            migrationBuilder.DropColumn(
                name: "FoulType",
                table: "PointsScored");

            migrationBuilder.AddColumn<int>(
                name: "BasketballGameId",
                table: "PointsScored",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "PointsScored",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PointsScored_BasketballGameId",
                table: "PointsScored",
                column: "BasketballGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsScored_Games_BasketballGameId",
                table: "PointsScored",
                column: "BasketballGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PointsScored_Teams_TeamId",
                table: "PointsScored",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsScored_Games_BasketballGameId",
                table: "PointsScored");

            migrationBuilder.DropForeignKey(
                name: "FK_PointsScored_Teams_TeamId",
                table: "PointsScored");

            migrationBuilder.DropIndex(
                name: "IX_PointsScored_BasketballGameId",
                table: "PointsScored");

            migrationBuilder.DropColumn(
                name: "BasketballGameId",
                table: "PointsScored");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "PointsScored");

            migrationBuilder.AddColumn<string>(
                name: "FoulType",
                table: "PointsScored",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsScored_Teams_TeamId",
                table: "PointsScored",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
