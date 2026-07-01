using Catalog.Products.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.UpdateProduct
{

    public record UpdateProductCommand(
        UpdateProductDto Product
        ) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool isSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Product.Id)
                .NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
            RuleFor(x => x.Product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            
            var product = await dbContext.Products.FindAsync(new object[] { command.Product.Id }, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException(command.Product.Id);
            }

            UpdateProductWithNewValues(product, command.Product);
 
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, UpdateProductDto updatedProduct)
        {
            product.Update(
                 updatedProduct.Name,
                 updatedProduct.Category,
                 updatedProduct.Description,
                 updatedProduct.ImageFile,
                 updatedProduct.Price
                 );
        }
    }
}
