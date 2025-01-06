namespace BasketBallLiveScore.Models
{
    public class PointsScored : Time
    {
        public int Id { get; set; } // Primary Key
        public required int PlayerId { get; set; } // Foreign Key to Player
        public required int TeamId { get; set; } // Foreign Key to Team
        public required int BasketballGameId { get; set; } // Foreign Key to BasketballGame
        public required int Points { get; set; } // Points scored by the player
    }
}
