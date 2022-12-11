using MOrders.BLL.Interfaces;
using MOrders.DAL.Interfaces;
using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.DataTransferObject.OrderItem;
using MOrders.Domain.Entities;
using MOrders.Domain.Models;

namespace MOrders.BLL.Services
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemServices(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItemDTO> Create(OrderItemDTO orderItem)
        {
            var result = new OrderItem() { Name = orderItem.Name, Quantity = orderItem.Quantity, OrderId = orderItem.OrderId, Unit = orderItem.Unit };
            var response = await _orderItemRepository.Create(result);
            if (response == null)
                return null;
            orderItem.Id = response.Id;
            return orderItem;
        }

        public async Task<bool> Delete(int id)
        {
            return await _orderItemRepository.Delete(id);
        }

        public async Task<OrderItemDTO> Get(int id)
        {
            var result = await _orderItemRepository.Get(id);
            return new OrderItemDTO { Id = result.Id, Name = result.Name, OrderId = result.OrderId, Quantity = result.Quantity, Unit = result.Unit, OrderNumber = result.Order.Number };
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAll()
        {
            var result = from f in await _orderItemRepository.GetAll()
                         select new OrderItemDTO
                         {
                             Id = f.Id,
                             Name = f.Name,
                             OrderId = f.OrderId,
                             Quantity = f.Quantity,
                             Unit = f.Unit,
                             OrderNumber = f.Order.Number
                         };

            return result.ToList();
        }

        public async Task<IEnumerable<OrderTableDTO>> GetOrderItemsTable(OrderFilter filter)
        {
            var result = from f in await _orderItemRepository.GetByFilter(filter)
                         select new OrderTableDTO
                         {
                             Id = f.Id,
                             OrderId = f.OrderId,
                             Name = f.Name,
                             Quantity = f.Quantity,
                             Unit = f.Unit,
                             Number = f.Order.Number,
                             Date = f.Order.Date,
                             ProviderId = f.Order.Provider.Id,
                             ProviderName = f.Order.Provider.Name,
                         };
            return result;
        }

        public async Task<bool> Update(int id, OrderItemDTO orderItem)
        {
            var result = new OrderItem { Id = orderItem.Id, Name = orderItem.Name, Quantity = orderItem.Quantity, Unit = orderItem.Unit };
            return await _orderItemRepository.Update(id, result); ;
        }
    }
}
