namespace Catalog.Products.Features.GetProductsByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category:string}", async (string category, ISender sender) =>
            {
                var query = new GetProductsByCategoryQuery(category);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsByCategoryResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products By Category")
            .WithDescription("Retrieves a list of products by category from the catalog.");
        }
    }
}
