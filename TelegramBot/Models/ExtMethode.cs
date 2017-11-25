namespace TelegramBot.Models
{
    static public class ExtMethode
    {
        public static string TrimAllSpase(this string value)
        {
            value = value.Replace(" ", "");
            return value;
        }
    }
}