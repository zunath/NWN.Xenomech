using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Anvil.Services;
using NLog;
using XM.Shared.Core.Dialog;

namespace XM.Shared.Core.Conversation
{
    [ServiceBinding(typeof(ConversationService))]
    [ServiceBinding(typeof(IInitializable))]
    public class ConversationService : IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, ConversationModel> _conversations = new();
        private readonly Dictionary<string, ActiveConversation> _activeConversations = new();
        private readonly string _conversationsPath = "Data/conversations";

        // Service dependencies will be injected as needed
        // For now, we'll use a generic action execution system

        public void Init()
        {
            LoadConversations();
        }

        private void LoadConversations()
        {
            if (!Directory.Exists(_conversationsPath))
            {
                _logger.Warn($"Conversations directory not found: {_conversationsPath}");
                return;
            }

            var conversationFiles = Directory.GetFiles(_conversationsPath, "*.json", SearchOption.AllDirectories);
            
            foreach (var file in conversationFiles)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var conversation = JsonSerializer.Deserialize<ConversationModel>(json);
                    
                    if (conversation?.Metadata?.Id != null)
                    {
                        _conversations[conversation.Metadata.Id] = conversation;
                        _logger.Info($"Loaded conversation: {conversation.Metadata.Id} from {file}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Failed to load conversation from {file}");
                }
            }

            _logger.Info($"Loaded {_conversations.Count} conversations from JSON files");
        }

        public ConversationModel GetConversation(string id)
        {
            return _conversations.TryGetValue(id, out var conversation) ? conversation : null;
        }

        public void SaveConversation(ConversationModel conversation)
        {
            if (conversation?.Metadata?.Id == null)
                return;

            var filePath = Path.Combine(_conversationsPath, $"{conversation.Metadata.Id}.json");
            var directory = Path.GetDirectoryName(filePath);
            
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            var json = JsonSerializer.Serialize(conversation, options);
            File.WriteAllText(filePath, json);
            
            _conversations[conversation.Metadata.Id] = conversation;
            _logger.Info($"Saved conversation: {conversation.Metadata.Id}");
        }

        public IEnumerable<string> GetConversationIds()
        {
            return _conversations.Keys;
        }

        /// <summary>
        /// Starts a new conversation for a player
        /// </summary>
        public void StartConversation(uint player, uint target, string conversationId)
        {
            if (!GetIsPC(player) || !GetIsObjectValid(player))
            {
                _logger.Error($"Cannot start conversation '{conversationId}' - invalid player");
                return;
            }

            var conversation = GetConversation(conversationId);
            if (conversation == null)
            {
                _logger.Error($"Conversation '{conversationId}' not found");
                return;
            }

            var playerId = PlayerId.Get(player);
            var activeConversation = new ActiveConversation
            {
                PlayerId = playerId,
                ConversationId = conversationId,
                CurrentPage = conversation.Conversation.DefaultPage,
                Target = target,
                Variables = new Dictionary<string, object>()
            };

            _activeConversations[playerId] = activeConversation;
            
            // Display the conversation UI
            DisplayConversation(player, activeConversation);
        }

        /// <summary>
        /// Handles a player's response selection
        /// </summary>
        public void HandleResponse(uint player, int responseIndex)
        {
            var playerId = PlayerId.Get(player);
            if (!_activeConversations.TryGetValue(playerId, out var activeConversation))
            {
                _logger.Warn($"No active conversation for player {playerId}");
                return;
            }

            var conversation = GetConversation(activeConversation.ConversationId);
            var currentPage = conversation.Conversation.Pages[activeConversation.CurrentPage];
            
            if (responseIndex < 0 || responseIndex >= currentPage.Responses.Count)
            {
                _logger.Warn($"Invalid response index {responseIndex} for player {playerId}");
                return;
            }

            var response = currentPage.Responses[responseIndex];
            ExecuteAction(player, response.Action, activeConversation);
        }

        /// <summary>
        /// Ends a player's active conversation
        /// </summary>
        public void EndConversation(uint player)
        {
            var playerId = PlayerId.Get(player);
            if (_activeConversations.Remove(playerId))
            {
                // Hide conversation UI
                HideConversationUI(player);
                _logger.Info($"Ended conversation for player {playerId}");
            }
        }

        private void DisplayConversation(uint player, ActiveConversation activeConversation)
        {
            var conversation = GetConversation(activeConversation.ConversationId);
            var page = conversation.Conversation.Pages[activeConversation.CurrentPage];

            // Build conversation display text
            var displayText = page.Header + "\n\n";
            
            for (int i = 0; i < page.Responses.Count; i++)
            {
                displayText += $"{i + 1}. {page.Responses[i].Text}\n";
            }

            // Send to player (this would integrate with your UI system)
            SendMessageToPC(player, displayText);
            
            // Store response count for validation
            activeConversation.CurrentResponseCount = page.Responses.Count;
        }

        private void ExecuteAction(uint player, ResponseAction action, ActiveConversation activeConversation)
        {
            switch (action.Type)
            {
                case ConversationActionType.OpenShop:
                    if (action.Parameters.TryGetValue("shopId", out var shopId))
                    {
                        // Open shop UI - will be implemented when shop system is available
                        _logger.Info($"Opening shop {shopId} for player {player}");
                        // ItemService.OpenShop(player, shopId.ToString());
                    }
                    break;

                case ConversationActionType.TeleportPlayer:
                    if (action.Parameters.TryGetValue("location", out var location))
                    {
                        // Teleport player - will be implemented when location system is available
                        _logger.Info($"Teleporting player {player} to {location}");
                        // LocationService.TeleportPlayer(player, location.ToString());
                    }
                    break;

                case ConversationActionType.AcceptQuest:
                    if (action.Parameters.TryGetValue("questId", out var questId))
                    {
                        // Accept quest - will be implemented when quest system is available
                        _logger.Info($"Accepting quest {questId} for player {player}");
                        // QuestService.AcceptQuest(player, questId.ToString());
                    }
                    break;

                case ConversationActionType.GiveItem:
                    if (action.Parameters.TryGetValue("itemId", out var itemId) && 
                        action.Parameters.TryGetValue("quantity", out var quantity))
                    {
                        // Give item to player - will be implemented when item system is available
                        _logger.Info($"Giving item {itemId} x{quantity} to player {player}");
                        // ItemService.GiveItem(player, itemId.ToString(), Convert.ToInt32(quantity));
                    }
                    break;

                case ConversationActionType.ChangePage:
                    if (action.Parameters.TryGetValue("pageId", out var pageId))
                    {
                        // Change to new page
                        activeConversation.CurrentPage = pageId.ToString();
                        DisplayConversation(player, activeConversation);
                        return; // Don't end conversation
                    }
                    break;

                case ConversationActionType.SetVariable:
                    if (action.Parameters.TryGetValue("name", out var varName) && 
                        action.Parameters.TryGetValue("value", out var varValue))
                    {
                        // Set conversation variable
                        activeConversation.Variables[varName.ToString()] = varValue;
                    }
                    break;

                case ConversationActionType.EndConversation:
                    // End conversation
                    break;

                default:
                    _logger.Warn($"Unknown action type: {action.Type}");
                    break;
            }

            // End conversation after action (unless it was a page change)
            EndConversation(player);
        }

        private void HideConversationUI(uint player)
        {
            // Hide conversation UI elements
            // This would integrate with your UI system
        }
    }
} 