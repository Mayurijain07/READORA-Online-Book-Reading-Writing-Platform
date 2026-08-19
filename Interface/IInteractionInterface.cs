using ReadoraProject.Models;

namespace ReadoraProject.Interface
{
    public interface IInteractionInterface
    {
        Task<string> ToggleFollowAsync(int followerId, int followingId);
        Task<int> GetFollowerCountAsync(int userId);
        Task<int> GetFollowingCountAsync(int userId);
        Task<bool> IsFollowingAsync(int followerId, int followingId);
        Task<string> ToggleLikeAsync(int userId, int contentId);
        Task<string> ToggleFavouriteAsync(int userId, int contentId);
        Task<bool> AddCommentAsync(CommentDetails comment);
        Task<bool> DeleteCommentAsync(int commentId, int userId);
        Task<bool> AddToHistoryAsync(int userId, int contentId);
    }
}
