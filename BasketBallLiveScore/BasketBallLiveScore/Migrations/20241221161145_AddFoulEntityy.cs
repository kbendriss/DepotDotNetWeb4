using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    /// <inheritdoc />
    public partial class AddFoulEntityy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foul_Games_BasketballGameId",
                table: "Foul");

            migrationBuilder.DropForeignKey(
                name: "FK_Foul_Players_AgainstPlayerId",
                table: "Foul");

            migrationBuilder.DropForeignKey(
                name: "FK_Foul_Players_CommittingPlayerId",
                table: "Foul");

            migrationBuilder.DropForeignKey(
                name: "FK_Foul_Teams_TeamId",
                table: "Foul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foul",
                table: "Foul");

            migrationBuilder.RenameTable(
                name: "Foul",
                newName: "Fouls");

            migrationBuilder.RenameIndex(
                name: "IX_Foul_TeamId",
                table: "Fouls",
                newName: "IX_Fouls_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Foul_CommittingPlayerId",
                table: "Fouls",
                newName: "IX_Fouls_CommittingPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Foul_BasketballGameId",
                table: "Fouls",
                newName: "IX_Fouls_BasketballGameId");

            migrationBuilder.RenameIndex(
                name: "IX_Foul_AgainstPlayerId",
                table: "Fouls",
                newName: "IX_Fouls_AgainstPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fouls",
                table: "Fouls",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fouls_Games_BasketballGameId",
                table: "Fouls",
                column: "BasketballGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fouls_Players_AgainstPlayerId",
                table: "Fouls",
                column: "AgainstPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fouls_Players_CommittingPlayerId",
                table: "Fouls",
                column: "CommittingPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fouls_Teams_TeamId",
                table: "Fouls",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fouls_Games_BasketballGameId",
                table: "Fouls");

            migrationBuilder.DropForeignKey(
                name: "FK_Fouls_Players_AgainstPlayerId",
                table: "Fouls");

            migrationBuilder.DropForeignKey(
                name: "FK_Fouls_Players_CommittingPlayerId",
                table: "Fouls");

            migrationBuilder.DropForeignKey(
                name: "FK_Fouls_Teams_TeamId",
                table: "Fouls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fouls",
                table: "Fouls");

            migrationBuilder.RenameTable(
                name: "Fouls",
                newName: "Foul");

            migrationBuilder.RenameIndex(
                name: "IX_Fouls_TeamId",
                table: "Foul",
                newName: "IX_Foul_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Fouls_CommittingPlayerId",
                table: "Foul",
                newName: "IX_Foul_CommittingPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Fouls_BasketballGameId",
                table: "Foul",
                newName: "IX_Foul_BasketballGameId");

            migrationBuilder.RenameIndex(
                name: "IX_Fouls_AgainstPlayerId",
                table: "Foul",
                newName: "IX_Foul_AgainstPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foul",
                table: "Foul",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foul_Games_BasketballGameId",
                table: "Foul",
                column: "BasketballGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foul_Players_AgainstPlayerId",
                table: "Foul",
                column: "AgainstPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foul_Players_CommittingPlayerId",
                table: "Foul",
                column: "CommittingPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foul_Teams_TeamId",
                table: "Foul",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
