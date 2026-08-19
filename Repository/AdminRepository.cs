using Microsoft.EntityFrameworkCore;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using System;

namespace ReadoraProject.Data
{
    public class AdminRepository : IAdminService
    {
        private readonly ReadoraDbContext _context;
        public AdminRepository(ReadoraDbContext context) { _context = context; }

        // ==========================================
        // FEEDBACK METHODS
       
        public async Task<List<FeedbackViewModel>> GetFeedbackListAsync()
        {
            return await _context.FeedbackDetails
                .Join(_context.UserDetails, f => f.UserId, u => u.UserId, (f, u) => new FeedbackViewModel
                {
                    FeedbackId = f.FeedbackId,
                    RegNo = u.RegistrationNumber,
                    Message = f.Message,
                    Date = f.Date,
                    Rating = f.Rating,
                    AdminId = f.AdminId
                }).ToListAsync();
        }

        public async Task<bool> UpdateFeedbackStatusAsync(int id, int adminId)
        {
            var f = await _context.FeedbackDetails.FindAsync(id);
            if (f == null) return false;
            f.AdminId = adminId;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var f = await _context.FeedbackDetails.FindAsync(id);
            if (f == null) return false;
            _context.FeedbackDetails.Remove(f);
            return await _context.SaveChangesAsync() > 0;
        }
        // QUERY METHODS
        // ==========================================
        public async Task<List<QueryViewModel>> GetQueryListAsync()
        {
            
            return await _context.QueryDetails
                .Join(_context.UserDetails, q => q.UserId, u => u.UserId, (q, u) => new QueryViewModel
                {
                    QueryId = q.QueryId,
                    RegNo = u.RegistrationNumber,
                    QueryTitle = q.QueryTitle,
                    QueryDescription = q.QueryDescription,
                    QueryStatus = q.QueryStatus,
                    ResponseMesssage = q.ResponseMesssage,
                    ResolvedDate = q.ResolvedDate,
                    QueryDate = q.QueryDate,
                    AdminId = q.AdminId
                }).ToListAsync();
        }

        
        public async Task<bool> UpdateQueryStatusAsync(int id, int adminId, string message)
        {
            var q = await _context.QueryDetails.FindAsync(id);
            if (q == null) return false;

            q.AdminId = adminId;
            q.QueryStatus = true; 
            q.ResponseMesssage = message; 
            q.ResolvedDate = DateTime.Now; 

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteQueryAsync(int id)
        {
            var q = await _context.QueryDetails.FindAsync(id);
            if (q == null) return false;
            _context.QueryDetails.Remove(q);
            return await _context.SaveChangesAsync() > 0;
        }
        
        // CATEGORY METHODS
        // ==========================================
        public async Task<List<CategoryDetails>> GetCategoryListAsync()
        {
            return await _context.CategoryDetails.ToListAsync();
        }

        public async Task<bool> AddCategoryAsync(CategoryDetails category)
        {
            _context.CategoryDetails.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDetails category)
        {
            var existing = await _context.CategoryDetails.FindAsync(category.CategoryId);
            if (existing == null) return false;

            existing.CategoryName = category.CategoryName;
           

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var cat = await _context.CategoryDetails.FindAsync(id);
            if (cat == null) return false;

            _context.CategoryDetails.Remove(cat);
            return await _context.SaveChangesAsync() > 0;
        }

        // USER OPERATIONS
        public async Task<List<UserDetails>> GetUserListAsync()
        {
            
            return await _context.UserDetails.ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.UserDetails.FindAsync(id);
            if (user == null) return false;

            _context.UserDetails.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        // --- CONTENT MANAGEMENT METHODS ---

        public async Task<List<ContentViewModel>> GetContentListAsync()
        {
            // Joining 3 tables: ContentDetails + UserDetails + CategoryDetails
            return await _context.ContentDetails
                .Where(c => c.Status == true)
                .Join(_context.UserDetails,
                    c => c.UserId,
                    u => u.UserId,
                    (c, u) => new { c, u })
                .Join(_context.CategoryDetails,
                    cu => cu.c.CategoryId,
                    cat => cat.CategoryId,
                    (cu, cat) => new ContentViewModel
                    {
                        ContentId = cu.c.ContentId,
                        Title = cu.c.Title,
                        AuthorName = cu.u.Username, // Only Username
                        CategoryName = cat.CategoryName,
                        Status = cu.c.Status,
                        UploadDate = cu.c.ContentDate
                    }).ToListAsync();
        }

        
        public async Task<bool> UpdateContentStatusAsync(int id, bool status)
        {
            var content = await _context.ContentDetails.FindAsync(id);
            if (content == null) return false;

           
            content.Status = status;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteContentAsync(int id)
        {
            var content = await _context.ContentDetails.FindAsync(id);
            if (content == null) return false;

            _context.ContentDetails.Remove(content);
            return await _context.SaveChangesAsync() > 0;
        }
        //------Dashboard-Stats----
        public async Task<DashboardViewModel> GetDashboardStatsAsync()
        {
         
            var stats = new DashboardViewModel();

           
            stats.TotalUsers = await _context.UserDetails.CountAsync();
            stats.TotalStories = await _context.ContentDetails.CountAsync(c => c.Status == true);
            stats.TotalCategories = await _context.CategoryDetails.CountAsync();
            stats.TotalFeedback = await _context.FeedbackDetails.CountAsync();
            stats.TotalQueries = await _context.QueryDetails.CountAsync();
            
            stats.UsersReport = await _context.UserDetails.ToListAsync();
            stats.StoriesReport = await _context.ContentDetails.ToListAsync();
            stats.FeedbackReport = await _context.FeedbackDetails.ToListAsync();
            stats.QueriesReport = await _context.QueryDetails.ToListAsync();
           
            var monthlyCounts = new List<int>();
            int currentYear = 2026;

            for (int month = 1; month <= 6; month++)
            {
                var startDate = new DateTime(currentYear, month, 1);
                var endDate = startDate.AddMonths(1);

                var count = await _context.ContentDetails
                    .CountAsync(c => c.Status == true &&
                                     c.ContentDate >= startDate &&
                                     c.ContentDate < endDate);

                monthlyCounts.Add(count);
            }

            stats.MonthlyData = monthlyCounts;

            return stats;
        }
    }
    }
