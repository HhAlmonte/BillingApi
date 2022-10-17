using BussinessLogic.Persistence.Context;
using Core.Interface;

namespace BussinessLogic.Logic
{
    public class BillingService : IBillingService
    {
        private readonly StoreDbContext _context;

        public BillingService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotal(int productId, decimal quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            return product.Price * quantity;
        }
    }
}
