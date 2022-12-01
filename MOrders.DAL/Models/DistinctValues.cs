using MOrders.DAL.Interfaces.IModels;

namespace MOrders.DAL.Models
{
    public class DistinctValues : IDistinctValues
    {
        public List<string>? Number { get; set; }
        public List<string>? ItemName { get; set; }
        public List<string>? ProviderName { get; set; }
        public List<string>? Unit { get; set; }
    }
}
