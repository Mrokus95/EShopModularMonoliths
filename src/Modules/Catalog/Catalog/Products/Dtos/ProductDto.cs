namespace Catalog.Products.Dtos
{
    public record CreateProductDto(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    );

    public record UpdateProductDto(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    );

    public record ProductDto(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
);
}
