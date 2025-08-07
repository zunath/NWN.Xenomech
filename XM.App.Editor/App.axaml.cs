using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XM.App.Editor.Services;

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
                    services.AddSingleton<IUserSettingsService, UserSettingsService>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();

            // Load user settings early
            var settingsService = _host.Services.GetRequiredService<IUserSettingsService>();
            settingsService.Load();

            desktop.MainWindow = _host.Services.GetRequiredService<MainWindow>();

            // Ensure settings are saved on shutdown
            desktop.ShutdownRequested += (_, _) =>
            {
                settingsService.Save();
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
} 