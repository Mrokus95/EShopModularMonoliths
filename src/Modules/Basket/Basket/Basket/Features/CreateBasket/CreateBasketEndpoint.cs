namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketRequest(ShoppingCartDto ShoppingCart);

    public record CreateBasketResponse(Guid Id);


    public class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (CreateBasketRequest request, ISender sender) =>
            {
               
                var command = request.Adapt<CreateBasketCommand>();
                var result = await sender.Send(command);
                var response = new CreateBasketResponse(result.Id);

                return Results.Created($"/basket/{response.Id}", response);
            })
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new basket for the user.")
            .WithDescription("Creates a new basket for the user with the provided shopping cart details.");
        }
    }
}
