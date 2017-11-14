using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data.Entity;
using GoogleMapBot.Models;

using System.Threading.Tasks;

namespace TelegramBot
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>());
           
            Telegram.Bot.Api bot = new Telegram.Bot.Api("423178669:AAE-lOeN5Hp0yC57FY_GiG5_JZxtvJNDk4I");
            //bot.SetWebhook("https://hozhan.ir/api/Webhook").Wait();

        }
        
    }
}