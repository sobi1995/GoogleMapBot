namespace TelegramBot.Migrations
{
    using System.Data.Entity.Migrations;

    public class Configuration : DbMigrationsConfiguration<GoogleMapBot.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(GoogleMapBot.Models.Context context)
        {
            //Context db = new Context();
            //db.ChatRoom.Add(new Models.ChatRoom() {Name="Iran",Discraption="Chat Room Iran"});
            //db.ChatRoom.Add(new Models.ChatRoom() { Name = "Programer", Discraption = "Chat Room Programing" });
            //db.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}