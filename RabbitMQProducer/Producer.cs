using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQProducer.Models;
using System.Net;
using System.Text;

namespace RabbitMQProducer
{
    public interface IProducer
    {
        bool CompletingOrderMessage(int id);
        bool SendingOrderMessage(int id);
    }

    public class Producer : IProducer
    {
        private readonly ProducerDbContext _dbContext;

        public Producer(ProducerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CompletingOrderMessage(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id.Equals(id));
            if (order is null || order.isCompleted == true) return false;

            order.isCompleted = true;
            _dbContext.SaveChanges();

            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "orders",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                string s = JsonConvert.SerializeObject(order);

                var body = Encoding.UTF8.GetBytes(s);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "orders",
                                     basicProperties: null,
                                     body: body);              
            }           
            return true;
        }
        public bool SendingOrderMessage(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id.Equals(id));
            if (order is null || order.isCompleted == false || order.isSent == true) return false;

            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                order.isSent = true;
                _dbContext.SaveChanges();

                channel.QueueDeclare(queue: "orders",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                string s = JsonConvert.SerializeObject(order);

                var body = Encoding.UTF8.GetBytes(s);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "orders",
                                     basicProperties: null,
                                     body: body);
            }
            return true;
        }
    }
}
