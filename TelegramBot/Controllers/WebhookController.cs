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
    public class webhookController : ApiController
    {
        dbService _dbService;
     

        private Api bot;
        private static ReplyKeyboardMarkup main_menu_key;
        string botToken = "Bot Token";
        private static string image_savePath = @"C://robot_files//1.jpg";
        int Instructions = 0;




        public webhookController()
        {
            bot = new Api("423178669:AAE-lOeN5Hp0yC57FY_GiG5_JZxtvJNDk4I");
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
            var chatType = update.Message.Chat.Type;

            //ربات به آپدیت های گروههای چت پاسخی ندهد
            if (chatType != ChatType.Private)
            {
                return Ok();
            }
            var text = update.Message.Text;



            string TrimMsg = text;
             
    
     
           if (TrimMsg == "/start")
            {

                string[] BtnImIbline = { "🔵  من  انلاین هستم" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(BtnImIbline, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "متن راهنمای ربات", replyMarkup: dynamicKeyBord);
            }
            if (text.TrimAllSpase() == "🔵  من  انلاین هستم".TrimAllSpase())
            {
                
             
                   Member UserStart = new Member(update.Message.From.Id,update.Message.From.FirstName, update.Message.From.LastName, update.Message.From.Username);
                _dbService.AddWhenStart(UserStart);
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
             
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
            }

            else if (TrimMsg.TrimAllSpase() == "بازگشت  🔙".TrimAllSpase())
            {
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
               
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);

            }
            else if (Instructions == 2)
            {
                
                string[] buttonChatRooms = { "🔴خروج" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(buttonChatRooms.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
                
            }
            else if (TrimMsg.TrimAllSpase() == "👥ساخت چت روم👥".TrimAllSpase())
            {
                Instructions = 1;
                string[] CreateChatRoom = { "بازگشت  🔙" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(CreateChatRoom, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "لطفا اسم  چت روم خود را وارد کنید", replyMarkup: dynamicKeyBord);
            }
            else if (TrimMsg.TrimAllSpase() == "📋    لیست روم ها    📋".TrimAllSpase())
            {

                var Rooms = _dbService.GetAllRoom();
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(Rooms.Select(x => x.Name).ToArray(), Rooms.Select(x => x.id.ToString()).ToArray(), 1, 2, null));
                //
                
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "👇🏻👇🏻👇🏻👇🏻👇🏻👫    لیســـــــــت  رومـــــــــــ ها    👫👇🏻👇🏻👇🏻👇🏻👇🏻👫", replyMarkup: dynamicKeyBord);
            }
            else if (Instructions == 1)
            {
                _dbService.CreateChatRooms(TrimMsg);
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
               
                await bot.SendTextMessage(chatId: update.Message.Chat.Id, text: "ثبت شد", replyMarkup: dynamicKeyBord);



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
    }
}
