namespace BasketBallLiveScore.Models
{
    public class TimerState
    {
        public int Id { get; set; } // Identifiant unique de l'état du chronomètre

        public int QuarterCount { get; set; }
        public int QuarterDuration { get; set; }

        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int CurrentPeriod { get; set; }
        public bool IsRunning { get; set; }

        // Identifiant du jeu de basket auquel ce chronomètre appartient
        public int BasketballGameId { get; set; }
    }
}
