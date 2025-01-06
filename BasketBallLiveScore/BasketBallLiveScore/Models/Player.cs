using System.Collections.Generic;

namespace BasketBallLiveScore.Models
{
    public class Player : Member
    {
        public required int Number { get; set; }
        public required bool IsCaptain { get; set; }
        public required bool IsPlaying { get; set; }
        public required int PlayerTeamId { get; set; }
    }
}
