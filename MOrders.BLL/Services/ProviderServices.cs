using MOrders.BLL.Interfaces;
using MOrders.DAL.Interfaces;
using MOrders.Domain.DataTransferObject.Provider;
using MOrders.Domain.Entities;

namespace MOrders.BLL.Services
{
    public class ProviderServices : IProviderServices
    {
        private readonly IRepository<Provider> _providerRepository;

        public ProviderServices(IRepository<Provider> providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<IEnumerable<ProviderDTO>> GetProviders()
        {
            var result = (await _providerRepository.GetAll())
                .Select(p => new ProviderDTO { Id = p.Id, Name = p.Name });
            return result;
        }

    }
}
