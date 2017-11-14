using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TelegramBot.Models;
using GoogleMapBot.Models;

namespace TelegramBot.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(Member DetailsUser)
        {
            // Call the addNewMessageToPage method to update clients.

            //Clients.All.addNewMessageToPage(name, message);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.addNewMessageToPage(DetailsUser);
        }
        public void brodcast( string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.MessageBrodcast(message);
        }
    }
}