using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    public class ContentDetails
    {
       
         [Key]
       public int ContentId { get; set; } 

          public int UserId { get; set; } 
        [ForeignKey("UserId")]
        public virtual UserDetails? User { get; set; }
        public int RoleId { get; set; } 
            public int CategoryId { get; set; } 
        public virtual CategoryDetails ? Category { get; set; }

        public string ? ContentType { get; set; } 
            public string ?Title { get; set; }
            public string ?Description { get; set; }
            public string ?CoverImage { get; set; }

           public int Views { get; set; } 
           public DateTime ContentDate { get; set; }
           public TimeSpan ContentTime { get; set; }
           public bool Status { get; set; } 
        [NotMapped]
        public bool IsLikedByMe { get; set; }
        [NotMapped]
        public bool IsFavByMe { get; set; }
    }
    }

