using XM.App.Editor.Services;

namespace XM.App.Editor.Tests;

public class ConversationServiceEdgeTests
{
    [Fact]
    public void GetConversationFiles_EmptyDirectory_ReturnsEmpty()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_empty", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var svc = new ConversationService(temp);
            var files = svc.GetConversationFiles();
            Assert.Empty(files);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void LoadConversation_FileMissing_ReturnsNull()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_missing", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var svc = new ConversationService(temp);
            var data = svc.LoadConversation("does_not_exist");
            Assert.Null(data);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


