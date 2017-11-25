using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TelegramBot.Startup))]

namespace TelegramBot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here

            app.MapSignalR();
            app.MapSignalR("/~/signalr", new HubConfiguration());
        }
    }
}