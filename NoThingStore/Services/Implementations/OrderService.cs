using Microsoft.AspNetCore.Identity;
using NoThingStore.Data;
using NoThingStore.Models;
using NoThingStore.Services.Interfaces;

namespace NoThingStore.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(string userId, string email, ShoppingCart cart)
        {
            // Calculate the total price of the order
            decimal totalPrice = cart.GetTotalPrice();

            // Create a new Order object
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Email = email,
                TotalPrice = totalPrice
            };

            // Add the order to the context
            _context.Orders.Add(order);

            // Create OrderItem objects for each cart item and add them to the order
            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price
                };

                order.OrderItems.Add(orderItem);
            }

            // Save changes to the context
            await _context.SaveChangesAsync();

            return order;
        }

        public Task SendOrderConfirmationEmail(Order order)
        {
            return Task.CompletedTask;
        }
    }
}
