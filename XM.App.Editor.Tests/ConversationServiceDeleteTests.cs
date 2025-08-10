using XM.App.Editor.Models;
using XM.App.Editor.Services;

namespace XM.App.Editor.Tests;

public class ConversationServiceDeleteTests
{
    [Fact]
    public void DeleteConversation_Removes_Both_Source_And_Output()
    {
        var tempSrc = Path.Combine(Path.GetTempPath(), "conv-tests-del-" + Guid.NewGuid());
        Directory.CreateDirectory(tempSrc);
        var svc = new ConversationService(tempSrc);

        var data = new ConversationData { Conversation = new ConversationContent { Root = new ConversationPage { Header = "H" } } };
        var fileName = "delete_test_" + Guid.NewGuid();
        var srcPath = Path.Combine(tempSrc, fileName + ".json");
        var outPath = Path.Combine(AppContext.BaseDirectory, "Data", "conversations", fileName + ".json");
        try
        {
            Assert.True(svc.SaveConversation(fileName, data));
            Assert.True(File.Exists(srcPath));
            Assert.True(File.Exists(outPath));

            Assert.True(svc.DeleteConversation(fileName));
            Assert.False(File.Exists(srcPath));
            Assert.False(File.Exists(outPath));
        }
        finally
        {
            try { if (File.Exists(srcPath)) File.Delete(srcPath); } catch { }
            try { if (File.Exists(outPath)) File.Delete(outPath); } catch { }
            try { if (Directory.Exists(tempSrc)) Directory.Delete(tempSrc, true); } catch { }
        }
    }
}


