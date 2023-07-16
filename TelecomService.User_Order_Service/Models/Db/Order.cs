using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelecomService.User_Order_Service.Models.Db
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата", Prompt = "Дата заказа")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Поле Статус обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Статус", Prompt = "Статус заказа")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Поле Адрес обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Адрес", Prompt = "Введите адрес доставки")]
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
        public Order()
        {
            Products = new List<Product>();
        }
    }
}
