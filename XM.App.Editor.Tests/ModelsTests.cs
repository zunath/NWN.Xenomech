using XM.App.Editor.Models;
using Xunit;

namespace XM.App.Editor.Tests;

public class ModelsTests
{
    [Fact]
    public void ConversationAction_ParameterProperties_WorkAndSanitize()
    {
        var a = new ConversationAction { Type = "GiveItem" };
        a.ItemId = "this_is_longer_than_16_chars";
        Assert.True(a.ItemId.Length <= 16);

        a.Quantity = "0005";
        Assert.Equal("5", a.Quantity);

        a.Tag = "BAD TAG!*@ with spaces";
        Assert.DoesNotContain(' ', a.Tag);
        Assert.True(a.Tag.Length <= 32);

        a.Type = "ExecuteScript";
        a.ScriptId = "doit";
        Assert.Equal("Script: doit", a.Summary);
    }

    [Fact]
    public void ConversationCondition_TypeChanges_SetDefaults_And_Summary()
    {
        var c = new ConversationCondition();
        c.Type = "PlayerLevel";
        Assert.Equal("GreaterThanOrEqual", c.Operator);
        c.NumericValue = "3";
        Assert.Equal("PlayerLevel GreaterThanOrEqual 3", c.Summary);

        c.Type = "Variable";
        c.VariableName = "flag";
        c.VariableExpectedValue = "1";
        c.Operator = "Equal";
        Assert.Equal("Variable 'flag' Equal 1", c.Summary);

        c.Type = "PlayerSkill";
        c.SkillType = "Longsword";
        c.SkillLevelNumeric = "5";
        // Operator remains Equal from previous step, per implementation when switching types
        Assert.Equal("PlayerSkill Equal Longsword:5", c.Summary);
    }

    [Fact]
    public void ConversationPage_RaisesIdChange_And_HoldsResponses()
    {
        var page = new ConversationPage { Header = "H" };
        var changed = false;
        page.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(ConversationPage.Id)) changed = true; };
        page.Id = "root";
        Assert.True(changed);
        page.Responses.Add(new ConversationResponse { Text = "Hi" });
        Assert.Single(page.Responses);
    }
}


