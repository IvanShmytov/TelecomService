using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelecomService.User_Order_Service.Models.Db
{
    public class OrdersRepository : IRepository<Order>
    {
        protected BlogContext _db;
        public DbSet<Order> Set { get; private set; }

        public OrdersRepository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<Order>();
            set.Load();
            Set = set;
        }
        public async Task Add(Order item)
        {
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Order item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Order> Get(int id)
        {
            var order = await Set.FindAsync(id);
            order.Product = _db.Products.FirstOrDefault(p => p.Id == order.ProductId);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Set.OrderBy(o => o.Pr_count * o.Product.Price).ToListAsync();
        }

        public async Task Update(Order item, Order newItem)
        {
            item.ProductId = newItem.ProductId;
            item.Product = newItem.Product;
            item.Date = newItem.Date;
            item.Address = newItem.Address;
            item.Status = newItem.Status;
            Set.Update(item);
            await _db.SaveChangesAsync();
        }

        public Task<IEnumerable<OrderViewModel>> GetStoryOfOrders(Order item)
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetByName(string Name)
        {
            throw new System.NotImplementedException();
        }
    }
}
