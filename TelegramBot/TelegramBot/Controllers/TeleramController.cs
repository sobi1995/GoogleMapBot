using GoogleMapBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TelegramBot.Controllers
{
    public class TeleramController : Controller
    {
        // GET: Teleram
        TelegramClass Telg = new TelegramClass();
        int Conter = 0;
        public ActionResult Index()
        {
            Task.Run(() => Telg.Start());

            Thread th = new Thread(new ThreadStart(FuncConter));

        
            th.Start();
            return View();

             
        }

        public void Test() {

            try
            {
                Response.ContentType = "text/event-stream";
                Response.Expires = -1;
                Response.Write("data: The server time is: "+DateTime.Now);
                Response.Flush();

            }
            catch (Exception)
            {

                ;
            }
     






        }

        public void FuncConter() {
            while (true) {
                Thread.Sleep(700);
                Conter++;
           
                Test();
             
            }
            }
        }
    }
 