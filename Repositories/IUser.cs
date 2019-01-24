using Market.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int UserId);
        Task<ILookup<int, User>> GetByIdAsync(IEnumerable<int> UserIds, CancellationToken token);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(int UserId, User user);
        Task<User> DeleteAsync(int UserId);
    }
}
