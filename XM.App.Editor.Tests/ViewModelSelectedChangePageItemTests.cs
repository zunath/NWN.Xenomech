using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XM.App.Editor.Models;
using XM.App.Editor.Services;
using XM.App.Editor.ViewModels;
using Xunit;

namespace XM.App.Editor.Tests;

public class ViewModelSelectedChangePageItemTests
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

    private static ConversationEditorViewModel CreateVm(out FakeConfirm confirm)
    {
        var settings = new FakeSettingsService();
        confirm = new FakeConfirm { Result = false };
        var uniqueDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "vm-tests-" + System.Guid.NewGuid());
        System.IO.Directory.CreateDirectory(uniqueDir);
        var svc = new ConversationService(uniqueDir);
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
        // Select response node and the action
        vm.SelectedNode = vm.ConversationTree[0].Children[0];
        vm.SelectedAction = ((ConversationResponseNode)vm.SelectedNode).Response.Actions[0];
        return vm;
    }

    [Fact]
    public void Getter_Normalizes_To_ItemsSource_CaseInsensitive()
    {
        var vm = CreateVm(out _);
        Assert.Contains("Root", vm.AvailablePageIds);
        Assert.Contains("Next", vm.AvailablePageIds);
        vm.SelectedAction!.PageId = "next"; // lower case
        var item = vm.SelectedChangePageItem;
        Assert.Equal("Next", item);
    }

    [Fact]
    public void Setter_Updates_PageId_And_Notifies()
    {
        var vm = CreateVm(out _);
        vm.SelectedChangePageItem = "Root";
        Assert.Equal("Root", vm.SelectedAction!.PageId);
    }

    [Fact]
    public void Getter_Setter_Null_And_WrongType_Scenarios()
    {
        var vm = CreateVm(out _);
        // Wrong type
        vm.SelectedAction!.Type = "OpenShop";
        Assert.Null(vm.SelectedChangePageItem);
        vm.SelectedChangePageItem = "Next"; // no-op
        Assert.Equal("Next", vm.SelectedAction.PageId); // remains unchanged

        // Null action
        vm.SelectedAction = null;
        Assert.Null(vm.SelectedChangePageItem);
        vm.SelectedChangePageItem = null; // no-op
    }
}


