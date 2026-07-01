namespace Catalog.Products.Features.CreateProduct
{

    public record CreateProductCommand(
        CreateProductDto Product
        ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
            RuleFor(x => x.Product.Category)
                .NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Product.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");
            RuleFor(x => x.Product.ImageFile)
                .NotEmpty().WithMessage("Image file is required.");
            RuleFor(x => x.Product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }


    internal class CreateProductHandler(
        CatalogDbContext dbContext) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
         
            var product = CreateNewProduct(command.Product);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(CreateProductDto productDto)
        {
            return Product.Create(
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price
            );
        }
    }
}
