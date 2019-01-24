using Market.Data;
using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly MarketContext _context;

        public CategoryRepository(MarketContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            EntityEntry<Category> item = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Category> DeleteAsync(int CategoryId)
        {
            Category category = await _context.Categories.FindAsync(CategoryId);
            if (category == null)
            {
                return category;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        //public Page<Category> GetFilteredEmployees(int pageSize, int currentPage, string searchText)
        //{
        //    Page<Category> employees;
        //    var filters = new Filters<Category>();
        //    var sorts = new Sorts<Category>();

        //    filters.Add(!string.IsNullOrEmpty(searchText), x => x.Name.StartsWith(searchText));

        //    employees = _context.Categories.AsNoTracking().Paginate(currentPage, pageSize, null, filters);

        //    return employees;
        //}

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int CategoryId)
        {
            return await _context.Categories.FindAsync(CategoryId);
        }

        public async Task<Category> UpdateAsync(int CategoryId, Category category)
        {
            Category oldcategory = await GetByIdAsync(CategoryId);
            if (oldcategory == null)
            {
                return oldcategory;
            }
            oldcategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
