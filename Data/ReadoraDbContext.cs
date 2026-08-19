using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadoraProject.Models;
using System.Collections.Generic;

namespace ReadoraProject.Data
{
    public class ReadoraDbContext : DbContext
    {
        public ReadoraDbContext(DbContextOptions<ReadoraDbContext> options) : base(options)
        { }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<FeedbackDetails> FeedbackDetails { get; set; }
        public DbSet<QueryDetails> QueryDetails { get; set; }
        public DbSet<AdminDetails> AdminDetails { get; set; }
        public DbSet<CategoryDetails> CategoryDetails { get; set; }
        public DbSet<ProfileDetails> ProfileDetails { get; set; }
        public DbSet<ContentDetails> ContentDetails { get; set; }
        public DbSet<FollowerDetails> FollowerDetails { get; set; }
        public DbSet<LikeDetails> LikeDetails { get; set; }
        public DbSet<FavouriteDetails> FavouriteDetails { get; set; }
        public DbSet<CommentDetails> CommentDetails { get; set; }
        public DbSet<ReadingHistoryDetails> ReadingHistoryDetails { get; set; }
       
        public class ApiSettings
        {
            public string BaseUrl { get; set; } = string.Empty;
        }
    }
   
}
