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
using TelegramBot.Models;

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
            try
            {
                Telegram.Bot.Api bot = new Telegram.Bot.Api("438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A");
                bot.SetWebhook("https://73c3bcda.ngrok.io/api/Webhook").Wait();
                //UserConfog d = new UserConfog();
                UserConfog d = Singleton.Instance;
                d.StartTemeUser();
            }
            
          

             catch
            {
                HttpRuntime.UnloadAppDomain();

            }

        }
        //public override void Init()
        //{
        //    this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
        //    base.Init();
        //}

        //void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    System.Web.HttpContext.Current.SetSessionStateBehavior(
        //        SessionStateBehavior.Required);
        //}

     

    }
}