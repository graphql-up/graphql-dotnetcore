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
    public class ProductRepository : IProduct
    {
        private readonly MarketContext _context;

        public ProductRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            EntityEntry<Product> item = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Product> DeleteAsync(int ProductId)
        {
            Product product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                return product;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Dictionary<int, Product>> GetProductsByCategoryIdAsync(IEnumerable<int> CategoryIds/*, CancellationToken token*/)
        {
            return await _context.Products.Where(x => CategoryIds.Contains(x.CategoryId)).ToDictionaryAsync(x => x.ProductId);
        }

        public async Task<Product> GetByIdAsync(int ProductId)
        {
            return await _context.Products.FindAsync(ProductId);
        }

        public async Task<Product> UpdateAsync(int ProductId, Product product)
        {
            Product oldproduct = await GetByIdAsync(ProductId);
            if (oldproduct == null)
            {
                return oldproduct;
            }

            oldproduct.Name = product.Name;
            oldproduct.Description = product.Description;
            oldproduct.Price = product.Price;
            oldproduct.ImagePath = product.ImagePath;
            oldproduct.CategoryId = product.CategoryId;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
