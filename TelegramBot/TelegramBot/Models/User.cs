using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapBot.Models
{
    public class User
    {
        public int id { get; set; }
        public long CodeTel { get; set; }
        public int? Age { get; set; }
        public string Name { get; set; }
        public string Discraption { get; set; }
    }
}