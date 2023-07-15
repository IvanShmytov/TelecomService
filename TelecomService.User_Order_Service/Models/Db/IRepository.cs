using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelecomService.User_Order_Service.Models.Db
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T item);
        Task Update(T item, T newItem);
        Task Delete(T item);
        Task<IEnumerable<OrderViewModel>> GetStoryOfOrders(T item);
        Task<T> GetByName(string Name);
    }
}
