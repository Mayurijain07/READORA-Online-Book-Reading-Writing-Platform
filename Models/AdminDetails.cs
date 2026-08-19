using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    public class AdminDetails
    {
        [Key] 
        public int AdminId { get; set; }

        public int UserId { get; set; }
        public string? AccessLevel { get; set; }
        public DateTime AssignedDate { get; set; }

        public string ?AdminName { get; set; }
        public string ?AdminPassword { get; set; }
    }
}
