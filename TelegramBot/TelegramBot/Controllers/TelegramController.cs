using GoogleMapBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TelegramBot.Hubs;

namespace TelegramBot.Controllers
{
    public class TelegramController : ApiController
    {

        [HttpGet]
        public IHttpActionResult StartBot() {

            TelegramClass Tbs = new TelegramClass();
            Task.Run(() =>  Tbs.Start());
            return Ok("1");
 
        }
    }
}
