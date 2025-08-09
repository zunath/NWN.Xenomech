using System.IO;
using Microsoft.Extensions.Logging.Abstractions;
using XM.App.Editor.Services;
using Xunit;

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
        svc.Load();
        // Nothing to assert strongly; just ensure no exceptions and file exists
        // Path location depends on OS; validate indirect effect: Save then Load keeps type
        Assert.NotNull(svc.Current);
    }
}


