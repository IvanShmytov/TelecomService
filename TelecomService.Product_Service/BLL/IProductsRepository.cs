using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelecomService.Models;

namespace TelecomService.Product_Service.BLL
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(int id);
        Task Add(Product item);
        Task Update(Product item, Product newItem);
        Task Delete(Product item);
        Task<Product> GetByName(string Name);
    }
}
