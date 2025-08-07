using System.Text.Json;
using System.Collections.ObjectModel;
using XM.App.Editor.Models;

namespace XM.App.Editor.Services;

public class ConversationService
{
    private readonly string _conversationsDirectory;

    public ConversationService()
    {
        _conversationsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data", "conversations");
    }

    public List<string> GetConversationFiles()
    {
        if (!Directory.Exists(_conversationsDirectory))
            return new List<string>();

        return Directory.GetFiles(_conversationsDirectory, "*.json")
                       .Select(Path.GetFileNameWithoutExtension)
                       .Where(name => !string.IsNullOrEmpty(name))
                       .Select(name => name!)
                       .ToList();
    }

    public ConversationData? LoadConversation(string fileName)
    {
        var filePath = Path.Combine(_conversationsDirectory, $"{fileName}.json");
        
        if (!File.Exists(filePath))
            return null;

        try
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ConversationData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            // TODO: Add proper logging
            Console.WriteLine($"Error loading conversation {fileName}: {ex.Message}");
            return null;
        }
    }

    public bool SaveConversation(string fileName, ConversationData conversation)
    {
        var filePath = Path.Combine(_conversationsDirectory, $"{fileName}.json");
        
        try
        {
            var json = JsonSerializer.Serialize(conversation, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            // TODO: Add proper logging
            Console.WriteLine($"Error saving conversation {fileName}: {ex.Message}");
            return false;
        }
    }

    public bool DeleteConversation(string fileName)
    {
        var filePath = Path.Combine(_conversationsDirectory, $"{fileName}.json");
        
        if (!File.Exists(filePath))
            return false;

        try
        {
            File.Delete(filePath);
            return true;
        }
        catch (Exception ex)
        {
            // TODO: Add proper logging
            Console.WriteLine($"Error deleting conversation {fileName}: {ex.Message}");
            return false;
        }
    }

    public ConversationData CreateNewConversation(string id, string name, string description = "")
    {
        return new ConversationData
        {
            Metadata = new ConversationMetadata
            {
                Id = id,
                Name = name,
                Description = description,
                Portrait = ""
            },
            Conversation = new ConversationContent
            {
                Root = new ConversationPage
                {
                    Header = "Hello! How can I help you?",
                    Responses = new List<ConversationResponse>
                    {
                        new ConversationResponse
                        {
                            Text = "Goodbye",
                            Conditions = new ObservableCollection<ConversationCondition>(),
                            Actions = new ObservableCollection<ConversationAction>
                            {
                                new ConversationAction
                                {
                                    Type = "EndConversation",
                                    Parameters = new Dictionary<string, object>()
                                }
                            }
                        }
                    }
                }
            }
        };
    }
} 