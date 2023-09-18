using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelecomService.Models;
using TelecomService.User_Order_Service.DAL;

namespace TelecomService.User_Order_Service.BLL
{
    public class ClientsRepository : IClientRepository 
    {
        protected BlogContext _db;
        public DbSet<Client> Set { get; private set; }

        public ClientsRepository(BlogContext db)
        {
            _db = db;
            var set = _db.Set<Client>();
            set.Load();
            Set = set;
        }

        public async Task Add(Client item)
        {
            item.Password = Encryptor.HashPassword(item.Password);
            Set.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Client item)
        {
            Set.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Client> Get(int id)
        { 
            var client = await Set.FindAsync(id);
            return client;
        }

        public async Task<Client> GetByName(string Name)
        {
            var client = await Set.FirstOrDefaultAsync(x => x.Email == Name);
            return client;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await Set.ToListAsync();
            return clients;
        }

        public async Task Update(Client item, Client newItem)
        {
            item.Name = newItem.Name;
            item.Password = Encryptor.HashPassword(newItem.Password);
            item.Email = newItem.Email;
            item.Phone = newItem.Phone;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetStoryOfOrders (Client item)
        {
            var storyOfOrders = await _db.Orders.Where(o => o.ClientId ==item.Id).Select(o => new OrderViewModel { Number = o.Id, 
                Status = o.Status, Address = o.Address, 
                CountOfProducts = (from or in _db.Orders
                                   join op in _db.Orders_Products on or.Id equals op.OrderId
                                   join p in _db.Products on op.ProductId equals p.Id
                                   where or.ClientId == item.Id && or.Id == o.Id
                                   select p).Count(),
                TotalPrice = (from or in _db.Orders
                              join op in _db.Orders_Products on or.Id equals op.OrderId
                              join p in _db.Products on op.ProductId equals p.Id
                              where or.ClientId == item.Id && or.Id == o.Id
                              select p.Price).Sum()
            }).OrderBy(ovm => ovm.TotalPrice).ToListAsync();
            return storyOfOrders;
        }
    }
}
