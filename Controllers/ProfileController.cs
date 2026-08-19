using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadoraProject.Models;
using ReadoraProject.Interface;
using ReadoraProject.Repository;
using ReadoraProject.Services;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileApiService _profileService;

        public ProfileController(ProfileApiService profileService)
        {
            _profileService = profileService;
        }

        // 1. GET: api/Profile/5 (Read)
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var profile = await _profileService.GetProfile(userId);
            if (profile == null) return NotFound("Profile not found");
            return Ok(profile);
        }

        // 2. POST: api/Profile/Update (Create or Update)
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileDetails profile, IFormFile? photo)
        {
            // Session se UserId lena better hai security ke liye
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            try
            {
                await _profileService.ProcessProfileUpdate(userId.Value, profile, photo);
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 3. DELETE: api/Profile/5 (Delete)
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            try
            {
                await _profileService.DeleteUserProfile(userId);
                return Ok(new { message = "Profile deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

