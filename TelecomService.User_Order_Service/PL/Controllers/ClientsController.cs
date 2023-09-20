using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TelecomService.Models;
using TelecomService.User_Order_Service.BLL;
using Microsoft.Extensions.Logging;

namespace TelecomService.User_Order_Service.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _repo;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientRepository repo, ILogger<ClientsController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into ClientsController");
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        [HttpPost]
        [Route("Authenticate/{email}")]
        public async Task<IActionResult> Authenticate([FromRoute] string email, [FromBody] string password)
        {
            password = Encryptor.HashPassword(password);
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
            _logger.LogInformation("ClientsController - Authenticate");
            return StatusCode(200, $"Пользователь {client.Email} прошел аутентификацию.");
        }

        /// <summary>
        /// Return all clients
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _repo.GetAll();
            _logger.LogInformation("ClientsController - GetAll");
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
            if (client is null)
                throw new NullReferenceException("Пользователь на найден");
            _logger.LogInformation("ClientsController - GetClientById");
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
            if (client is null)
                throw new NullReferenceException("Пользователь на найден");
            _logger.LogInformation("ClientsController - GetClientByEmail");
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
            _logger.LogInformation("ClientsController - GetClientsOrderStory");
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
            _logger.LogInformation("ClientsController - Register");
            return StatusCode(201, $"Пользователь {newClient.Email} добавлен.");
        }
        /// <summary>
        /// Delete client
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var client = await _repo.Get(id);
            await _repo.Delete(client);
            _logger.LogInformation("ClientsController - Delete");
            return StatusCode(204);
        }
        /// <summary>
        /// Update client
        /// </summary>
        //[Authorize (Roles = "Admin")]
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client newClient)
        {
            var client = await _repo.Get(id);
            await _repo.Update(client, newClient);
            _logger.LogInformation("ClientsController - Update");
            return StatusCode(204);
        }
    }
}
