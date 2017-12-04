using System;
using System.Collections.Generic;
using System.Linq;
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
        public int AddWhenStart(Member Ueser)
        {
            try
            {
                if (!_db.Member.Any(x => x.UserId == Ueser.UserId))
                {
                    Member StrtUser = new Member()
                    {
                        UserId = Ueser.UserId,
                        FirstName = Ueser.FirstName,
                        Username = Ueser.Username,
                        LastName = Ueser.LastName,
                        Role = 0,
                        Location = new LocationM() { X = 0, Y = 0 },
                        Instructions = 0
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
            return _db.Member.Where(x => x.id == id).FirstOrDefault();
        }
        public int CreateChatRooms(int Userid)
        {

            Member usercreatechatRoom = _db.Member.Where(x => x.UserId == Userid).FirstOrDefault();

            _db.ChatRoom.Add(new ChatRoom() { Location = usercreatechatRoom.Location, Discraption = "asdas" });
            _db.SaveChanges();
            return 1;


        }
        public List<ChatRoom> GetAllRoom()
        {
            return _db.ChatRoom.ToList();
        }
        public int LoginRoom(int id, int ChatRoom)
        {
            var FindR = _db.Member.Where(x => x.UserId == id).FirstOrDefault();

            FindR.ChatRoomId = ChatRoom;
            _db.SaveChanges();
            return 1;
        }
        public List<int?> GetAllMembersChat(int? idChatRoom)
        {
            return _db.Member.Where(x => x.ChatRoomId == idChatRoom && x.ChatRoomId != null).Select(x => x.ChatRoomId).ToList();
        }
        public int? GetThisMemberChatRoom(int id)
        {
            return _db.Member.Where(x => x.UserId == id && x.ChatRoomId != null).Select(x => x.ChatRoomId).FirstOrDefault();
        }
        public int UpdateLocation(LocationM Location, int UserId)
        {
            string s1 = Location.X.ToString();
            string s2 = Location.Y.ToString();
            Decimal Latitude = Convert.ToDecimal(s1);
            Decimal Longitude = Convert.ToDecimal(s2);

            var userIdUbdatelocation = _db.Member.Where(x => x.UserId == UserId).FirstOrDefault();
            userIdUbdatelocation.Location = new LocationM() { X = Location.X.LocationDecimals(), Y = Location.Y.LocationDecimals() };
            userIdUbdatelocation.Adrress = GeoCodeCalc.GetAddressFromLatLon(Latitude, Longitude);
            _db.SaveChanges();
            SetCurrentInstructionsUser(UserId, Selectoption.Mnu);
            return 0;
        }
        public void LoginChatRoom(int UserId, int IdChatRooms)
        {

            var FindUser = _db.Member.Where(X => X.UserId == UserId).FirstOrDefault();
            FindUser.ChatRoomId = IdChatRooms;
            _db.SaveChanges();



        }
        public int SearchByNeartsRoom(int userid)
        {
            var user = _db.Member.Where(x => x.UserId == userid).FirstOrDefault();
            var ChatRooms = _db.ChatRoom.ToList();

            int result = 0;

            foreach (var item in ChatRooms)
            {
                int Distanes = (int)Math.Round(GeoCodeCalc.CalcDistance(user.Location, item.Location, GeoCodeCalcMeasurement.Kilometers));

                if (Distanes <= 10 && Distanes >= 0)
                {
                    result = item.id;
                    break;
                }
            }




            return result;
        }
        public Selectoption GetCurrentInstructionsUser(int userid)
        {


            return _db.Member.Where(x => x.UserId.Equals(userid)).Select(x => x.Instructions).FirstOrDefault();
        }
        public int SetCurrentInstructionsUser(int userid, Selectoption CurrentInstructionsUser)
        {
            var user = _db.Member.Where(x => x.UserId.Equals(userid)).FirstOrDefault();
            user.Instructions = CurrentInstructionsUser;
            _db.SaveChanges();
            return 1;


        }
        public int? GetCahtRoomidUser(int id)
        {

            return _db.Member.Where(x => x.UserId == id).Select(x => x.ChatRoomId).SingleOrDefault();
        }
        public List<int> GetUserOnCharRoom(int? IdChatrooms)
        {



            return _db.Member.Where(x => x.ChatRoomId == IdChatrooms).Select(x => x.UserId).ToList();

        }
        public void LogOutChatRoom(int UserId) {

            var UserLogOutChatRoom = _db.Member.Where(x => x.UserId == UserId).FirstOrDefault();
            UserLogOutChatRoom.ChatRoomId = null;
            _db.SaveChanges();

        }
        public string GetFirstnameId(int UserId) {

            return _db.Member.Where(x => x.UserId == UserId).Select(x => x.FirstName).SingleOrDefault();

        }
    }
}