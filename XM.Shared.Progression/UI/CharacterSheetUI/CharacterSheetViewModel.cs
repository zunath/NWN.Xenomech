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

        public string KeeperLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MenderLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string BrawlerLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string BeastmasterLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string TechweaverLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ElementalistLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string NightstalkerLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string HunterLevel
        {
            get => Get<string>();
            set => Set(value);
        }

        [Inject]
        public DBService DB { get; set; }

        public CharacterSheetViewModel()
        {
            SelectedTab = 0;
        }

        public override void OnOpen()
        {
            LoadJobData();

            WatchOnClient(model => model.SelectedTab);
        }

        public override void OnClose()
        {
            
        }

        private void LoadJobData()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerJob = DB.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);

            var lvText = Locale.GetString(LocaleString.Lv);
            KeeperLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Keeper]}";
            MenderLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Mender]}";
            BrawlerLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Brawler]}";
            BeastmasterLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Beastmaster]}";
            TechweaverLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Techweaver]}";
            ElementalistLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Elementalist]}";
            NightstalkerLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Nightstalker]}";
            HunterLevel = $"{lvText} {dbPlayerJob.JobLevels[JobType.Hunter]}";
        }

        public Action OnChangeTab => () =>
        {
            switch (SelectedTab)
            {
                case 0: // 0 = Stats
                    ChangePartialView(MainView, StatPartialId);
                    break;
                case 1: // 1 = Job
                    ChangePartialView(MainView, JobPartialId);
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
