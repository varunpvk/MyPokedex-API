namespace MyPokedex.Tests.Helper
{
    using Microsoft.Extensions.Configuration;

    public class ConfigBuilder
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            return config;
        }
    }
}
