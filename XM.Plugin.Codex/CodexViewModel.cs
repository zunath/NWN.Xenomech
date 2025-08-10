using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using XM.Codex.Codex;
using XM.Codex.Codex.Model;
using XM.Shared.Core;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Codex
{
    [ServiceBinding(typeof(IViewModel))]
    internal class CodexViewModel : ViewModel<CodexViewModel>
    {
        private const string CodexFolderName = "codex";
        private static LocaleString MapCategoryToLocale(CodexCategory cat)
        {
            return cat switch
            {
                CodexCategory.Lore => LocaleString.Lore,
                CodexCategory.History => LocaleString.History,
                CodexCategory.Factions => LocaleString.Factions,
                CodexCategory.Regions => LocaleString.Regions,
                CodexCategory.Culture => LocaleString.CultureAndDailyLife,
                CodexCategory.Religion => LocaleString.ReligionsAndBeliefs,
                CodexCategory.Legends => LocaleString.MythsAndLegends,
                CodexCategory.Threats => LocaleString.MajorThreatsAndAdversaries,
                CodexCategory.Technology => LocaleString.Technology,
                CodexCategory.Systems => LocaleString.Systems,
                CodexCategory.Classes => LocaleString.Classes,
                _ => LocaleString.Miscellaneous,
            };
        }

        private sealed class CodexEntry
        {
            public LocaleString Title { get; init; }
            public CodexCategory Category { get; init; }
            public LocaleString Content { get; init; }
        }

        private readonly List<CodexEntry> _allEntries = new();
        private readonly List<CodexCategory> _categoryIndexToName = new();

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }

        public XMBindingList<NuiComboEntry> CategoryOptions
        {
            get => Get<XMBindingList<NuiComboEntry>>();
            set => Set(value);
        }

        public int SelectedCategory
        {
            get => Get<int>();
            set
            {
                Set(value);
                BuildTopics();
            }
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

        [Inject]
        public CodexContentService Content { get; set; }

        public override void OnOpen()
        {
            SelectedCategory = -1;
            SelectedTopic = -1;
            LoadEntries();
            BuildCategories();
            BuildTopics();
            WatchOnClient(m => m.SearchText);
            WatchOnClient(m => m.SelectedCategory);
        }

        public override void OnClose()
        {
            // No-op
        }

        private void LoadEntries()
        {
            _allEntries.Clear();

            var items = Content.GetAllEntries();
            foreach (var item in items)
            {
                _allEntries.Add(new CodexEntry
                {
                    Title = item.Title,
                    Category = item.Category,
                    Content = item.Content
                });
            }
        }

        private void BuildCategories()
        {
            var distinct = _allEntries.Select(e => e.Category).Distinct().OrderBy(s => s.ToString()).ToList();
            _categoryIndexToName.Clear();
            _categoryIndexToName.AddRange(distinct);

            var options = new XMBindingList<NuiComboEntry>();
            options.Add(new NuiComboEntry(Locale.GetString(LocaleString.AllCategories), -1));
            for (var i = 0; i < _categoryIndexToName.Count; i++)
            {
                var cat = _categoryIndexToName[i];
                options.Add(new NuiComboEntry(Locale.GetString(MapCategoryToLocale(cat)), i));
            }

            CategoryOptions = options;
        }

        private void BuildTopics()
        {
            var topics = new XMBindingList<string>();
            var toggles = new XMBindingList<bool>();

            IEnumerable<CodexEntry> query = _allEntries;
            if (SelectedCategory > -1 && SelectedCategory < _categoryIndexToName.Count)
            {
                var cat = _categoryIndexToName[SelectedCategory];
                query = query.Where(e => e.Category == cat);
            }

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var s = SearchText.Trim().ToLowerInvariant();
                query = query.Where(e => Locale.GetString(e.Title).ToLowerInvariant().Contains(s));
            }

            foreach (var e in query.OrderBy(e => Locale.GetString(e.Title)))
            {
                topics.Add(Locale.GetString(e.Title));
                toggles.Add(false);
            }

            TopicTitles = topics;
            TopicToggles = toggles;

            SelectedTopic = -1;
            TopicContent = LocaleString.SelectAQuest.ToLocalizedString();
        }

        public System.Action OnClearSearch() => () =>
        {
            SearchText = string.Empty;
            BuildTopics();
        };

        public System.Action OnSearch() => BuildTopics;

        public System.Action OnSelectCategory() => () =>
        {
            if (SelectedCategory > -1)
            {
                // no-op for combo based selection
            }

            var index = NuiGetEventArrayIndex();
            SelectedCategory = index;

            BuildTopics();
        };

        public System.Action OnSelectTopic() => () =>
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
            CodexCategory? selectedCat = null;
            if (SelectedCategory > -1 && SelectedCategory < _categoryIndexToName.Count)
            {
                selectedCat = _categoryIndexToName[SelectedCategory];
            }
            var entry = _allEntries.FirstOrDefault(e => Locale.GetString(e.Title) == title && (selectedCat == null || e.Category == selectedCat.Value));
            if (entry == null)
            {
                TopicContent = string.Empty;
                return;
            }

            try
            {
                TopicContent = NormalizeMarkdown(Locale.GetString(entry.Content));
            }
            catch
            {
                TopicContent = string.Empty;
            }
        }

        private static string PrettifyTitle(string name)
        {
            var s = name.Replace('-', ' ').Replace('_', ' ');
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s);
        }

        

        private static string NormalizeMarkdown(string text)
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            var normalized = new List<string>(lines.Length);
            foreach (var l in lines)
            {
                var line = l;
                line = line.TrimStart();
                if (line.StartsWith("#"))
                {
                    line = line.TrimStart('#', ' ');
                }
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


