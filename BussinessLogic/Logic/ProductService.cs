using BussinessLogic.Persistence.Context;
using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Logic
{
    public class ProductService : IProductService
    {
        private readonly StoreDbContext _context;

        public ProductService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsByBrandId(int id)
        {
            var result = await _context.Products
                .Where(p => p.BrandId == id)
                .ToListAsync();

            return result;
        }

        public async Task<List<Product>> GetProductsByCategoryId(int id)
        {
            var result = await _context.Products
                .Where(c => c.CategoryId == id)
                .ToListAsync();

            return result;
        }

        public async void SubstractStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            product.Stock -= quantity;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            return product.Stock >= quantity;
        }

        public async Task<string> GetProductName(int id)
        {
            var result = _context.Products
                .Where(p => p.Id == id)
                .Select(p => p.Name)
                .FirstOrDefaultAsync();
            
            return await result;
        }
    }
}