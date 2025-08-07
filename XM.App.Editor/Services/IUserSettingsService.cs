namespace XM.App.Editor.Services;

public interface IUserSettingsService
{
    UserSettings Current { get; }

    void Load();

    void Save();
}


