


namespace Batsy.Infrastructures.DI;

using Batsy.Resources.Interfaces;
using Batsy.Resources.Services;
using Batsy.ViewModels;
using Microsoft.Extensions.DependencyInjection;
public static class ViewModelDependencies
{
    public static void RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<CandidateViewModel>();
        services.AddSingleton<DashbordViewModel>();

        services.AddSingleton<Func<Type, ViewModel>>(servireProvider => 
                                viewmodelType => 
                                (ViewModel)servireProvider.GetRequiredService(viewmodelType));
    }
}
