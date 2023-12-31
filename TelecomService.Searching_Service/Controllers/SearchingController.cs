﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelecomService.Models;

namespace TelecomService.Searching_Service.Controllers
{
    public class SearchingController : Controller
    {
        Uri ClientsOrdersAddress = new Uri("https://localhost:5001/api"); 
        Uri ProductsAddress = new Uri("https://localhost:5002/api");
        HttpClientHandler _httpClientHandler = new HttpClientHandler();
        private readonly ILogger<SearchingController> _logger;
        public SearchingController(ILogger<SearchingController> logger)
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, sert, chain, sslPolicyErrors) => { return true; };
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into SearchingController");
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
            _logger.LogInformation("SearchingController - GetAllClients");
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
            _logger.LogInformation("SearchingController - GetClientById");
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
            _logger.LogInformation("SearchingController - GetClientByEmail");
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

            _logger.LogInformation("SearchingController - GetAllProducts");
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
            _logger.LogInformation("SearchingController - GetProductById");
            return StatusCode(200, product);
        }
        /// <summary>
        /// Return product by Name
        /// </summary>
        [HttpGet]
        [Route("GetProductByName/{name}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string name)
        {
            var product = new Product();
            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (HttpResponseMessage response = httpClient.GetAsync(ProductsAddress + $"/Products/GetProductByName/GetProductByName/{name}").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        product = JsonConvert.DeserializeObject<Product>(data);
                    }
                }
            }
            _logger.LogInformation("SearchingController - GetProductByName");
            return StatusCode(200, product);
        }
    }
}
