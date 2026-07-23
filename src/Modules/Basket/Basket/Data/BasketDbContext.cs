namespace Basket.Data
{
    public class BasketDbContext : DbContext
    {

        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options) { }

        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();

        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(o => o.MigrationsHistoryTable("__EFMigrationsHistory", "basket"));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("basket");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
