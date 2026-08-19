 
namespace ReadoraProject.Models
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalStories { get; set; }
        public int TotalCategories { get; set; }
        public int TotalFeedback { get; set; }
        public List<int> ?MonthlyData { get; set; } // Graph ke liye numbers
        public int TotalQueries { get; set; }
        // --- REPORTS ---------
        public List<UserDetails> UsersReport { get; set; } = new();
        public List<ContentDetails> StoriesReport { get; set; } = new();
        public List<FeedbackDetails> FeedbackReport { get; set; } = new();
        public List<QueryDetails> QueriesReport { get; set; } = new();
    }
}
