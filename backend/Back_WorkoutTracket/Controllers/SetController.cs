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
    public class SetController : Controller
	{
		private readonly ISetService _setService;

		public SetController(ISetService setService)
        {
			_setService = setService;
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> AddSet([FromBody] CreateSetDto setDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId == null) 
				return Unauthorized();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var createdSet = await _setService.AddSetAsync(setDto, userId);

			return StatusCode(201, createdSet);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteSet(int id)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var errorMessage = await _setService.DeleteSetAsync(id, userId);

			if (errorMessage != null)
				return BadRequest(new { message = errorMessage });

			return NoContent();

		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> UpdateSet(int id, [FromBody] UpdateSetDto dto)
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var errorMessage = await _setService.UpdateSetAsync(id, dto, userId);

			if (errorMessage != null)
				return BadRequest(new { message = errorMessage });

			return NoContent();
		}
	}
}
