using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ReadoraProject.Models
{
    public class AdminLoginResponse
    {
        public int AdminId { get; set; }
        public string? AdminName { get; set; }
        public string ? Token { get; set; } // JWT Token
        public string? Message { get; set; }
    }
}
