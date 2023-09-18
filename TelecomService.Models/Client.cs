using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelecomService.Models
{
    [Table("Clients")]
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите полное имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Телефон обязательно для заполнения")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон", Prompt = "8-***-***-**-**")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Роль обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Роль", Prompt = "Введите роль")]
        public string Role { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Client()
        {
            Orders = new List<Order>();
        }
    }
}
