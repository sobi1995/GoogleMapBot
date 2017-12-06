using GoogleMapBot.Models;
using Microsoft.AspNet.SignalR;
using TelegramBot.Models;

namespace TelegramBot.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(UserDetails DetailsUser)
        {
            // Call the addNewMessageToPage method to update clients.

             Clients.All.addnewmessagetopage("dsfsd");
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            //hubContext.Clients.All.addNewMessageToPage(DetailsUser);
        }

        public void brodcast(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.MessageBrodcast(message);
        }
    }
}