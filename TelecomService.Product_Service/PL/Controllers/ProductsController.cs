using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TelecomService.Models;
using TelecomService.Product_Service.BLL;

namespace TelecomService.Product_Service.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _repo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsRepository repo, ILogger<ProductsController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into ProductsController");
        }
        /// <summary>
        /// Return all products
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repo.GetAll();
            _logger.LogInformation("ProductsController - GetAll");
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
            _logger.LogInformation("ProductsController - GetProductById");
            return StatusCode(200, product);
        }
        /// <summary>
        /// Return product by id
        /// </summary>
        [HttpGet]
        [Route("GetProductByName/{name}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            var product = await _repo.GetByName(name);
            _logger.LogInformation("ProductsController - GetProductByName");
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
            _logger.LogInformation("ProductsController - Add");
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
            _logger.LogInformation("ProductsController - Delete");
            return StatusCode(204);
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
            _logger.LogInformation("ProductsController - Update");
            return StatusCode(204);
        }
    }
}
