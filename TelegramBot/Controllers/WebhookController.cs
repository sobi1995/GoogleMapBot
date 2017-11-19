using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using GoogleMapBot.Models;
using TelegramBot.Models;
 
namespace CodeBlock.Bot.Engine.Controllers
{
    public class WebhookController : ApiController
    {
        dbService _dbService;
     

        private Api bot;
        private static ReplyKeyboardMarkup main_menu_key;
      
        private static string image_savePath = @"C://robot_files//1.jpg";
        int Instructions = 0;




        public WebhookController()
        {
            bot = new Api("438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A");
            _dbService = new dbService();
            //تعریف کیبورد
            main_menu_key = new ReplyKeyboardMarkup
            {
                Keyboard = new KeyboardButton[][] { new KeyboardButton[] { "ارسال عکس" }, new KeyboardButton[] { "درباره", "راهنما" } },
                ResizeKeyboard = true,
                OneTimeKeyboard = true,
            };
        }

       

 
        public async Task<IHttpActionResult> Post(Update update)
        {

         
            UserDetails user = new UserDetails()
            {
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName,
                UserId = update.Message.From.Id,
                Username = update.Message.From.Username
            };
          
 
            try
            {
                System.Web.HttpContext.Current.Session["LogID"] = 10;
                if (update.Message.Type == MessageType.TextMessage)
                    TextMessage(update.Message.Text, user);
                else if (update.Message.Type == MessageType.LocationMessage)

                    ;


            }
            catch (Exception ex)
            {
                var b = update;
                var a = ex;
          
            }
            return Ok();
          
        }



        /// <summary>
        /// یک متد برای تست وب سرویس
        /// </summary>
        public string Get()
        {
            return "Yes Its Work";
        }

        public async void TextMessage(string text,UserDetails user) {



            if (text == "/start")
            {
                Member UserStart = new Member(user.UserId, user.FirstName, user.LastName, user.Username);
                _dbService.AddWhenStart(UserStart);
                string[] BtnImIbline = { "🔵  من  انلاین هستم" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(BtnImIbline, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                await bot.SendTextMessage(user.UserId, text: "متن راهنمای ربات", replyMarkup: dynamicKeyBord);
            }
            if (text.TrimAllSpase() == "🔵  من  انلاین هستم".TrimAllSpase())
            {



                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                await bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
            }

            else if (text.TrimAllSpase() == "بازگشت  🔙".TrimAllSpase())
            {
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                await bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);

            }

            else if (Instructions == 2)
            {

                string[] buttonChatRooms = { "🔴خروج" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(buttonChatRooms.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                await bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);

            }
            else if (text.TrimAllSpase() == "👥ساخت چت روم👥".TrimAllSpase())
            {
                Instructions = 1;
                string[] CreateChatRoom = { "ارسال موقعیت % 📍", "بازگشت  🔙" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(CreateChatRoom, 1, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                await bot.SendTextMessage(chatId: user.UserId, text: "لطفا اسم  چت روم خود را وارد کنید", replyMarkup: dynamicKeyBord);
            }
            else if (text.TrimAllSpase() == "📋    لیست روم ها    📋".TrimAllSpase())
            {

                var Rooms = _dbService.GetAllRoom();
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(Rooms.Select(x => x.Name).ToArray(), Rooms.Select(x => x.id.ToString()).ToArray(), 1, 2, null));
                //

                await bot.SendTextMessage(chatId: user.UserId, text: "👇🏻👇🏻👇🏻👇🏻👇🏻👫    لیســـــــــت  رومـــــــــــ ها    👫👇🏻👇🏻👇🏻👇🏻👇🏻👫", replyMarkup: dynamicKeyBord);
            }
            else if (Instructions == 1)
            {
                _dbService.CreateChatRooms(text);
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                await bot.SendTextMessage(chatId: user.UserId, text: "ثبت شد", replyMarkup: dynamicKeyBord);



            }


        }
        public async void LocationMessage(string text, UserDetails user) {



        }

    }
}
