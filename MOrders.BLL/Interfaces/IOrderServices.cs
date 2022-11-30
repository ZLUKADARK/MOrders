using MOrders.BLL.DataTransferObject;

namespace MOrders.BLL.Interfaces
{
    public interface IOrderServices
    {
        public Task<IEnumerable<ProvidersDTO>> GetProviders();
    }
}
