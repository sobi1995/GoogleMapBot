using GoogleMapBot.Models;
using System.Collections.Generic;

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