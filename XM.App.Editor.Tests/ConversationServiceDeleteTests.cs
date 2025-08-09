using System.IO;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using Xunit;

namespace XM.App.Editor.Tests;

public class ConversationServiceDeleteTests
{
    [Fact]
    public void DeleteConversation_Removes_Both_Source_And_Output()
    {
        var tempSrc = Path.Combine(Path.GetTempPath(), "conv-tests-del");
        var svc = new ConversationService(tempSrc);
        Directory.CreateDirectory(tempSrc);
        var data = new ConversationData { Conversation = new ConversationContent { Root = new ConversationPage { Header = "H" } } };
        var fileName = "delete_test";
        Assert.True(svc.SaveConversation(fileName, data));
        var srcPath = Path.Combine(tempSrc, fileName + ".json");
        var outPath = Path.Combine(AppContext.BaseDirectory, "Data", "conversations", fileName + ".json");
        Assert.True(File.Exists(srcPath));
        Assert.True(File.Exists(outPath));

        Assert.True(svc.DeleteConversation(fileName));
        Assert.False(File.Exists(srcPath));
        Assert.False(File.Exists(outPath));
    }
}


