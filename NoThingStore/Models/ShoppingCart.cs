namespace NoThingStore.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; }

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

        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(i => i.Price * i.Quantity);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
