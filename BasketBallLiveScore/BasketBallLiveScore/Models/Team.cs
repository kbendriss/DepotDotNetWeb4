using System.Collections.Generic;

namespace BasketBallLiveScore.Models
{
    public class Team
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ShortCode { get; set; }
        public string? LongCode { get; set; }
        public string Color { get; set; } = "White";

        public required int BasketballGameId { get; set; }

        //public ICollection<Player> Players { get; set; } = new List<Player>();
        //public ICollection<TeamStaff> TeamStaff { get; set; } = new List<TeamStaff>();
    }
}
