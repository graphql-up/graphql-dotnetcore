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
    public class UserRepository : IUser
    {
        private readonly MarketContext _context;

        public UserRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            EntityEntry<User> item = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<User> DeleteAsync(int UserId)
        {

            User user = await _context.Users.FindAsync(UserId);
            if (user == null)
            {
                return user;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int UserId)
        {
            return await _context.Users.FindAsync(UserId);
        }

        public async Task<ILookup<int, User>> GetByIdAsync(IEnumerable<int> UserIds, CancellationToken token)
        {
            IEnumerable<User> users = await _context.Users.Where(x => UserIds.Contains(x.UserId)).ToListAsync();
            return users.ToLookup(i => i.UserId);
        }

        public async Task<User> UpdateAsync(int UserId, User user)
        {
            User olduser = await GetByIdAsync(UserId);
            if (olduser == null)
            {
                return olduser;
            }

            olduser.FullName = user.FullName;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
