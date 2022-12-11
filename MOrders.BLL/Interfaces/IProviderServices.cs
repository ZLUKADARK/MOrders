using MOrders.Domain.DataTransferObject.Provider;

namespace MOrders.BLL.Interfaces
{
    public interface IProviderServices
    {
        public Task<IEnumerable<ProviderDTO>> GetProviders();
    }
}
