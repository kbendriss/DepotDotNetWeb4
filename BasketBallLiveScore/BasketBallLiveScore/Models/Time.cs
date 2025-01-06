namespace BasketBallLiveScore.Models
{
    public abstract class Time
    {
        public required int Quarter { get; set; } // Quarter during which the points were scored
        public required string Chrono { get; set; } // Time in the match (e.g., "00:05:23")
    }
}
