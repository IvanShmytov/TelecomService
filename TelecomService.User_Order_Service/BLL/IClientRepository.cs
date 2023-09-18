using System.Collections.Generic;
using System.Threading.Tasks;
using TelecomService.Models;

namespace TelecomService.User_Order_Service.BLL
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> Get(int id);
        Task Add(Client item);
        Task Update(Client item, Client newItem);
        Task Delete(Client item);
        Task<IEnumerable<OrderViewModel>> GetStoryOfOrders(Client item);
        Task<Client> GetByName(string Name);
    }
}
