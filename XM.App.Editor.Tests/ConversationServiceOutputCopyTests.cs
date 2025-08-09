using System.IO;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using Xunit;

namespace XM.App.Editor.Tests;

public class ConversationServiceOutputCopyTests
{
    [Fact]
    public void SaveConversation_Creates_Output_Copy_When_Different_Dir()
    {
        var tempSrc = Path.Combine(Path.GetTempPath(), "conv-tests-src");
        var svc = new ConversationService(tempSrc);
        var data = new ConversationData { Conversation = new ConversationContent { Root = new ConversationPage { Header = "H" } } };
        var fileName = "copy_test";

        var ok = svc.SaveConversation(fileName, data);
        Assert.True(ok);

        var outputPath = Path.Combine(AppContext.BaseDirectory, "Data", "conversations", fileName + ".json");
        Assert.True(File.Exists(outputPath));

        // Cleanup
        try { File.Delete(Path.Combine(tempSrc, fileName + ".json")); } catch { }
        try { File.Delete(outputPath); } catch { }
    }
}


