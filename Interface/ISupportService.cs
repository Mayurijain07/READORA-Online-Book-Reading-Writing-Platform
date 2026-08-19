using Microsoft.EntityFrameworkCore;
using ReadoraProject.Models;

namespace ReadoraProject.Interface
{
    public interface ISupportService
    {
        Task AddFeedbackAsync(FeedbackDetails feedback);
        Task AddQueryAsync(QueryDetails query);
        Task<IEnumerable<QueryDetails>> GetUserQueriesAsync(int userId);
    }
}
