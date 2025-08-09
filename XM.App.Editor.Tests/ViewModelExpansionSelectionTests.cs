using System.Collections.ObjectModel;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelExpansionSelectionTests
{
    private sealed class FakeSettingsService : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    private static ConversationEditorViewModel CreateVm()
    {
        var settings = new FakeSettingsService();
        var confirm = new TestConfirm();
        var svc = new ConversationService(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "vm-tests-2"));
        var vm = new ConversationEditorViewModel(settings, svc, confirm);
        vm.CurrentConversation = BuildDeep();
        return vm;
    }

    private sealed class TestConfirm : IConfirmationService
    {
        public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(true);
    }

    private static ConversationData BuildDeep()
    {
        var root = new ConversationPage { Id = "A", Header = "A", Responses = new() };
        var r1 = new ConversationResponse { Text = "1", Actions = new ObservableCollection<ConversationAction>(), Conditions = new ObservableCollection<ConversationCondition>() };
        r1.Next = new ConversationPage { Id = "B", Header = "B", Responses = new() };
        r1.Next.Responses.Add(new ConversationResponse { Text = "1.1" });
        root.Responses.Add(r1);
        root.Responses.Add(new ConversationResponse { Text = "2" });
        return new ConversationData { Conversation = new ConversationContent { Root = root } };
    }

    [Fact]
    public void Restore_Expansion_And_Selection_On_Rebuild()
    {
        var vm = CreateVm();
        var root = vm.ConversationTree[0];
        root.IsExpanded = true;
        var respNode = root.Children[0];
        respNode.IsSelected = true;
        vm.SelectedNode = respNode;

        // Trigger a rebuild by adding an action and then removing it
        vm.AddActionCommand.Execute(null);
        vm.DeleteActionCommand.Execute(null);

        // After rebuild, ensure expansion on root and selection restored to same path
        Assert.True(vm.ConversationTree[0].IsExpanded);
        Assert.NotNull(vm.SelectedNode);
        Assert.Equal("P:root/R:0", vm.SelectedNode!.NodePath);
    }
}


