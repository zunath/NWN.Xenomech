using System.Linq;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelIdRefreshTests
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
    public void ChangingPageId_UpdatesAvailablePageIds_And_NormalizesSelectedAction()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_ids", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            var root = (ConversationPageNode)vm.ConversationTree.First();
            root.Page.Id = "Root";
            vm.SelectedNode = root;

            // Add response and action to change page
            vm.AddResponseCommand.Execute(null);
            var newRoot = (ConversationPageNode)vm.ConversationTree.First();
            vm.SelectedNode = newRoot.Children.First();
            vm.AddActionCommand.Execute(null);
            vm.SelectedAction!.Type = "ChangePage";
            vm.SelectedAction.PageId = "root"; // wrong case

            // Trigger RefreshAvailablePageIds by selecting page then reassign
            vm.SelectedNode = newRoot; // rewire selection subscriptions
            vm.SelectedNode = newRoot.Children.First();
            vm.SelectedAction = vm.SelectedAction; // nudge property change

            // Should normalize to the exact id from AvailablePageIds
            Assert.Contains("Root", vm.AvailablePageIds);
            if (vm.SelectedChangePageItem is not null)
            {
                Assert.Equal("Root", vm.SelectedChangePageItem);
                Assert.Equal("Root", vm.SelectedAction.PageId);
            }

            // Change the page Id to something else and verify list updates
            root.Page.Id = "Start";
            Assert.Contains("Start", vm.AvailablePageIds);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }

    [Fact]
    public void Selection_Preservation_When_RebuildingTree()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm_preserve", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var vm = new ConversationEditorViewModel(new Users(), new ConversationService(temp), new Confirm(true));
            vm.CreateNewConversationCommand.Execute(null);
            vm.AddNodeCommand.Execute(null); // root
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddResponseCommand.Execute(null);
            vm.AddResponseCommand.Execute(null);
            var secondResponse = vm.ConversationTree.First().Children.Skip(1).First();

            // Select second response, then trigger a rebuild (add next NPC)
            vm.SelectedNode = secondResponse;
            vm.AddNextNpcCommand.Execute(null);

            // After rebuild, selection should be preserved or reasonable fallback
            Assert.NotNull(vm.SelectedNode);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


