namespace TelecomService.User_Order_Service.Models.Db
{
    public class OrderViewModel
    {
        public int Number { get; set; }
        public int CountOfProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
    }
}
