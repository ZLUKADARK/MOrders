using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Data;
using MOrders.DAL.Entities;
using MOrders.DAL.Interfaces;

namespace MOrders.DAL.Repository
{
    public class ProviderRepository : IRepository<Provider>
    {
        private readonly MOrdersContext _context;
        public ProviderRepository(MOrdersContext context)
        {
            _context = context;
        }
        public async Task<Provider> Create(Provider item)
        {
            _context.Provider.Add(item);
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
            var item = await _context.Provider.FindAsync(id);
            if (item == null)
                return false;
            _context.Provider.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Provider> Get(int id)
        {
            return await _context.Provider.Include(o => o.Orders).Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Provider>> GetAll()
        {
            return await _context.Provider.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Update(int id, Provider item)
        {
            if (item.Id != id)
                return false;

            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                if (!ProviderExists(id))
                    return false;
                else
                    return false;
            }
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.Id == id);
        }
    }
}
