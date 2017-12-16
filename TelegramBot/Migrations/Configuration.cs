namespace TelegramBot.Migrations
{
    using GoogleMapBot.Models;
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

        
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}