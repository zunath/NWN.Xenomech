using System.IO;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using Xunit;

namespace XM.App.Editor.Tests;

public class ServicesTests
{
    [Fact]
    public void ConversationService_CreateLoadSaveDelete_RoundTrip()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var svc = new ConversationService(temp);
            var data = svc.CreateNewConversation("id1", "Name1", "Desc");
            Assert.True(svc.SaveConversation("file1", data));

            var files = svc.GetConversationFiles();
            Assert.Contains("file1", files);

            var loaded = svc.LoadConversation("file1");
            Assert.NotNull(loaded);
            Assert.Equal("Name1", loaded!.Metadata.Name);

            Assert.True(svc.DeleteConversation("file1"));
            Assert.DoesNotContain("file1", svc.GetConversationFiles());
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void UserSettingsService_LoadsAndSaves_NoThrow()
    {
        // Use a separate directory via environment variable redirection of AppContext.BaseDirectory if needed.
        // The service writes to per-user app data; basic invocation paths tested here.
        var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<UserSettingsService>();
        var svc = new UserSettingsService(logger);
        svc.Load();
        svc.Current.LastOpenedConversationPath = "abc";
        svc.Save();
        Assert.True(true);
    }
}


