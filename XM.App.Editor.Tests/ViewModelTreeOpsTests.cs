using System.Linq;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelTreeOpsTests
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
    public void BuildDeepTree_PerformActions_Deletions_And_Saves()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_ops", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);

            // Add root
            vm.AddNodeCommand.Execute(null);
            var root = (ConversationPageNode)vm.ConversationTree.First();

            // Add two responses
            vm.SelectedNode = root;
            vm.AddResponseCommand.Execute(null);
            vm.AddResponseCommand.Execute(null);
            var resp1 = (ConversationResponseNode)vm.ConversationTree.First().Children[0];
            var resp2 = (ConversationResponseNode)vm.ConversationTree.First().Children[1];

            // Add next NPC to first response
            vm.SelectedNode = resp1;
            vm.AddNextNpcCommand.Execute(null);
            // Re-fetch after mutation
            resp1 = (ConversationResponseNode)vm.ConversationTree.First().Children[0];
            var childPage = (ConversationPageNode)resp1.Children.First();

            // Add actions and reorder
            vm.SelectedNode = resp1;
            vm.AddActionCommand.Execute(null);
            vm.AddActionCommand.Execute(null);
            vm.AddActionCommand.Execute(null);
            var actions = resp1.Response.Actions;
            var second = actions[1];
            vm.MoveActionUp(second);
            vm.MoveActionDown(actions[0]);

            // Change page selection list: set root explicit Id and choose it
            root.Page.Id = "Root";
            vm.SelectedAction = actions[0];
            vm.SelectedAction.Type = "ChangePage";
            vm.SelectedChangePageItem = "Root";

            // Delete next NPC, re-add
            vm.DeleteNextNpcCommand.Execute(null);
            vm.AddNextNpcCommand.Execute(null);

            // Remove next NPC via dedicated command and verify
            vm.SelectedNode = resp1;
            vm.DeleteNextNpcCommand.Execute(null);
            resp1 = (ConversationResponseNode)vm.ConversationTree.First().Children[0];
            Assert.Null(resp1.Response.Next);

            // Delete the second response
            vm.SelectedNode = (ConversationResponseNode)vm.ConversationTree.First().Children[1];
            vm.DeleteNodeCommand.Execute(null);
            // Re-fetch root after deletion to reflect latest tree
            root = (ConversationPageNode)vm.ConversationTree.First();
            Assert.Single(root.Children);

            // Save conversation
            vm.SelectedConversationFile = vm.SelectedConversationFile ?? "file_ops";
            vm.SaveConversationCommand.Execute(null);

            // Delete root page and rebuild; root should be cleared
            vm.SelectedNode = (ConversationPageNode)vm.ConversationTree.First();
            vm.DeleteNodeCommand.Execute(null);
            Assert.Empty(vm.ConversationTree);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


