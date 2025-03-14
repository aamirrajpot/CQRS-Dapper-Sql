using System.Data;
using CQRS_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CQRS_Dapper.Queries
{
    public class GetAllProductsQuerySimple
    {
        private readonly string? _connectionString;

        public GetAllProductsQuerySimple(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> ExecuteAsync()
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