using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;

namespace XM.App.Editor.Tests;

public class ViewModelMoreTests
{
    private class Confirm : IConfirmationService
    {
        private readonly bool _result;
        public Confirm(bool result) { _result = result; }
        public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(_result);
    }

    private class Users : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    [Fact]
    public void DeleteAction_RespectsConfirmation()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm2", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(false));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddActionCommand.Execute(null);
            var count = vm.SelectedResponseNode!.Response.Actions.Count;
            vm.DeleteActionCommand.Execute(null); // should not delete when confirm=false
            Assert.Equal(count, vm.SelectedResponseNode!.Response.Actions.Count);

            // Now confirm=true
            vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddActionCommand.Execute(null);
            vm.DeleteActionCommand.Execute(null);
            Assert.Empty(vm.SelectedResponseNode!.Response.Actions);
        }
        finally
        {
            try { if (Directory.Exists(temp)) Directory.Delete(temp, true); } catch { }
        }
    }

    [Fact]
    public void DeleteCondition_RespectsConfirmation()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm3", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(false));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddConditionCommand.Execute(null);
            var count = vm.SelectedResponseNode!.Response.Conditions.Count;
            vm.DeleteConditionCommand.Execute(null); // no delete
            Assert.Equal(count, vm.SelectedResponseNode!.Response.Conditions.Count);

            vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddConditionCommand.Execute(null);
            vm.DeleteConditionCommand.Execute(null);
            Assert.Empty(vm.SelectedResponseNode!.Response.Conditions);
        }
        finally
        {
            try { if (Directory.Exists(temp)) Directory.Delete(temp, true); } catch { }
        }
    }

    [Fact]
    public void SelectedChangePageItem_NormalizesCase()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm4", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddActionCommand.Execute(null);

            // Prepare ChangePage action referencing root id in wrong case
            vm.SelectedAction!.Type = "ChangePage";
            ((ConversationPageNode)vm.ConversationTree.First()).Page.Id = "Root";
            vm.SelectedAction.PageId = "root";

            // Trigger refresh by re-selecting action
            vm.SelectedAction = vm.SelectedAction;
            // Force refresh of AvailablePageIds normalization by toggling SelectedAction
            var act = vm.SelectedAction;
            vm.SelectedAction = null;
            vm.SelectedAction = act;
            // SelectedChangePageItem returns exact match from AvailablePageIds if present
            Assert.Equal("Root", vm.SelectedChangePageItem);
            Assert.Equal("Root", vm.SelectedAction.PageId);

            // Clear next NPC then delete
            vm.AddNextNpcCommand.Execute(null);
            Assert.NotNull(vm.SelectedResponseNode!.Response.Next);
            vm.DeleteNextNpcCommand.Execute(null);
            Assert.Null(vm.SelectedResponseNode!.Response.Next);
        }
        finally
        {
            try { if (Directory.Exists(temp)) Directory.Delete(temp, true); } catch { }
        }
    }
}


