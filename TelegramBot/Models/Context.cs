using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TelegramBot.Models;

namespace GoogleMapBot.Models
{
    public class Context:DbContext
    {
        public DbSet<Member> Member { get; set; }
        public DbSet<ChatRoom> ChatRoom { get; set; }
        //ChatRoom

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>().HasOptional(x => x.ChatRoom).WithMany(x => x.Member).
                HasForeignKey(x => x.ChatRoomId).
                WillCascadeOnDelete();
            base.OnModelCreating(modelBuilder);
        }
    }
}