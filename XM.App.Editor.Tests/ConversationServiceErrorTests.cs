using XM.App.Editor.Services;
using Xunit;

namespace XM.App.Editor.Tests;

public class ConversationServiceErrorTests
{
    [Fact]
    public void SaveConversation_BadDirectory_False()
    {
        // Create a temp file and pass its path as the "directory"; saving should fail across OSes
        var tempFile = Path.GetTempFileName();
        try
        {
            var svc = new ConversationService(tempFile); // using a file path where a directory is expected
            var ok = svc.SaveConversation("file", new XM.App.Editor.Models.ConversationData());
            Assert.False(ok);
        }
        finally
        {
            try { if (File.Exists(tempFile)) File.Delete(tempFile); } catch { }
        }
    }
}


