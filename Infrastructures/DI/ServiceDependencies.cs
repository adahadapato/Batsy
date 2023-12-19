

namespace Batsy.Infrastructures.DI;

using Batsy.Resources.Interfaces;
using Batsy.Resources.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Net.Http;


public static class ServiceDependencies
{
    public static void RegisterServices(this IServiceCollection services,
       IConfiguration configuration)
    {
        //services.AddDbContext<ToDoDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("SimpleTodoApp")));
        //services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton(typeof(CandidateService));
        services.AddSingleton(typeof(CandidateService));
        services.AddScoped<Resources.Interfaces.IResourceService, ResourceService>();  
        services.AddScoped<ITokenContainer, TokenContainer>();

       
    }
}
