using MediatR;
using Dapper;
using CQRS_Dapper.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CQRS_Dapper.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly string? _connectionString;

        public GetProductByIdQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Products WHERE Id = @Id";
                var product = await db.QueryFirstOrDefaultAsync<Product>(sqlQuery, new { Id = request.ProductId });
                return product;
            }
        }
    }
}