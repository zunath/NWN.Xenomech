using System.Collections.ObjectModel;
using XM.App.Editor.Models;
using Xunit;

namespace XM.App.Editor.Tests;

public class ModelsExhaustiveTests
{
    [Fact]
    public void ConversationAction_AllTypes_Parameters_And_Summary()
    {
        var a = new ConversationAction();

        a.Type = "ExecuteScript";
        Assert.Equal("", a.ScriptId);
        a.ScriptId = "scr";
        Assert.Contains("Script: scr", a.Summary);

        a.Type = "Teleport";
        Assert.Equal("", a.Tag);
        a.Tag = "Bad Tag*! ";
        Assert.DoesNotContain(' ', a.Tag);

        a.Type = "ChangePage";
        Assert.Equal("", a.PageId);
        a.PageId = "Page1";
        Assert.Contains("Page: Page1", a.Summary);

        a.Type = "OpenShop";
        Assert.Equal("", a.ShopId);
        a.ShopId = "ShopX";
        Assert.Contains("Shop: ShopX", a.Summary);

        a.Type = "GiveItem";
        Assert.Equal("", a.ItemId);
        Assert.Equal("0", a.Quantity);
        a.ItemId = "verylongresref_name_exceeds16";
        Assert.True(a.ItemId.Length <= 16);
        a.Quantity = "0005";
        Assert.Equal("5", a.Quantity);
        Assert.Contains("Resref: ", a.Summary);

        a.Type = "StartQuest";
        Assert.Equal("", a.QuestId);
        a.QuestId = "Q1";
        Assert.Contains("Quest: Q1", a.Summary);

        a.Type = "EndConversation";
        Assert.Empty(a.Parameters);
        Assert.Equal("End conversation", a.Summary);
    }

    [Fact]
    public void ConversationCondition_TypeTransitions_And_Values()
    {
        var c = new ConversationCondition();

        c.Type = "PlayerLevel";
        Assert.Equal("GreaterThanOrEqual", c.Operator);
        c.NumericValue = "10";
        Assert.Equal("10", c.NumericValue);
        Assert.Contains("PlayerLevel", c.Summary);

        c.Type = "Variable";
        Assert.Equal("Equal", c.Operator);
        c.VariableName = "flag";
        c.VariableExpectedValue = "on";
        Assert.Contains("Variable 'flag' Equal on", c.Summary);

        c.Type = "PlayerSkill";
        // Operator might remain from previous type if already set; ensure it's valid for PlayerSkill
        Assert.Contains(c.Operator, c.AvailableOperators);
        c.SkillType = "Longsword";
        c.SkillLevelNumeric = "3";
        Assert.Contains("Longsword:3", c.Summary);

        c.Type = "UnknownType";
        Assert.Contains("Equal", c.AvailableOperators);
        c.Operator = "NotEqual";
        c.StringValue = "abc";
        Assert.Equal("UnknownType NotEqual abc", c.Summary);
    }

    [Fact]
    public void ConversationPage_And_Response_NodeHelpers()
    {
        var page = new ConversationPage { Header = "NPC" };
        page.Id = "root";
        page.Responses = new List<ConversationResponse>
        {
            new ConversationResponse { Text = "Hello" },
            new ConversationResponse { Text = "Bye" }
        };

        var pnode = new ConversationPageNode { Page = page, PageId = "root", Name = "Page" };
        Assert.Equal("NPC", pnode.NpcText);

        var rnode = new ConversationResponseNode { Response = page.Responses[0], Name = "Resp" };
        Assert.Equal("Hello", rnode.PlayerText);
    }
}


