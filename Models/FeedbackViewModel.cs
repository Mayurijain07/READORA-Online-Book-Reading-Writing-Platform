using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadoraProject.Models
{
    public class FeedbackViewModel
    {
        // FeedbackDetails ke main fields
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public int? AdminId { get; set; }
        public string? Message { get; set; }
        public DateTime? Date { get; set; }
        public string? Rating { get; set; }

        public string RegNo { get; set; } = string.Empty;
    }
}
