using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Back_WorkoutTracket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class WorkoutExerciseController : Controller
	{
		private readonly IWorkoutExerciseService _workoutExerciseService;

		public WorkoutExerciseController(IWorkoutExerciseService workoutExerciseService)
        {
			_workoutExerciseService = workoutExerciseService;
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> AddExerciseToWorkout([FromBody] CreateWorkoutExerciseDto workoutExerciseDto)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var createdWorkoutExercise = await _workoutExerciseService.AddExerciseToWorkoutAsync(workoutExerciseDto, userId);

			return StatusCode(201, createdWorkoutExercise);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> RemoveExerciseFromWorkout(int id)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var errorMessage = await _workoutExerciseService.RemoveExerciseFromWorkoutAsync(id, userId);

			if (errorMessage != null)
				return BadRequest(new { message = errorMessage });

			return NoContent();

		}

	}
}
