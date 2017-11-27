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
        private int Instructions = 0;

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

        
            TelegramBot.Models.Location l1 = new TelegramBot.Models.Location() { X =float.Parse("35/693124"), Y = float.Parse("51/417835") };
            TelegramBot.Models.Location l2 = new TelegramBot.Models.Location() { X = float.Parse("35/698701"), Y = float.Parse("51/337525") };
                //TelegramBot.Models.Location l1 = new TelegramBot.Models.Location() {X= 35/7009232,Y= 51/422829 };
                //TelegramBot.Models.Location l2 = new TelegramBot.Models.Location() { X = 35/7009232, Y = 51/422829 };
                //l1.X = 35/7009232;
                //l1.Y = 51/422829;
                //l2.X = 35/7009232;
                //l2.Y = 51/422829;
                var a = GeoCodeCalc.CalcDistance(l1.X, l1.Y, l2.X, l2.Y, GeoCodeCalcMeasurement.Kilometers);
            }
            catch (Exception ex)
            {

                throw;
            }
           
            UserDetails user = new UserDetails()
            {
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName,
                UserId = update.Message.From.Id,
                Username = update.Message.From.Username
            };

            try
            {
                if (update.Message.Type == MessageType.TextMessage)
                    TextMessage(update.Message.Text, user);
                else if (update.Message.Type == MessageType.LocationMessage)
                    LocationMessage(new TelegramBot.Models.Location() { X = update.Message.Location.Latitude, Y = update.Message.Location.Longitude }, user);
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

        private void TextMessage(string text, UserDetails user)
        {

            if (text == "/start")
            {
                Member UserStart = new Member(user.UserId, user.FirstName, user.LastName, user.Username);
                _dbService.AddWhenStart(UserStart);
                string[] BtnImIbline = { "🔵  من  انلاین هستم" };

                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(BtnImIbline, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                bot.SendTextMessage(user.UserId, text: "متن راهنمای ربات", replyMarkup: dynamicKeyBord);
            }
            if (text.TrimAllSpase() == "🔵  من  انلاین هستم".TrimAllSpase())
            {
                userconfog.Adduser(user.UserId);

                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
            }
            else if (text.TrimAllSpase() == "بازگشت  🔙".TrimAllSpase())
            {
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
            }
            else if (Instructions == 2)
            {
                string[] buttonChatRooms = { "🔴خروج" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(buttonChatRooms.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                bot.SendTextMessage(chatId: user.UserId, text: "گزینه مورد نظر را انتخاب کنید", replyMarkup: dynamicKeyBord);
            }
            else if (text.TrimAllSpase() == "👥ساخت چت روم👥".TrimAllSpase())
            {
                Instructions = 1;
                string[] CreateChatRoom = { "ارسال موقعیت % 📍", "بازگشت  🔙" };
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(CreateChatRoom, 1, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                bot.SendTextMessage(chatId: user.UserId, text: "لطفا اسم  چت روم خود را وارد کنید", replyMarkup: dynamicKeyBord);
            }
            else if (text.TrimAllSpase() == "📋    لیست روم ها    📋".TrimAllSpase())
            {
                var Rooms = _dbService.GetAllRoom();
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(Rooms.Select(x => x.Name).ToArray(), Rooms.Select(x => x.id.ToString()).ToArray(), 1, 2, null));
                //

                bot.SendTextMessage(chatId: user.UserId, text: "👇🏻👇🏻👇🏻👇🏻👇🏻👫    لیســـــــــت  رومـــــــــــ ها    👫👇🏻👇🏻👇🏻👇🏻👇🏻👫", replyMarkup: dynamicKeyBord);
            }
            else if (Instructions == 1)
            {
                _dbService.CreateChatRooms(text);
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;

                bot.SendTextMessage(chatId: user.UserId, text: "ثبت شد", replyMarkup: dynamicKeyBord);
            }
            var a = userconfog
;
        }

        private async void LocationMessage(TelegramBot.Models.Location Location, UserDetails user)
        {
            _dbService.UpdateLocation(Location, user.UserId);
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