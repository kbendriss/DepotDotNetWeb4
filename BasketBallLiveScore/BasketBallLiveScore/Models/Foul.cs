namespace BasketBallLiveScore.Models
{
    public class Foul : Time
    {
        public int Id { get; set; } // Clé primaire
        public required int CommittingPlayerId { get; set; } // ID du joueur qui commet la faute
        public required int AgainstPlayerId { get; set; } // ID du joueur contre qui la faute est commise
        public required int TeamId { get; set; } // ID de l'équipe qui commet la faute
        public required string FoulType { get; set; } // Type de faute (P0, P1, etc.)
        public required int BasketballGameId { get; set; } // ID du match
    }
}
