using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using BCrypt.Net;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _repo;
        private readonly IConfiguration _config;
        public UserController(IUserInterface repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repo.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.GetUserByIdAsync(id, "");
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("profile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _repo.GetProfileAsync(id, "");
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPost("switch-role/{id}")]
        [Authorize]
        public async Task<IActionResult> SwitchRole(int id)
        {
            await _repo.SwitchUserRoleAsync(id);
            return Ok(new { message = "Role switched successfully" });
        }



        [HttpPost("register")]
        [AllowAnonymous]

        public async Task<IActionResult> Register([FromBody] UserDetails user)
        {
            if (await _repo.IsUsernameTaken(user.Username))
                return BadRequest(new { message = "Username already exists" });

            await _repo.RegisterUser(user);
            return Ok(new { message = "Registration Successful" });
        }
       
    }
}
