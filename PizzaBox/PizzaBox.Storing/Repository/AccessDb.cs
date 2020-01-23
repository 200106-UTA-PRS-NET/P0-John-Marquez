using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaBox.Domain.Models;
using System.IO;

namespace PizzaBox.Storing
{


    public sealed class AccessDb // singleton to create single instances for database access throught my account
    {
        private static readonly AccessDb instance = new AccessDb();
        private static DbContextOptions<Entity> options;

        static AccessDb() { }

        private AccessDb() // boilerplate code used to access the databse
        {
            var configurBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Entity>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaDb"));
            options = optionsBuilder.Options;
        }
        public static AccessDb Instance // instance 
        {
            get
            {
                return instance;
            }

        }

        public static Entity acc()
        {
           return new Entity(options);          
        }   
    }

}
    
