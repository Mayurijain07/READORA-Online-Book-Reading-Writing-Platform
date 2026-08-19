using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;
namespace ReadoraProject.Repository
{
    public class SupportRepository:ISupportService
    {
        private readonly ReadoraDbContext _context; // Apne DbContext ka sahi naam likhein

        public SupportRepository(ReadoraDbContext context)
        {
            _context = context;
        }

        public async Task AddFeedbackAsync(FeedbackDetails feedback)
        {
            _context.FeedbackDetails.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task AddQueryAsync(QueryDetails query)
        {
            // 1. Reset QueryId to 0 so DB generates it
            query.QueryId = 0;

            // 2. Ensure non-null values for DB constraints
            query.QueryDate = DateTime.Now;
            query.QueryStatus = false; // 1 for Active/Pending

            _context.QueryDetails.Add(query);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<QueryDetails>> GetUserQueriesAsync(int userId)
        {
            return await _context.QueryDetails
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.QueryDate)
                .ToListAsync();
        }
    }
}

  
