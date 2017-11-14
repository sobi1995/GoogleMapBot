using GoogleMapBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class ChatRoom
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Discraption { get; set; }
        public virtual List<Member> Member { get; set; }
      
    }
}