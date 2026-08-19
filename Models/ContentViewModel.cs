namespace ReadoraProject.Models
{
    public class ContentViewModel
    {
        public int ?ContentId { get; set; }
        public string? Title { get; set; }
        public string ?AuthorName { get; set; }
        public string ?CategoryName { get; set; }
        public bool Status { get; set; } // Approved, Pending, Rejected
        public DateTime UploadDate { get; set; }
    }
}
