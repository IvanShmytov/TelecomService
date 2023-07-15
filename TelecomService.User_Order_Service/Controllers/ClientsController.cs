using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TelecomService.User_Order_Service.Models.Db;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TelecomService.User_Order_Service.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientsController : Controller
    {
        private readonly IRepository<Client> _repo;
        
        public ClientsController(IRepository<Client> repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        [HttpPost]
        [Route("Authenticate/{email}")]
        public async Task<IActionResult> Authenticate([FromRoute] string email, [FromBody] string password)
        {
            if (String.IsNullOrEmpty(email) ||
            String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            Client client = await _repo.GetByName(email);
            if (client is null)
                throw new AuthenticationException("Пользователь на найден");

            if (client.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");
            List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, client.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, client.Role)
                    };

            var identity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return StatusCode(200, $"Пользователь {client.Email} прошел аутентификацию.");
        }

        /// <summary>
        /// Return all clients
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _repo.GetAll();
            return StatusCode(200, clients);
        }
        /// <summary>
        /// Return client by id
        /// </summary>
        [HttpGet]
        [Route("GetClientById/{id}")]
        public async Task<IActionResult> GetClientById([FromRoute] int id)
        {
            var client = await _repo.Get(id);
            return StatusCode(200, client);
        }
        /// <summary>
        /// Return client by email
        /// </summary>
        [HttpGet]
        [Route("GetClientByEmail/{email}")]
        public async Task<IActionResult> GetClientByEmail([FromRoute] string email)
        {
            var client = await _repo.GetByName(email);
            return StatusCode(200, client);
        }
        /// <summary>
        /// Return client's story of orders
        /// </summary>
        [HttpGet]
        [Route("GetClientsOrderStory/{id}")]
        public async Task<IActionResult> GetClientsOrderStory([FromRoute] int id)
        {
            var client = await _repo.Get(id);
            var story = await _repo.GetStoryOfOrders(client);
            return StatusCode(200, story);
        }
        /// <summary>
        /// Add client
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Client newClient)
        {
            await _repo.Add(newClient);
            return StatusCode(201, $"Пользователь {newClient.Email} добавлен.");
        }
        /// <summary>
        /// Delete client
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var client = await _repo.Get(id);
            await _repo.Delete(client);
            return StatusCode(204, $"Пользователь удален.");
        }
        /// <summary>
        /// Update client
        /// </summary>
        [Authorize (Roles = "Admin")]
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client newClient)
        {
            var client = await _repo.Get(id);
            await _repo.Update(client, newClient);
            return StatusCode(200, $"Пользователь {client.Email} обновлен!");
        }
    }
}
