namespace MOrders.DAL.Interfaces.IModels
{
    public interface IOrderFilter
    {
        public DateTime DateNow { get; set; }
        public DateTime DatePast { get; set; }
        public string[]? Name { get; set; }
        public string[]? Unit { get; set; }
        public string[]? Number { get; set; }
        public string[]? ProviderName { get; set; }
    }
}
