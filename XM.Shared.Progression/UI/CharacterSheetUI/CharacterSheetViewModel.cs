using System;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Inventory.Entity;
using XM.Inventory.KeyItem;
using XM.Progression.Event;
using XM.Progression.Job;
using XM.Progression.Job.Entity;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Progression.Stat.Entity;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Progression.UI.CharacterSheetUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class CharacterSheetViewModel : 
        ViewModel<CharacterSheetViewModel>,
        IRefreshable<StatEvent.PlayerHPAdjustedEvent>,
        IRefreshable<StatEvent.PlayerEPAdjustedEvent>
    {
        internal const string StatPartialId = "STAT_PARTIAL";
        internal const string MechPartialId = "MECH_PARTIAL";
        internal const string JobPartialId = "JOB_PARTIAL";
        internal const string SkillsPartialId = "SKILLS_PARTIAL";
        internal const string KeyItemsPartialId = "KEYITEMS_PARTIAL";
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

        public XMBindingList<string> JobNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> JobLevels
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> JobIcons
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }
        public XMBindingList<float> JobProgresses
        {
            get => Get<XMBindingList<float>>();
            set => Set(value);
        }

        public XMBindingList<string> SkillNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> SkillLevels
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> SkillIcons
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<float> SkillProgresses
        {
            get => Get<XMBindingList<float>>();
            set => Set(value);
        }

        public XMBindingList<NuiComboEntry> KeyItemCategories
        {
            get => Get<XMBindingList<NuiComboEntry>>();
            set => Set(value);
        }

        public int SelectedKeyItemCategory
        {
            get => Get<int>();
            set
            {
                Set(value);
                LoadKeyItems();
            }
        }

        public XMBindingList<string> KeyItemNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> KeyItemDescriptions
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public bool IsDisplayServerResetRemindersChecked
        {
            get => Get<bool>();
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

        [Inject]
        public KeyItemService KeyItem { get; set; }

        [Inject]
        public SkillService Skill { get; set; }


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
            RefreshHP();
            RefreshEP();
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

        private void RefreshHP()
        {
            HP = Stat.GetCurrentHP(Player) + " / " + Stat.GetMaxHP(Player);
            HPRegen = Stat.GetHPRegen(Player).ToString();
        }

        private void RefreshEP()
        {
            EP = Stat.GetCurrentEP(Player) + " / " + Stat.GetMaxEP(Player);
            EPRegen = Stat.GetEPRegen(Player).ToString();
        }

        private void LoadMechView()
        {
            ChangePartialView(MainView, MechPartialId);

        }

        private void LoadJobView()
        {
            ChangePartialView(MainView, JobPartialId);

            var playerId = PlayerId.Get(Player);
            var dbPlayerJob = DB.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            var allJobs = Job.GetAllJobDefinitions();

            var lvText = Locale.GetString(LocaleString.Lv);
            var jobNames = new XMBindingList<string>();
            var jobLevels = new XMBindingList<string>();
            var jobIcons = new XMBindingList<string>();
            var jobProgresses = new XMBindingList<float>();

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

        private void LoadSkillsView()
        {
            ChangePartialView(MainView, SkillsPartialId);

            var playerId = PlayerId.Get(Player);
            var dbPlayerSkill = DB.Get<PlayerSkill>(playerId);
            var job = Job.GetActiveJob(Player);
            var jobLevel = Stat.GetLevel(Player);

            var skillNames = new XMBindingList<string>();
            var skillIcons = new XMBindingList<string>();
            var skillLevels = new XMBindingList<string>();
            var skillProgresses = new XMBindingList<float>();

            var skills = Skill.GetAllSkillDefinitions();
            foreach (var skill in skills)
            {
                var level = 0;
                if (dbPlayerSkill.Skills.ContainsKey(skill.Type))
                    level = dbPlayerSkill.Skills[skill.Type];

                var grade = Skill.GetGrade(Player, job, skill);
                var skillCap = Skill.GetSkillCap(grade, jobLevel);
                if (skillCap <= 0)
                    continue;

                var progress = (float)level / (float)skillCap;

                skillNames.Add(skill.Name.ToLocalizedString());
                skillIcons.Add(skill.IconResref);
                skillLevels.Add($"{level} / {skillCap}");
                skillProgresses.Add(progress);
            }

            SkillNames = skillNames;
            SkillIcons = skillIcons;
            SkillLevels = skillLevels;
            SkillProgresses = skillProgresses;
        }

        private void LoadKeyItemsView()
        {
            ChangePartialView(MainView, KeyItemsPartialId);

            var categoryOptions = new XMBindingList<NuiComboEntry>();
            var categories = KeyItem.GetActiveCategories();

            var allCategoriesText = Locale.GetString(LocaleString.AllCategories);
            categoryOptions.Add(new NuiComboEntry(allCategoriesText, -1));
            foreach (var (type, detail) in categories)
            {
                if (!detail.IsActive)
                    continue;

                var text = Locale.GetString(detail.Name);
                var typeId = (int)type;
                categoryOptions.Add(new NuiComboEntry(text, typeId));
            }

            KeyItemCategories = categoryOptions;

            SelectedKeyItemCategory = -1;
            LoadKeyItems();
            WatchOnClient(model => model.SelectedKeyItemCategory);
        }

        private void LoadKeyItems()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerKeyItems = DB.Get<PlayerKeyItem>(playerId) ?? new PlayerKeyItem(playerId);

            var keyItemNames = new XMBindingList<string>();
            var keyItemDescriptions = new XMBindingList<string>();

            foreach (var (type, _) in dbPlayerKeyItems.KeyItems)
            {
                var detail = KeyItem.GetKeyItem(type);
                if (SelectedKeyItemCategory > -1 &&
                    detail.Category != (KeyItemCategoryType)SelectedKeyItemCategory)
                    continue;

                keyItemNames.Add(detail.Name.ToLocalizedString());
                keyItemDescriptions.Add(detail.Description.ToLocalizedString());
            }

            KeyItemNames = keyItemNames;
            KeyItemDescriptions = keyItemDescriptions;
        }

        private void LoadSettingsView()
        {
            ChangePartialView(MainView, SettingsPartialId);

            var playerId = PlayerId.Get(Player);
            var dbPlayerSettings = DB.Get<PlayerSettings>(playerId);
            IsDisplayServerResetRemindersChecked = dbPlayerSettings.DisplayServerResetReminders;

            WatchOnClient(model => model.IsDisplayServerResetRemindersChecked);
        }

        public Action OnChangeTab => () =>
        {
            switch (SelectedTab)
            {
                case 0: // 0 = Character
                    LoadCharacterView();
                    break;
                case 1: // 1 = Mech
                    LoadMechView();
                    break;
                case 2: // 2 = Job
                    LoadJobView();
                    break;
                case 3: // 3 = Skills
                    LoadSkillsView();
                    break;
                case 4: // 4 = Key Items
                    LoadKeyItemsView();
                    break;
                case 5: // 5 = Settings
                    LoadSettingsView();
                    break;
            }
        };

        public Action OnClickDisplayServerReminders => () =>
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerSettings = DB.Get<PlayerSettings>(playerId) ?? new PlayerSettings(playerId);

            dbPlayerSettings.DisplayServerResetReminders = IsDisplayServerResetRemindersChecked;
            DB.Set(dbPlayerSettings);
        };

        public Action OnClickQuests => () =>
        {
            Event.ExecuteScript(EventScript.OnXMPlayerOpenedQuestsMenuScript, Player);
        };
        public Action OnClickAppearance => () =>
        {
            Event.ExecuteScript(EventScript.OnXMPlayerOpenedAppearanceMenuScript, Player);
        };
        public Action OnClickOpenTrash => () =>
        {
            var location = GetLocation(Player);
            var trash = CreateObject(ObjectType.Placeable, "reo_trash_can", location);
            AssignCommand(Player, () => ActionInteractObject(trash));
            DelayCommand(0.2f, () => SetUseableFlag(trash, false));
        };

        public void Refresh(StatEvent.PlayerHPAdjustedEvent @event)
        {
            RefreshHP();
        }

        public void Refresh(StatEvent.PlayerEPAdjustedEvent @event)
        {
            RefreshEP();
        }
    }
}
