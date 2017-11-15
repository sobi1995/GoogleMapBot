using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class Member

    {
        
        public int id { get; set; }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string UserName { get; set; }
        public byte Role { get; set; }
        public Location Location { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
        public int? ChatRoomId { get; set; }


        public Member(int UserId,string FirstName, string lastName, string UserName  )
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.lastName = lastName;
            this.UserName = UserName;
            ChatRoomId = 0;
        }
        public Member()
        {

        }

    }
}