namespace MOrders.DAL.Interfaces
{
    public interface IDistinctRepository<T> where T : class
    {
        public Task<T> GetDistinct();
    }
}
