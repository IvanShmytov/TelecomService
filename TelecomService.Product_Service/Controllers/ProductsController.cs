using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TelecomService.User_Order_Service.Models.Db;

namespace TelecomService.User_Order_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repo;

        public ProductsController(IRepository<Product> repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Return all products
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repo.GetAll();
            return StatusCode(200, products);
        }
        /// <summary>
        /// Return product by id
        /// </summary>
        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _repo.Get(id);
            return StatusCode(200, product);
        }
        /// <summary>
        /// Add product
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add ([FromBody] Product newProduct)
        {
            await _repo.Add(newProduct);
            return StatusCode(201, $"Продукт {newProduct.Id} добавлен в базу.");
        }
        /// <summary>
        /// Delete product
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _repo.Get(id);
            await _repo.Delete(product);
            return StatusCode(204, $"Продукт удален.");
        }
        /// <summary>
        /// Update order
        /// </summary>
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Product newProduct)
        {
            var product = await _repo.Get(id);
            await _repo.Update(product, newProduct);
            return StatusCode(200, $"Продукт {product.Id} обновлен!");
        }
    }
}
