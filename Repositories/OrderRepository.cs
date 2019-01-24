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
    public class OrderRepository : IOrder
    {
        private readonly MarketContext _context;

        public OrderRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            EntityEntry<Order> item = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Order> DeleteAsync(int OrderId)
        {
            Order order = await _context.Orders.FindAsync(OrderId);
            if (order == null)
            {
                return order;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int OrderId)
        {
            return await _context.Orders.FindAsync(OrderId);
        }

        public async Task<Dictionary<int, Order>> GetOrdersByProductIdAsync(IEnumerable<int> ProductIds/*, CancellationToken token*/)
        {
            return await _context.Orders.Where(x => ProductIds.Contains(x.ProductId)).ToDictionaryAsync(x => x.OrderId);
        }

        public async Task<ILookup<int, Order>> GetOrdersByUserIdAsync(IEnumerable<int> UserIds/*, CancellationToken token*/)
        {
            IEnumerable<Order> orders = await _context.Orders.Where(x => UserIds.Contains(x.UserId)).ToListAsync();
            return orders.ToLookup(i => i.UserId);
        }

        public async Task<Order> UpdateAsync(int OrderId, Order order)
        {
            Order oldorder = await GetByIdAsync(OrderId);
            if (oldorder == null)
            {
                return oldorder;
            }
            oldorder.Quantity = order.Quantity;
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
