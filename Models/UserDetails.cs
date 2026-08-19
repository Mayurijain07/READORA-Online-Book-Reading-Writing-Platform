using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{

    [Table("UserDetails")]
    public class UserDetails
    {
           [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("UserId")]
            public int UserId { get; set; }

            [MaxLength(50)]
        [Column("Username")]
            public string? Username { get; set; }

            [MaxLength(300)]
        [Column("Address")]
        public string? Address { get; set; }

            [MaxLength(100)]
        [Column("Password")]
            public string? Password { get; set; }
        [NotMapped] // DB me store nahi hoga
        [Compare("Password")]
        public string ?ConfirmPassword { get; set; }

        [MaxLength(100)]
        [Column("EmailId")]
            public string? EmailId { get; set; }

            [MaxLength(20)]
        [Column("Gender")]
            public string? Gender { get; set; }

            [MaxLength(15)]
        [Column("Contact")]
            public string? Contact { get; set; }

            [MaxLength(20)]
        [Column("RoleType")]
            public string? RoleType { get; set; }
        [Column("CreateDate")]
            public DateTime? CreateDate { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        [MaxLength(50)]
        [Column("RegistrationNumber")]
        public string? RegistrationNumber { get; set; }

        [NotMapped] //  prevents EF from mapping to DB
        public string? Token { get; set; }
        }
    }


