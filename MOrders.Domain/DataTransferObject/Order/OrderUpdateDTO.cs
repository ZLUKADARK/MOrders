namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
