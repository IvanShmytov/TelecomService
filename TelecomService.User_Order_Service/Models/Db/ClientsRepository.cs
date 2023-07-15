using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelecomService.User_Order_Service.Models.Db
{
    public class ClientsRepository : IRepository<Client> 
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
            item.Password = Encrypt(item.Password);
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
            client.Password = Decrypt(client.Password);
            return client;
        }

        public async Task<Client> GetByName(string Name)
        {
            var client = Set.FirstOrDefault(x => x.Email == Name);
            client.Password = Decrypt(client.Password);
            return client;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await Set.ToListAsync();
            foreach (var client in clients) 
            { 
                client.Password = Decrypt(client.Password); 
            }
            return clients;
        }

        public async Task Update(Client item, Client newItem)
        {
            item.Name = newItem.Name;
            item.Password = Encrypt(newItem.Password);
            item.Email = newItem.Email;
            item.Phone = newItem.Phone;
            Set.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetStoryOfOrders (Client item)
        {
            var storyOfOrders = _db.Orders.Where(o => o.ClientId ==item.Id).Select(o => new OrderViewModel { Number = o.Id, CountOfProducts = o.Pr_count, 
                TotalPrice = o.Pr_count * (int)o.Product.Price, Status = o.Status, Address = o.Address }).OrderBy(o => o.TotalPrice);
            return storyOfOrders;
        }
        public string Encrypt(string text)
        {
            string newtext = null;
            for (int i = 0; i < text.Length; i++)
            {
                newtext += (char)(text[i] + 3);
            }
            return newtext;
        }
        public string Decrypt(string text)
        {
            string newtext = null;
            for (int i = 0; i < text.Length; i++)
            {
                newtext += (char)(text[i] - 3);
            }
            return newtext;
        }
    }
}
