using System.Data;
using Dapper;
using CQRS_Dapper.Models;
using Microsoft.Data.SqlClient;

namespace CQRS_Dapper.Queries
{
    public class GetProductByIdQuerySimple
    {
        private readonly string? _connectionString;

        public GetProductByIdQuerySimple(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Product> ExecuteAsync(int productId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Products WHERE Id = @Id";
                var product = await db.QueryFirstOrDefaultAsync<Product>(sqlQuery, new { Id = productId });
                return product;
            }
        }
    }
}