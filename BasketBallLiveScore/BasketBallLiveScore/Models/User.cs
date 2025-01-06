using System.Collections.Generic;

namespace BasketBallLiveScore.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } // Nouveau champ
        public required string LastName { get; set; }  // Nouveau champ
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        // public ICollection<BasketballGame> Games { get; set; } = new List<BasketballGame>();
    }
}
