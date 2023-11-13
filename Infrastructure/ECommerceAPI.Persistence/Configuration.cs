using Microsoft.Extensions.Configuration;


namespace ECommerceAPI.Persistence
{
    static class Configuration
    {
        public static string ConnectionString { get {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("PostgreSQL");

            } }

    }
}
