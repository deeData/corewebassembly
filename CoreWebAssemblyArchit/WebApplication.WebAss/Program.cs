using App.Repository.webApi;
using App.Repository.webApi.ApiClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyApp.ApplicationLogic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.WebAss
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //host is built and runs inside the browser (note there is no startup.cs file)
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //root component is App.razor
            builder.RootComponents.Add<App>("#app");

            //need to add libraries for dependency injection
            //transient creates a new instance each time
            builder.Services.AddTransient<IProjectsScreenUseCases, ProjectsScreenUseCases>();
            //above class is dependent on the repository library
            builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
            //above class is dependent on this library-- webApiExecuter should be a singleton with httpClient, need to change to diff signature
            builder.Services.AddSingleton<IWebApiExecuter, WebApiExecuter>(sp => new WebApiExecuter(
                    "https://localhost:44349", new HttpClient()));

            //in webassembly, scope is also singleton
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
