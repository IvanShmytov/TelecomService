using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelecomService.Models;
using TelecomService.Product_Service.DAL;

namespace TelecomService.Product_Service.BLL
{
    public class ProductsRepository : IProductsRepository
    {
        protected BlogContext _db;
        public DbSet<Product> Set { get; private set; }

        public ProductsRepository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<Product>();
            set.Load();
            Set = set;
        }
        public async Task Add(Product item)
        {
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Product item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> Get(int id)
        {
            var product = await Set.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await Set.OrderBy(p => p.Price).ToListAsync();
        }

        public async Task Update(Product item, Product newItem)
        {
            item.Price = newItem.Price;
            item.Name = newItem.Name;
            item.In_stock = newItem.In_stock;
            await _db.SaveChangesAsync();
        }

        public async Task<Product> GetByName(string Name)
        {
            return await Set.FirstOrDefaultAsync(x => x.Name == Name);
        }
    }
}
