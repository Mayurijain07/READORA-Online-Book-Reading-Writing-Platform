using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using ReadoraProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ReadoraDbContext _context;
        private readonly IUserInterface _userInterface;
        public AuthController(IConfiguration configuration, ReadoraDbContext context, IUserInterface userInterface)
        {
            _configuration = configuration;
            _context = context;
            _userInterface = userInterface;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest login)  
        {
            var user = _userInterface.GetLoginDetails(login.Username, login.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid Credentials" });  

            var token = GenerateToken(user.Username, user.RoleType);  


            return Ok(new LoginResponse
            {
                UserId = user.UserId,
                Token = token,
                Username = user.Username,
                Role = user.RoleType
            });

        }
        [NonAction]
        public string GenerateToken(string username, string role)   
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)  
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin([FromBody] AdminLoginRequest login) 
        {
           
            var admin = _userInterface.GetAdminDetails(login.AdminName, login.AdminPassword);

            if (admin == null)
                return Unauthorized(new { message = "Invalid Admin Credentials" });

           
            var token = GenerateToken(admin.AdminName ?? "Admin", "Admin");

          
            HttpContext.Session.SetInt32("AdminId", admin.AdminId);
            HttpContext.Session.SetString("AdminName", admin.AdminName ?? "Admin");

            
            return Ok(new AdminLoginResponse
            {
                AdminId = admin.AdminId,
                AdminName = admin.AdminName,
                Token = token,
                Message = "Admin Login Successful"
            });
        }
    }
}






    