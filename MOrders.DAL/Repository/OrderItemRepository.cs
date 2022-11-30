using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Data;
using MOrders.DAL.Entities;
using MOrders.DAL.Interfaces;

namespace MOrders.DAL.Repository
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly MOrdersContext _context;
        public OrderItemRepository(MOrdersContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> Create(OrderItem item)
        {
            _context.OrderItem.Add(item);
            try
            {
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.OrderItem.FindAsync(id);
            if (item == null)
                return false;
            _context.OrderItem.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderItem> Get(int id)
        {
            return await _context.OrderItem.Include(o => o.Order).ThenInclude(p => p.Provider).Where(o => o.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await _context.OrderItem.Include(o => o.Order).ThenInclude(p => p.Provider).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Update(int id, OrderItem item)
        {
            if (item.Id != id)
                return false;

            if (OrdernNameExists(item.Name))
                return false;

            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                if (!OrderItemsExists(id))
                    return false;
                else
                    return false;
            }
        }
        private bool OrdernNameExists(string name)
        {
            return _context.Order.Any(e => e.Number == name);
        }

        private bool OrderItemsExists(int id)
        {
            return _context.OrderItem.Any(e => e.Id == id);
        }
    }
}
