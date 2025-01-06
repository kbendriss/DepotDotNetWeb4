using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketBallLiveScore.Data;
using BasketBallLiveScore.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BasketBallLiveScore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasketballGameController : ControllerBase
    {
        private readonly BasketballContext _context;

        public BasketballGameController(BasketballContext context)
        {
            _context = context;
        }

        // Méthode pour créer un jeu
        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] BasketballGame basketballGame)
        {
            try
            {
                // Vérification si le SetupManager existe
                var setupManager = await _context.Users.FindAsync(basketballGame.SetupManagerId);
                if (setupManager == null)
                {
                    return BadRequest(new { message = "Setup Manager not found." });
                }

                // Ajouter le jeu à la base de données
                _context.Games.Add(basketballGame);
                await _context.SaveChangesAsync();

                // Retourner l'ID du jeu créé
                return CreatedAtAction(nameof(CreateGame), new { id = basketballGame.Id }, basketballGame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the game.", details = ex.Message });
            }
        }

        // Méthode pour ajouter une équipe à un jeu existant
        [HttpPost("{gameId}/addTeam")]
        public async Task<IActionResult> AddTeamToGame(int gameId, [FromBody] Team team)
        {
            try
            {
                // Vérification si le jeu existe
                var basketballGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
                if (basketballGame == null)
                {
                    return NotFound(new { message = "Game not found." });
                }

                // Vérification si une équipe avec le même ID de jeu existe déjà
                var existingTeam = await _context.Teams.FirstOrDefaultAsync(t => t.BasketballGameId == gameId && t.Id == team.Id);
                if (existingTeam != null)
                {
                    return BadRequest(new { message = "This team is already associated with the game." });
                }

                // Associer l'équipe au jeu
                team.BasketballGameId = gameId;
                _context.Teams.Add(team);

                // Sauvegarder les modifications
                await _context.SaveChangesAsync();

                return Ok(new { message = "Team added successfully to the game.", teamId = team.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the team to the game.", details = ex.Message });
            }
        }


        [HttpPost("{teamId}/addPlayer")]
        public async Task<IActionResult> AddPlayerToTeam(int teamId, [FromBody] Player player)
        {
            try
            {
                // Vérification si l'équipe existe
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == teamId);
                if (!teamExists)
                {
                    return NotFound(new { message = "Team not found." });
                }

                // Vérification si le joueur existe déjà dans la base de données (par exemple via le numéro de licence et teamId)
                var existingPlayer = await _context.Players
                    .FirstOrDefaultAsync(p => p.PlayerTeamId == teamId && p.LicenseNumber == player.LicenseNumber);

                if (existingPlayer != null)
                {
                    return BadRequest(new { message = "This player is already associated with the team." });
                }

                // Associer le joueur à l'équipe
                player.PlayerTeamId = teamId;
                _context.Players.Add(player);

                // Sauvegarder les modifications
                await _context.SaveChangesAsync();

                return Ok(new { message = "Player added successfully to the team.", playerId = player.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the player to the team.", details = ex.Message });
            }
        }


        [HttpPost("{teamId}/addTeamStaff")]
        public async Task<IActionResult> AddTeamStaffToTeam(int teamId, [FromBody] TeamStaff teamStaff)
        {
            try
            {
                // Vérification si l'équipe existe
                var teamExists = await _context.Teams.AnyAsync(t => t.Id == teamId);
                if (!teamExists)
                {
                    return NotFound(new { message = "Team not found." });
                }

                // Vérification si le membre du personnel existe déjà dans la base de données pour cette équipe
                var existingStaff = await _context.TeamStaff
                    .FirstOrDefaultAsync(ts => ts.StaffTeamId == teamId && ts.LicenseNumber == teamStaff.LicenseNumber);

                if (existingStaff != null)
                {
                    return BadRequest(new { message = "This staff member is already associated with the team." });
                }

                // Associer le membre du personnel à l'équipe
                teamStaff.StaffTeamId = teamId;
                _context.TeamStaff.Add(teamStaff);

                // Sauvegarder les modifications
                await _context.SaveChangesAsync();

                return Ok(new { message = "Team staff added successfully to the team.", teamStaffId = teamStaff.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the team staff to the team.", details = ex.Message });
            }
        }

        [HttpPost("{gameId}/addGameStaff")]
        public async Task<IActionResult> AddGameStaffToGame(int gameId, [FromBody] GameStaff gameStaff)
        {
            try
            {
                // Vérification si le match existe
                var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
                if (!gameExists)
                {
                    return NotFound(new { message = "Game not found." });
                }

                // Vérification si le membre du personnel existe déjà pour ce match (par exemple via le numéro de licence)
                var existingStaff = await _context.GameStaff
                    .FirstOrDefaultAsync(gs => gs.BasketballGameId == gameId && gs.LicenseNumber == gameStaff.LicenseNumber);

                if (existingStaff != null)
                {
                    return BadRequest(new { message = "This game staff member is already associated with the game." });
                }

                // Associer le membre du personnel au match
                gameStaff.BasketballGameId = gameId;
                _context.GameStaff.Add(gameStaff);

                // Sauvegarder les modifications
                await _context.SaveChangesAsync();

                return Ok(new { message = "Game staff added successfully to the game.", gameStaffId = gameStaff.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the game staff to the game.", details = ex.Message });
            }
        }

        [HttpGet("validateGameNumber/{gameNumber}")]
        public async Task<IActionResult> ValidateGameNumber(string gameNumber)
        {
            try
            {
                var exists = await _context.Games.AnyAsync(g => g.GameNumber == gameNumber);
                return Ok(new { isValid = !exists }); // Retourne `isValid` comme `true` si le numéro n'existe pas.
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while validating the game number.", details = ex.Message });
            }
        }

        [HttpGet("getGame/{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            try
            {
                // Rechercher un jeu spécifique par son Id
                var game = await _context.Games
                    .Where(g => g.Id == id)
                    .Select(g => new
                    {
                        g.Id,
                        g.GameNumber,
                        g.Competition,
                        g.GameDate,
                        g.Venue,
                        g.City,
                        g.Country,
                        g.SpectatorCount,
                        g.QuarterCount,
                        g.QuarterDuration,
                        g.TimeoutDuration,
                        g.SetupManagerId
                    })
                    .FirstOrDefaultAsync();

                // Vérifier si le jeu existe
                if (game == null)
                {
                    return NotFound(new { message = "Game not found." });
                }

                return Ok(game); // Retourner les détails du jeu
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the game.", details = ex.Message });
            }
        }

        [HttpGet("getTeamsByGameId/{basketballGameId}")]
        public async Task<IActionResult> GetTeamsByGameId(int basketballGameId)
        {
            try
            {
                // Récupérer les équipes associées au jeu
                var teams = await _context.Teams
                    .Where(t => t.BasketballGameId == basketballGameId)
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        t.ShortCode,
                        t.LongCode,
                        t.Color,
                        t.BasketballGameId
                    })
                    .ToListAsync();

                // Vérifier si des équipes existent
                if (!teams.Any())
                {
                    return NotFound(new { message = "No teams found for this game." });
                }

                return Ok(teams); // Retourner les équipes
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the teams.", details = ex.Message });
            }
        }

        [HttpGet("getPlayersByTeamId/{teamId}")]
        public async Task<IActionResult> GetPlayersByTeamId(int teamId)
        {
            try
            {
                // Récupérer les joueurs associés à l'équipe
                var players = await _context.Players
                    .Where(p => p.PlayerTeamId == teamId)
                    .Select(p => new
                    {
                        p.Id,
                        p.Number,
                        p.IsCaptain,
                        p.IsPlaying,
                        p.PlayerTeamId,
                        p.FirstName,
                        p.LastName,
                        p.BirthDate,
                        p.LicenseNumber
                    })
                    .ToListAsync();

                // Vérifier si des joueurs existent pour cette équipe
                if (!players.Any())
                {
                    return NotFound(new { message = "No players found for this team." });
                }

                return Ok(players); // Retourner les joueurs
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the players.", details = ex.Message });
            }
        }

        [HttpGet("getTeamStaffByTeamId/{teamId}")]
        public async Task<IActionResult> GetTeamStaffByTeamId(int teamId)
        {
            try
            {
                // Récupérer les membres du personnel associés à l'équipe
                var teamStaff = await _context.TeamStaff
                    .Where(ts => ts.StaffTeamId == teamId)
                    .Select(ts => new
                    {
                        ts.Id,
                        ts.FirstName,
                        ts.LastName,
                        ts.Position,
                        ts.LicenseNumber,
                        ts.StaffTeamId
                    })
                    .ToListAsync();

                // Vérifier si des membres du personnel existent pour cette équipe
                if (!teamStaff.Any())
                {
                    return NotFound(new { message = "No team staff found for this team." });
                }

                return Ok(teamStaff); // Retourner les membres du personnel
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the team staff.", details = ex.Message });
            }
        }

        [HttpGet("getGameStaffByGameId/{basketballGameId}")]
        public async Task<IActionResult> GetGameStaffByGameId(int basketballGameId)
        {
            try
            {
                // Récupérer les membres du personnel associés au jeu
                var gameStaff = await _context.GameStaff
                    .Where(gs => gs.BasketballGameId == basketballGameId)
                    .Select(gs => new
                    {
                        gs.Id,
                        gs.Role,
                        gs.BasketballGameId,
                        gs.FirstName,
                        gs.LastName,
                        gs.BirthDate,
                        gs.LicenseNumber
                    })
                    .ToListAsync();

                // Vérifier si des membres du personnel existent pour ce match
                if (!gameStaff.Any())
                {
                    return NotFound(new { message = "No game staff found for this game." });
                }

                return Ok(gameStaff); // Retourner les membres du personnel
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the game staff.", details = ex.Message });
            }
        }

        [HttpGet("getGameIdByGameNumber/{gameNumber}")]
        public async Task<IActionResult> GetGameIdByGameNumber(string gameNumber)
        {
            try
            {
                // Rechercher le jeu correspondant au GameNumber
                var game = await _context.Games
                    .Where(g => g.GameNumber == gameNumber)
                    .Select(g => new { g.Id })
                    .FirstOrDefaultAsync();

                // Vérifier si le jeu existe
                if (game == null)
                {
                    return NotFound(new { message = "Game not found for the given GameNumber." });
                }

                return Ok(new { gameId = game.Id }); // Retourner l'ID du jeu
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the game ID.", details = ex.Message });
            }
        }

    }

}
