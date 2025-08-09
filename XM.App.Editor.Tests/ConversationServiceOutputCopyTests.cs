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
        var tempSrc = Path.Combine(Path.GetTempPath(), "conv-tests-src-" + Guid.NewGuid());
        var fileName = "copy_test_" + Guid.NewGuid();
        Directory.CreateDirectory(tempSrc);
        var svc = new ConversationService(tempSrc);
        var data = new ConversationData { Conversation = new ConversationContent { Root = new ConversationPage { Header = "H" } } };

        var srcPath = Path.Combine(tempSrc, fileName + ".json");
        var outputPath = Path.Combine(AppContext.BaseDirectory, "Data", "conversations", fileName + ".json");
        try
        {
            var ok = svc.SaveConversation(fileName, data);
            Assert.True(ok);
            Assert.True(File.Exists(srcPath));
            Assert.True(File.Exists(outputPath));
        }
        finally
        {
            try { if (File.Exists(srcPath)) File.Delete(srcPath); } catch { }
            try { if (File.Exists(outputPath)) File.Delete(outputPath); } catch { }
            try { if (Directory.Exists(tempSrc)) Directory.Delete(tempSrc, true); } catch { }
        }
    }
}


