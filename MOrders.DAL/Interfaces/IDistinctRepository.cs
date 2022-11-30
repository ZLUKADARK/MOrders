namespace MOrders.DAL.Interfaces
{
    internal interface IDistinctRepository<T> where T : class
    {
        public Task<T> GetDistinct()
    }
}
