using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Models
{
    [ComplexType]
    public class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}