using Microsoft.EntityFrameworkCore;
using TelecomService.Models;

namespace TelecomService.Product_Service.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
