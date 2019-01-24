using Market.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Repositories
{
    public interface IOrder
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int OrderId);
        Task<Dictionary<int, Order>> GetOrdersByProductIdAsync(IEnumerable<int> ProductIds /*CancellationToken cancellationToken*/);
        Task<ILookup<int, Order>> GetOrdersByUserIdAsync(IEnumerable<int> UserIds/*, CancellationToken cancellationToken*/);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(int OrderId, Order order);
        Task<Order> DeleteAsync(int OrderId);
    }
}
