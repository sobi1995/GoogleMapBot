namespace TelegramBot.Models
{
    static public class ExtMethode
    {
        public static string TrimAllSpase(this string value)
        {
            value = value.Replace(" ", "");
            return value;
        }
        public static double LocationDecimals(this double value)
        {
            //int ConterDecimals = 0;
            string ResVal = "";
            string StrVal = value.ToString();
            int IndexChar = StrVal.IndexOf("/");
            ResVal = StrVal.Substring(0, IndexChar + 7);
            //for (int i = 0; i < value.ToString().Length; i++)
            //{
            //    ResVal += StrVal[i].ToString();
            //    if (StrVal[i].ToString() != "/") continue;
            //    ConterDecimals++;
            //    if (ConterDecimals >= 7) break;
            //}
             return double.Parse(ResVal);
        }

        public static string ToLocationDistance(this string Distance) {

           return Distance.Substring(0, Distance.IndexOf("/") +3);
          
         
          
            
        }



    }
}