using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class TelegramClass
    {
        private TelegramBotClient Bot = new TelegramBotClient("423178669:AAE-lOeN5Hp0yC57FY_GiG5_JZxtvJNDk4I");
       private dbService _dbService;
        public TelegramClass()
        {
            Bot.OnMessage += Bot_OnMessage;
            _dbService = new dbService();
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
                var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                PropertyUserTelegram UserStart = new PropertyUserTelegram()
                {
                    FirstName = message.From.FirstName,
                    id = message.From.Id,
                    lastName = message.From.LastName,
                    UserName = message.From.Username
                };
                if (_dbService.AddWhenStart(UserStart) != 1) { 
                    await Bot.SendTextMessageAsync(message.Chat.Id, "شما از قبل ثبت نام  شده اید", true, true, 0, dynamicKeyBord);
                    return;
                }

                await Bot.SendTextMessageAsync(message.Chat.Id, "شما ثبت نام شدید لطفا بر رویه پروفیل بروید و اطلاعات ضوروری رو پر کنید", true, true, 0, dynamicKeyBord);
            }
            else if (message.Text == "پروفایل")
            {
                User Me = _dbService.GetUser(message.Chat.Id);
                string Profile = "FirstName : " + Me.FirstName+ "\n lastName : " + Me.lastName
                    + "\n UserName : " + Me.UserName + "\n Name : " + Me.Name
                   + Me.Age + "\n Discraption : " + Me.Discraption;

               List<string> NullFild = _dbService.ProfileNull(message.Chat.Id);
                string combindedString = string.Join(",", NullFild.ToArray());
                Profile += "\nلطفن فیلد های زیر را پر کنید\n" + combindedString;
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(KeyBord.Profile, 2, 0, null, ""));


                await Bot.SendTextMessageAsync(message.Chat.Id, Profile, true, true, 0, dynamicKeyBord);
            }
        }
    }
}