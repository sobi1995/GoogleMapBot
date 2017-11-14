using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
 static   public class ExtMethode
    {
        public static string TrimAllSpase(this string value)
        {
            value = value.Replace(" ", "");
            return value;

        }
    }
}