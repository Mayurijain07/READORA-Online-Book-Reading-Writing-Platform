using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ReadoraProject.Repository
{
    public class InteractionRepository:IInteractionInterface
    {
       
        
            private readonly ReadoraDbContext _db;
            public InteractionRepository(ReadoraDbContext db) { _db = db; }

            public async Task<string> ToggleFollowAsync(int followerId, int followingId)
            {
                var follow = await _db.FollowerDetails.FirstOrDefaultAsync(f => f.FollowerUserId == followerId && f.FollowingUserId == followingId);
                if (follow != null)
                {
                    _db.FollowerDetails.Remove(follow);
                    await _db.SaveChangesAsync();
                    return "Unfollowed";
                }
                _db.FollowerDetails.Add(new FollowerDetails { FollowerUserId = followerId, FollowingUserId = followingId });
                await _db.SaveChangesAsync();
                return "Followed";
            }

            public async Task<string> ToggleLikeAsync(int userId, int contentId)
            {
                var like = await _db.LikeDetails.FirstOrDefaultAsync(l => l.UserId == userId && l.ContentId == contentId);
                if (like != null)
                {
                    _db.LikeDetails.Remove(like);
                    await _db.SaveChangesAsync();
                    return "Unliked";
                }
                _db.LikeDetails.Add(new LikeDetails { UserId = userId, ContentId = contentId });
                await _db.SaveChangesAsync();
                return "Liked";
            }

            public async Task<string> ToggleFavouriteAsync(int userId, int contentId)
            {
                var fav = await _db.FavouriteDetails.FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId);
                if (fav != null)
                {
                    _db.FavouriteDetails.Remove(fav);
                    await _db.SaveChangesAsync();
                    return "Removed from Favourites";
                }
                _db.FavouriteDetails.Add(new FavouriteDetails { UserId = userId, ContentId = contentId });
                await _db.SaveChangesAsync();
                return "Added to Favourites";
            }
        public async Task<bool> DeleteCommentAsync(int commentId, int userId)
        {
            var comment = await _db.CommentDetails
                .FirstOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId);

            if (comment != null)
            {
                _db.CommentDetails.Remove(comment);
                return await _db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> AddCommentAsync(CommentDetails comment)
            {
                _db.CommentDetails.Add(comment);
                return await _db.SaveChangesAsync() > 0;
            }
        public async Task<int> GetFollowerCountAsync(int userId)
    => await _db.FollowerDetails.CountAsync(f => f.FollowingUserId == userId);

        public async Task<int> GetFollowingCountAsync(int userId)
            => await _db.FollowerDetails.CountAsync(f => f.FollowerUserId == userId);

        public async Task<bool> IsFollowingAsync(int followerId, int followingId)
            => await _db.FollowerDetails.AnyAsync(f => f.FollowerUserId == followerId && f.FollowingUserId == followingId);
        
        //ReadingHistory:

        public async Task<bool> AddToHistoryAsync(int userId, int contentId)
        {
            var existing = await _db.ReadingHistoryDetails
                .FirstOrDefaultAsync(rh => rh.UserId == userId && rh.ContentId == contentId);

            if (existing != null)
            {
                existing.ReadDate = DateTime.Now;
                existing.ReadDuration += TimeSpan.FromMinutes(1);
            }
            else
            {
                await _db.ReadingHistoryDetails.AddAsync(new ReadingHistoryDetails
                {
                    UserId = userId,
                    ContentId = contentId,
                    ReadDate = DateTime.Now,
                    ReadDuration = TimeSpan.FromMinutes(1)
                });
            }

            return await _db.SaveChangesAsync() > 0;
        }
    }

    }

