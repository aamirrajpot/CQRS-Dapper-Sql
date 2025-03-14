using CQRS_Dapper.Data;
using CQRS_Dapper.Models;

namespace CQRS_Dapper.Commands
{
    public class UpdateProductCommand
    {
        private readonly ApplicationDbContext _context;

        public UpdateProductCommand(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                await _context.SaveChangesAsync();
            }
        }
    }
}