using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using XM.Shared.Core.Conversation;

namespace XM.App.Editor.Services
{
    public class FileService
    {
        private readonly string _conversationsPath = "Data/conversations";
        private readonly JsonSerializerOptions _jsonOptions;

        public FileService()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            // Add custom converters
            _jsonOptions.Converters.Add(new XM.Shared.Core.Conversation.ConversationActionTypeConverter());
        }

        /// <summary>
        /// Loads all conversation files from the conversations directory
        /// </summary>
        public async Task<List<ConversationModel>> LoadAllConversationsAsync()
        {
            var conversations = new List<ConversationModel>();

            if (!Directory.Exists(_conversationsPath))
            {
                Directory.CreateDirectory(_conversationsPath);
                return conversations;
            }

            var files = Directory.GetFiles(_conversationsPath, "*.json", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                try
                {
                    var conversation = await LoadConversationAsync(file);
                    if (conversation != null)
                    {
                        conversations.Add(conversation);
                    }
                }
                catch (Exception ex)
                {
                    // Log error but continue loading other files
                    Console.WriteLine($"Error loading conversation from {file}: {ex.Message}");
                }
            }

            return conversations;
        }

        /// <summary>
        /// Loads a single conversation from a file
        /// </summary>
        public async Task<ConversationModel?> LoadConversationAsync(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                var conversation = JsonSerializer.Deserialize<ConversationModel>(json, _jsonOptions);
                
                if (conversation?.Metadata?.Id != null)
                {
                    return conversation;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing conversation from {filePath}: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Saves a conversation to a file
        /// </summary>
        public async Task SaveConversationAsync(ConversationModel conversation, string? filePath = null)
        {
            if (conversation?.Metadata?.Id == null)
                throw new ArgumentException("Conversation must have a valid ID");

            if (filePath == null)
            {
                filePath = Path.Combine(_conversationsPath, $"{conversation.Metadata.Id}.json");
            }

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Update last modified timestamp
            conversation.Metadata.LastModified = DateTime.Now;

            var json = JsonSerializer.Serialize(conversation, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json);
        }

        /// <summary>
        /// Deletes a conversation file
        /// </summary>
        public async Task DeleteConversationAsync(string conversationId)
        {
            var filePath = Path.Combine(_conversationsPath, $"{conversationId}.json");
            
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        /// <summary>
        /// Gets the list of available conversation IDs
        /// </summary>
        public List<string> GetConversationIds()
        {
            var ids = new List<string>();

            if (!Directory.Exists(_conversationsPath))
                return ids;

            var files = Directory.GetFiles(_conversationsPath, "*.json", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var conversation = JsonSerializer.Deserialize<ConversationModel>(json, _jsonOptions);
                    
                    if (conversation?.Metadata?.Id != null)
                    {
                        ids.Add(conversation.Metadata.Id);
                    }
                }
                catch
                {
                    // Skip invalid files
                }
            }

            return ids;
        }

        /// <summary>
        /// Creates a new conversation with default values
        /// </summary>
        public ConversationModel CreateNewConversation()
        {
            return new ConversationModel
            {
                Metadata = new ConversationMetadata
                {
                    Id = $"conversation_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Name = "New Conversation",
                    Description = "A new conversation",
                    Version = "1.0",
                    LastModified = DateTime.Now,
                    Author = Environment.UserName,
                    Tags = new List<string>()
                },
                Conversation = new ConversationData
                {
                    DefaultPage = "main",
                    Pages = new Dictionary<string, ConversationPage>
                    {
                        ["main"] = new ConversationPage
                        {
                            Header = "Welcome! What can I help you with?",
                            Responses = new List<ConversationResponse>
                            {
                                new ConversationResponse
                                {
                                    Text = "Goodbye",
                                    Action = new ResponseAction
                                    {
                                        Type = ConversationActionType.EndConversation
                                    }
                                }
                            }
                        }
                    },
                    Actions = new Dictionary<string, ActionDefinition>(),
                    Conditions = new Dictionary<string, ConditionDefinition>()
                }
            };
        }

        /// <summary>
        /// Validates a conversation model
        /// </summary>
        public List<string> ValidateConversation(ConversationModel conversation)
        {
            var errors = new List<string>();

            if (conversation?.Metadata?.Id == null)
            {
                errors.Add("Conversation must have a valid ID");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(conversation.Metadata.Name))
            {
                errors.Add("Conversation must have a name");
            }

            if (conversation.Conversation?.Pages == null || conversation.Conversation.Pages.Count == 0)
            {
                errors.Add("Conversation must have at least one page");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(conversation.Conversation.DefaultPage))
            {
                errors.Add("Conversation must have a default page");
            }
            else if (!conversation.Conversation.Pages.ContainsKey(conversation.Conversation.DefaultPage))
            {
                errors.Add($"Default page '{conversation.Conversation.DefaultPage}' does not exist");
            }

            // Validate all pages have responses
            foreach (var page in conversation.Conversation.Pages)
            {
                if (page.Value.Responses == null || page.Value.Responses.Count == 0)
                {
                    errors.Add($"Page '{page.Key}' must have at least one response");
                }
            }

            return errors;
        }
    }
} 