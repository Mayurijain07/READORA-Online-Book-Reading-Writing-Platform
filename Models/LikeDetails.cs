using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadoraProject.Models
{
    public class LikeDetails
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ContentId { get; set; }

        public DateTime LikeDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual UserDetails ?User { get; set; }

        [ForeignKey("ContentId")]
        public virtual ContentDetails ?Content { get; set; }
    }
}
