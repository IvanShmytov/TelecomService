using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TelecomService.Models;
using TelecomService.User_Order_Service.BLL;

namespace TelecomService.User_Order_Service.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repo;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderRepository repo, ILogger<OrdersController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into OrdersController");
        }
        /// <summary>
        /// Return all orders
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _repo.GetAll();
            _logger.LogInformation("OrdersController - GetAll");
            return StatusCode(200, orders);
        }
        /// <summary>
        /// Return order by id
        /// </summary>
        [HttpGet]
        [Route("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var order = await _repo.Get(id);
            _logger.LogInformation("OrdersController - GetOrderById");
            return StatusCode(200, order);
        }
        /// <summary>
        /// Add order
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add ([FromBody] Order newOrder)
        {
            await _repo.Add(newOrder);
            _logger.LogInformation("OrdersController - Add");
            return StatusCode(201, $"Заказ {newOrder.Id} добавлен в корзину.");
        }
        /// <summary>
        /// Delete order
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var order = await _repo.Get(id);
            await _repo.Delete(order);
            _logger.LogInformation("OrdersController - Delete");
            return StatusCode(204);
        }
        /// <summary>
        /// Update order
        /// </summary>
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Order newOrder)
        {
            var order = await _repo.Get(id);
            await _repo.Update(order, newOrder);
            _logger.LogInformation("OrdersController - Update");
            return StatusCode(204);
        }
    }
}
