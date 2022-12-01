using MOrders.DAL.Interfaces.IModels;
using MOrders.DAL.Models;

namespace MOrders.BLL.DataTransferObject.Order
{
    public class DistinctValuesForSelect : DistinctValues
    {
        public List<string>? Number { get; set; }
        public List<string>? ItemName { get; set; }
        public List<string>? ProviderName { get; set; }
        public List<string>? Unit { get; set; }
    }
}
