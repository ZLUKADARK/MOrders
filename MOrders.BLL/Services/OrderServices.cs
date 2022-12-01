using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.DataTransferObject.OrderItem;
using MOrders.Domain.DataTransferObject.Provider;
using MOrders.BLL.Interfaces;
using MOrders.DAL.Interfaces;
using MOrders.Domain.Entities;
using MOrders.Domain.Models;

namespace MOrders.BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Provider> _providerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem, OrderFilter> _orderItemRepository;
        private readonly IDistinctRepository<DistinctValues> _distinctRepository;
        public OrderServices(IRepository<Provider> providerRepository, 
            IRepository<Order> orderRepository, 
            IRepository<OrderItem, OrderFilter> orderItemRepository, 
            IDistinctRepository<DistinctValues> distinctRepository)
        {
            _providerRepository = providerRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _distinctRepository = distinctRepository;
        }
        public async Task<OrderDTO> CreateOrder(OrderDTO orders)
        {
            var result = new Order() { Date = orders.Date, Number = orders.Number, ProviderId = orders.ProviderId };  
            orders.Id = (await _orderRepository.Create(result)).Id;
            return orders;
        }

        public async Task<OrderItemDTO> CreateOrderItemToOrder(OrderItemDTO orderItem)
        {
            var result = new OrderItem() { Name = orderItem.Name, Quantity = orderItem.Quantity, OrderId = orderItem.OrderId, Unit = orderItem.Unit };
            orderItem.Id = (await _orderItemRepository.Create(result)).Id;
            return orderItem;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            var result = from f in await _orderRepository.GetAll()
                         select new OrderDTO { Id = f.Id, Date = f.Date, Number = f.Number, ProviderId = f.ProviderId, ProviderName = f.Provider.Name };
            return result;
        }

        public async Task<OrderDTO> GetOrder(int id)
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

        public async Task<OrderItemDTO> GetOrderItem(int id)
        {
            var result = await _orderItemRepository.Get(id);
            return new OrderItemDTO { Id = result.Id, Name = result.Name, OrderId = result.OrderId, Quantity = result.Quantity, Unit = result.Unit, OrderNumber = result.Order.Number };
        }

        public async Task<IEnumerable<ProviderDTO>> GetProviders()
        {
            var result = (await _providerRepository.GetAll())
                .Select(p => new ProviderDTO { Id = p.Id, Name = p.Name });
            return result;
        }

        public async Task<IEnumerable<OrderTableDTO>> GetOrdersTable(OrderFilter filter)
        {
            var result = from f in await _orderItemRepository.GetByFilter(filter)
                         select new OrderTableDTO
                         {
                             OrderItemId = f.Id,
                             OrderId = f.OrderId,
                             Name = f.Name,
                             Quantity = f.Quantity,
                             Unit = f.Unit,
                             Number = f.Order.Number,
                             Date = f.Order.Date,
                             ProviderId = f.Order.Provider.Id,
                             ProviderName = f.Order.Provider.Name
                         };
            return result;
        }

        public async Task<DistinctValues> GetDistinct()
        {
            DistinctValues distinct = await _distinctRepository.GetDistinct();
            return distinct;
        }

        public async Task<bool> UpdateOrder(int id, OrderUpdateDTO orders)
        {
            var result = new Order { Date = orders.Date, Number = orders.Number, Id = orders.Id, ProviderId = orders.ProviderId};
            return await _orderRepository.Update(id, result);
        }

        public async Task<bool> UpdateOrderItem(int id, OrderItemUpdateDTO orderItem)
        {
            var result = new OrderItem { Id = orderItem.Id, Name = orderItem.Name, Quantity = orderItem.Quantity, Unit = orderItem.Unit };
            return await _orderItemRepository.Update(id, result); ;
        }

        public async Task<bool> DeleteOrderItem(int id)
        {
            return await _orderItemRepository.Delete(id);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderRepository.Delete(id);
        }
    }
}
