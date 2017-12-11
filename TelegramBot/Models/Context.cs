using System.Data.Entity;
using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class Context : DbContext
    {
        public DbSet<Member> Member { get; set; }
        public DbSet<ChatRoom> ChatRoom { get; set; }
        public DbSet<Commants> Commants { get; set; }
        //Commants

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<UserDetails>();
            modelBuilder.ComplexType<LocationM>();
            modelBuilder.Entity<Member>().HasOptional(x => x.ChatRoom).WithMany(x => x.Member).
                HasForeignKey(x => x.ChatRoomId).
                WillCascadeOnDelete();
            base.OnModelCreating(modelBuilder);
        }
    }
}