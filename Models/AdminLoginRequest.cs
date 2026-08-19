
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace ReadoraProject.Models
{
    public class AdminLoginRequest
    {
        public string AdminName { get; set; } = string.Empty;
       public string AdminPassword { get; set; } = string.Empty;
        
    }
}
