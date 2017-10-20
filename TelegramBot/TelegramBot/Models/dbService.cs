using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelegramBot.Models;
using System.Data.Entity.Migrations;
namespace GoogleMapBot.Models
{
    public class dbService
    {
        private Context _db= new Context();
        public dbService()
        {
            
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

        public void UpdateRecord(User _Update) {
            User UserUpdate = new User();
           
                UserUpdate = _db.User.Where(s => s.id == _Update.id).FirstOrDefault();
           
        if(_Update.Name!=null)    UserUpdate.Name = _Update.Name;
            if (_Update.Age != null) UserUpdate.Age = _Update.Age;
            if (_Update.Discraption != null) UserUpdate.Discraption = _Update.Discraption;
          
                _db.Entry(UserUpdate).State = System.Data.Entity.EntityState.Modified;


            _db.SaveChanges();
           


        }
    }
}