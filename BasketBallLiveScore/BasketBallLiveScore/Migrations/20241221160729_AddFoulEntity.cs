using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    /// <inheritdoc />
    public partial class AddFoulEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foul",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommittingPlayerId = table.Column<int>(type: "int", nullable: false),
                    AgainstPlayerId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    FoulType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasketballGameId = table.Column<int>(type: "int", nullable: false),
                    Quarter = table.Column<int>(type: "int", nullable: false),
                    Chrono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foul", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foul_Games_BasketballGameId",
                        column: x => x.BasketballGameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foul_Players_AgainstPlayerId",
                        column: x => x.AgainstPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foul_Players_CommittingPlayerId",
                        column: x => x.CommittingPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foul_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foul_AgainstPlayerId",
                table: "Foul",
                column: "AgainstPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Foul_BasketballGameId",
                table: "Foul",
                column: "BasketballGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Foul_CommittingPlayerId",
                table: "Foul",
                column: "CommittingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Foul_TeamId",
                table: "Foul",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foul");
        }
    }
}
