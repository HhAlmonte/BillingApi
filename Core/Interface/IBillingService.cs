namespace Core.Interface
{
    public interface IBillingService
    {
        Task<decimal> GetTotal(int productId, decimal quantity);
    }
}
