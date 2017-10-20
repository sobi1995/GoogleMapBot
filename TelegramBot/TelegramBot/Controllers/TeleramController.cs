using GoogleMapBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TelegramBot.Controllers
{
    public class TeleramController : Controller
    {
        // GET: Teleram
        TelegramClass Telg = new TelegramClass();
        public ActionResult Index()
        {
            Task.Run(() => Telg.Start());

            return View();
        }
    }
}