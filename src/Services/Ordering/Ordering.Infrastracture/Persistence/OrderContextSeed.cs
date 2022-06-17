using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastracture.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            await SeedOrder(orderContext, logger);
        }

        private static async Task SeedOrder(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!(await orderContext.Orders.AnyAsync()))
            {
                await orderContext.Orders.AddRangeAsync(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"seed Order done");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>()
             {
                 new Order()
                 {
                     FirstName="farhad",
                     CardName="1",
                     EmailAddress="f.mohamamdfazeli@gmail.com"
                 }
             };
        }
    }
}