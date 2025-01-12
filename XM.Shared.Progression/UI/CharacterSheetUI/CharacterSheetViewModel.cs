using System;
using Anvil.Services;
using XM.Progression.Job;
using XM.Progression.Job.Entity;
using XM.Progression.Stat;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.Event;
using XM.Shared.API.Constants;
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

        public string CharacterName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string PortraitResref
        {
            get => Get<string>();
            set => Set(value);
        }

        public string HP
        {
            get => Get<string>();
            set => Set(value);
        }
        public string HPRegen
        {
            get => Get<string>();
            set => Set(value);
        }
        public string EP
        {
            get => Get<string>();
            set => Set(value);
        }
        public string EPRegen
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Might
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Perception
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Vitality
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Willpower
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Agility
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Social
        {
            get => Get<string>();
            set => Set(value);
        }
        public string RecastReduction
        {
            get => Get<string>();
            set => Set(value);
        }


        public string MainHand
        {
            get => Get<string>();
            set => Set(value);
        }
        public string OffHand
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Attack
        {
            get => Get<string>();
            set => Set(value);
        }
        public string EtherAttack
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Accuracy
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Evasion
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Defense
        {
            get => Get<string>();
            set => Set(value);
        }
        public string FireResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string IceResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string EarthResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string WindResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string WaterResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string LightningResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MindResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string LightResist
        {
            get => Get<string>();
            set => Set(value);
        }
        public string DarknessResist
        {
            get => Get<string>();
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

        [Inject]
        public StatService Stat { get; set; }

        public CharacterSheetViewModel()
        {
            SelectedTab = 0;
        }

        public override void OnOpen()
        {
            LoadCharacterView();
            WatchOnClient(model => model.SelectedTab);
        }

        public override void OnClose()
        {
            
        }

        private void LoadCharacterView()
        {
            ChangePartialView(MainView, StatPartialId);

            CharacterName = GetName(Player);
            PortraitResref = GetPortraitResRef(Player) + "l";
            HP = Stat.GetCurrentHP(Player) + " / " + Stat.GetMaxHP(Player);
            HPRegen = Stat.GetHPRegen(Player).ToString();
            EP = Stat.GetCurrentEP(Player) + " / " + Stat.GetMaxEP(Player);
            EPRegen = Stat.GetEPRegen(Player).ToString();
            Might = Stat.GetAttribute(Player, AbilityType.Might).ToString();
            Perception = Stat.GetAttribute(Player, AbilityType.Perception).ToString();
            Vitality = Stat.GetAttribute(Player, AbilityType.Vitality).ToString();
            Willpower = Stat.GetAttribute(Player, AbilityType.Willpower).ToString();
            Agility = Stat.GetAttribute(Player, AbilityType.Agility).ToString();
            Social = Stat.GetAttribute(Player, AbilityType.Social).ToString();
            RecastReduction = Stat.GetAbilityRecastReduction(Player).ToString();

            MainHand = $"{Stat.GetMainHandDMG(Player)} {Locale.GetString(LocaleString.DMG)}";
            OffHand = $"{Stat.GetOffHandDMG(Player)} {Locale.GetString(LocaleString.DMG)}";
            Attack = Stat.GetAttack(Player).ToString();
            EtherAttack = Stat.GetEtherAttack(Player).ToString();
            Accuracy = Stat.GetAccuracy(Player).ToString();
            Evasion = Stat.GetEvasion(Player).ToString();
            Defense = Stat.GetDefense(Player).ToString();

            FireResist = Stat.GetResist(Player, ResistType.Fire) + "%";
            IceResist = Stat.GetResist(Player, ResistType.Ice) + "%";
            EarthResist = Stat.GetResist(Player, ResistType.Earth) + "%";
            WindResist = Stat.GetResist(Player, ResistType.Wind) + "%";
            LightningResist = Stat.GetResist(Player, ResistType.Lightning) + "%";
            LightResist = Stat.GetResist(Player, ResistType.Light) + "%";
            DarknessResist = Stat.GetResist(Player, ResistType.Darkness) + "%";
            WaterResist = Stat.GetResist(Player, ResistType.Water) + "%";
            MindResist = Stat.GetResist(Player, ResistType.Mind) + "%";
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

        private void LoadSettingsView()
        {
            ChangePartialView(MainView, SettingsPartialId);
        }

        public Action OnChangeTab => () =>
        {
            switch (SelectedTab)
            {
                case 0: // 0 = Character
                    LoadCharacterView();
                    break;
                case 1: // 1 = Job
                    LoadJobView();
                    break;
                case 2: // 2 = Settings
                    LoadSettingsView();
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
