using GoogleMapBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class HistoryChating
    {
        public int Id { get; set; }
        public string Msg { get; set; }
 
        public int MemberId { get; set; }


    }
}