using System;
using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Entity;
using XM.Inventory.KeyItem;
using XM.Progression.Job;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Progression.UI.CharacterSheet
{
    [ServiceBinding(typeof(IViewModel))]
    [ServiceBinding(typeof(IRefreshable))]
    internal class CharacterSheetViewModel : 
        ViewModel<CharacterSheetViewModel>,
        IRefreshable
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

        public XMBindingList<Color> JobColors
        {
            get => Get<XMBindingList<Color>>();
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
                RefreshKeyItems();
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
            RecastReduction = Stat.GetRecastReduction(Player).ToString();

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
            RefreshJobs();
        }

        private void RefreshJobs()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerJob = DB.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            var allJobs = Job.GetAllJobDefinitions();
            var currentJob = Job.GetActiveJob(Player);

            var lvText = Locale.GetString(LocaleString.Lv);
            var jobNames = new XMBindingList<string>();
            var jobLevels = new XMBindingList<string>();
            var jobColors = new XMBindingList<Color>();
            var jobIcons = new XMBindingList<string>();
            var jobProgresses = new XMBindingList<float>();

            foreach (var (type, definition) in allJobs)
            {
                if (!definition.IsVisibleToPlayers)
                    continue;

                jobNames.Add(Locale.GetString(definition.Name));
                jobLevels.Add($"{lvText} {dbPlayerJob.JobLevels[type.Value]}");

                jobColors.Add(currentJob.Type == type 
                    ? new Color(255, 215, 0) 
                    : new Color(255, 255, 255));

                jobIcons.Add(definition.IconResref);

                var ratio = (float)dbPlayerJob.JobXP[type.Value] / (float)XP[dbPlayerJob.JobLevels[type.Value]];
                jobProgresses.Add(Math.Clamp(ratio, 0f, 1f));
            }

            JobNames = jobNames;
            JobLevels = jobLevels;
            JobColors = jobColors;
            JobIcons = jobIcons;
            JobProgresses = jobProgresses;
        }

        private void LoadSkillsView()
        {
            ChangePartialView(MainView, SkillsPartialId);

            RefreshSkills();
        }

        private void RefreshSkills()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerSkill = DB.Get<PlayerSkill>(playerId);
            var job = Job.GetActiveJob(Player);
            var jobLevel = Stat.GetLevel(Player);

            var skillNames = new XMBindingList<string>();
            var skillIcons = new XMBindingList<string>();
            var skillLevels = new XMBindingList<string>();
            var skillProgresses = new XMBindingList<float>();

            var skills = Skill.GetAllCombatSkillDefinitions();
            foreach (var skill in skills)
            {
                var level = 0;
                if (dbPlayerSkill.Skills.ContainsKey(skill.Type.Value))
                    level = dbPlayerSkill.Skills[skill.Type.Value];

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


            var dbPlayerCraft = DB.Get<PlayerCraft>(playerId);
            if (dbPlayerCraft.PrimaryCraftSkillCode != 0)
            {
                var primary = SkillType.FromValue(dbPlayerCraft.PrimaryCraftSkillCode);
                var skill = Skill.GetCraftSkillDefinition(primary);
                var level = Skill.GetCraftSkillLevel(Player, primary);
                var progress = (float)level / (float)skill.LevelCap;

                skillNames.Add(skill.Name.ToLocalizedString());
                skillIcons.Add(skill.IconResref);
                skillLevels.Add($"{level} / {skill.LevelCap}");
                skillProgresses.Add(progress);
            }

            if (dbPlayerCraft.SecondaryCraftSkillCode != 0)
            {
                var secondary = SkillType.FromValue(dbPlayerCraft.SecondaryCraftSkillCode);
                var skill = Skill.GetCraftSkillDefinition(secondary);
                var level = Skill.GetCraftSkillLevel(Player, secondary);
                var progress = (float)level / (float)skill.LevelCap;

                skillNames.Add(skill.Name.ToLocalizedString());
                skillIcons.Add(skill.IconResref);
                skillLevels.Add($"{level} / {skill.LevelCap}");
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
            RefreshKeyItems();
            WatchOnClient(model => model.SelectedKeyItemCategory);
        }

        private void RefreshKeyItems()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerKeyItems = DB.Get<PlayerKeyItem>(playerId) ?? new PlayerKeyItem(playerId);

            var keyItemNames = new XMBindingList<string>();
            var keyItemDescriptions = new XMBindingList<string>();

            foreach (var (typeId, _) in dbPlayerKeyItems.KeyItems)
            {
                var type = KeyItem.GetKeyItemTypeById(typeId);
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

        public Action OnChangeTab() => () =>
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

        public Action OnClickDisplayServerReminders() => () =>
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerSettings = DB.Get<PlayerSettings>(playerId) ?? new PlayerSettings(playerId);

            dbPlayerSettings.DisplayServerResetReminders = IsDisplayServerResetRemindersChecked;
            DB.Set(dbPlayerSettings);
        };

        public Action OnClickQuests() => () =>
        {
            Event.PublishEvent<XMEvent.OnPlayerOpenQuestsMenu>(Player);
        };
        public Action OnClickCodex() => () =>
        {
            Event.PublishEvent<XMEvent.OnPlayerOpenCodexMenu>(Player);
        };
        public Action OnClickAppearance() => () =>
        {
            Event.PublishEvent<XMEvent.OnPlayerOpenAppearanceMenu>(Player);
        };
        public Action OnClickOpenTrash() => () =>
        {
            var location = GetLocation(Player);
            var trash = CreateObject(ObjectType.Placeable, "reo_trash_can", location);
            AssignCommand(Player, () => ActionInteractObject(trash));
            DelayCommand(0.2f, () => SetUseableFlag(trash, false));
        };

        public void Refresh()
        {
            RefreshHP();
            RefreshEP();
            RefreshSkills();
            RefreshJobs();
            RefreshKeyItems();
        }
    }
}
