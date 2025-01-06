using Microsoft.AspNetCore.Mvc;
using BasketBallLiveScore.Models;
using BasketBallLiveScore.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketBallLiveScore.Controllers
{
    [ApiController]
    [Route("api/timer")]
    public class TimerController : ControllerBase
    {
        private readonly BasketballContext _context;

        public TimerController(BasketballContext context)
        {
            _context = context;
        }

        // Initialiser un nouveau timer
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializeTimer([FromBody] TimerState timerState)
        {
            var gameExists = await _context.Games.AnyAsync(g => g.Id == timerState.BasketballGameId);
            if (!gameExists)
            {
                return NotFound(new { message = "Basketball game not found." });
            }

            var timerExists = await _context.TimerStates.AnyAsync(t => t.BasketballGameId == timerState.BasketballGameId);
            if (timerExists)
            {
                return BadRequest(new { message = "Timer already exists for this game." });
            }

            _context.TimerStates.Add(timerState);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Timer successfully initialized.", timerId = timerState.Id });
        }

        // Mettre à jour le timer depuis le frontend
        [HttpPost("update")]
        public async Task<IActionResult> UpdateTimer([FromBody] TimerState updatedTimer)
        {
            var timer = await _context.TimerStates.FirstOrDefaultAsync(t => t.BasketballGameId == updatedTimer.BasketballGameId);
            if (timer == null)
            {
                return NotFound(new { message = "Timer not found for this game." });
            }

            timer.Minutes = updatedTimer.Minutes;
            timer.Seconds = updatedTimer.Seconds;
            timer.CurrentPeriod = updatedTimer.CurrentPeriod;
            timer.IsRunning = updatedTimer.IsRunning;


            _context.TimerStates.Update(timer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Timer updated successfully." });
        }

        // Récupérer les informations d'un timer
        [HttpGet("{basketballGameId}")]
        public async Task<IActionResult> GetTimer(int basketballGameId)
        {
            var timer = await _context.TimerStates.FirstOrDefaultAsync(t => t.BasketballGameId == basketballGameId);
            if (timer == null)
            {
                return NotFound(new { message = "Timer not found for this game." });
            }

            return Ok(new
            {
                timer.Minutes,
                timer.Seconds,
                timer.CurrentPeriod,
                timer.IsRunning,
            });
        }

        // Supprimer un timer
        [HttpDelete("{basketballGameId}")]
        public async Task<IActionResult> DeleteTimer(int basketballGameId)
        {
            var timer = await _context.TimerStates.FirstOrDefaultAsync(t => t.BasketballGameId == basketballGameId);
            if (timer == null)
            {
                return NotFound(new { message = "Timer not found for this game." });
            }

            _context.TimerStates.Remove(timer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Timer deleted successfully." });
        }

        [HttpPut("isRunning")]
        public async Task<IActionResult> UpdateIsRunning([FromBody] int basketballGameId, [FromQuery] bool isRunning)
        {
            var timer = await _context.TimerStates.FirstOrDefaultAsync(t => t.BasketballGameId == basketballGameId);
            if (timer == null)
            {
                return NotFound(new { message = "Timer not found for this game." });
            }

            // Met à jour uniquement l'état "isRunning"
            timer.IsRunning = isRunning;
            _context.TimerStates.Update(timer);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Timer running state updated to {isRunning} for game {basketballGameId}." });
        }



    }
}
