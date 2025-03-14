using CQRS_Dapper.Data;
using CQRS_Dapper.Models;

namespace CQRS_Dapper.Commands
{
    public class AddProductCommand
    {
        private readonly ApplicationDbContext _context;

        public AddProductCommand(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}