using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    /// <inheritdoc />
    public partial class AddTimerStateTimeOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTimeout",
                table: "TimerStates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TimeoutDuration",
                table: "TimerStates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTimeout",
                table: "TimerStates");

            migrationBuilder.DropColumn(
                name: "TimeoutDuration",
                table: "TimerStates");
        }
    }
}
