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
	public class WorkoutController : Controller
	{
		private readonly IWorkoutService _workoutService;

		public WorkoutController(IWorkoutService workoutService)
		{
			_workoutService = workoutService;
		}

		[HttpGet]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WorkoutDto>))]
		public async Task<IActionResult> GetWorkoutsForUser(
			[FromQuery] PaginationParams paginationParams, 
			[FromQuery] DateTime? startDate,
			[FromQuery] DateTime? endDate)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId == null)
				return Unauthorized();

			var workouts = await _workoutService.GetWorkoutsByUserIdAsync(userId, paginationParams, startDate, endDate);

			Response.Headers["X-Pagination"] = JsonSerializer.Serialize(
				new { workouts.TotalCout, workouts.PageSize, workouts.CurrentPage, workouts.TotalPages });

			return Ok(workouts);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(WorkoutDetailDto))]
		public async Task<IActionResult> GetById(int id)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var workoutDetail = await _workoutService.GetWorkoutByIdAsync(id, userId);

			if (workoutDetail == null)
			{
				return NotFound(new { message = "Not Found" });
			}

			return Ok(workoutDetail);
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Create([FromBody] CreateAndUpdateWorkoutDto workoutDto)
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId == null) 
				return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdWorkout = await _workoutService.CreateWorkoutAsync(workoutDto, userId);

			return CreatedAtAction(nameof(GetById), new { id = createdWorkout.Id }, createdWorkout);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int id)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var errorMessage = await _workoutService.DeleteWorkoutAsync(id, userId);

			if (errorMessage != null)
				return BadRequest(new { message = errorMessage });

			return NoContent();

		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Update(int id, [FromBody] CreateAndUpdateWorkoutDto dto)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) 
				return Unauthorized();

            var errorMessage = await _workoutService.UpdateWorkoutAsync(id, dto, userId);

			if (errorMessage != null)
				return BadRequest(new { message = errorMessage });

			return NoContent();
		}

	}
}
