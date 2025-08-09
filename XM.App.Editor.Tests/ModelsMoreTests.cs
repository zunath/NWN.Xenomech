using XM.App.Editor.Models;
using Xunit;

namespace XM.App.Editor.Tests;

public class ModelsMoreTests
{
    [Fact]
    public void ConversationAction_InitializeParametersForAllTypes()
    {
        var a = new ConversationAction();

        a.Type = "ExecuteScript";
        Assert.Equal("", a.ScriptId);

        a.Type = "Teleport";
        Assert.Equal("", a.Tag);

        a.Type = "ChangePage";
        Assert.Equal("", a.PageId);

        a.Type = "OpenShop";
        Assert.Equal("", a.ShopId);

        a.Type = "GiveItem";
        Assert.Equal("", a.ItemId);
        Assert.Equal("0", a.Quantity);

        a.Type = "StartQuest";
        Assert.Equal("", a.QuestId);

        a.Type = "EndConversation";
        Assert.Empty(a.Parameters);
        Assert.Equal("End conversation", a.Summary);
    }

    [Fact]
    public void ConversationCondition_DefaultOperators_UnknownType()
    {
        var c = new ConversationCondition();
        c.Type = "Unknown";
        Assert.Contains("Equal", c.AvailableOperators);
        c.Operator = "NotEqual";
        c.StringValue = "x";
        Assert.Equal("Unknown NotEqual x", c.Summary);
    }
}


