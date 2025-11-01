using BusinessLogic.Dtos;
using BusinessLogic.Helper;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Back_WorkoutTracket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ExerciseTypesController : Controller
	{
		private readonly IExerciseTypeService _exerciseTypeService;

		public ExerciseTypesController(IExerciseTypeService exerciseTypeService)
		{
			_exerciseTypeService = exerciseTypeService;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ExerciseType>))]
		public async Task<IActionResult> GetAll(
			[FromQuery] PaginationParams paginationParams,
			[FromQuery] string? muscleGroup,
            [FromQuery] string? nameSearch)
		{
			var exerciseTypes = await _exerciseTypeService.GetAllAsync(paginationParams, muscleGroup, nameSearch);

			Response.Headers["X-Pagination"] = JsonSerializer.Serialize(
				new { exerciseTypes.TotalCout, exerciseTypes.PageSize, exerciseTypes.CurrentPage, exerciseTypes.TotalPages });

			return Ok(exerciseTypes);
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Create([FromBody] CreateAndUpdateExerciseTypeDto exerciseTypeDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var createdExercise = await _exerciseTypeService.CreateAsync(exerciseTypeDto);

			return CreatedAtAction(nameof(GetAll), new { id = createdExercise.Id }, createdExercise);
		}

		[HttpGet("{exId}")]
		[ProducesResponseType(200, Type = typeof(ExerciseType))]
		public async Task<IActionResult> GetById(int exId)
		{
			var exerciseTypeDto = await _exerciseTypeService.GetByIdAsync(exId);

			if (exerciseTypeDto == null)
			{
				return NotFound(new {message = "Not Found"});
			}

			return Ok(exerciseTypeDto);
		}

		[HttpPut("{exId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Update(int exId , [FromBody] CreateAndUpdateExerciseTypeDto dto)
		{
			var isUpdated = await _exerciseTypeService.UpdateAsync(exId, dto);

			if(!isUpdated)
				return NotFound(new { message = "Not Found" });

			return NoContent();
		}

		[HttpDelete("{exId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int exId)
		{
			var errorMessage = await _exerciseTypeService.DeleteAsync(exId);

			if (errorMessage != null)
				return BadRequest(new { message = errorMessage });

			return NoContent();

		}

	}
}
