using GoogleMapBot.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace CodeBlock.Bot.Engine.Controllers
{
    public class WebhookController : ApiController
    {
        private dbService _dbService;
        private UserConfog userconfog = Singleton.Instance;// new UserConfog();

        private Api bot;
        private static ReplyKeyboardMarkup main_menu_key;

        private static string image_savePath = @"C://robot_files//1.jpg";
 

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

        [HttpPost]
        public async Task<IHttpActionResult> UpdateMsg(Update update)
       {
            try
            {
              
              

                TelegramBot.Models.LocationM l1 = new TelegramBot.Models.LocationM() { X = 35.693124, Y = 51.417835 };
                TelegramBot.Models.LocationM l2 = new TelegramBot.Models.LocationM() { X = 35.698701, Y = 51.337525 };
   
              var a = GeoCodeCalc.CalcDistance(l1,l2, GeoCodeCalcMeasurement.Kilometers);
      
            UserDetails user = new UserDetails()
            {
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName,
                UserId = update.Message.From.Id,
                Username = update.Message.From.Username,
            };
            Selectoption Instructions = new Selectoption();
            Instructions =(Selectoption) _dbService.GetCurrentInstructionsUser(update.Message.From.Id);
           
                if (update.Message.Type == MessageType.TextMessage)
                    TextMessage(update.Message.Text, user, Instructions);
                else if (update.Message.Type == MessageType.LocationMessage)
                    LocationMessage(new TelegramBot.Models.LocationM() { X = update.Message.Location.Latitude, Y = update.Message.Location.Longitude }, user);
            }
            catch (Exception ex)
            {
                ;
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

        private void TextMessage(string text, UserDetails user, Selectoption Instructions)
        {

            if (text == "/start")
            {
                Member UserStart = new Member(user.UserId, user.FirstName, user.LastName, user.Username);
                _dbService.AddWhenStart(UserStart);
                string[] BtnImIbline = { "🔵%  من  انلاین هستم" };
                _dbService.SetCurrentInstructionsUser(user.UserId, Selectoption.UpdateLocationMember);
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(BtnImIbline, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                bot.SendTextMessage(user.UserId, text: "متن راهنمای ربات", replyMarkup: dynamicKeyBord);
            }
          
            else if (text.TrimAllSpase() == "بازگشت  🔙".TrimAllSpase())
            {
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
            }
           
            else if (text.TrimAllSpase() == "👥ساخت چت روم👥".TrimAllSpase())
            {
                int StatusCharRoom = _dbService.SearchByNeartsRoom(user.UserId);
                if (StatusCharRoom != 0) {
                    _dbService.LoginChatRoom(user.UserId, StatusCharRoom);
                    string[] CreateChatRoom = { "end" };
                    var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(CreateChatRoom, 1, 2, null));
                    dynamicKeyBord.ResizeKeyboard = true;
                    bot.SendTextMessage(chatId: user.UserId, text: "چت روم در محوطه مورد نظر ساخته شده است و شما هم اکنون به الان لاگین شدید", replyMarkup: dynamicKeyBord);
                }
                else
                {
                    _dbService.CreateChatRooms(user.UserId);
                    _dbService.LoginChatRoom(user.UserId, _dbService.SearchByNeartsRoom(user.UserId));
                    string[] CreateChatRoom = { "end" };
                    var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(CreateChatRoom, 1, 2, null));
                    dynamicKeyBord.ResizeKeyboard = true;
                    bot.SendTextMessage(chatId: user.UserId, text: "چت روم ساحته شد و لفرار د نجاور 10 کیلومتری میتوانند لاگین بوشدن", replyMarkup: dynamicKeyBord);
                }

            }
            else if (text.TrimAllSpase() == "📋    لیست روم ها    📋".TrimAllSpase())
            {
                var Rooms = _dbService.GetAllRoom();
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(Rooms.Select(x => x.Name).ToArray(), Rooms.Select(x => x.id.ToString()).ToArray(), 1, 2, null));
                //

                bot.SendTextMessage(chatId: user.UserId, text: "👇🏻👇🏻👇🏻👇🏻👇🏻👫    لیســـــــــت  رومـــــــــــ ها    👫👇🏻👇🏻👇🏻👇🏻👇🏻👫", replyMarkup: dynamicKeyBord);
            }

            var a = userconfog;
        }

        private async void LocationMessage(TelegramBot.Models.LocationM Location, UserDetails user)
        {
         
            _dbService.UpdateLocation(Location, user.UserId);
            userconfog.Adduser(user.UserId);
   
            var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
            dynamicKeyBord.ResizeKeyboard = true;

            bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
        }

        private void TimeOut(int UserId)
        {
            string[] BtnImIbline = { "🔵  من  انلاین هستم" };

            var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(BtnImIbline, 2, 2, null));
            dynamicKeyBord.ResizeKeyboard = true;
            bot.SendTextMessage(UserId, text: "متن راهنمای ربات", replyMarkup: dynamicKeyBord);
        }
    }
}