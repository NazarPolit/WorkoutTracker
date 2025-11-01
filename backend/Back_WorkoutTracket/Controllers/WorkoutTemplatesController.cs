using AutoMapper;
using BusinessLogic.Helper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Back_WorkoutTracket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WorkoutTemplatesController: Controller
	{
		private readonly IWorkoutTemplateService _workoutTemplateService;

		public WorkoutTemplatesController(IWorkoutTemplateService workoutTemplateService)
        {
			_workoutTemplateService = workoutTemplateService;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<WorkoutTemplate>))]
		public async Task<IActionResult> GetAllTemplates(
			[FromQuery] PaginationParams paginationParams,
			[FromQuery] string? nameSearch)
		{
			var exerciseTypes = await _workoutTemplateService.GetAllTemplatesAsync(paginationParams, nameSearch);

            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(
                new { exerciseTypes.TotalCout, exerciseTypes.PageSize, exerciseTypes.CurrentPage, exerciseTypes.TotalPages });

            return Ok(exerciseTypes);
		}

		[HttpGet("{weId}")]
		[ProducesResponseType(200, Type = typeof(ExerciseType))]
		public async Task<IActionResult> GetAllTemplateById(int weId)
		{
			var templeDto = await _workoutTemplateService.GetTemplateByIdAsync(weId);

			if (templeDto == null)
			{
				return NotFound(new { message = "Not Found" });
			}

			return Ok(templeDto);
		}
	}
}
