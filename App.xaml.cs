

namespace Batsy;

using Batsy.Infrastructures.DI;
using Batsy.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using System.Windows;


/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false, true);

        

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        ServiceProvider = serviceCollection.BuildServiceProvider();
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        //base.OnStartup(e);
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.RegisterInfrastructureDependencies(Configuration);
        serviceCollection.RegisterLogicDependencies();
        serviceCollection.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        }) ;
    }
}
