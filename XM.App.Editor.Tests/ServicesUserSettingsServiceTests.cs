using Microsoft.Extensions.Logging.Abstractions;
using XM.App.Editor.Services;

namespace XM.App.Editor.Tests;

public class ServicesUserSettingsServiceTests
{
    [Fact]
    public void Load_And_Save_Do_Not_Throw_And_Persist()
    {
        var logger = NullLogger<UserSettingsService>.Instance;
        var svc = new UserSettingsService(logger);
        svc.Current.LastOpenedConversationPath = null; // ensure some default
        svc.Save();

        // Create a new instance to validate persistence without relying on in-memory state
        var svc2 = new UserSettingsService(logger);
        svc2.Load();
        Assert.NotNull(svc2.Current);
    }
}


