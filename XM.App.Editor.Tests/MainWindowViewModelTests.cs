using XM.App.Editor.Services;

namespace XM.App.Editor.Tests;

public class MainWindowViewModelTests
{
    private sealed class FakeSettingsService : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    [Fact]
    public void OpenConversationEditor_Toggles_Visibility_And_ViewModel()
    {
        var vm = new MainWindowViewModel(new FakeSettingsService());
        Assert.False(vm.IsConversationEditorVisible);
        Assert.Null(vm.ConversationEditorViewModel);

        vm.OpenConversationEditor();
        Assert.True(vm.IsConversationEditorVisible);
        Assert.NotNull(vm.ConversationEditorViewModel);

        vm.OpenConversationEditor();
        Assert.False(vm.IsConversationEditorVisible);
        Assert.Null(vm.ConversationEditorViewModel);
    }

    [Fact]
    public void Commands_Invoke_Without_Exception()
    {
        var vm = new MainWindowViewModel(new FakeSettingsService());
        vm.About();
        vm.Initialize();
        vm.Exit();
    }

    [Fact]
    public void InverseBooleanConverter_Works_BothWays()
    {
        var c = new InverseBooleanConverter();
        Assert.Equal(true, c.Convert(false, typeof(bool), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, c.Convert(true, typeof(bool), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(true, c.ConvertBack(false, typeof(bool), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(false, c.ConvertBack(true, typeof(bool), null, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void BooleanToIconAndTooltipConverters_Work()
    {
        Assert.Equal("💬", BooleanToIconConverter.Instance.Convert(null, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("❌", BooleanToIconConverter.Instance.Convert(true, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("💬", BooleanToIconConverter.Instance.Convert(false, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));

        Assert.Equal("Conversation Editor", BooleanToTooltipConverter.Instance.Convert(null, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("Hide Conversation Editor", BooleanToTooltipConverter.Instance.Convert(true, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal("Show Conversation Editor", BooleanToTooltipConverter.Instance.Convert(false, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void RelayCommand_Executes_And_CanExecute()
    {
        var invoked = 0;
        var can = false;
        var cmd = new RelayCommand(() => invoked++, () => can);
        Assert.False(cmd.CanExecute(null));
        can = true;
        Assert.True(cmd.CanExecute(null));
        cmd.Execute(null);
        Assert.Equal(1, invoked);
    }
}


