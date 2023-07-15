using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelecomService.User_Order_Service.Models.Db;

namespace TelecomService.Searching_Service.Controllers
{
    public class SearchingController : Controller
    {
        Uri ClientsOrdersAddress = new Uri("https://localhost:5001/api"); 
        Uri ProductsAddress = new Uri("https://localhost:5002/api");
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        public SearchingController()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, sert, chain, sslPolicyErrors) => { return true; };
        }
        /// <summary>
        /// Return all clients
        /// </summary>
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = new List<Client>();
            using (var httpClient = new HttpClient(_httpClientHandler)) 
            {
                using (HttpResponseMessage response = httpClient.GetAsync(ClientsOrdersAddress + "/Clients/GetAll").Result) 
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        clients = JsonConvert.DeserializeObject<List<Client>>(data);
                    }
                }
            }          
            return StatusCode(200, clients);
          
        }
        /// <summary>
        /// Return client by id
        /// </summary>
        [HttpGet]
        [Route("GetClientById/{id}")]
        public async Task<IActionResult> GetClientById([FromRoute] int id)
        {
            var client = new Client();
            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (HttpResponseMessage response = httpClient.GetAsync(ClientsOrdersAddress + $"/Clients/GetClientById/GetClientById/{id}").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        client = JsonConvert.DeserializeObject<Client>(data);
                    }
                }
            }
            return StatusCode(200, client);
        }
        /// <summary>
        /// Return client by email
        /// </summary>
        [HttpGet]
        [Route("GetClientByEmail/{email}")]
        public async Task<IActionResult> GetClientByEmail([FromRoute] string email)
        {
            var client = new Client();
            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (HttpResponseMessage response = httpClient.GetAsync(ClientsOrdersAddress + $"/Clients/GetClientByEmail/GetClientByEmail/{email}").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        client = JsonConvert.DeserializeObject<Client>(data);
                    }
                }
            }
            return StatusCode(200, client);
        }
        /// <summary>
        /// Return all products
        /// </summary>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = new List<Product>();
            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (HttpResponseMessage response = httpClient.GetAsync(ProductsAddress + "/Products/GetAll").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        products = JsonConvert.DeserializeObject<List<Product>>(data);
                    }
                }
            }
            return StatusCode(200, products);

        }
        /// <summary>
        /// Return product by id
        /// </summary>
        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = new Product();
            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (HttpResponseMessage response = httpClient.GetAsync(ProductsAddress + $"/Products/GetProductById/GetProductById/{id}").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        product = JsonConvert.DeserializeObject<Product>(data);
                    }
                }
            }
            return StatusCode(200, product);
        }
    }
}
