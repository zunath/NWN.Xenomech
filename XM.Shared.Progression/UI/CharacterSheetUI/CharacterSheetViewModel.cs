using System;
using Anvil.Services;
using XM.Progression.Job;
using XM.Progression.Job.Entity;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.Event;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Progression.UI.CharacterSheetUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class CharacterSheetViewModel : 
        ViewModel<CharacterSheetViewModel>,
        IRefreshable<PlayerHPAdjustedEvent>,
        IRefreshable<PlayerEPAdjustedEvent>
    {
        internal const string StatPartialId = "STAT_PARTIAL";
        internal const string JobPartialId = "JOB_PARTIAL";
        internal const string SettingsPartialId = "SETTINGS_PARTIAL";
        internal const string MainView = "MAIN_VIEW";

        public int SelectedTab
        {
            get => Get<int>();
            set => Set(value);
        }

        public GuiBindingList<string> JobNames
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public GuiBindingList<string> JobLevels
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public GuiBindingList<string> JobIcons
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }
        public GuiBindingList<float> JobProgresses
        {
            get => Get<GuiBindingList<float>>();
            set => Set(value);
        }


        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public JobService Job { get; set; }

        [Inject]
        public XPChart XP { get; set; }

        public CharacterSheetViewModel()
        {
            SelectedTab = 0;
        }

        public override void OnOpen()
        {
            

            WatchOnClient(model => model.SelectedTab);
        }

        public override void OnClose()
        {
            
        }

        private void LoadJobView()
        {
            ChangePartialView(MainView, JobPartialId);

            var playerId = PlayerId.Get(Player);
            var dbPlayerJob = DB.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            var allJobs = Job.GetAllJobDefinitions();

            var lvText = Locale.GetString(LocaleString.Lv);
            var jobNames = new GuiBindingList<string>();
            var jobLevels = new GuiBindingList<string>();
            var jobIcons = new GuiBindingList<string>();
            var jobProgresses = new GuiBindingList<float>();

            foreach (var (job, definition) in allJobs)
            {
                if(!definition.IsVisibleToPlayers)
                    continue;

                jobNames.Add(Locale.GetString(definition.Name));
                jobLevels.Add($"{lvText} {dbPlayerJob.JobLevels[job]}");
                jobIcons.Add(definition.IconResref);

                var ratio = dbPlayerJob.JobXP[job] / XP[dbPlayerJob.JobLevels[job]];
                jobProgresses.Add(Math.Clamp(ratio, 0f, 1f));
            }
            
            JobNames = jobNames;
            JobLevels = jobLevels;
            JobIcons = jobIcons;
            JobProgresses = jobProgresses;
        }

        public Action OnChangeTab => () =>
        {
            switch (SelectedTab)
            {
                case 0: // 0 = Stats
                    ChangePartialView(MainView, StatPartialId);
                    break;
                case 1: // 1 = Job
                    LoadJobView();
                    break;
                case 2: // 2 = Settings
                    ChangePartialView(MainView, SettingsPartialId);
                    break;
            }
        };

        public Action OnClickQuests => () =>
        {

        };
        public Action OnClickAppearance => () =>
        {

        };
        public Action OnClickKeyItems => () =>
        {

        };
        public Action OnClickOpenTrash => () =>
        {

        };

        public void Refresh(PlayerHPAdjustedEvent @event)
        {
            
        }

        public void Refresh(PlayerEPAdjustedEvent @event)
        {
            
        }
    }
}
