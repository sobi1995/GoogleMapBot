using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TelegramBot.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message,string x="",string y="")
        {
            // Call the addNewMessageToPage method to update clients.

            //Clients.All.addNewMessageToPage(name, message);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.addNewMessageToPage("x="+x+"   y="+y);
        }
        public void Brodcast( string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addMessage("xczxcxzc");
        }
    }
}