using CodeBlock.Bot.Engine.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class UserConfog
    {
        private int a = 0;
        private Dictionary<int, int> userDic = new Dictionary<int, int>();

        public UserConfog()
        {
        }

        public void StartTemeUser()
        {
            Task.Run(() => TimeStayUser());
        }

        public void Adduser(int UserId)
        {
     //userDic.Add(UserId, 250);
        }

        public void RemoveUser(int UserId)
        {
     
            userDic.Remove(UserId);
    
        }

        public async Task TimeStayUser()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                  
                    if (userDic.Count <= 0)
                        continue;
                    foreach (var item in userDic.ToList())
                    {
                        userDic[item.Key] -= 1;
                        var a = userDic[item.Key];
                        int Key = userDic.Select(x => x.Key).FirstOrDefault();
                        Debug.WriteLine(userDic[item.Key].ToString());
                        if (userDic[item.Key] <= 0)
                        {
                            WebhookController d = new WebhookController();
                               d.LogOut(Key,0);

                            RemoveUser(Key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddTime(int UserId)
        {
            userDic[UserId] = userDic[UserId]++;
        }
    }

    public sealed class Singleton
    {
        private static readonly Lazy<UserConfog> lazy =
            new Lazy<UserConfog>(() => new UserConfog());

        public static UserConfog Instance { get { return lazy.Value; } }

        private Singleton()
        {
        }
    }
}