using CQRS_Dapper.Queries;
using MediatR;
using CQRS_Dapper.Models;
using CQRS_Dapper.Commands;

namespace CQRS_Dapper.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            var productApi = routes.MapGroup("/api/products").WithTags("Products");

            productApi.MapPost("/", async (IMediator mediator, CQRS_Dapper.Models.ProductCommandModel request) =>
            {
                var command = new CreateProductCommand { Product = request };
                var productId = await mediator.Send(command);
                return TypedResults.Created($"/api/movies/{productId}", productId);
            });

            productApi.MapGet("/", async (IMediator mediator) =>
            {
                var query = new GetAllProductsQuery();
                var products = await mediator.Send(query);
                return TypedResults.Ok(products);
            });

            routes.MapGet("/products/{id}", async (IMediator mediator, int id) =>
            {
                var query = new GetProductByIdQuery { ProductId = id };
                var product = await mediator.Send(new GetProductByIdQuery { ProductId = id });
                if (product == null)
                {
                    return (IResult)TypedResults.NotFound(new { Message = $"Product with ID {id} not found." });
                }
                return TypedResults.Ok(product);
            });
        }
    }
}
