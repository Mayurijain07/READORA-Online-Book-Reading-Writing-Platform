using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    [Table("FeedbackDetails")]
    public class FeedbackDetails
    {
        [Key]
        public int FeedbackId { get; set; } 

        public int? UserId { get; set; } 

        public int? AdminId { get; set; } 

        public string? Message { get; set; } 

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; } 

        public string? Rating { get; set; } 
    }
}
