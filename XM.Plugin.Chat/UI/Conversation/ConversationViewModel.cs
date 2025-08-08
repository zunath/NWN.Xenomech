using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Shared.Core;
using XM.Shared.Core.Conversation;
using XM.UI;
using XM.Inventory;
using XM.Shared.Core.Data;
using XM.Chat.UI.Conversation.Actions;
using XM.Chat.UI.Conversation.Conditions;
using XM.Progression.Stat;
using XM.Progression.Skill;

namespace XM.Chat.UI.Conversation
{
    /// <summary>
    /// ViewModel for the conversation UI that handles dynamic conversation data.
    /// </summary>
    [ServiceBinding(typeof(IViewModel))]
    public class ConversationViewModel : ViewModel<ConversationViewModel>, IConversationCallback
    {
        private ConversationDefinition _conversationDefinition;
        private ConversationPage _currentPage;
        private readonly List<ConversationResponse> _responseObjects;
        
        private ConversationConditionHandlerFactory _conditionHandlerFactory;

        [Inject]
        public InventoryService Inventory { get; set; }

        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public StatService Stat { get; set; }

        [Inject]
        public ConversationActionHandlerFactory ActionHandlerFactory { get; set; }

        [Inject]
        public SkillService Skill { get; set; }

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

        public ConversationViewModel()
        {
            AvailableResponses = new XMBindingList<string>();
            _responseObjects = new List<ConversationResponse>();
        }

        public override void OnOpen()
        {
            // Initialize the condition handler factory with injected services
            _conditionHandlerFactory = new ConversationConditionHandlerFactory(Stat, Skill);
            
            // Get initial data
            var initialData = GetInitialData<ConversationInitialData>();
            if (initialData != null)
            {
                LoadConversation(initialData.ConversationDefinition, initialData.NpcName, initialData.NpcPortrait);
            }
            else
            {
                // Initialize with root if no initial data (tree-based model)
                if (_conversationDefinition?.Conversation?.Root != null)
                {
                    CurrentPageId = "root";
                    CurrentPage = _conversationDefinition.Conversation.Root;
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

            // Navigate to root page (tree-based model)
            if (conversationDefinition?.Conversation?.Root != null)
            {
                CurrentPageId = "root";
                CurrentPage = conversationDefinition.Conversation.Root;
            }
        }

        /// <summary>
        /// Navigates to a specific page in the conversation.
        /// </summary>
        /// <param name="pageId">The ID of the page to navigate to.</param>
        public void NavigateToPage(string pageId)
        {
            // Navigation via pageId is not used in tree-based model; handled by Next pointers
            return;
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

            // Handle multiple actions if present
            if (response.Actions != null && response.Actions.Count > 0)
            {
                HandleResponseActions(response.Actions);
            }

            // Advance to next NPC page if present
            if (response.Next != null)
            {
                CurrentPage = response.Next;
            }
            else if (response.Actions == null || response.Actions.Count == 0)
            {
                // No next page and no actions -> end the conversation by default
                CloseConversation();
            }
        }

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

            foreach (var condition in conditions)
            {
                var handler = _conditionHandlerFactory.GetHandler(condition.ConditionType);
                
                if (handler != null)
                {
                    var conditionMet = handler.EvaluateCondition(condition, Player);
                    if (!conditionMet)
                        return false;
                }
                else
                {
                    // Unknown condition type - fail by default for safety
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Handles the action associated with a response.
        /// </summary>
        /// <param name="action">The action to handle.</param>
        private void HandleResponseAction(ConversationAction action)
        {
            if (action == null) return;

            var handler = ActionHandlerFactory.GetHandler(action.ActionType);
            
            if (handler != null)
            {
                handler.HandleAction(action, Player, this);
            }
            else
            {
                // Unknown or invalid action type
                // Could log this for debugging purposes
            }
        }

        /// <summary>
        /// Handles multiple actions associated with a response.
        /// </summary>
        /// <param name="actions">The list of actions to handle.</param>
        private void HandleResponseActions(List<ConversationAction> actions)
        {
            if (actions == null || actions.Count == 0) return;

            foreach (var action in actions)
            {
                HandleResponseAction(action);
            }
        }

        /// <summary>
        /// Closes the conversation window.
        /// </summary>
        public void CloseConversation()
        {
            CloseWindow();
        }
    }
} 