namespace Basket.Basket.Models
{
    public class ShoppingCart : Aggregate<Guid>
    {
        public string UserName { get; set; } = default!;
        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();
        public decimal TotalPrice => _items.Sum(item => item.Price * item.Quantity);
    
        public static ShoppingCart Create(Guid id, string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName, nameof(userName));

            var shoppingCart = new ShoppingCart
            {
                Id = id,
                UserName = userName
            };
            return shoppingCart;
        }

        public void AddItem(Guid productId, int quantity, string color, decimal price, string productName)
        {
            ArgumentException.ThrowIfNullOrEmpty(productName, nameof(productName));
            ArgumentException.ThrowIfNullOrEmpty(color, nameof(color));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var existingItem = _items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new ShoppingCartItem(Id, productId, quantity, color, price, productName);
                _items.Add(newItem);
            }
        }

        public void RemoveItem(Guid productId)
        {
            var itemToRemove = _items.FirstOrDefault(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
            }
        }

    }
}
