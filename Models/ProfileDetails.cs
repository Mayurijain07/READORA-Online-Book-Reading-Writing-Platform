using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    public class ProfileDetails
    {
        [Key]
        public int ProfileId { get; set; }     
        public int? UserId { get; set; }         
        public string? FullName { get; set; }    
        public string? Bio { get; set; }         
        public string? ProfilePhoto { get; set; } 
        public DateTime LastUpdated { get; set; } 
    }
}
