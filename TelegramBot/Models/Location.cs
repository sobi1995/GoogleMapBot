using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    [ComplexType]
    public class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}