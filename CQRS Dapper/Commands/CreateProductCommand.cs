using MediatR;
using CQRS_Dapper.Data;
using CQRS_Dapper.Models;

namespace CQRS_Dapper.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public ProductCommandModel Product { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,int>
    {
        private readonly ApplicationDbContext _context;

        public CreateProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                Stock = request.Product.Stock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}