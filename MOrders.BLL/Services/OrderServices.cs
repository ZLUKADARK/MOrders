using MOrders.BLL.DataTransferObject;
using MOrders.BLL.Interfaces;
using MOrders.DAL.Entities;
using MOrders.DAL.Interfaces;


namespace MOrders.BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Provider> _providerRepository;
        public OrderServices(IRepository<Provider> providerRepository)
        {
            _providerRepository = providerRepository;
        }
        public async Task<IEnumerable<ProvidersDTO>> GetProviders()
        {
            return from p in await _providerRepository.GetAll()
                   select new ProvidersDTO { Id = p.Id, Name = p.Name };
        }
    }
}
