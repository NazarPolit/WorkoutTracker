using BusinessLogic.Dtos;
using BusinessLogic.Helper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;


namespace Back_WorkoutTracket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController : Controller
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("personal-records/{exerciseTypeId}")]
        public async Task<IActionResult> GetPersonalRecord(int exerciseTypeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var record = await _statsService.GetUserPersonalRecordAsync(userId, exerciseTypeId);

            if (record == null)
            {
                return NotFound(new {message = "Exercise type not found."});
            }
            return Ok(record);
        }

        [HttpGet("volume-history/{exerciseTypeId}")]
        public async Task<IActionResult> GetVolumeHistory(int exerciseTypeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var history = await _statsService.GetUserVolumeHistoryAsync(userId, exerciseTypeId);
            if (history == null)
            {
                return NotFound(new { message = "Exercise type not found." });
            }
            return Ok(history);
        }
    }
}
