using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    

    }
}