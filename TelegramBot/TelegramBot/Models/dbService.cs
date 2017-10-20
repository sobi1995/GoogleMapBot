using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapBot.Models
{
    public class dbService
    {
        private Context _db;

        public dbService()
        {
            _db = new Context();
        }

      public  void AddWhenStart(long CodeTel) {


      if(!_db.User.Where(x => x.CodeTel.Equals(CodeTel)).Any()){
                User StrtUser = new User() { CodeTel = CodeTel };
                _db.User.Add(StrtUser);
                _db.SaveChanges();

            }

            var a = _db.User.Where(x => x.CodeTel.Equals(CodeTel)).First();

        }
    }
}