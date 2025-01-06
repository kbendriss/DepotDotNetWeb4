using System;

namespace BasketBallLiveScore.Models
{
    public class GameStaff : Member
    {
        // Le rôle spécifique du personnel (scorekeeper, timekeeper, statistician, encoder, etc.)
        public required string Role { get; set; }

        // Clé étrangère pour le match auquel le personnel est assigné
        public required int BasketballGameId { get; set; }
    }
}
