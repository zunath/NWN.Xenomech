using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;

namespace XM.App.Editor.Tests;

public class ViewModelCommandTests
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
    public void AddNodeButtonText_CoversAllStates()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_cmd", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            // No conversation loaded
            Assert.Equal("Add", vm.AddNodeButtonText);

            vm.CreateNewConversationCommand.Execute(null);
            // Root exists, but nothing selected
            Assert.Equal("Add NPC", vm.AddNodeButtonText);
            vm.SelectedNode = vm.ConversationTree.First(); // select root page
            Assert.Equal("Add Response", vm.AddNodeButtonText);
            vm.AddNodeCommand.Execute(null); // add response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            Assert.Equal("Add NPC", vm.AddNodeButtonText);
            vm.AddNextNpcCommand.Execute(null);
            Assert.Equal("Add", vm.AddNodeButtonText);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void DeleteNode_DeletesResponseAndPage()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_cmd2", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // response
            // Delete response
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.DeleteNodeCommand.Execute(null);
            Assert.Empty(vm.ConversationTree.First().Children);

            // Add response + next page, then delete child page
            vm.AddNodeCommand.Execute(null); // add response again
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddNextNpcCommand.Execute(null);
            // Select the child page and delete
            vm.SelectedNode = vm.ConversationTree.First().Children.First().Children.First();
            vm.DeleteNodeCommand.Execute(null);
            Assert.Null(((ConversationResponseNode)vm.ConversationTree.First().Children.First()).Response.Next);

            // Delete root page
            vm.SelectedNode = vm.ConversationTree.First();
            vm.DeleteNodeCommand.Execute(null);
            Assert.Empty(vm.ConversationTree);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void MoveAction_UpAndDown_Reorders()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_cmd3", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddActionCommand.Execute(null);
            vm.AddActionCommand.Execute(null);
            var first = vm.SelectedResponseNode!.Response.Actions[0];
            var second = vm.SelectedResponseNode!.Response.Actions[1];
            vm.MoveActionDown(first);
            Assert.Same(first, vm.SelectedResponseNode!.Response.Actions[1]);
            vm.MoveActionUp(first);
            Assert.Same(first, vm.SelectedResponseNode!.Response.Actions[0]);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


