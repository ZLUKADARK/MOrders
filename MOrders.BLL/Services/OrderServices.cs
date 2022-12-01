using Microsoft.EntityFrameworkCore;
using MOrders.BLL.DataTransferObject;
using MOrders.BLL.DataTransferObject.Order;
using MOrders.BLL.DataTransferObject.OrderItem;
using MOrders.BLL.Interfaces;
using MOrders.DAL.Entities;
using MOrders.DAL.Interfaces;
using MOrders.DAL.Models;
using Orders.ViewModels.Provider;


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
        public async Task<OrderViewModel> CreateOrder(OrderViewModel orders)
        {
            var result = new Order() { Date = orders.Date, Number = orders.Number, ProviderId = orders.ProviderId };  
            orders.Id = (await _orderRepository.Create(result)).Id;
            return orders;
        }

        public async Task<OrderItemViewModel> CreateOrderItemToOrder(OrderItemViewModel orderItem)
        {
            var result = new OrderItem() { Name = orderItem.Name, Quantity = orderItem.Quantity, OrderId = orderItem.OrderId, Unit = orderItem.Unit };
            orderItem.Id = (await _orderItemRepository.Create(result)).Id;
            return orderItem;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrders()
        {
            var result = from f in await _orderRepository.GetAll()
                         select new OrderViewModel { Id = f.Id, Date = f.Date, Number = f.Number, ProviderId = f.ProviderId, ProviderName = f.Provider.Name };
            return result;
        }

        public async Task<OrderViewModel> GetOrder(int id)
        {
            var result = await _orderRepository.Get(id);
            return new OrderViewModel { Id = result.Id, Date = result.Date, Number = result.Number, ProviderId = result.ProviderId, ProviderName = result.Provider.Name };
        }

        public async Task<OrderDetailWithItemsViewModel> GetOrderWithItems(int id)
        {
            var result = await _orderRepository.Get(id);
            return new OrderDetailWithItemsViewModel
            {
                Id = result.Id,
                Date = result.Date,
                Number = result.Number,
                ProviderId = result.ProviderId,
                ProviderName = result.Provider.Name,
                OrderItem = result.OrderItem.Select(i => new OrderItemShorViewModel { Id = i.Id, Name = i.Name, Quantity = i.Quantity, Unit = i.Unit }).ToList()
            };
        }

        public async Task<OrderItemViewModel> GetOrderItem(int id)
        {
            var result = await _orderItemRepository.Get(id);
            return new OrderItemViewModel { Id = result.Id, Name = result.Name, OrderId = result.OrderId, Quantity = result.Quantity, Unit = result.Unit, OrderNumber = result.Order.Number };
        }

        public async Task<IEnumerable<ProviderViewModel>> GetProviders()
        {
            var result = (await _providerRepository.GetAll())
                .Select(p => new ProviderViewModel { Id = p.Id, Name = p.Name });
            return result;
        }

        public async Task<IEnumerable<OrderTableViewModel>> GetOrdersTable(OrdersFilterViewModel filter)
        {
            var result = from f in await _orderItemRepository.GetByFilter(filter)
                         select new OrderTableViewModel
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

        public async Task<DistinctValuesForSelect> GetDistinct()
        {
            DistinctValuesForSelect distinct = (DistinctValuesForSelect)await _distinctRepository.GetDistinct();
            return distinct;
        }

        public async Task<bool> UpdateOrder(int id, OrderUpdateViewModel orders)
        {
            var result = new Order { Date = orders.Date, Number = orders.Number, Id = orders.Id, ProviderId = orders.ProviderId};
            return await _orderRepository.Update(id, result);
        }

        public async Task<bool> UpdateOrderItem(int id, OrderItemUpdateViewModel orderItem)
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
