using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelecomService.Models;
using TelecomService.User_Order_Service.DAL;

namespace TelecomService.User_Order_Service.BLL
{
    public class OrdersRepository : IOrderRepository
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
            var list = (IQueryable<Product>) (from op in _db.Orders_Products
                                        join p in _db.Products on op.ProductId equals p.Id
                                        where op.OrderId == order.Id 
                                        select p).OrderBy(p => p.Price);
            foreach (var item in list) 
            {
                order.Products.Add(item);
            }    
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Set.OrderBy(o => o.Date).ToListAsync();
        }

        public async Task Update(Order item, Order newItem)
        {
            item.Date = newItem.Date;
            item.Address = newItem.Address;
            item.Status = newItem.Status;
            await _db.SaveChangesAsync();
        }

    }
}
