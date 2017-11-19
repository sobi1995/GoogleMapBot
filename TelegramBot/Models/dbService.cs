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
        private Context _db;
        public dbService()
        {
            _db = new Context();
        }
        public  int AddWhenStart(Member Ueser) {
            try
            {

            

            if (!_db.Member.Any(x => x.UserId==Ueser.UserId))
            {
                    Member StrtUser = new Member() {
                        UserId = Ueser.UserId,
                        FirstName = Ueser.FirstName,
                        Username = Ueser.Username,
                        LastName = Ueser.LastName,
                        Role = 0,
                        Location = new Location() { X = 0, Y = 0 }
                      
                    
                };
                _db.Member.Add(StrtUser);
                _db.SaveChanges();
                return 1;

            }
            }
            catch (Exception ex)
            {

                throw;
            }
            return 0;
        }
 
        public Member GetUser(long id)
        {
            var r = _db.Member.Where(x => x.id.Equals(id)).FirstOrDefault();
            return _db.Member.Where(x => x.id==id).FirstOrDefault();

        }

        public int   CreateChatRooms(string    Name)
        {
            _db.ChatRoom.Add(new ChatRoom() { Name = Name });
            _db.SaveChanges();
            return 1;

        }
        public List<ChatRoom>    GetAllRoom()
        {

            return _db.ChatRoom.ToList();
            

        }

        public int LoginRoom(int id,int ChatRoom)
        {
            var FindR = _db.Member.Where(x => x.UserId == id).FirstOrDefault();
        
            FindR.ChatRoomId = ChatRoom;
            _db.SaveChanges();
            return 1;
        }

        public List<int?> GetAllMembersChat(int? idChatRoom) {


            return _db.Member.Where(x => x.ChatRoomId == idChatRoom && x.ChatRoomId != null).Select(x => x.ChatRoomId).ToList();
        }

        public int? GetThisMemberChatRoom(int id)
        {


            return _db.Member.Where(x => x.UserId == id && x.ChatRoomId != null).Select(x => x.ChatRoomId).FirstOrDefault();
        }
        public int UpdateLocation(Location Location) {



            return 0;
        }
    }
}