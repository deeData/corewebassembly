using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp
{
    public class Program
    {
        //console application
        public static void Main(string[] args)
        {
            //runs in an infinite loop
            CreateHostBuilder(args).Build().Run();
        }

        //confiure host and middleware
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //middleware configs
                    webBuilder.UseStartup<Startup>();
                });
    }
}
