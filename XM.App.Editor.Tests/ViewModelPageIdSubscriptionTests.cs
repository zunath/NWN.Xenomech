using System.Collections.ObjectModel;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;

namespace XM.App.Editor.Tests;

public class ViewModelPageIdSubscriptionTests
{
    private sealed class FakeSettingsService : IUserSettingsService
    {
        public UserSettings Current { get; } = new();
        public void Load() { }
        public void Save() { }
    }

    private sealed class AlwaysConfirm : IConfirmationService
    {
        public Task<bool> ShowConfirmationAsync(string title, string message) => Task.FromResult(true);
    }

    [Fact]
    public void Changing_Page_Id_Updates_AvailablePageIds_And_Normalizes_SelectedChangePageItem()
    {
        var settings = new FakeSettingsService();
        var confirm = new AlwaysConfirm();
        var svc = new ConversationService(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "vm-tests-4"));
        var vm = new ConversationEditorViewModel(settings, svc, confirm);

        vm.CurrentConversation = new ConversationData
        {
            Conversation = new ConversationContent
            {
                Root = new ConversationPage
                {
                    Id = "Root",
                    Header = "NPC",
                    Responses = new()
                    {
                        new ConversationResponse
                        {
                            Text = "Go",
                            Actions = new ObservableCollection<ConversationAction>
                            {
                                new ConversationAction{ Type = "ChangePage", PageId = "next" }
                            },
                            Next = new ConversationPage { Id = "Next", Header = "Next" }
                        }
                    }
                }
            }
        };

        // Select response node and action
        vm.SelectedNode = vm.ConversationTree[0].Children[0];
        vm.SelectedAction = ((ConversationResponseNode)vm.SelectedNode).Response.Actions[0];

        // Sanity
        Assert.Contains("Next", vm.AvailablePageIds);
        Assert.Equal("Next", vm.SelectedChangePageItem);

        // Change page Id to different case and ensure normalization and list refresh
        var selectedNext = ((ConversationResponseNode)vm.SelectedNode).Response.Next!;
        selectedNext.Id = "nExT";
        Assert.Contains("nExT", vm.AvailablePageIds);
        Assert.Equal("nExT", vm.SelectedChangePageItem);
    }
}


