namespace Catalog.Data.Seed
{
    internal static class InitialData
    {
        internal static IEnumerable<Product> Products => [
        Product.Create( "IPhone X", ["category1"], "Long description IPhone X", "imagefile", 500),
        Product.Create( "Samsung 10", ["category1"], "Long description Samsung 10", "imagefile", 400),
        Product.Create( "Huawei Plus", ["category2"], "Long description Huawei Plus", "imagefile", 650),
        Product.Create( "Xiaomi Mi", ["category2"], "Long description Xiaomi Mi", "imagefile", 450)
    ];
    }
}
