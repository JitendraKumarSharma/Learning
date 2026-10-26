using MicroserviceKafka.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceKafka.OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<OrderModel> Orders { get; set; }
    }
}
