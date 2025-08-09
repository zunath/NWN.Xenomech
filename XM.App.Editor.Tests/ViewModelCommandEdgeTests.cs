using System.Windows.Input;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelCommandEdgeTests
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
    public void MoveAction_CanExecute_Edges_And_PagesCount()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_edges", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            // New conversation creates a default root page
            Assert.Equal(1, vm.PagesCount);

            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddResponseCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First().Children.First();

            // Add three actions
            vm.AddActionCommand.Execute(null);
            vm.AddActionCommand.Execute(null);
            vm.AddActionCommand.Execute(null);

            // Re-fetch latest action references from current list to avoid stale instances after rebuilds
            var a0 = vm.SelectedResponseNode!.Response.Actions[0];
            var a1 = vm.SelectedResponseNode!.Response.Actions[1];
            var a2 = vm.SelectedResponseNode!.Response.Actions[2];

            // CanExecute edges
            Assert.False(((ICommand)vm.MoveActionUpCommand).CanExecute(a0));
            Assert.True(((ICommand)vm.MoveActionUpCommand).CanExecute(a1));
            Assert.True(((ICommand)vm.MoveActionDownCommand).CanExecute(a1));
            // Attempt to move last item down should have no effect (still last)
            vm.MoveActionDown(a2);
            Assert.Same(a2, vm.SelectedResponseNode!.Response.Actions.Last());
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void CanAddNode_States()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_canadd", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            // A default root exists and no selection => adding root not applicable, should still allow add (adds NPC/response depending on state)
            Assert.True(((RelayCommand)vm.AddNodeCommand).CanExecute(null));
            // Select root explicitly
            vm.SelectedNode = vm.ConversationTree.First();
            // On page => true (add response)
            Assert.True(((RelayCommand)vm.AddNodeCommand).CanExecute(null));
            vm.AddNodeCommand.Execute(null); // add response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            // On response without next NPC => true (add next NPC)
            Assert.True(((RelayCommand)vm.AddNodeCommand).CanExecute(null));
            vm.AddNextNpcCommand.Execute(null);
            // Now response has next => false (no further add)
            Assert.False(((RelayCommand)vm.AddNodeCommand).CanExecute(null));
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


