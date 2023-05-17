namespace NoThingStore.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; }

        public decimal Total => Items.Sum(i => i.Price * i.Quantity);

        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }

        public void AddItem(CartItem item)
        {
            var existingItem = Items.Find(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.Find(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                });
            }
        }


        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
