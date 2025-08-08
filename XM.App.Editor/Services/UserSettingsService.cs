using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace XM.App.Editor.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly ILogger<UserSettingsService> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true
    };

    private readonly string _settingsDirectory;
    private readonly string _settingsFilePath;

    public UserSettings Current { get; private set; } = new();

    public UserSettingsService(ILogger<UserSettingsService> logger)
    {
        _logger = logger;
        _settingsDirectory = GetAppDataDirectory();
        _settingsFilePath = Path.Combine(_settingsDirectory, "settings.json");
    }

    public void Load()
    {
        try
        {
            Directory.CreateDirectory(_settingsDirectory);

            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath);
                var loaded = JsonSerializer.Deserialize<UserSettings>(json, _jsonOptions);
                if (loaded != null)
                {
                    Current = loaded;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load user settings from {SettingsFile}. Using defaults.", _settingsFilePath);
            Current = new UserSettings();
        }
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(_settingsDirectory);
            var json = JsonSerializer.Serialize(Current, _jsonOptions);
            File.WriteAllText(_settingsFilePath, json);
        }
        catch (Exception ex)
        {
            // Log but do not crash the editor due to settings persistence failures
            _logger.LogError(ex, "Failed to save user settings to {SettingsFile}.", _settingsFilePath);
        }
    }

    private static string GetAppDataDirectory()
    {
        string appFolderName = "XM.App.Editor";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, appFolderName);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // ~/Library/Application Support/XM.App.Editor
            var home = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(home, appFolderName);
        }

        // Linux and others: use XDG base dir spec if set, else ~/.config
        var xdg = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
        if (!string.IsNullOrWhiteSpace(xdg))
        {
            return Path.Combine(xdg, appFolderName);
        }

        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(userProfile, ".config", appFolderName);
    }
}


