using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((context, options) =>
                //{
                //    string environmentName = context.HostingEnvironment.EnvironmentName;

                //    options.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //    options.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);                    
                //    options.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);

                //    // https://github.com/mcrio/Configuration.Provider.Docker.Secrets
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
