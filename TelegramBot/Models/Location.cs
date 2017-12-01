using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Models
{
    [ComplexType]
    public class LocationM
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}