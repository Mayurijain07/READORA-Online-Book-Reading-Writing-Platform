using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadoraProject.Models
{
    public class FollowerDetails
    {
        [Key]
        public int FollowerId { get; set; }

        [Required]
        public int FollowerUserId { get; set; } 

        [Required]
        public int FollowingUserId { get; set; } 

        public DateTime FollowDate { get; set; } = DateTime.Now;

        // Navigation Properties ( for Easy Joins)
        [ForeignKey("FollowerUserId")]
        public virtual UserDetails ?Follower { get; set; }

        [ForeignKey("FollowingUserId")]
        public virtual UserDetails ?Following { get; set; }
    }

}

