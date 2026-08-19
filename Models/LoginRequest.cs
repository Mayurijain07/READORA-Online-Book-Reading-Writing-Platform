using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ReadoraProject.Models
{
    public class LoginRequest

    {
        public int LoginId { get; set; }
        [Required] public string Username { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        
        public string RoleType { get; set; } = string.Empty;
    }
}

