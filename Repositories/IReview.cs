using Market.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public interface IReview
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int ReviewId);
        Task<Dictionary<int, Review>> GetReviewsByProductIdAsync(IEnumerable<int> ProductIds/*, CancellationToken token*/);
        Task<ILookup<int, Review>> GetReviewsByUserIdAsync(IEnumerable<int> UserIds/*, CancellationToken token*/);
        Task<IEnumerable<Review>> GetReviewsAsync(int UserId);
        Task<Review> CreateAsync(Review review);
        Task<Review> UpdateAsync(int ReviewId, Review review);
        Task<Review> DeleteAsync(int ReviewId);
    }
}
