using System.Text.Json;
using System.Collections.ObjectModel;
using XM.App.Editor.Models;

namespace XM.App.Editor.Services;

public class ConversationService
{
    private readonly string _conversationsDirectory;

    public ConversationService(string? conversationsDirectory = null)
    {
        _conversationsDirectory = ResolveConversationsDirectory(conversationsDirectory);
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
            Directory.CreateDirectory(_conversationsDirectory);
            var json = JsonSerializer.Serialize(conversation, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
            
            // Also update the output-local copy if different, so preview/runtime sees latest
            var outputDir = Path.Combine(AppContext.BaseDirectory, "Data", "conversations");
            var outputPath = Path.Combine(outputDir, $"{fileName}.json");
            if (!Path.GetFullPath(outputDir).Equals(Path.GetFullPath(_conversationsDirectory), StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(outputDir);
                File.WriteAllText(outputPath, json);
            }
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
        var outputPath = Path.Combine(AppContext.BaseDirectory, "Data", "conversations", $"{fileName}.json");

        var deletedAny = false;
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                deletedAny = true;
            }

            // Also remove output-local copy to avoid stale duplicates
            if (!Path.GetFullPath(_conversationsDirectory).Equals(Path.GetFullPath(Path.GetDirectoryName(outputPath)!), StringComparison.OrdinalIgnoreCase)
                && File.Exists(outputPath))
            {
                File.Delete(outputPath);
                deletedAny = true;
            }

            return deletedAny;
        }
        catch (Exception ex)
        {
            // TODO: Add proper logging
            Console.WriteLine($"Error deleting conversation {fileName}: {ex.Message}");
            return deletedAny;
        }
    }

    private static string ResolveConversationsDirectory(string? explicitDirectory)
    {
        // 1) Explicit parameter wins
        if (!string.IsNullOrWhiteSpace(explicitDirectory))
            return explicitDirectory!;

        // 2) Prefer repository root's Data/conversations (contains solution file)
        static string? FindRepoRoot(string startPath)
        {
            var dirInfo = new DirectoryInfo(startPath);
            for (int i = 0; i < 12 && dirInfo != null; i++)
            {
                var sln = Path.Combine(dirInfo.FullName, "Xenomech.sln");
                var git = Path.Combine(dirInfo.FullName, ".git");
                if (File.Exists(sln) || Directory.Exists(git))
                    return dirInfo.FullName;
                dirInfo = dirInfo.Parent;
            }
            return null;
        }

        var repoFromBase = FindRepoRoot(AppContext.BaseDirectory);
        if (!string.IsNullOrWhiteSpace(repoFromBase))
        {
            var repoConvos = Path.Combine(repoFromBase!, "Data", "conversations");
            if (Directory.Exists(repoConvos))
                return repoConvos;
        }

        var repoFromCwd = FindRepoRoot(Directory.GetCurrentDirectory());
        if (!string.IsNullOrWhiteSpace(repoFromCwd))
        {
            var repoConvos = Path.Combine(repoFromCwd!, "Data", "conversations");
            if (Directory.Exists(repoConvos))
                return repoConvos;
        }

        // 3) Environment variable
        var envDir = Environment.GetEnvironmentVariable("XM_CONVERSATIONS_DIR");
        if (!string.IsNullOrWhiteSpace(envDir))
            return envDir!;

        // 4) As another attempt, find a Data/conversations up the tree that is not under bin/ or obj/
        static string? TryFindDataExcludingBuildDirs(string startPath)
        {
            var dirInfo = new DirectoryInfo(startPath);
            for (int i = 0; i < 12 && dirInfo != null; i++)
            {
                var candidate = Path.Combine(dirInfo.FullName, "Data", "conversations");
                if (Directory.Exists(candidate))
                {
                    var full = Path.GetFullPath(candidate);
                    if (full.IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase) < 0 &&
                        full.IndexOf("/bin/", StringComparison.OrdinalIgnoreCase) < 0 &&
                        full.IndexOf("\\obj\\", StringComparison.OrdinalIgnoreCase) < 0 &&
                        full.IndexOf("/obj/", StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        return candidate;
                    }
                }
                dirInfo = dirInfo.Parent;
            }
            return null;
        }

        var nonBuild = TryFindDataExcludingBuildDirs(AppContext.BaseDirectory) ??
                       TryFindDataExcludingBuildDirs(Directory.GetCurrentDirectory());
        if (!string.IsNullOrWhiteSpace(nonBuild))
            return nonBuild!;

        // 5) Fallback to output-local copy
        return Path.Combine(AppContext.BaseDirectory, "Data", "conversations");
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