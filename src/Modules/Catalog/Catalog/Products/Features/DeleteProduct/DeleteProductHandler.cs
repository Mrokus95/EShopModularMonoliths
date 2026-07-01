using Catalog.Products.Exceptions;

namespace Catalog.Products.Features.DeleteProduct
{

    public record DeleteProductCommand(Guid Id
        ) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool isSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required.");
        }
    }

    internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            
            var product = await dbContext.Products.FindAsync(new object[] { command.Id }, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            dbContext.Products.Remove(product);
 
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}