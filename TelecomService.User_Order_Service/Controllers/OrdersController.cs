using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TelecomService.User_Order_Service.Models.Db;

namespace TelecomService.User_Order_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> _repo;

        public OrdersController(IRepository<Order> repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Return all orders
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _repo.GetAll();
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
            return StatusCode(204, $"Заказ удален.");
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
            return StatusCode(200, $"Заказ {order.Id} обновлен!");
        }
    }
}
