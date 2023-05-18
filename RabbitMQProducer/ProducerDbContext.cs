using Microsoft.EntityFrameworkCore;
using RabbitMQProducer.Models;

namespace RabbitMQProducer
{
    public class ProducerDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=ProducerDb;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
