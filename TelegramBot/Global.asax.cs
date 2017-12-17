using CodeBlock.Bot.Engine.Controllers;
using GoogleMapBot.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace TelegramBot
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>());


           
            try
            {
            UpdateApp();
                Telegram.Bot.Api bot = new Telegram.Bot.Api("438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A");
                bot.SetWebhook("https://482f5211.ngrok.io/api/Webhook").Wait();
                //UserConfog d = new UserConfog();
                UserConfog d = Singleton.Instance;
                d.StartTemeUser();
                MaperConfig ConfigInCtor = new MaperConfig();
            }
            catch
            {
                HttpRuntime.UnloadAppDomain();
            }
        }


        public void UpdateApp()
        {


            Context db = new Context();
            if (db.Member.Count() <= 0) return;
            db.Database.ExecuteSqlCommand("UPDATE Members SET Instructions = 1 , ChatRoomId = NULL");
            db.SaveChanges();
            BrodCastMsgToUpdate();

        }
        public void BrodCastMsgToUpdate()
        {
            KeyBord KeyBord = new KeyBord();
            string []A = { "🔵%  من  انلاین هستم" };
            var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(A,1, 0, null));
            dynamicKeyBord.ResizeKeyboard = true;
            Telegram.Bot.Api bot = new Telegram.Bot.Api("438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A");
            dbService _dbService = new dbService();
            var AllUser = _dbService.GetAllUserid();
            foreach (var item in AllUser)
            {
                bot.SendTextMessage(item, "😃😃😃\n" +
 "بات آبدیت شد"+
  "شما هم اکنون میتونید از قابلیت های جدید   آن   و عدم مشاهده خطا مشاهده شده از آن لدت ببرید" +

 "\n❗️لطفا در صورت هر گونه خطا ان را با ادمین درمیان بگزارید", replyMarkup: dynamicKeyBord);
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