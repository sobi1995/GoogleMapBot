using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TelegramBot.Models
{
   static public class GeneralFunactions
    {
   static     public void save_file_from_url(string file_name, string url)
        {
              file_name = System.Web.Hosting.HostingEnvironment.MapPath("/ImgProfiles\\Profile") +file_name.Replace("/","\\")+".jpg";

            byte[] content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                content = br.ReadBytes(500000);
                br.Close();
            }
            response.Close();

            FileStream fs = new FileStream(file_name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(content);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }
        }
    }
}
 