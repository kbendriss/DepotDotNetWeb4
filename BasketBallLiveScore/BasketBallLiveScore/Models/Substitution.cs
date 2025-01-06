namespace BasketBallLiveScore.Models
{
    public class Substitution : Time
    {
        public int Id { get; set; } // Clé primaire
        public required int GameId { get; set; } // ID du match
        public required int TeamId { get; set; } // ID de l'équipe
        public required int PlayerOutId { get; set; } // ID du joueur sortant
        public required int PlayerInId { get; set; } // ID du joueur entrant
    }
}
