using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class Member : UserDetails

    {
        public int id { get; set; }
        public byte Role { get; set; }
        public LocationM Location { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
        public int? ChatRoomId { get; set; }
        public string Adrress { get; set; }
        public Selectoption Instructions { get; set; }
        public Member(int UserId, string FirstName, string lastName, string UserName)
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.LastName = lastName;
            this.Username = UserName;
            ChatRoomId = 0;
        }

        public Member()
        {
        }
    }
}