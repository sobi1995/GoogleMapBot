using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace GoogleMapBot.Models
{
    public class TelegramClass
    {
        private TelegramBotClient Bot = new TelegramBotClient("423178669:AAE-lOeN5Hp0yC57FY_GiG5_JZxtvJNDk4I");
       private dbService _db;
        public TelegramClass()
        {
            Bot.OnMessage += Bot_OnMessage;
            _db = new dbService();
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
                _db.AddWhenStart(message.Chat.Id);

                   var dynamicKeyBord = new ReplyKeyboardMarkup(KeyBord.GetReplyKeyboardMarkup(KeyBord.Menu, 2, 2, null));
                dynamicKeyBord.ResizeKeyboard = true;
                await Bot.SendTextMessageAsync(message.Chat.Id, "Choose", true, true, 0, dynamicKeyBord);
            }
            else if (message.Text == "ثبت نام")
            {
                var dynamicKeyBord = new InlineKeyboardMarkup(KeyBord.GetInlineKeyboard(KeyBord.Profile, 2, 0, null));


                await Bot.SendTextMessageAsync(message.Chat.Id, "Choose", true, true, 0, dynamicKeyBord);
            }
        }
    }
}