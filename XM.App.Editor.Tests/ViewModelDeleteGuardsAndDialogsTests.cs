using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelDeleteGuardsAndDialogsTests
{
    private sealed class FakeSettingsService : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    private sealed class FakeConfirm : IConfirmationService
    {
        public bool Result { get; set; }
        public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(Result);
    }

    private static ConversationEditorViewModel CreateVm(bool confirmResult)
    {
        var settings = new FakeSettingsService();
        var confirm = new FakeConfirm { Result = confirmResult };
        var svc = new ConversationService(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "vm-tests-3"));
        var vm = new ConversationEditorViewModel(settings, svc, confirm);
        vm.CurrentConversation = new ConversationData
        {
            Conversation = new ConversationContent
            {
                Root = new ConversationPage
                {
                    Header = "NPC",
                    Responses = new()
                    {
                        new ConversationResponse
                        {
                            Text = "Hello",
                            Actions = new ObservableCollection<ConversationAction>(),
                            Conditions = new ObservableCollection<ConversationCondition>()
                        }
                    }
                }
            }
        };
        vm.SelectedNode = vm.ConversationTree[0];
        return vm;
    }

    [Fact]
    public void DeleteNode_Respect_Confirmation_No()
    {
        var vm = CreateVm(confirmResult: false);
        var countBefore = ((ConversationPageNode)vm.SelectedNode!).Page.Responses.Count;
        vm.DeleteNodeCommand.Execute(null);
        var countAfter = ((ConversationPageNode)vm.SelectedNode!).Page.Responses.Count;
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public void DeleteAction_Respect_Confirmation_No()
    {
        var vm = CreateVm(confirmResult: true);
        vm.SelectedNode = vm.ConversationTree[0].Children[0];
        vm.AddActionCommand.Execute(null);
        vm.SelectedAction = ((ConversationResponseNode)vm.SelectedNode).Response.Actions[0];

        // Now decline deletion
        ((FakeConfirm)vm.GetType().GetField("_confirmationService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!.GetValue(vm)!).Result = false;
        vm.DeleteActionCommand.Execute(null);
        Assert.Single(((ConversationResponseNode)vm.SelectedNode).Response.Actions);
    }
}


