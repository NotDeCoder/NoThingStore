using NoThingStore.Models;

namespace NoThingStore.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(string userId, string email, ShoppingCart cart);

        public Task SendOrderConfirmationEmail(Order order);
    }
}
