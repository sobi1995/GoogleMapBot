using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class MemberLocations
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Location { get; set; }
    }
}