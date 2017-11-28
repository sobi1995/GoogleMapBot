using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Models
{
    [ComplexType]
    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}