using System.Data;
using CQRS_Dapper.Models;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CQRS_Dapper.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {

    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly string? _connectionString;

        public GetAllProductsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Products";
                var products = await db.QueryAsync<Product>(sqlQuery);
                return products;
            }
        }
    }
    
}