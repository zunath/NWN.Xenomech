using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Chat.UI.Conversation.Services;
using XM.Shared.Core;
using XM.Shared.Core.Conversation;
using XM.UI;

namespace XM.Chat.UI.Conversation.ViewModels
{
    /// <summary>
    /// ViewModel for the conversation UI that handles dynamic conversation data.
    /// </summary>
    [ServiceBinding(typeof(IViewModel))]
    public class ConversationViewModel : ViewModel<ConversationViewModel>
    {
        private ConversationDefinition _conversationDefinition;
        private ConversationPage _currentPage;
        private readonly List<ConversationResponse> _responseObjects;

        /// <summary>
        /// The current conversation page being displayed.
        /// </summary>
        private ConversationPage CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                // Update the conversation header when the page changes
                ConversationHeader = value?.Header ?? string.Empty;
                UpdateAvailableResponses();
            }
        }

        /// <summary>
        /// The ID of the current page.
        /// </summary>
        public string CurrentPageId
        {
            get => Get<string>();
            set => Set(value);
        }

        /// <summary>
        /// The NPC's name displayed in the conversation.
        /// </summary>
        public string NpcName
        {
            get => Get<string>();
            set => Set(value);
        }

        /// <summary>
        /// The NPC's portrait image path.
        /// </summary>
        public string NpcPortrait
        {
            get => Get<string>();
            set => Set(value);
        }

        /// <summary>
        /// Available response options for the current page.
        /// </summary>
        public XMBindingList<string> AvailableResponses
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        /// <summary>
        /// The conversation header text displayed in the main panel.
        /// </summary>
        public string ConversationHeader
        {
            get => CurrentPage?.Header ?? string.Empty;
            set => Set(value);
        }

        /// <summary>
        /// Event raised when a response is selected.
        /// </summary>
        public event EventHandler<ConversationResponseSelectedEventArgs> OnResponseSelected;

        /// <summary>
        /// Event raised when the conversation should be closed.
        /// </summary>
        public event EventHandler OnConversationClosed;

        public ConversationViewModel()
        {
            AvailableResponses = new XMBindingList<string>();
            _responseObjects = new List<ConversationResponse>();
        }

        public override void OnOpen()
        {
            // Get initial data
            var initialData = GetInitialData<ConversationInitialData>();
            if (initialData != null)
            {
                LoadConversation(initialData.ConversationDefinition, initialData.NpcName, initialData.NpcPortrait);
            }
            else
            {
                // Initialize with default page if no initial data
                if (_conversationDefinition?.Conversation?.Pages != null)
                {
                    var defaultPageId = _conversationDefinition.Conversation.DefaultPage;
                    NavigateToPage(defaultPageId);
                }
            }
        }

        public override void OnClose()
        {
            // Cleanup when conversation is closed
        }

        /// <summary>
        /// Loads a conversation definition and initializes the UI.
        /// </summary>
        /// <param name="conversationDefinition">The conversation definition to load.</param>
        /// <param name="npcName">The name of the NPC.</param>
        /// <param name="npcPortrait">The portrait image path for the NPC.</param>
        public void LoadConversation(ConversationDefinition conversationDefinition, string npcName, string npcPortrait = "")
        {
            _conversationDefinition = conversationDefinition;
            NpcName = npcName;
            NpcPortrait = npcPortrait;

            // Navigate to default page
            if (conversationDefinition?.Conversation?.Pages != null)
            {
                var defaultPageId = conversationDefinition.Conversation.DefaultPage;
                NavigateToPage(defaultPageId);
            }
        }

        /// <summary>
        /// Navigates to a specific page in the conversation.
        /// </summary>
        /// <param name="pageId">The ID of the page to navigate to.</param>
        public void NavigateToPage(string pageId)
        {
            if (_conversationDefinition?.Conversation?.Pages == null || 
                !_conversationDefinition.Conversation.Pages.ContainsKey(pageId))
            {
                return;
            }

            CurrentPageId = pageId;
            CurrentPage = _conversationDefinition.Conversation.Pages[pageId];
        }

        /// <summary>
        /// Handles when a player selects a response option.
        /// </summary>
        /// <param name="index">The index of the selected response.</param>
        public void SelectResponse(int index)
        {
            if (index < 0 || index >= _responseObjects.Count)
                return;

            var response = _responseObjects[index];
            if (response == null) return;

            // Check if response meets conditions
            if (!EvaluateConditions(response.Conditions))
            {
                return;
            }

            // Raise event for response selection
            OnResponseSelected?.Invoke(this, new ConversationResponseSelectedEventArgs(response));

            // Handle the action
            HandleResponseAction(response.Action);
        }

        /// <summary>
        /// Closes the conversation window.
        /// </summary>
        public Action CloseConversation() => () =>
        {
            OnConversationClosed?.Invoke(this, EventArgs.Empty);
            CloseWindow();
        };

        /// <summary>
        /// Handles when a player selects a response option from the UI.
        /// </summary>
        public Action OnSelectResponse() => () =>
        {
            var index = NuiGetEventArrayIndex();
            SelectResponse(index);
        };

        /// <summary>
        /// Updates the available responses based on current conditions.
        /// </summary>
        private void UpdateAvailableResponses()
        {
            if (CurrentPage?.Responses == null)
            {
                AvailableResponses.Clear();
                _responseObjects.Clear();
                return;
            }

            // Filter responses based on conditions
            var availableResponses = CurrentPage.Responses
                .Where(response => EvaluateConditions(response.Conditions))
                .ToList();

            AvailableResponses.Clear();
            _responseObjects.Clear();
            
            foreach (var response in availableResponses)
            {
                AvailableResponses.Add(response.Text);
                _responseObjects.Add(response);
            }
        }

        /// <summary>
        /// Evaluates whether a list of conditions are met.
        /// </summary>
        /// <param name="conditions">The conditions to evaluate.</param>
        /// <returns>True if all conditions are met, false otherwise.</returns>
        private bool EvaluateConditions(List<ConversationCondition> conditions)
        {
            if (conditions == null || conditions.Count == 0)
                return true;

            // TODO: Implement condition evaluation logic
            // This would check player level, items, quests, variables, etc.
            return true;
        }

        /// <summary>
        /// Handles the action associated with a response.
        /// </summary>
        /// <param name="action">The action to handle.</param>
        private void HandleResponseAction(ConversationAction action)
        {
            if (action == null) return;

            switch (action.Type?.ToLower())
            {
                case "changepage":
                    var pageId = action.Parameters?.GetValueOrDefault("pageId")?.ToString();
                    if (!string.IsNullOrEmpty(pageId))
                    {
                        NavigateToPage(pageId);
                    }
                    break;

                case "endconversation":
                    CloseConversation();
                    break;

                case "openshop":
                    // TODO: Implement shop opening logic
                    break;

                case "teleport_player":
                    // TODO: Implement teleport logic
                    break;

                case "accept_quest":
                    // TODO: Implement quest acceptance logic
                    break;

                case "give_item":
                    // TODO: Implement item giving logic
                    break;

                case "set_variable":
                    // TODO: Implement variable setting logic
                    break;

                case "execute_script":
                    // TODO: Implement script execution logic
                    break;

                default:
                    // Unknown action type
                    break;
            }
        }
    }

    /// <summary>
    /// Event arguments for when a conversation response is selected.
    /// </summary>
    public class ConversationResponseSelectedEventArgs : EventArgs
    {
        public ConversationResponse Response { get; }

        public ConversationResponseSelectedEventArgs(ConversationResponse response)
        {
            Response = response;
        }
    }
} 