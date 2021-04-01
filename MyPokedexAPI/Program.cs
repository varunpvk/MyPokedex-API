namespace MyPokedexAPI
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                           .ConfigureWebHostDefaults(webBuilder => {
                               webBuilder.UseKestrel();
                               webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                               webBuilder.UseUrls("http://*:5000");
                               webBuilder.UseIISIntegration();
                               webBuilder.UseStartup<Startup>();
                           });
        }
    }
}
