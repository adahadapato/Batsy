


namespace Batsy.Infrastructures.DI;

using Batsy.Resources.Interfaces;
using Batsy.Resources.Services;
using Batsy.ViewModels;
using Microsoft.Extensions.DependencyInjection;
public static class LogicDependencies
{
    public static void RegisterLogicDependencies(this IServiceCollection services)
    {
        //services.AddScoped<ITodoItemsService, TodoItemsService>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<Func<Type, ViewModel>>(servireProvider => 
                                viewmodelType => 
                                (ViewModel)servireProvider.GetRequiredService(viewmodelType));
    }
}
