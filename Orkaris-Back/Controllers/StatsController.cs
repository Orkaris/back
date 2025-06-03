using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orkaris_Back.Models.EntityFramework;
using System.Security.Claims;

namespace Orkaris_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly WorkoutDBContext _context;

        public StatsController(WorkoutDBContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("weekly-volume/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetWeeklyVolume(Guid userId)
        {
            var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1); // Lundi
            var endOfWeek = startOfWeek.AddDays(7);

            // Récupère les sessions de l'utilisateur pour la semaine
            var sessionIds = await _context.Sessions
                .Where(s => s.UserId == userId && s.CreatedAt >= startOfWeek && s.CreatedAt < endOfWeek)
                .Select(s => s.Id)
                .ToListAsync();

            // Calcule le volume total (sets * reps * weight) pour ces sessions
            var totalVolume = await _context.ExerciseGoalPerformances
                .Where(egp => sessionIds.Contains(
                    _context.SessionExercises
                        .Where(se => se.ExerciseId == egp.ExerciseGoalId)
                        .Select(se => se.SessionId)
                        .FirstOrDefault()))
                .SumAsync(egp => egp.Sets * egp.Reps * egp.Weight);

            return Ok(totalVolume);
        }

        [Authorize]
        [HttpGet("monthly-sessions/{userId}")]
        public async Task<IActionResult> GetSessionsCountThisMonth(Guid userId)
        {
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endOfMonth = startOfMonth.AddMonths(1);

            var sessionsCount = await _context.Sessions
                .Where(s => s.UserId == userId && s.CreatedAt >= startOfMonth && s.CreatedAt < endOfMonth)
                .CountAsync();

            return Ok(sessionsCount);
        }


        [Authorize]
        [HttpGet("weekly-sets/{userId}")]
        public async Task<IActionResult> GetSetsCountThisWeek(Guid userId)
        {
            var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            var setsCount = await _context.ExerciseGoalPerformances
                .Where(egp =>
                    egp.CreatedAt >= startOfWeek &&
                    egp.CreatedAt < endOfWeek &&
                    egp.ExerciseGoalExerciseGoalPerformance != null &&
                    egp.ExerciseGoalExerciseGoalPerformance.SessionExerciseExerciseGoal != null
                )
                .SumAsync(egp => egp.Sets);
            if (setsCount < 0)
            {
                return BadRequest("0 sets found for this week.");
            }

            return Ok(setsCount);
        }

        [Authorize]
        [HttpGet("last-8-weeks-sessions/{userId}")]
        public async Task<IActionResult> GetSessionsLast8Weeks(Guid userId)
        {
            var now = DateTime.UtcNow;
            // On prend le lundi il y a 7 semaines (début de la première semaine affichée)
            var startOfFirstWeek = now.Date.AddDays(-(int)now.DayOfWeek + 1).AddDays(-7 * 7);

            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId && s.CreatedAt >= startOfFirstWeek)
                .Select(s => new
                {
                    date = s.CreatedAt,
                    duration = s.Duration
                })
                .ToListAsync();
            var testSessions = new[]
            {
                new { date = DateTime.UtcNow.AddDays(-1), duration = 60 },
                new { date = DateTime.UtcNow.AddDays(-2), duration = 45 },
                new { date = DateTime.UtcNow.AddDays(-4), duration = 90 },
                new { date = DateTime.UtcNow.AddDays(-8), duration = 60 },
                new { date = DateTime.UtcNow.AddDays(-10), duration = 30 },
                new { date = DateTime.UtcNow.AddDays(-15), duration = 50 },
                new { date = DateTime.UtcNow.AddDays(-17), duration = 40 },
                new { date = DateTime.UtcNow.AddDays(-21), duration = 30 },
                new { date = DateTime.UtcNow.AddDays(-28), duration = 75 },
                new { date = DateTime.UtcNow.AddDays(-32), duration = 60 },
                new { date = DateTime.UtcNow.AddDays(-37), duration = 90 },
                new { date = DateTime.UtcNow.AddDays(-41), duration = 45 },
                new { date = DateTime.UtcNow.AddDays(-48), duration = 30 },
            };
            return Ok(testSessions);
            //return Ok(sessions);
        }







    }
}
