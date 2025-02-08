using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anvil.Services;
using XM.Quest.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;
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

        public XMBindingList<bool> QuestToggles
        {
            get => Get<XMBindingList<bool>>();
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
            SelectedQuest = -1;

            Search();
            WatchOnClient(model => model.SearchText);
        }

        public override void OnClose()
        {
            
        }

        private void Search()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerQuest = DB.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

            var questNames = new XMBindingList<string>();
            var questToggles = new XMBindingList<bool>();

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
                questToggles.Add(false);
            }

            QuestNames = questNames;
            QuestToggles = questToggles;

            LoadQuest();
        }

        private void LoadQuest()
        {
            if (SelectedQuest > -1)
            {
                var questId = _questIds[SelectedQuest];
                var playerId = GetObjectUUID(Player);
                var dbPlayer = DB.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

                var dbPlayerQuest = dbPlayer.Quests[questId];
                var questDetail = Quest.GetQuestById(questId);

                ActiveQuestName = questDetail.Name;
                ActiveQuestDescription = BuildDescription(questDetail, dbPlayerQuest.CurrentState);
                IsAbandonQuestEnabled = true;
            }
            else
            {
                ActiveQuestName = LocaleString.SelectAQuest.ToLocalizedString();
                ActiveQuestDescription = LocaleString.SelectAQuest.ToLocalizedString();
                IsAbandonQuestEnabled = false;
            }
        }

        private string BuildDescription(QuestDetail questDetail, int currentState)
        {
            var sb = new StringBuilder();
            var state = questDetail.States[currentState];
            var objectives = state.GetObjectives().ToList();
            sb.Append(state.JournalText);

            if (objectives.Count > 0)
            {
                sb.Append("\n\n");
                sb.Append("Objectives:\n\n");

                foreach (var objective in state.GetObjectives())
                {
                    sb.Append(objective.GetCurrentStateText(Player, questDetail.QuestId));
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        public Action OnSelectQuest() => () =>
        {
            if (SelectedQuest > -1)
                QuestToggles[SelectedQuest] = false;

            var index = NuiGetEventArrayIndex();
            SelectedQuest = index;
            QuestToggles[index] = true;

            LoadQuest();
        };

        public Action OnClearSearch() => () =>
        {
            SearchText = string.Empty;
            Search();
        };

        public Action OnSearch() => Search;

        public Action OnAbandonQuest() => () =>
        {
            if (SelectedQuest <= -1)
                return;

            var questId = _questIds[SelectedQuest];
            ShowModal(LocaleString.AbandonQuestConfirmation.ToLocalizedString(), 
                () =>
            {
                Quest.AbandonQuest(Player, questId);

                _questIds.RemoveAt(SelectedQuest);
                QuestNames.RemoveAt(SelectedQuest);
                QuestToggles.RemoveAt(SelectedQuest);
                ActiveQuestName = LocaleString.SelectAQuest.ToLocalizedString();
                ActiveQuestDescription = LocaleString.SelectAQuest.ToLocalizedString();
                IsAbandonQuestEnabled = false;

                SelectedQuest = -1;
            });

        };
    }
}
