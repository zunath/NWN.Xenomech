using Anvil.API;
using Anvil.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text.Json;
using XM.Chat.UI.Conversation.Views;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Conversation;
using XM.UI;

namespace XM.Chat.UI.Conversation.Services
{
    /// <summary>
    /// Service for managing conversation definitions, loading conversation data, and opening conversation windows.
    /// </summary>
    [ServiceBinding(typeof(ConversationService))]
    public class ConversationService
    {
        private readonly Dictionary<string, ConversationDefinition> _cachedConversations = new();
        private readonly string _conversationDataPath = "anvil/Plugins/resources/conversations";
        private readonly GuiService _guiService;
        private readonly XMSettingsService _settings;

        public ConversationService(GuiService guiService, XMSettingsService settings)
        {
            _guiService = guiService;
            _settings = settings;
        }

        /// <summary>
        /// Loads a conversation definition from JSON file.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to load.</param>
        /// <returns>The loaded conversation definition, or null if not found.</returns>
        public ConversationDefinition LoadConversation(string conversationId)
        {
            var fileName = _settings.ResourcesDirectory + $"conversations/{conversationId}.json";

            // Check cache first
            if (_cachedConversations.ContainsKey(conversationId))
            {
                return _cachedConversations[conversationId];
            }

            // Try to load from file
            if (!File.Exists(fileName))
            {
                return null;
            }

            try
            {
                var jsonContent = File.ReadAllText(fileName);
                var conversation = JsonSerializer.Deserialize<ConversationDefinition>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (conversation != null)
                {
                    _cachedConversations[conversationId] = conversation;
                }

                return conversation;
            }
            catch (Exception ex)
            {
                // Log error and return null
                Console.WriteLine($"Error loading conversation {conversationId}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Opens a conversation window for a player.
        /// </summary>
        /// <param name="player">The player to open the conversation for.</param>
        /// <param name="conversationId">The ID of the conversation to load.</param>
        /// <param name="npcName">The name of the NPC.</param>
        /// <param name="npcPortrait">The portrait image path for the NPC.</param>
        /// <param name="tetherObject">The object to tether the window to (usually the NPC).</param>
        /// <returns>True if the conversation was opened successfully, false otherwise.</returns>
        public bool OpenConversation(uint player, string conversationId, string npcName, string npcPortrait = "", uint tetherObject = 0)
        {
            // Load the conversation definition
            var conversationDefinition = LoadConversation(conversationId);
            if (conversationDefinition == null)
            {
                Console.WriteLine($"Failed to load conversation: {conversationId}");
                return false;
            }

            // Create initial data for the conversation
            var initialData = new ConversationInitialData
            {
                ConversationDefinition = conversationDefinition,
                NpcName = npcName,
                NpcPortrait = npcPortrait
            };

            // Open the conversation window
            try
            {
                _guiService.ShowWindow<ConversationView>(player, initialData, tetherObject);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening conversation window: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Opens a conversation window for a player using an NPC object.
        /// </summary>
        /// <param name="player">The player to open the conversation for.</param>
        /// <param name="conversationId">The ID of the conversation to load.</param>
        /// <param name="npc">The NPC object.</param>
        /// <returns>True if the conversation was opened successfully, false otherwise.</returns>
        public bool OpenConversation(uint player, string conversationId, NwCreature npc)
        {
            if (npc == null)
            {
                return false;
            }

            var npcName = npc.Name;
            var npcPortrait = npc.PortraitResRef;
            var tetherObject = npc.ObjectId;

            return OpenConversation(player, conversationId, npcName, npcPortrait, tetherObject);
        }

        /// <summary>
        /// Gets a list of available conversation IDs.
        /// </summary>
        /// <returns>List of conversation IDs.</returns>
        public List<string> GetAvailableConversations()
        {
            var conversations = new List<string>();

            if (!Directory.Exists(_conversationDataPath))
            {
                return conversations;
            }

            try
            {
                var files = Directory.GetFiles(_conversationDataPath, "*.json");
                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    conversations.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting available conversations: {ex.Message}");
            }

            return conversations;
        }

        /// <summary>
        /// Clears the conversation cache.
        /// </summary>
        public void ClearCache()
        {
            _cachedConversations.Clear();
        }

        /// <summary>
        /// Reloads a specific conversation from file.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to reload.</param>
        /// <returns>The reloaded conversation definition, or null if not found.</returns>
        public ConversationDefinition ReloadConversation(string conversationId)
        {
            _cachedConversations.Remove(conversationId);
            return LoadConversation(conversationId);
        }
    }
} 