using System;
using System.Collections.Generic;

namespace BasketBallLiveScore.Models
{
    public class BasketballGame
    {
        public int Id { get; set; }
        public required string GameNumber { get; set; }
        public required string Competition { get; set; }
        public DateTime GameDate { get; set; }
        public string? Venue { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? SpectatorCount { get; set; }
        public required int QuarterCount { get; set; }
        public required int QuarterDuration { get; set; }
        public required int TimeoutDuration { get; set; }

        // Clé étrangère pour l'utilisateur (SetupManager)
        public required int SetupManagerId { get; set; }


        //public ICollection<Team> Teams { get; set; } = new List<Team>();
        // public ICollection<Player> StartingFive { get; set; } = new List<Player>();
        //public ICollection<GameStaff> GameStaff { get; set; } = new List<GameStaff>();

        //public ICollection<BasketballEvent> Events { get; set; } = new List<BasketballEvent>();
    }
}
