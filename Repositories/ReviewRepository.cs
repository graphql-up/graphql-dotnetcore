using Market.Data;
using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public class ReviewRepository : IReview
    {
        private readonly MarketContext _context;

        public ReviewRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateAsync(Review review)
        {
            EntityEntry<Review> item = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Review> DeleteAsync(int ReviewId)
        {
            Review review = await _context.Reviews.FindAsync(ReviewId);
            if (review == null)
            {
                return review;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.AsNoTracking().ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int ReviewId)
        {
            return await _context.Reviews.FindAsync(ReviewId);
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync(int UserId)
        {
            return await _context.Reviews.Include(ss => ss.User).Where(ss => ss.UserId == UserId).ToListAsync();
        }

        public async Task<Dictionary<int, Review>> GetReviewsByProductIdAsync(IEnumerable<int> ProductIds/*, CancellationToken token*/)
        {
            return await _context.Reviews.Where(x => ProductIds.Contains(x.ProductId)).ToDictionaryAsync(x => x.ReviewId);
        }

        public async Task<ILookup<int, Review>> GetReviewsByUserIdAsync(IEnumerable<int> UserIds/*, CancellationToken token*/)
        {
            IEnumerable<Review> reviews = await _context.Reviews.Where(x => UserIds.Contains(x.UserId)).ToListAsync();
            return reviews.ToLookup(i => i.UserId);
        }

        public async Task<Review> UpdateAsync(int ReviewId, Review review)
        {
            Review oldreview = await GetByIdAsync(ReviewId);
            if (oldreview == null)
            {
                return oldreview;
            }

            oldreview.Text = review.Text;
            oldreview.Star = review.Star;
            await _context.SaveChangesAsync();
            return review;
        }
    }
}
