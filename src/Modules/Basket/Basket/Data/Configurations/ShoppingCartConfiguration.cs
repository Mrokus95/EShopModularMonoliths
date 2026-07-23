using System.Security.Cryptography.X509Certificates;

namespace Basket.Data.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey("ShoppingCartId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
