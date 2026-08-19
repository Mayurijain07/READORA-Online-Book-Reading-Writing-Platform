using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadoraProject.Models
{
    public class CommentDetails
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ContentId { get; set; }

        [Required(ErrorMessage = "Comment cannot be empty")]
        public string ?CommentText { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual UserDetails ?User { get; set; }

        [ForeignKey("ContentId")]
        public virtual ContentDetails ? Content { get; set; }
    }
}
