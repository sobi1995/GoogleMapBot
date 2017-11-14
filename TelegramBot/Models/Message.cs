using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class Message
    {
        public string username { get; set; }
        public string text { get; set; }
        public string dt { get; set; }
    }
}