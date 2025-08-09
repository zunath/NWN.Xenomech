using XM.App.Editor.Services;
using Xunit;

namespace XM.App.Editor.Tests;

public class ConversationServiceErrorTests
{
    [Fact]
    public void SaveConversation_BadDirectory_False()
    {
        // Use an invalid path character to force an error on Windows
        var badDir = "*:/not/a/path";
        var svc = new ConversationService(badDir);
        var ok = svc.SaveConversation("file", new XM.App.Editor.Models.ConversationData());
        Assert.False(ok);
    }
}


