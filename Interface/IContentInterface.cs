using ReadoraProject.Models;

namespace ReadoraProject.Interface
{
    public interface IContentInterface
    {
       Task AddContentAsync(ContentDetails content);
      IEnumerable<ContentDetails> GetPublishedContentByUserId(int userId);
       
        IEnumerable<ContentDetails> GetDraftsByUserId(int userId);
        Task<ContentDetails?> GetContentByIdAsync(int id);
        Task UpdateContentAsync(ContentDetails content);
        Task DeleteContentAsync(int id);
    }
}
