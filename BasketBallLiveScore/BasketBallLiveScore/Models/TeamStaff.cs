namespace BasketBallLiveScore.Models
{
    public class TeamStaff : Member
    {
        public required string Position { get; set; }
        public required int StaffTeamId { get; set; }
    }
}
