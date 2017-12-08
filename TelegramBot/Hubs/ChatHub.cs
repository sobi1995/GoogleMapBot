using GoogleMapBot.Models;
using Microsoft.AspNet.SignalR;
using TelegramBot.Models;

namespace TelegramBot.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            message = "sdjfksjdfjhfk";
            //Clients.All.addnewmessagetopage(message);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
             hubContext.Clients.All.addNewMessageToPage(message);
        }

        public void brodcast(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.MessageBrodcast(message);
            //Clients.All.MessageBrodcast(message);
        }
    }
}