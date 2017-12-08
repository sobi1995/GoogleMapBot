using GoogleMapBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Hubs;
using TelegramBot.Models;

namespace CodeBlock.Bot.Engine.Controllers
{
    public class WebhookController : ApiController
    {
        private dbService _dbService;
        private UserConfog userconfog = Singleton.Instance;// new UserConfog();

        private Api bot;
        private static ReplyKeyboardMarkup main_menu_key;
        Thread d;
        private static string image_savePath = @"C://robot_files//1.jpg";
        public WebhookController()
        {
            bot = new Api("438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A");
             d = new Thread(new ThreadStart(Send));
            d.Start();
        }
        void Send() {
            ChatHub d = new ChatHub();
          
                
            while (true)
            {
                Thread.Sleep(1000);
                  d.Send("dddd");
            }


        }
        [HttpPost]
        public async Task<IHttpActionResult> UpdateMsg(Update update)
        {
            try
            {

                UserDetails user = new UserDetails()
                {
                    FirstName = update.Message.From.FirstName,
                    LastName = update.Message.From.LastName,
                    UserId = update.Message.From.Id,
                    Username = update.Message.From.Username,
                };
              

                Selectoption Instructions = new Selectoption();
                Instructions = (Selectoption)_dbService.GetCurrentInstructionsUser(update.Message.From.Id);
                if (update.Message.Text == "من افلاین هستم  🔴")
                    LogOut(update.Message.From.Id, 1);
                else if (Instructions == Selectoption.LoginInChatRoom)
                    SendMesgOnChatRoom(user, update.Message.Text);
                else if (Instructions == Selectoption.Start)
                    Start(update.Message.Text, user);
                else if (Instructions == Selectoption.Mnu)
                    Mnu(update.Message.Text, user);
                else if (Instructions == Selectoption.ImOnline)
                    Updatelocation(new TelegramBot.Models.LocationM() { X = update.Message.Location.Latitude, Y = update.Message.Location.Longitude }, user);
            }
            catch (Exception ex)
            {
                ;
            }
            return Ok(update);
        }
        /// <summary>
        /// یک متد برای تست وب سرویس
        /// </summary>
        public string Get()
        {
            return "Yes Its Work";
        }
        public void LogOut(int UserId, int TypeLogOut)
        {
            string strMsgLogOut = "";
            if (TypeLogOut == 1)
                strMsgLogOut = " شما م اکنور به حالت افلاین رفتید در صورت تمایل بر رویع من انلاین هسان کلیک کنید";
            else
                strMsgLogOut = " ب دلیل استفاده  نکردن مداوم از بات   شما به حالت تعویق  در امدید در صورت تمایل بر رویع من انلاین هستم کلیک کنید ";
            _dbService.SetCurrentInstructionsUser(UserId, Selectoption.ImOnline);
            LogChatRoom(UserId);
            SendMesgOnChatRoom(new UserDetails() { FirstName = "Bot  : ", UserId = UserId }, _dbService.GetFirstnameId(UserId) + "❌  از بات روم خارج شد");
            Sendmsg(UserId, strMsgLogOut, new List<string> { "🔵%  من  انلاین هستم" });

        }
        public void LogChatRoom(int UserId)
        {
            _dbService.LogOutChatRoom(UserId);
        }
        void SendMesgOnChatRoom(UserDetails user, string Msg)
        {


            var userOnChaRoom = _dbService.GetUserOnCharRoom(_dbService.GetCahtRoomidUser(user.UserId));
            foreach (var item in userOnChaRoom)
            {
                if (item == user.UserId) continue;
                bot.SendTextMessage(item, user.FirstName + " : " + Msg);
            }


        }
        void Mnu(string text, UserDetails user)
        {
            if (text.TrimAllSpase() == "👥ساخت چت روم👥".TrimAllSpase()
            || text.TrimAllSpase() == "عضویت در نزدیک ترین روم  📡".TrimAllSpase())
            {
                int StatusCharRoom = _dbService.SearchByNeartsRoom(user.UserId);
                if (StatusCharRoom != 0)
                {
                    _dbService.LoginChatRoom(user.UserId, StatusCharRoom);
                    Sendmsg(user.UserId, "چت روم در محوطه مورد نظر ساخته شده است و شما هم اکنون به الان لاگین شدید", new List<string> { " بازگشت   🔙" });
                    SendMesgOnChatRoom(user, user.FirstName + "به رم لاگین شد");
                }

                else
                {
                    _dbService.CreateChatRooms(user.UserId);
                    _dbService.LoginChatRoom(user.UserId, _dbService.SearchByNeartsRoom(user.UserId));
                    Sendmsg(user.UserId, "چت روم با موفقیت ساحته شد و افرد میتوانند در صورت جست وجو نزدریک ترین چت روم در ان عضو شودند", new List<string> { " بازگشت   🔙" });
                }
                _dbService.SetCurrentInstructionsUser(user.UserId, Selectoption.LoginInChatRoom);
            }




        }
        void Start(string text, UserDetails user)
        {
            if (text == "/start")
            {
                if (!_dbService.IsUser(user.UserId))
                {
                    Member UserStart = new Member(user.UserId, user.FirstName, user.LastName, user.Username);
                    _dbService.AddWhenStart(UserStart);
                    SaveProfileOnDisk(user.UserId);
                }
                _dbService.SetCurrentInstructionsUser(user.UserId, Selectoption.ImOnline);
                Sendmsg(user.UserId, "برای اینکه  بتوانید از سرویس های بات استفاده کنید  رو گزینه زیر کلیک کرده  نا موقعیت کنونی شما برا اطرافیان  تشخیص داده شود", new List<string>() { "🔵% من  انلاین هستم" });
            }

        }
        void Sendmsg(int UserId, string Msg, List<string> Buuton)
        {


            if (_dbService.GetCurrentInstructionsUser(UserId) != Selectoption.ImOnline) Buuton.Add("من افلاین هستم  🔴");
            var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(Buuton.ToArray(), 2, 2, null));
            dynamicKeyBord.ResizeKeyboard = true;
            bot.SendTextMessage(UserId, text: Msg, replyMarkup: dynamicKeyBord);



        }
        void Updatelocation(TelegramBot.Models.LocationM Location, UserDetails user)
        {
         
          
            _dbService.UpdateLocation(Location, user.UserId);
            userconfog.Adduser(user.UserId);
            _dbService.SetCurrentInstructionsUser(user.UserId, Selectoption.Mnu);
            SendLocationOnGoogleMap(user.UserId);
            Sendmsg(user.UserId, "مکان شما با موفقیت ثبت شد\n  از منو زیر سرویس مور علاقع خود را انتخاب کنید", KeyBord.Menu.ToList());
         

        }
        void SendLocationOnGoogleMap(int userID) {
            ChatHub WebSocket = new ChatHub();
            WebSocket.Send("fsdf");

        }
        void back(int UserId, string Msg)
        {

            if (_dbService.GetCurrentInstructionsUser(UserId) == Selectoption.LoginInChatRoom)
                LogChatRoom(UserId);
            Sendmsg(UserId, "لطفن از سرویس هی زیر یکی را  انتخاب کندی", KeyBord.Menu);
        }
        string GetFileNameProfile(string FileId)
        {

            string Filepatch = "";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://api.telegram.org/bot438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A/getFile?file_id=" + FileId);

                JObject obj = JObject.Parse(json);
                JObject name = (JObject)obj["result"];
                Filepatch = (string)name["file_path"];
            }
            return Filepatch;
        }
        string GetUrlProfile(string FileId)
        {

            return "https://api.telegram.org/file/bot438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A/" + FileId;

        }
        async void SaveProfileOnDisk(int id)
        {

            var Photos = bot.GetUserProfilePhotosAsync(id).Result.Photos;
            string FileId = Photos[0][2].FileId;
            string FileName = GetFileNameProfile(FileId);
            string FileUrl = GetUrlProfile(FileName);

            GeneralFunactions.save_file_from_url(id.ToString(), FileUrl);
            //GeneralFunactions.save_file_from_url(FilePatch, FileUrl);

            //using (var stream = System.IO.File.Open(FileUrl, FileMode.Open))
            //{

            //    FileToSend fts = new FileToSend();
            //    fts.Content = stream;
            //    fts.Filename = FileUrl.Split('\\').Last();
            //    var test = await bot.SendPhoto(id, fts, "My Text");
            //}


            //await Bot.SendPhoto(id,photo.Photos,"sdfsdf");
        }
        void SendPhto(int id)
        {

            //  bot.SendPhoto(id, photo: "https://api.telegram.org/file/bot438518161:AAG5xVKFbV4uLf_6CtbyocQhbBv7hHLyL5A/"+, caption: "hii");


        }
        void SendMsgToGrouWhenLoginInRoom(UserDetails user)
        {
            var userOnChaRoom = _dbService.GetUserOnCharRoom(_dbService.GetCahtRoomidUser(user.UserId));
            foreach (var item in userOnChaRoom)
            {
                if (item == user.UserId) continue;
                SendPhoto(item, "/ImgProfiles/Profile" + user.UserId + ".jpg");
            }

        }
        async void SendPhoto(int userid, string url)
        {
            string FilePatch = System.Web.Hosting.HostingEnvironment.MapPath(url);
               using (var stream = System.IO.File.Open(FilePatch, FileMode.Open))
            {
                FileToSend fts = new FileToSend();
                fts.Content = stream;
                fts.Filename = FilePatch.Split('\\').Last();
               await bot.SendPhoto(userid, fts, "My Text");
            }


        }
    }

}
