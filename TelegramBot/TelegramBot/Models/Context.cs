using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoogleMapBot.Models
{
    public class Context:DbContext
    {
        public DbSet<User> User { get; set; }
    }
}