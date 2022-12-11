using MOrders.Domain.DataTransferObject.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderDTO : OrderBase
    {
        public string? ProviderName { get; set; }
    }
}
