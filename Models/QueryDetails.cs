using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    [Table("QueryDetails")]
    public class QueryDetails
    {
        [Key]
        public int QueryId { get; set; } 

        public int? UserId { get; set; } 

        public int? AdminId { get; set; } 

        public string? QueryTitle { get; set; } 

        public string? QueryDescription { get; set; } 

        public bool? QueryStatus { get; set; } 

        public string? ResponseMesssage { get; set; } 

        public DateTime? ResolvedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? QueryDate { get; set; }
    }
}
