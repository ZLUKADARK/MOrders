using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.DataTransferObject.OrderItem;
using MOrders.BLL.Interfaces;
using MOrders.DAL.Interfaces;
using MOrders.Domain.Entities;
using MOrders.Domain.Models;

namespace MOrders.BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IDistinctRepository<DistinctValues> _distinctRepository;
        
        public OrderServices(IRepository<Order> orderRepository, IDistinctRepository<DistinctValues> distinctRepository)
        {
            _orderRepository = orderRepository;
            _distinctRepository = distinctRepository;
        }

        public async Task<OrderDTO> Create(OrderDTO orders)
        {
            var result = new Order() { Date = orders.Date, Number = orders.Number, ProviderId = orders.ProviderId };
            var response = await _orderRepository.Create(result);
            if (response == null)
                return null;
            orders.Id = response.Id;
            return orders;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var result = from f in await _orderRepository.GetAll()
                         select new OrderDTO { Id = f.Id, Date = f.Date, Number = f.Number, ProviderId = f.ProviderId, ProviderName = f.Provider.Name };
            return result;
        }

        public async Task<OrderDTO> Get(int id)
        {
            var result = await _orderRepository.Get(id);
            return new OrderDTO { Id = result.Id, Date = result.Date, Number = result.Number, ProviderId = result.ProviderId, ProviderName = result.Provider.Name };
        }

        public async Task<OrderDetailWithItemsDTO> GetOrderWithItems(int id)
        {
            var result = await _orderRepository.Get(id);
            return new OrderDetailWithItemsDTO
            {
                Id = result.Id,
                Date = result.Date,
                Number = result.Number,
                ProviderId = result.ProviderId,
                ProviderName = result.Provider.Name,
                OrderItem = result.OrderItem.Select(i => new OrderItemShortDTO { Id = i.Id, Name = i.Name, Quantity = i.Quantity, Unit = i.Unit }).ToList()
            };
        }

        public async Task<DistinctValues> GetDistinct()
        {
            DistinctValues distinct = await _distinctRepository.GetDistinct();
            return distinct;
        }

        public async Task<bool> Update(int id, OrderDTO orders)
        {
            var result = new Order { Date = orders.Date, Number = orders.Number, Id = orders.Id, ProviderId = orders.ProviderId};
            return await _orderRepository.Update(id, result);
        }

        public async Task<bool> Delete(int id)
        {
            return await _orderRepository.Delete(id);
        }
    }
}
