using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    public class RoleDetailstbl
    {
        [Key]
        public int RoleId { get; set; }     
        public int? UserId { get; set; }     
        public string? RoleType { get; set; } 
        public DateTime? Date { get; set; }   
        public int ContentId { get; set; }  
    }
}
