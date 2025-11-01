using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Back_WorkoutTracket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("assign-admin/{userId}")]
        public async Task<IActionResult> AssignAdminRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                NotFound(new {message = "User not found"});
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                return BadRequest(new {message = "User is already admin"});
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new {message = $"Admin role was successfully assigned to user {user.UserName}"});
        }
    }
}
