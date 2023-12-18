

namespace Batsy;

using Batsy.Infrastructures.DI;
using Batsy.Resources.Services;
using Batsy.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;


/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static string _examYear;
    private static string _examType;
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
        base.OnStartup(e);
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.RegisterViewModels();
        serviceCollection.RegisterServices(Configuration);
        serviceCollection.AddSingleton(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        }) ;

        serviceCollection.AddHttpClient<CandidateService>((httpClient) =>
        {
            httpClient.BaseAddress = new Uri("http://10.0.1.31:9300/api/");
        })
          .ConfigurePrimaryHttpMessageHandler(() =>
           {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                };
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }

    public static string ExamYear
    { 
        get => _examYear; 
        set => _examYear = value;
    }
    public static string ExamType { 
        get => _examType; 
        set => _examType = value; 
    }
}
