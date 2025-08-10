using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Anvil.Services;
using XM.Shared.Core;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.UI.Codex
{
    [ServiceBinding(typeof(IViewModel))]
    internal class CodexViewModel : ViewModel<CodexViewModel>
    {
        private const string CodexFolderName = "codex";

        private sealed class CodexEntry
        {
            public string Title { get; init; }
            public string Category { get; init; }
            public string FilePath { get; init; }
        }

        private readonly List<CodexEntry> _allEntries = new();

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }

        public XMBindingList<string> Categories
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> CategoryToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public int SelectedCategory
        {
            get => Get<int>();
            set => Set(value);
        }

        public XMBindingList<string> TopicTitles
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> TopicToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public int SelectedTopic
        {
            get => Get<int>();
            set => Set(value);
        }

        public string TopicContent
        {
            get => Get<string>();
            set => Set(value);
        }

        [Inject]
        public XMSettingsService Settings { get; set; }

        public override void OnOpen()
        {
            SelectedCategory = -1;
            SelectedTopic = -1;
            LoadEntries();
            BuildCategories();
            BuildTopics();
            WatchOnClient(m => m.SearchText);
        }

        public override void OnClose()
        {
            // No-op
        }

        private void LoadEntries()
        {
            _allEntries.Clear();

            var root = Settings.ResourcesDirectory;
            if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root))
            {
                TopicContent = LocaleString.Invalid.ToLocalizedString();
                return;
            }

            var codexPath = Path.Combine(root, CodexFolderName);
            if (!Directory.Exists(codexPath))
            {
                TopicContent = LocaleString.Invalid.ToLocalizedString();
                return;
            }

            foreach (var file in Directory.GetFiles(codexPath, "*.md"))
            {
                var title = TryReadTitleFromFile(file) ?? PrettifyTitle(Path.GetFileNameWithoutExtension(file));
                var category = DeriveCategoryFromFileName(Path.GetFileName(file));

                _allEntries.Add(new CodexEntry
                {
                    Title = title,
                    Category = category,
                    FilePath = file
                });
            }
        }

        private void BuildCategories()
        {
            var categories = new XMBindingList<string>();
            var toggles = new XMBindingList<bool>();

            var distinct = _allEntries.Select(e => e.Category).Distinct().OrderBy(s => s).ToList();
            foreach (var c in distinct)
            {
                categories.Add(c);
                toggles.Add(false);
            }

            Categories = categories;
            CategoryToggles = toggles;
        }

        private void BuildTopics()
        {
            var topics = new XMBindingList<string>();
            var toggles = new XMBindingList<bool>();

            IEnumerable<CodexEntry> query = _allEntries;
            if (SelectedCategory > -1 && SelectedCategory < Categories.Count)
            {
                var cat = Categories[SelectedCategory];
                query = query.Where(e => e.Category.Equals(cat, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var s = SearchText.Trim().ToLowerInvariant();
                query = query.Where(e => e.Title.ToLowerInvariant().Contains(s));
            }

            foreach (var e in query.OrderBy(e => e.Title))
            {
                topics.Add(e.Title);
                toggles.Add(false);
            }

            TopicTitles = topics;
            TopicToggles = toggles;

            SelectedTopic = -1;
            TopicContent = LocaleString.SelectAQuest.ToLocalizedString();
        }

        public Action OnClearSearch() => () =>
        {
            SearchText = string.Empty;
            BuildTopics();
        };

        public Action OnSearch() => BuildTopics;

        public Action OnSelectCategory() => () =>
        {
            if (SelectedCategory > -1)
            {
                CategoryToggles[SelectedCategory] = false;
            }

            var index = NuiGetEventArrayIndex();
            SelectedCategory = index;
            CategoryToggles[index] = true;

            BuildTopics();
        };

        public Action OnSelectTopic() => () =>
        {
            if (SelectedTopic > -1)
            {
                TopicToggles[SelectedTopic] = false;
            }

            var index = NuiGetEventArrayIndex();
            SelectedTopic = index;
            TopicToggles[index] = true;

            LoadSelectedTopicContent();
        };

        private void LoadSelectedTopicContent()
        {
            if (SelectedTopic < 0 || SelectedTopic >= TopicTitles.Count)
            {
                TopicContent = string.Empty;
                return;
            }

            var title = TopicTitles[SelectedTopic];
            var cat = SelectedCategory > -1 && SelectedCategory < Categories.Count ? Categories[SelectedCategory] : null;
            var entry = _allEntries.FirstOrDefault(e => e.Title == title && (cat == null || e.Category == cat));
            if (entry == null)
            {
                TopicContent = string.Empty;
                return;
            }

            try
            {
                var text = File.ReadAllText(entry.FilePath);
                TopicContent = NormalizeMarkdown(text);
            }
            catch
            {
                TopicContent = string.Empty;
            }
        }

        private static string TryReadTitleFromFile(string file)
        {
            try
            {
                using var reader = new StreamReader(file);
                for (var i = 0; i < 50 && !reader.EndOfStream; i++)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    if (line.StartsWith("#"))
                    {
                        return line.TrimStart('#', ' ', '\t');
                    }
                }
            }
            catch
            {
                // ignore
            }
            return null;
        }

        private static string PrettifyTitle(string name)
        {
            var s = name.Replace('-', ' ').Replace('_', ' ');
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s);
        }

        private static string DeriveCategoryFromFileName(string fileName)
        {
            var n = Path.GetFileNameWithoutExtension(fileName).ToLowerInvariant();
            if (n.Contains("history")) return "History";
            if (n.Contains("culture")) return "Culture";
            if (n.Contains("faction")) return "Factions";
            if (n.Contains("religion")) return "Religion";
            if (n.Contains("class")) return "Classes";
            if (n.Contains("mech")) return "Technology";
            if (n.Contains("teleport") || n.Contains("resonance")) return "Technology";
            if (n.Contains("myth")) return "Legends";
            if (n.Contains("hook")) return "Adventure Hooks";
            if (n.Contains("world") || n.Contains("overview") || n.Contains("conclusion") || n.Contains("project")) return "Overview";
            if (n.Contains("threat")) return "Threats";
            return "Misc";
        }

        private static string NormalizeMarkdown(string text)
        {
            // Basic normalization: strip leading markdown headers while keeping readable content
            var lines = text.Replace("\r\n", "\n").Split('\n');
            var normalized = new List<string>(lines.Length);
            foreach (var l in lines)
            {
                var line = l;
                // Remove markdown header symbols and list markers for readability
                line = line.TrimStart();
                line = line.StartsWith("#") ? line.TrimStart('#', ' ') : line;
                if (line.StartsWith("- ") || line.StartsWith("* ") || line.StartsWith("+ "))
                {
                    line = "• " + line.Substring(2);
                }
                normalized.Add(line);
            }
            return string.Join("\n", normalized);
        }
    }
}


