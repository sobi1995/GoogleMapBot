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
            userDic.Add(UserId, 7);
        }

        public void RemoveUser(int UserId)
        {
            var b = userDic;
            userDic.Remove(UserId);
            var a = userDic;
        }

        public async Task TimeStayUser()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    Debug.WriteLine(userDic.Count.ToString());
                    if (userDic.Count <= 0)
                        continue;
                    foreach (var item in userDic.ToList())
                    {
                        userDic[item.Key] -= 1;
                        var a = userDic[item.Key];
                        int Key = userDic.Select(x => x.Key).FirstOrDefault();
                        if (userDic[item.Key] <= 0)
                        {
                            WebhookController d = new WebhookController();
                           // d.TimeOut(Key);

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