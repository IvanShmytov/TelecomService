using System.Security.Cryptography;
using System.Text;

namespace TelecomService.User_Order_Service.BLL
{
    public static class Encryptor
    {
        public static string HashPassword(string password) 
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var a in hash) 
            {
                sb.Append(a.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
