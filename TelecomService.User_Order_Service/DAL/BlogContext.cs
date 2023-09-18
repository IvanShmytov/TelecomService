using Microsoft.EntityFrameworkCore;
using TelecomService.Models;

namespace TelecomService.User_Order_Service.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> Orders_Products { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
