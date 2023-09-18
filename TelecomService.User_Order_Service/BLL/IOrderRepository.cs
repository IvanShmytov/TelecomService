using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelecomService.Models;

namespace TelecomService.User_Order_Service.BLL
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> Get(int id);
        Task Add(Order item);
        Task Update(Order item, Order newItem);
        Task Delete(Order item);
    }
}
