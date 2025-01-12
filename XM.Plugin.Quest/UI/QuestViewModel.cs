using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Quest.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.UI;

namespace XM.Quest.UI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class QuestViewModel :
        ViewModel<QuestViewModel>
    {
        private readonly List<string> _questIds = new();
        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }

        public XMBindingList<string> QuestNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public int SelectedQuest
        {
            get => Get<int>();
            set => Set(value);
        }

        public string ActiveQuestName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ActiveQuestDescription
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsAbandonQuestEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }


        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public QuestService Quest { get; set; }

        public override void OnOpen()
        {
            SearchText = string.Empty;

            Search();
            WatchOnClient(model => model.SearchText);
        }

        public override void OnClose()
        {
            
        }

        [ScriptHandler("bread_test")]
        public void Test()
        {
            Quest.AcceptQuest(Player, "goblin_test");
            Quest.AcceptQuest(Player, "goblin_test2");
            Quest.AcceptQuest(Player, "goblin_test3");
        }

        private void Search()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerQuest = DB.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

            var questNames = new XMBindingList<string>();

            foreach (var (questId, quest) in dbPlayerQuest.Quests)
            {

                // Ignore completed quests.
                if (quest.DateLastCompleted != null)
                    continue;

                var questDetail = Quest.GetQuestById(questId);
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    if (!questDetail.Name.ToLower().Contains(SearchText))
                        continue;
                }

                _questIds.Add(questId);
                questNames.Add(questDetail.Name);
            }

            QuestNames = questNames;
        }

        public Action OnClearSearch => () =>
        {
            
        };

        public Action OnSearch => Search;

        public Action OnAbandonQuest => () =>
        {

        };
    }
}
