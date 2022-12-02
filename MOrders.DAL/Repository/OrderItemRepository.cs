using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Data;
using MOrders.DAL.Interfaces;
using MOrders.Domain.Entities;
using MOrders.Domain.Models;

namespace MOrders.DAL.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly MOrdersContext _context;
        public OrderItemRepository(MOrdersContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> Create(OrderItem item)
        {
            if (OrdernNameExists(item.Name))
                return null;

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

        public async Task<IEnumerable<OrderItem>> GetByFilter(OrderFilter filter)
        {
            var resuslt = _context.OrderItem.Include(o => o.Order).ThenInclude(p => p.Provider).AsNoTracking()
                         .Where(f =>
                         (filter.Name == null ? true : filter.Name.Length == 0 ? true : filter.Name[0] == null ? true : filter.Name.Contains(f.Name))
                         & (filter.Number == null ? true : filter.Number.Length == 0 ? true : filter.Number[0] == null ? true : filter.Number.Contains(f.Order.Number))
                         & (filter.ProviderName == null ? true : filter.ProviderName.Length == 0 ? true : filter.ProviderName[0] == null ? true : filter.ProviderName.Contains(f.Order.Provider.Name))
                         & (filter.Unit == null ? true : filter.Unit.Length == 0 ? true : filter.Unit[0] == null ? true : filter.Unit.Contains(f.Unit))
                         & (filter.DateNow == DateTime.MinValue & filter.DatePast == DateTime.MinValue ? true : f.Order.Date <= filter.DateNow & f.Order.Date >= filter.DatePast));
            
            return await resuslt.Where(
                x => filter.Search == null ? true :
                x.Order.Number.Contains(filter.Search)
                | x.Order.Provider.Name.Contains(filter.Search)
                | x.Unit.Contains(filter.Search)
                | x.Name.Contains(filter.Search)).ToListAsync();
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

            var newOrderItem = await _context.OrderItem.FindAsync(item.Id);
            newOrderItem.Name = item.Name;
            newOrderItem.Quantity = item.Quantity;
            newOrderItem.Unit = item.Unit;
            _context.Entry(newOrderItem).State = EntityState.Modified;
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
