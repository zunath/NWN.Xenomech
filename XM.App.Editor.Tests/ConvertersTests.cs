using System.Collections.ObjectModel;
using XM.App.Editor.Converters;
using XM.App.Editor.Models;

namespace XM.App.Editor.Tests;

public class ConvertersTests
{
    [Fact]
    public void ActionSummaryConverter_ReturnsExpected_ForEachType()
    {
        var c = ActionSummaryConverter.Instance;

        var a1 = new ConversationAction { Type = "ExecuteScript" }; a1.ScriptId = "scr";
        Assert.Equal("Script: scr", c.Convert(a1, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a2 = new ConversationAction { Type = "Teleport" }; a2.Tag = "TAG_1";
        Assert.Equal("Tag: TAG_1", c.Convert(a2, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a3 = new ConversationAction { Type = "ChangePage" }; a3.PageId = "P1";
        Assert.Equal("Page: P1", c.Convert(a3, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a4 = new ConversationAction { Type = "OpenShop" }; a4.ShopId = "ShopX";
        Assert.Equal("Shop: ShopX", c.Convert(a4, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a5 = new ConversationAction { Type = "GiveItem" }; a5.ItemId = "itm"; a5.Quantity = "3";
        Assert.Equal("Resref: itm x3", c.Convert(a5, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a6 = new ConversationAction { Type = "StartQuest" }; a6.QuestId = "Q1";
        Assert.Equal("Quest: Q1", c.Convert(a6, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a7 = new ConversationAction { Type = "EndConversation" };
        Assert.Equal("End conversation", c.Convert(a7, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var a8 = new ConversationAction { Type = "Unknown" };
        Assert.Equal("Unknown Action", c.Convert(a8, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void ActionTypeToVisibilityConverter_Works()
    {
        var c = ActionTypeToVisibilityConverter.Instance;
        Assert.Equal(true, c.Convert("OpenShop", typeof(bool), "OpenShop", System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, c.Convert("OpenShop", typeof(bool), "ChangePage", System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, c.Convert(123, typeof(bool), "OpenShop", System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void StringEqualsConverter_Works()
    {
        var c = StringEqualsConverter.Instance;
        Assert.Equal(true, c.Convert("abc", typeof(bool), "abc", System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, c.Convert("abc", typeof(bool), "def", System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void NotNullToVisibilityConverter_Works()
    {
        var c = NotNullToVisibilityConverter.Instance;
        Assert.Equal(true, c.Convert(new object(), typeof(bool), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, c.Convert(null, typeof(bool), null, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void StringLengthConverter_UsesDefaultAndParameter()
    {
        var c = new StringLengthConverter();
        Assert.Equal("0/16 characters", c.Convert(null, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("5/16 characters", c.Convert("hello", typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("5/10 characters", c.Convert("hello", typeof(string), "10", System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void DictionaryToJsonConverter_SerializeDeserialize_Robust()
    {
        var c = DictionaryToJsonConverter.Instance;
        var dict = new Dictionary<string, object> { { "a", 1 }, { "b", "x" } };
        var json = c.Convert(dict, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture) as string;
        Assert.Contains("\"a\"", json);
        var back = (Dictionary<string, object>)c.ConvertBack(json, typeof(Dictionary<string, object>), null, System.Globalization.CultureInfo.InvariantCulture)!;
        Assert.Equal("1", back["a"].ToString());
    }

    [Fact]
    public void CanMoveUpDownConverters_Work()
    {
        var a1 = new ConversationAction { Type = "OpenShop" };
        var a2 = new ConversationAction { Type = "OpenShop" };
        var a3 = new ConversationAction { Type = "OpenShop" };
        var list = new ObservableCollection<ConversationAction> { a1, a2, a3 };

        var up = CanMoveUpConverter.Instance;
        var down = CanMoveDownConverter.Instance;

        Assert.Equal(false, up.Convert(a1, typeof(bool), list, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(true, up.Convert(a2, typeof(bool), list, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(true, down.Convert(a2, typeof(bool), list, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, down.Convert(a3, typeof(bool), list, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void NodeConverters_Work()
    {
        var pageNode = new ConversationPageNode();
        var respNode = new ConversationResponseNode();

        Assert.Equal("Page", NodeTypeConverter.Instance.Convert(pageNode, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("Response", NodeTypeConverter.Instance.Convert(respNode, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("Unknown", NodeTypeConverter.Instance.Convert(123, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        Assert.Equal("🗣️", NodeIconConverter.Instance.Convert(pageNode, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("👤", NodeIconConverter.Instance.Convert(respNode, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        // FontWeight type comes from Avalonia; compare via ToString to avoid adding framework refs
        Assert.Equal("Bold", NodeFontWeightConverter.Instance.Convert(pageNode, typeof(object), null, System.Globalization.CultureInfo.InvariantCulture)!.ToString());
        Assert.Equal("Normal", NodeFontWeightConverter.Instance.Convert(respNode, typeof(object), null, System.Globalization.CultureInfo.InvariantCulture)!.ToString());
    }

    [Fact]
    public void PageIdConverter_ConvertAndBack()
    {
        var node = new ConversationPageNode { Page = new ConversationPage(), PageId = "R" };
        var c = PageIdConverter.Instance;
        Assert.Equal("R", c.Convert(node, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        var back = c.ConvertBack("R2", typeof(string), node, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal("R2", node.PageId);
        Assert.Equal("R2", back);
    }
}


