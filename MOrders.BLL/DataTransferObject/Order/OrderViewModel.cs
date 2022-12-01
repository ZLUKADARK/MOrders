namespace MOrders.BLL.DataTransferObject.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string ProviderName { get; set; }
        public int ProviderId { get; set; }
    }
}
