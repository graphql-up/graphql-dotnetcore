using Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int CategoryId);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(int CategoryId, Category category);
        Task<Category> DeleteAsync(int CategoryId);
    }
}
