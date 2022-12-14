using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Data;
using MOrders.DAL.Interfaces;
using MOrders.Domain.Entities;

namespace MOrders.DAL.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly MOrdersContext _context;
        public OrderRepository(MOrdersContext context)
        {
            _context = context; 
        }  
        public async Task<Order> Create(Order item)
        {
            if (OrderItemsNumberExists(item.Number))
                return null;
            
            _context.Order.Add(item);
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
            var item = await _context.Order.FindAsync(id);
            if (item == null)
                return false;
            _context.Order.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Order> Get(int id)
        {
            return await _context.Order.Include(i => i.OrderItem).Include(p => p.Provider).Where(o => o.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Order.Include(p => p.Provider).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Update(int id, Order item)
        {
            if (item.Id != id)
                return false;

            if (OrderItemsNumberExists(item.Number))
                return false;
            var newOrder = await _context.Order.FindAsync(item.Id);
            newOrder.Number = item.Number;
            newOrder.Date = item.Date;
            newOrder.ProviderId = item.ProviderId;
            _context.Entry(newOrder).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                if (!OrderExists(id))
                    return false;
                else
                    return false;
            }
        }

        private bool OrderItemsNumberExists(string number)
        {
            return _context.OrderItem.Any(e => e.Name == number);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
