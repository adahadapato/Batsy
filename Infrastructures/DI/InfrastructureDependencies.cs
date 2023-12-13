

namespace Batsy.Infrastructures.DI;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public static class InfrastructureDependencies
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection services,
       IConfiguration configuration)
    {
        //services.AddDbContext<ToDoDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("SimpleTodoApp")));
        //services.AddScoped<ITodoRepository, TodoRepository>();
    }
}
