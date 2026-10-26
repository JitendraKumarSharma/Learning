using Confluent.Kafka;
using MicroserviceKafka.Model;
using MicroserviceKafka.OrderService.Data;
using MicroserviceKafka.OrderService.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MicroserviceKafka.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(OrderDbContext dbContext, IKafkaProducer producer) : ControllerBase
    {
        [HttpGet]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await dbContext.Orders.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrder(OrderModel order)
        {
            order.OrderDate = DateTime.Now;
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            var orderMessage = new OrderMessage
            {
                OrderId = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };

            await producer.ProduceAsync("order-topic", new Message<string, string>
            {
                Key = order.Id.ToString(),
                Value = JsonSerializer.Serialize(orderMessage)
            });

            return order;
        }
    }
}
