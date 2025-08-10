using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;

namespace XM.App.Editor.Tests;

public class ViewModelTests
{
    private class TestConfirmationService : IConfirmationService
    {
        private readonly bool _result;
        public TestConfirmationService(bool result) { _result = result; }
        public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(_result);
    }

    private class TestUserSettingsService : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    [Fact]
    public void ConversationEditor_BasicAddDeleteFlows()
    {
        var temp = Path.Combine(Path.GetTempPath(), "xm_editor_vm", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);
        try
        {
            var user = new TestUserSettingsService();
            var convSvc = new ConversationService(temp);
            var confirm = new TestConfirmationService(true);
            var vm = new ConversationEditorViewModel(user, convSvc, confirm);

            // Create new conversation
            vm.CreateNewConversationCommand.Execute(null);
            Assert.NotNull(vm.SelectedConversationFile);
            Assert.NotNull(vm.CurrentConversation);

            // Add root, then a response, then next NPC
            vm.AddNodeCommand.Execute(null); // add root page
            Assert.NotNull(vm.CurrentConversation!.Conversation.Root);
            vm.SelectedNode = vm.ConversationTree.First();
            vm.AddNodeCommand.Execute(null); // add response
            vm.SelectedNode = (vm.ConversationTree.First().Children.First());
            vm.AddNodeCommand.Execute(null); // add next NPC
            Assert.True(vm.ConversationTree.First().Children.First().Children.Count == 1);

            // Select response and add action and condition
            vm.SelectedNode = vm.ConversationTree.First().Children.First();
            vm.AddActionCommand.Execute(null);
            Assert.NotNull(vm.SelectedAction);
            vm.AddConditionCommand.Execute(null);
            Assert.NotNull(vm.SelectedCondition);

            // Move action up/down no-op edges
            var action = vm.SelectedAction!;
            vm.MoveActionUp(action);
            vm.MoveActionDown(action);

            // Save and delete conversation
            vm.SaveConversationCommand.Execute(null);
            vm.DeleteConversationCommand.Execute(null); // confirm == true
            Assert.Null(vm.SelectedConversationFile);
        }
        finally
        {
            if (Directory.Exists(temp)) Directory.Delete(temp, true);
        }
    }
}


