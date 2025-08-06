using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace XM.App.Editor;

public partial class App : Application
{
    private IHost? _host;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Configure dependency injection
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register services
                    services.AddLogging();
                    services.AddSingleton<MainWindow>();
                })
                .Build();

            desktop.MainWindow = _host.Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
} 