using System.Linq;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ConversationEditorViewModelAdditionalTests
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
    public void BuildConversationTree_AssignsNodePaths_And_PageIdsList()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_extra", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddPageCommand.Execute(null); // create root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddResponseCommand.Execute(null);
            vm.AddResponseCommand.Execute(null);
            // Re-fetch after mutations
            var rebuilt = vm.ConversationTree.First();
            // Add next NPC for first response
            vm.SelectedNode = rebuilt.Children.First();
            vm.AddNextNpcCommand.Execute(null);

            // Verify node paths
            var root = vm.ConversationTree.First();
            Assert.StartsWith("P:", root.NodePath);
            var firstResp = root.Children.First();
            Assert.Contains("/R:", firstResp.NodePath);

            // Set explicit Ids and ensure list contains them
            ((ConversationPageNode)root).Page.Id = "Root";
            ((ConversationPageNode)firstResp.Children.First()).Page.Id = "Child";
            // Changing Id triggers AvailablePageIds refresh via subscription
            Assert.Contains("Root", vm.AvailablePageIds);
            Assert.Contains("Child", vm.AvailablePageIds);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void SelectedChangePageItem_Setter_Updates_SelectedAction_PageId()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_extra2", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddResponseCommand.Execute(null);
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddActionCommand.Execute(null);
            vm.SelectedAction!.Type = "ChangePage";
            // Create an available page id by setting root id
            ((ConversationPageNode)vm.ConversationTree.First()).Page.Id = "MyPage";
            // Set via property
            vm.SelectedChangePageItem = "MyPage";
            Assert.Equal("MyPage", vm.SelectedAction.PageId);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


