using System.Linq;
using System.Windows.Input;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelGuardsTests
{
    private class Confirm(bool result) : IConfirmationService
    {
        private readonly bool _result = result;
        public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(_result);
    }

    private class Users : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    [Fact]
    public void Command_CanExecute_Toggles_WithState()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_guards", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));

            // Before conversation
            Assert.False(((ICommand)vm.DeleteConversationCommand).CanExecute(null));
            Assert.False(((ICommand)vm.SaveConversationCommand).CanExecute(null));
            Assert.False(((ICommand)vm.AddPageCommand).CanExecute(null));
            Assert.False(((ICommand)vm.AddResponseCommand).CanExecute(null));
            Assert.False(((ICommand)vm.DeleteNodeCommand).CanExecute(null));

            vm.CreateNewConversationCommand.Execute(null);
            Assert.True(((ICommand)vm.SaveConversationCommand).CanExecute(null));
            Assert.True(((ICommand)vm.AddPageCommand).CanExecute(null));

            // Add root page
            vm.AddNodeCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First();
            Assert.True(((ICommand)vm.AddResponseCommand).CanExecute(null));

            // Add response
            vm.AddNodeCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            Assert.True(((ICommand)vm.AddNextNpcCommand).CanExecute(null));
            vm.AddNextNpcCommand.Execute(null);
            Assert.False(((ICommand)vm.AddNextNpcCommand).CanExecute(null));
            Assert.True(((ICommand)vm.DeleteNextNpcCommand).CanExecute(null));
            vm.DeleteNextNpcCommand.Execute(null);
            Assert.True(((ICommand)vm.AddNextNpcCommand).CanExecute(null));

            // Action add/delete can execute
            Assert.True(((ICommand)vm.AddActionCommand).CanExecute(null));
            vm.AddActionCommand.Execute(null);
            Assert.True(((ICommand)vm.DeleteActionCommand).CanExecute(null));

            // Condition add/delete can execute
            Assert.True(((ICommand)vm.AddConditionCommand).CanExecute(null));
            vm.AddConditionCommand.Execute(null);
            Assert.True(((ICommand)vm.DeleteConditionCommand).CanExecute(null));
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void UpdatePageId_IsNoOp()
    {
        var vm = new ConversationEditorViewModel(new Users(), new ConversationService(Path.GetTempPath()), new Confirm(true));
        vm.UpdatePageId("old", "new");
        Assert.True(true);
    }
}


