using Core.Entities;

namespace Core.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsByCategoryId(int id);
        Task<List<Product>> GetProductsByBrandId(int id);
        Task<string> GetProductName(int id);
        void SubstractStock(int productId, int quantity);
        Task<bool> HasStock(int productId, int quantity);
    }
}
