using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Hubs;
using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class TelegramClass
    {
        byte Instructions = 0;


        private TelegramBotClient Bot = new TelegramBotClient("423178669:AAE-lOeN5Hp0yC57FY_GiG5_JZxtvJNDk4I");
        private dbService _dbService;
        public TelegramClass()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnUpdate += Bot_OnUpdate;
            _dbService = new dbService();
        }
        public void SendLocationToClint(string  x,string    y,long id) {
            ChatHub h = new ChatHub();
            var UserLocation = _dbService.GetUser(id);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Profile, User>()
                   .ReverseMap();
            });
            var a = Mapper.Map<Profile>(UserLocation);
           a.UrlUserProfile =GetPhotoProile(id);
            
            a.X = x;
            a.Y = y;
            h.Send(a);

        }
        async void SedMsg(long id, string Txt)
        {

           
            await Bot.SendTextMessage(id, Txt);
        }
        private void Bot_OnUpdate(object sender, UpdateEventArgs e)
        {
            var Message = e.Update;
            if (e.Update.Type == UpdateType.CallbackQueryUpdate)
            {
                string Msg = Message.CallbackQuery.Data;
                if (Msg == "نام")
                {

                    SedMsg(e.Update.CallbackQuery.From.Id, "نام را وارد کنید");
                    Instructions = 1;
                }
                else if (Msg == "سن")
                {
                    Instructions = 2;
                    SedMsg(e.Update.CallbackQuery.From.Id, "سن را وارد کنید");

                }

                else if (Msg == "Bio")
                {
                    Instructions = 3;
                    SedMsg(e.Update.CallbackQuery.From.Id, "Bio را وارد کنید");

                }


            }
            else if (e.Update.Message.Location!=null)
            {
                string x = e.Update.Message.Location.Latitude.ToString() ;
                string y = e.Update.Message.Location.Longitude.ToString();
                SendLocationToClint(x,y,e.Update.Message.From.Id);
            }
         


        }
        public void Start()
        {

            Bot.StartReceiving();
        }
        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
         
            var message = e.Message;

            if (message.Text == "/start")
            {
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                PropertyUserTelegram UserStart = new PropertyUserTelegram()
                {
                    FirstName = message.From.FirstName,
                    id = message.From.Id,
                    lastName = message.From.LastName,
                    UserName = message.From.Username
                };
                if (_dbService.AddWhenStart(UserStart) != 1)
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "شما از قبل ثبت نام  شده اید", true, true, 0, dynamicKeyBord);
                    return;
                }

                await Bot.SendTextMessageAsync(message.Chat.Id, "شما ثبت نام شدید لطفا بر رویه پروفیل بروید و اطلاعات ضوروری رو پر کنید", true, true, 0, dynamicKeyBord);
            }
            else if (message.Text == "پروفایل")
            {
                User Me = _dbService.GetUser(message.Chat.Id);
                string Profile = "FirstName : " + Me.FirstName + "\n lastName : " + Me.lastName
                    + "\n UserName : " + Me.UserName + "\n Name : " + Me.Name
                   + Me.Age + "\n Discraption : " + Me.Discraption;
              
                List<string> NullFild = _dbService.ProfileNull(message.Chat.Id);
                if (NullFild.Count <= 0)
                {
                    Profile += "😃😃😃اطلاعات شما کامل است";
                    if (!KeyBord.Menu.Contains("ارسال لوکیشن")) {
                        KeyBord.Menu.Add("ارسال لوکیشن");
                        var dynamicKeyBordSuccessRegister = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                        dynamicKeyBordSuccessRegister.ResizeKeyboard = true;
                        await Bot.SendTextMessageAsync(message.Chat.Id, Profile, true, true, 0, dynamicKeyBordSuccessRegister);
                        return;
                    }
                }

                if (NullFild.Count >= 1)
                {
                    string combindedString = string.Join(",", NullFild.ToArray());
                    Profile += "\nلطفن فیلد های زیر را پر کنید\n" + combindedString;
                }
               
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(KeyBord.Profile.ToArray(), 2, 0, null, ""));


                await Bot.SendTextMessageAsync(message.Chat.Id, Profile, true, true, 0, dynamicKeyBord);
            
            }
            else if (Instructions == 1)
            {

                User UserName = new User() { Name = message.Text, id = message.Chat.Id };
                _dbService.UpdateRecord(UserName);
                Instructions = 0;
                SedMsg(UserName.id, "اطلاعات شما ثبت شد");

            }
            else if (Instructions == 2)
            {

                User UserAge = new User() { Age = Int32.Parse(message.Text), id = message.Chat.Id };
                _dbService.UpdateRecord(UserAge);
                Instructions = 0;
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                await Bot.SendTextMessageAsync(message.Chat.Id, "ثبت شد", true, true, 0, dynamicKeyBord);
            }

            else if (Instructions == 3)
            {

                User UserBio = new User() { Discraption = message.Text, id = message.Chat.Id };
                _dbService.UpdateRecord(UserBio);
                Instructions = 0;
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu.ToArray(), 2, 2, null));
                await Bot.SendTextMessageAsync(message.Chat.Id, "ثبت شد", true, true, 0, dynamicKeyBord);


            }
        }

       string GetPhotoProile(long id)
        {
          var photo = Bot.GetUserProfilePhotosAsync(Int32.Parse(id.ToString())).Result;

            return  photo.Photos[0][0].FilePath;
             //await Bot.SendPhoto(id,photo.Photos,"sdfsdf");
        }

    }
}