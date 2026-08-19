using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReadoraProject.Models
{
    public class CategoryDetails
    {
       
            [Key]
            public int CategoryId { get; set; }
            public string? CategoryName { get; set; }
        }
    }

