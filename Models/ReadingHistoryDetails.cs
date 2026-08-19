using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    public class ReadingHistoryDetails
    {
        [Key]
        public int HistoryId { get; set; }        
        public int? UserId { get; set; }         
        public int? ContentId { get; set; }      
        public DateTime? ReadDate { get; set; }   
        public TimeSpan? ReadDuration { get; set; } // time(7), Nullable
        [ForeignKey("ContentId")]
        public virtual ContentDetails Content { get; set; }
    }
}
