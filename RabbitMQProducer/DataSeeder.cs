using Microsoft.AspNetCore.Identity;
using RabbitMQProducer.Models;
using System.Security.Claims;

namespace RabbitMQProducer
{
    public static class DataSeeder
    {
        public static void Initialize()
        {
            using (var dbContext = new ProducerDbContext())
            {
                if (!dbContext.Orders.Any())
                {
                    var order1 = new Order
                    {
                        Email = "example1@example.com",
                        isCompleted = false,
                        isSent = false
                    };
                    dbContext.Orders.Add(order1);
                    var order2 = new Order
                    {
                        Email = "example2@example.com",
                        isCompleted = true,
                        isSent = false
                    };
                    dbContext.Orders.Add(order2);
                    var order3 = new Order
                    {
                        Email = "example3@example.com",
                        isCompleted = true,
                        isSent = true
                    };
                    dbContext.Orders.Add(order3);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
