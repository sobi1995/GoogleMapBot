using GoogleMapBot.Models;
using Microsoft.AspNet.SignalR;
using TelegramBot.Models;

namespace TelegramBot.Hubs
{
    public class ChatHub : Hub
    {
        public void SendPhotoOnMap(UserDetails UserDetails)
        {
            // Call the addNewMessageToPage method to update clients.
         //Clients.All.addnewmessagetopage(message);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
             hubContext.Clients.All.addNewMessageToPage(UserDetails);
        }

        public void brodcast(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.MessageBrodcast(message);
            //Clients.All.MessageBrodcast(message);
        }

        public void deleteonmap(string username)
        {
            // Call the addNewMessageToPage method to update clients.
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.DeleteOnMap(username);
            //Clients.All.MessageBrodcast(message);
        }

    }
}