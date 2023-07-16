using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelecomService.User_Order_Service.Models.Db
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Название обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название товара")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Цена", Prompt = "Укажите цену товара")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "В наличии", Prompt = "Количество товара в наличии")]
        public int In_stock { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Product()
        {
            Orders = new List<Order>();
        }
    }
}
