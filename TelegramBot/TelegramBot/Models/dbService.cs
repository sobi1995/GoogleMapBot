using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class dbService
    {
        private Context _db;
        public dbService()
        {
            _db = new Context();
        }
        public  int AddWhenStart(PropertyUserTelegram Ueser) {


      if(!_db.User.Where(x => x.id.Equals(Ueser.id)).Any()){
                User StrtUser = new User() { id = Ueser.id,FirstName= Ueser.FirstName,UserName= Ueser.UserName,lastName= Ueser.lastName };
                _db.User.Add(StrtUser);
                _db.SaveChanges();
                return 1;

            }
            return 0;
        }
        public List<string> ProfileNull(long CodeTel) {


            List<string> Register = new List<string>();
            var User = _db.User.Where(x => x.id.Equals(CodeTel)).First();
            if (User.Age == null)
                Register.Add("سن");
            if (User.Name == null)
                Register.Add("نام"); ;

            return Register;
        }
        public User GetUser(long id)
        {

            return _db.User.Where(x => x.id==id).FirstOrDefault();

        }
    }
}