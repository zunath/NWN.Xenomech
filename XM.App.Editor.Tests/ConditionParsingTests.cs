using XM.App.Editor.Models;

namespace XM.App.Editor.Tests;

public class ConditionParsingTests
{
    [Fact]
    public void Variable_Format_Typed_Parses_Name_And_Value()
    {
        var c = new ConversationCondition { Type = "Variable" };
        c.Value = "int:Level:3";
        Assert.Equal("Level", c.VariableName);
        Assert.Equal("3", c.VariableExpectedValue);
        Assert.Equal("Variable 'Level' Equal 3", c.Summary);

        c.VariableExpectedValue = string.Empty;
        Assert.Equal("Level", c.VariableName);
        Assert.Equal(string.Empty, c.VariableExpectedValue);
        Assert.Equal("Variable Equal Level (exists)", c.Summary);
    }

    [Fact]
    public void Variable_Format_Untyped_Parses_Two_Parts()
    {
        var c = new ConversationCondition { Type = "Variable" };
        c.Value = "Flag:On";
        Assert.Equal("Flag", c.VariableName);
        Assert.Equal("On", c.VariableExpectedValue);
        Assert.Equal("Variable 'Flag' Equal On", c.Summary);
    }

    [Fact]
    public void NumericValue_Invalid_Ignores_And_Keeps_Default()
    {
        var c = new ConversationCondition { Type = "PlayerLevel", Operator = "GreaterThanOrEqual" };
        c.NumericValue = "abc";
        // Stays at default 1 per type initialization
        Assert.Equal("1", c.NumericValue);
        Assert.Equal("PlayerLevel GreaterThanOrEqual 1", c.Summary);
    }
}


