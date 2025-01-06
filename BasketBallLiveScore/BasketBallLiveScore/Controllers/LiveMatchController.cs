using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketBallLiveScore.Data;
using BasketBallLiveScore.Models;
using System.Threading.Tasks;

namespace BasketBallLiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveMatchController : ControllerBase
    {
        private readonly BasketballContext _context;

        public LiveMatchController(BasketballContext context)
        {
            _context = context;
        }

        [HttpPost("score")]
        public async Task<IActionResult> MarkPoints([FromBody] PointsScored pointsScored)
        {
            try
            {
                // Validation : Vérifiez si le joueur existe
                var playerExists = await _context.Players.AnyAsync(p => p.Id == pointsScored.PlayerId);
                if (!playerExists)
                {
                    return NotFound(new { message = "Player not found." });
                }

                // Validation : Vérifiez si l'équipe existe
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == pointsScored.TeamId);
                if (!teamExists)
                {
                    return NotFound(new { message = "Team not found." });
                }

                // Validation : Vérifiez si le match existe
                var gameExists = await _context.Games.AnyAsync(g => g.Id == pointsScored.BasketballGameId);
                if (!gameExists)
                {
                    return NotFound(new { message = "Basketball game not found." });
                }

                // Ajouter l'entrée de points à la base de données
                _context.PointsScored.Add(pointsScored);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Points scored successfully recorded.", pointsId = pointsScored.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while recording points.", details = ex.Message });
            }
        }


        [HttpPost("commitFoul")]
        public async Task<IActionResult> CommitFoul([FromBody] Foul foul)
        {
            try
            {
                // Validation : Vérifiez si le joueur qui commet la faute existe
                var committingPlayerExists = await _context.Players.AnyAsync(p => p.Id == foul.CommittingPlayerId);
                if (!committingPlayerExists)
                {
                    return NotFound(new { message = "Committing player not found." });
                }

                // Validation : Vérifiez si le joueur contre qui la faute est commise existe
                var againstPlayerExists = await _context.Players.AnyAsync(p => p.Id == foul.AgainstPlayerId);
                if (!againstPlayerExists)
                {
                    return NotFound(new { message = "Player against whom the foul is committed not found." });
                }

                // Validation : Vérifiez si l'équipe existe
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == foul.TeamId);
                if (!teamExists)
                {
                    return NotFound(new { message = "Team not found." });
                }

                // Validation : Vérifiez si le match existe
                var gameExists = await _context.Games.AnyAsync(g => g.Id == foul.BasketballGameId);
                if (!gameExists)
                {
                    return NotFound(new { message = "Basketball game not found." });
                }

                // Ajouter l'entrée de faute à la base de données
                _context.Fouls.Add(foul);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Foul successfully recorded.", foulId = foul.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while recording the foul.", details = ex.Message });
            }
        }

        [HttpPost("substitute")]
        public async Task<IActionResult> SubstitutePlayer([FromBody] Substitution substitution)
        {
            try
            {
                // Validation : Vérifiez si le joueur qui sort existe
                var playerOut = await _context.Players.FirstOrDefaultAsync(p => p.Id == substitution.PlayerOutId);
                if (playerOut == null)
                {
                    return NotFound(new { message = "Player who is substituted out not found." });
                }

                // Validation : Vérifiez si le joueur qui entre existe
                var playerIn = await _context.Players.FirstOrDefaultAsync(p => p.Id == substitution.PlayerInId);
                if (playerIn == null)
                {
                    return NotFound(new { message = "Player who is substituted in not found." });
                }

                // Validation : Vérifiez si l'équipe existe
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == substitution.TeamId);
                if (!teamExists)
                {
                    return NotFound(new { message = "Team not found." });
                }

                // Validation : Vérifiez si le match existe
                var gameExists = await _context.Games.AnyAsync(g => g.Id == substitution.GameId);
                if (!gameExists)
                {
                    return NotFound(new { message = "Basketball game not found." });
                }

                // Vérification : Assurez-vous que les deux joueurs appartiennent à la même équipe
                if (playerOut.PlayerTeamId != substitution.TeamId || playerIn.PlayerTeamId != substitution.TeamId)
                {
                    return BadRequest(new { message = "Both players must belong to the same team." });
                }

                // Mettre à jour les propriétés IsPlaying des joueurs
                playerOut.IsPlaying = false; // Le joueur sortant n'est plus en jeu
                playerIn.IsPlaying = true;  // Le joueur entrant est maintenant en jeu

                // Si le joueur sortant est capitaine, transférer le rôle au joueur entrant
                if (playerOut.IsCaptain)
                {
                    playerOut.IsCaptain = false;
                    playerIn.IsCaptain = true;
                }

                // Enregistrez les modifications dans la base de données
                _context.Players.Update(playerOut);
                _context.Players.Update(playerIn);

                // Enregistrez la substitution dans la base de données
                _context.Substitutions.Add(substitution);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Substitution successfully recorded.", substitutionId = substitution.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while recording the substitution.", details = ex.Message });
            }
        }

        [HttpGet("points/{basketballGameId}")]
        public async Task<IActionResult> GetPoints(int basketballGameId)
        {
            try
            {
                var points = await _context.PointsScored
                    .Where(p => p.BasketballGameId == basketballGameId)
                    .ToListAsync();

                if (!points.Any())
                {
                    return NotFound(new { message = "No points found for this game." });
                }

                return Ok(points);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving points.", details = ex.Message });
            }
        }

        [HttpGet("fouls/{basketballGameId}")]
        public async Task<IActionResult> GetFouls(int basketballGameId)
        {
            try
            {
                var fouls = await _context.Fouls
                    .Where(f => f.BasketballGameId == basketballGameId)
                    .ToListAsync();

                if (!fouls.Any())
                {
                    return NotFound(new { message = "No fouls found for this game." });
                }

                return Ok(fouls);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving fouls.", details = ex.Message });
            }
        }

        [HttpGet("substitutions/{basketballGameId}")]
        public async Task<IActionResult> GetSubstitutions(int basketballGameId)
        {
            try
            {
                var substitutions = await _context.Substitutions
                    .Where(s => s.GameId == basketballGameId)
                    .ToListAsync();

                if (!substitutions.Any())
                {
                    return NotFound(new { message = "No substitutions found for this game." });
                }

                return Ok(substitutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving substitutions.", details = ex.Message });
            }
        }

        [HttpGet("actions/{gameId}")]
        public async Task<IActionResult> GetActions(int gameId)
        {
            // Vérifier si le match existe
            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!gameExists)
            {
                return NotFound(new { message = "Basketball game not found." });
            }

            var actions = new List<dynamic>();

            // Récupérer les points marqués
            var points = await _context.PointsScored
                .Where(p => p.BasketballGameId == gameId)
                .ToListAsync();

            foreach (var p in points)
            {
                var playerName = await GetPlayerName(p.PlayerId);
                actions.Add(new
                {
                    Quarter = p.Quarter,
                    Chrono = p.Chrono,
                    Minutes = int.Parse(p.Chrono.Split(':')[0]),
                    Seconds = int.Parse(p.Chrono.Split(':')[1]),
                    Description = $"{playerName} scored {p.Points} points in Quarter {p.Quarter} at {p.Chrono}"
                });
            }

            // Récupérer les fautes
            var fouls = await _context.Fouls
                .Where(f => f.BasketballGameId == gameId)
                .ToListAsync();

            foreach (var f in fouls)
            {
                var committingPlayerName = await GetPlayerName(f.CommittingPlayerId);
                actions.Add(new
                {
                    Quarter = f.Quarter,
                    Chrono = f.Chrono,
                    Minutes = int.Parse(f.Chrono.Split(':')[0]),
                    Seconds = int.Parse(f.Chrono.Split(':')[1]),
                    Description = $"{committingPlayerName} committed a {f.FoulType} foul in Quarter {f.Quarter} at {f.Chrono}"
                });
            }

            // Récupérer les substitutions
            var substitutions = await _context.Substitutions
                .Where(s => s.GameId == gameId)
                .ToListAsync();

            foreach (var s in substitutions)
            {
                var playerOutName = await GetPlayerName(s.PlayerOutId);
                var playerInName = await GetPlayerName(s.PlayerInId);
                actions.Add(new
                {
                    Quarter = s.Quarter,
                    Chrono = s.Chrono,
                    Minutes = int.Parse(s.Chrono.Split(':')[0]),
                    Seconds = int.Parse(s.Chrono.Split(':')[1]),
                    Description = $"{playerOutName} substituted by {playerInName} in Quarter {s.Quarter} at {s.Chrono}"
                });
            }

            // Trier les actions par quart (descendant), minutes (descendant) et secondes (descendant)
            var sortedActions = actions
                .OrderByDescending(a => a.Quarter)
                .ThenByDescending(a => a.Minutes)
                .ThenByDescending(a => a.Seconds)
                .ToList();

            return Ok(sortedActions);
        }

        private async Task<string> GetPlayerName(int playerId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
            if (player == null)
            {
                return "Unknown Player";
            }
            return $"{player.FirstName} {player.LastName}";
        }

        [HttpDelete("actions/{gameId}")]
        public async Task<IActionResult> DeleteActions(int gameId)
        {
            // Vérifier si le match existe
            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!gameExists)
            {
                return NotFound(new { message = "Basketball game not found." });
            }

            try
            {
                // Supprimer les points marqués
                var points = await _context.PointsScored
                    .Where(p => p.BasketballGameId == gameId)
                    .ToListAsync();
                if (points.Any())
                {
                    _context.PointsScored.RemoveRange(points);
                }

                // Supprimer les fautes
                var fouls = await _context.Fouls
                    .Where(f => f.BasketballGameId == gameId)
                    .ToListAsync();
                if (fouls.Any())
                {
                    _context.Fouls.RemoveRange(fouls);
                }

                // Supprimer les substitutions
                var substitutions = await _context.Substitutions
                    .Where(s => s.GameId == gameId)
                    .ToListAsync();
                if (substitutions.Any())
                {
                    _context.Substitutions.RemoveRange(substitutions);
                }

                // Sauvegarder les modifications
                await _context.SaveChangesAsync();

                return Ok(new { message = "All actions for the game have been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting actions.", details = ex.Message });
            }
        }


        [HttpGet("teamPoints")]
        public async Task<IActionResult> GetTeamPoints(int basketballGameId, int teamId)
        {
            try
            {
                // Vérifiez si la partie existe
                var gameExists = await _context.Games.AnyAsync(g => g.Id == basketballGameId);
                if (!gameExists)
                {
                    return NotFound(new { message = "Basketball game not found." });
                }

                // Vérifiez si l'équipe existe
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == teamId);
                if (!teamExists)
                {
                    return NotFound(new { message = "Team not found." });
                }

                // Additionnez les points pour l'équipe et la partie spécifiées
                var totalPoints = await _context.PointsScored
                    .Where(p => p.BasketballGameId == basketballGameId && p.TeamId == teamId)
                    .SumAsync(p => p.Points);

                return Ok(new { teamId, basketballGameId, totalPoints });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching team points.", details = ex.Message });
            }
        }

    }
}
