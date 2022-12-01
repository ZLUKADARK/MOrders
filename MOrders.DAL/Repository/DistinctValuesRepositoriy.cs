using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Data;
using MOrders.DAL.Interfaces;
using MOrders.Domain.Models;

namespace MOrders.DAL.Repository
{
    public class DistinctValuesRepositoriy : IDistinctRepository<DistinctValues>
    {
        private readonly MOrdersContext _context;
        public DistinctValuesRepositoriy(MOrdersContext context)
        {
            _context = context;
        }
        public async Task<DistinctValues> GetDistinct()
        {
            var number = await _context.Order.Select(o => o.Number).Distinct().AsNoTracking().ToListAsync();
            var providerName = await _context.Provider.Select(o => o.Name).Distinct().AsNoTracking().ToListAsync();
            var itemName = await _context.OrderItem.Select(o => o.Name).Distinct().AsNoTracking().ToListAsync();
            var Unit = await _context.OrderItem.Select(o => o.Unit).Distinct().AsNoTracking().ToListAsync();
            return new DistinctValues() { Number = number, ItemName = itemName, ProviderName = providerName, Unit = Unit }; 
        }
    }
}
