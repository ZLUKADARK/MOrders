namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string ProviderName { get; set; }
        public int ProviderId { get; set; }
    }
}
