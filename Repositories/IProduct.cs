using Market.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int ProductId);
        Task<Dictionary<int,Product>> GetProductsByCategoryIdAsync(IEnumerable<int> CategoryIds);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(int ProductId, Product product);
        Task<Product> DeleteAsync(int ProductId);
    }
}
