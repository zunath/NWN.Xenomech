using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Job;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Progression.UI.JobUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class JobViewModel: ViewModel<JobViewModel>
    {
        [Inject]
        public JobService Job { get; set; }

        [Inject]
        public StatService Stat { get; set; }

        [Inject]
        public AbilityService Ability { get; set; }

        internal const string AvailableAbilitiesPartialId = "AVAILABLE_ABILITIES_PARTIAL";
        internal const string EquippedAbilitiesPartialId = "EQUIPPED_ABILITIES_PARTIAL";
        internal const string MainView = "JOB_MAIN_VIEW";

        public const string PipLit = "icon_pip_lit";
        private const string PipUnlit = "icon_pip_unlit";
        private const string PipGrey = "icon_pip_grey";

        private readonly Color _white = new(255, 255, 255);
        private readonly Color _green = new(0, 255, 0);
        private readonly Color _red = new(255, 0, 0);

        private JobType _selectedJob;
        private JobType _selectedJobFilter;
        private int _availableNodes;
        private int _spentNodes;
        private readonly List<FeatType> _equippedAbilities = new();

        public string KeeperJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MenderJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string BrawlerJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string BeastmasterJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ElementalistJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string TechweaverJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string HunterJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public string NightstalkerJobLevel
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsKeeperEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsMenderEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsBrawlerEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsBeastmasterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsElementalistEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsTechweaverEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHunterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsNightstalkerEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsKeeperFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsMenderFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsBrawlerFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsBeastmasterFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsElementalistFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsTechweaverFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHunterFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsNightstalkerFilterEncouraged
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsKeeperFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsMenderFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsBrawlerFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsBeastmasterFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsElementalistFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsTechweaverFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHunterFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsNightstalkerFilterEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string ResonancePip1
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip2
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip3
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip4
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip5
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ResonancePip6
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip7
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip8
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip9
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePip10
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ResonancePipTooltip1
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip2
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip3
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip4
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip5
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ResonancePipTooltip6
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip7
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip8
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip9
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonancePipTooltip10
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResonanceNodeText
        {
            get => Get<string>();
            set => Set(value);
        }

        public int SelectedTab
        {
            get => Get<int>();
            set => Set(value);
        }

        public XMBindingList<string> AbilityIcons
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        } 
        public XMBindingList<string> AbilityLevels
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> AbilityNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<Color> AbilityColors
        {
            get => Get<XMBindingList<Color>>();
            set => Set(value);
        }

        private int _selectedAbilityIndex = -1;
        private readonly List<FeatType> _availableAbilityFeats = new();
        public XMBindingList<bool> AbilityToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public XMBindingList<bool> AbilityPip1Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<bool> AbilityPip2Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<bool> AbilityPip3Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<bool> AbilityPip4Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public string SelectedAbilityDescription
        {
            get => Get<string>();
            set => Set(value);
        }

        public string EquipUnequipButtonText
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsEquipUnequipEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }


        private int _selectedEquippedAbilityIndex = -1;
        public XMBindingList<bool> EquippedAbilityToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<string> EquippedAbilityIcons
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }
        
        public XMBindingList<string> EquippedAbilityLevels
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> EquippedAbilityNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }
        public XMBindingList<Color> EquippedAbilityColors
        {
            get => Get<XMBindingList<Color>>();
            set => Set(value);
        }

        public XMBindingList<bool> EquippedAbilityPip1Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<bool> EquippedAbilityPip2Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<bool> EquippedAbilityPip3Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<bool> EquippedAbilityPip4Enabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public bool IsUnequipAbilityEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            LoadJobLevels();
            LoadActiveJob();
            LoadAvailableAbilitiesView();
            RefreshPips();
            RefreshAbilitiesList();
            RefreshAbilityDetails();

            WatchOnClient(model => model.SelectedTab);
        }

        private void LoadJobLevels()
        {
            var jobs = Job.GetJobLevels(Player);
            string BuildJobLevel(JobType type)
            {
                return $"{LocaleString.Lv} {jobs[type]}";
            }

            KeeperJobLevel = BuildJobLevel(JobType.Keeper);
            MenderJobLevel = BuildJobLevel(JobType.Mender);
            BrawlerJobLevel = BuildJobLevel(JobType.Brawler);
            BeastmasterJobLevel = BuildJobLevel(JobType.Beastmaster);
            ElementalistJobLevel = BuildJobLevel(JobType.Elementalist);
            TechweaverJobLevel = BuildJobLevel(JobType.Techweaver);
            HunterJobLevel = BuildJobLevel(JobType.Hunter);
            NightstalkerJobLevel = BuildJobLevel(JobType.Nightstalker);
        }

        private void LoadActiveJob()
        {
            var job = Job.GetActiveJob(Player);
            _selectedJob = job.Type;
            ClearAllJobEncouragedFlags();
            RefreshAllJobFilterFlags();

            switch (job.Type)
            {
                case JobType.Keeper:
                    IsKeeperEncouraged = true;
                    IsKeeperFilterEnabled = false;
                    break;
                case JobType.Mender:
                    IsMenderEncouraged = true;
                    IsMenderFilterEnabled = false;
                    break;
                case JobType.Techweaver:
                    IsTechweaverEncouraged = true;
                    IsTechweaverFilterEnabled = false;
                    break;
                case JobType.Beastmaster:
                    IsBeastmasterEncouraged = true;
                    IsBeastmasterFilterEnabled = false;
                    break;
                case JobType.Brawler:
                    IsBrawlerEncouraged = true;
                    IsBrawlerFilterEnabled = false;
                    break;
                case JobType.Nightstalker:
                    IsNightstalkerEncouraged = true;
                    IsNightstalkerFilterEnabled = false;
                    break;
                case JobType.Hunter:
                    IsHunterEncouraged = true;
                    IsHunterFilterEnabled = false;
                    break;
                case JobType.Elementalist:
                    IsElementalistEncouraged = true;
                    IsElementalistFilterEnabled = false;
                    break;
            }
        }

        private void RefreshPips()
        {
            var level = Stat.GetLevel(Player);
            _availableNodes = level / JobService.ResonanceNodeLevelAcquisitionRate;
            var nodes = _availableNodes - _spentNodes;

            if (_availableNodes < 1)
            {
                ResonancePip1 = PipGrey;
                ResonancePipTooltip1 = LocaleString.AvailableAtLevel5.ToLocalizedString();
            }
            else
            {
                ResonancePip1 = nodes >= 1 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 2)
            {
                ResonancePip2 = PipGrey;
                ResonancePipTooltip2 = LocaleString.AvailableAtLevel10.ToLocalizedString();
            }
            else
            {
                ResonancePip2 = nodes >= 2 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 3)
            {
                ResonancePip3 = PipGrey;
                ResonancePipTooltip3 = LocaleString.AvailableAtLevel15.ToLocalizedString();
            }
            else
            {
                ResonancePip3 = nodes >= 3 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 4)
            {
                ResonancePip4 = PipGrey;
                ResonancePipTooltip4 = LocaleString.AvailableAtLevel20.ToLocalizedString();
            }
            else
            {
                ResonancePip4 = nodes >= 4 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 5)
            {
                ResonancePip5 = PipGrey;
                ResonancePipTooltip5 = LocaleString.AvailableAtLevel25.ToLocalizedString();
            }
            else
            {
                ResonancePip5 = nodes >= 5 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 6)
            {
                ResonancePip6 = PipGrey;
                ResonancePipTooltip6 = LocaleString.AvailableAtLevel30.ToLocalizedString();
            }
            else
            {
                ResonancePip6 = nodes >= 6 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 7)
            {
                ResonancePip7 = PipGrey;
                ResonancePipTooltip7 = LocaleString.AvailableAtLevel35.ToLocalizedString();
            }
            else
            {
                ResonancePip7 = nodes >= 7 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 8)
            {
                ResonancePip8 = PipGrey;
                ResonancePipTooltip8 = LocaleString.AvailableAtLevel40.ToLocalizedString();
            }
            else
            {
                ResonancePip8 = nodes >= 8 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 9)
            {
                ResonancePip9 = PipGrey;
                ResonancePipTooltip9 = LocaleString.AvailableAtLevel45.ToLocalizedString();
            }
            else
            {
                ResonancePip9 = nodes >= 9 ? PipLit : PipUnlit;
            }

            if (_availableNodes < 10)
            {
                ResonancePip10 = PipGrey;
                ResonancePipTooltip10 = LocaleString.AvailableAtLevel50.ToLocalizedString();
            }
            else
            {
                ResonancePip10 = nodes >= 10 ? PipLit : PipUnlit;
            }
        }

        private void ClearAbilitiesList()
        {
            if (_availableAbilityFeats.Count <= 0)
                return;

            AbilityIcons.Clear();
            AbilityLevels.Clear();
            AbilityNames.Clear();
            AbilityColors.Clear();
            AbilityToggles.Clear();
            AbilityPip1Enabled?.Clear();
            AbilityPip2Enabled?.Clear();
            AbilityPip3Enabled?.Clear();
            AbilityPip4Enabled?.Clear();
        }

        private void RefreshAbilitiesList()
        {
            if (_selectedJobFilter == JobType.Invalid)
                return;

            var feats = Ability.GetAbilityFeatsByJob(_selectedJobFilter);
            var job = Job.GetJobDefinition(_selectedJobFilter);
            var level = Job.GetJobLevel(Player, _selectedJob);

            var abilityIcons = new XMBindingList<string>();
            var abilityLevels = new XMBindingList<string>();
            var abilityNames = new XMBindingList<string>();
            var abilityColors = new XMBindingList<Color>();
            var abilityToggles = new XMBindingList<bool>();
            var abilityPip1Enabled = new XMBindingList<bool>();
            var abilityPip2Enabled = new XMBindingList<bool>();
            var abilityPip3Enabled = new XMBindingList<bool>();
            var abilityPip4Enabled = new XMBindingList<bool>();
            _availableAbilityFeats.Clear();

            foreach (var feat in feats)
            {
                var ability = Ability.GetAbilityDetail(feat);
                var levelAcquired = job.GetFeatAcquiredLevel(feat);
                var abilityName = BuildAbilityName(feat);

                _availableAbilityFeats.Add(feat);
                abilityIcons.Add(ability.IconResref);
                abilityLevels.Add($"{LocaleString.Lv.ToLocalizedString()} {levelAcquired}");
                abilityNames.Add(abilityName);
                abilityColors.Add(level >= levelAcquired ? _green : _red);
                abilityToggles.Add(false);

                abilityPip1Enabled.Add(ability.ResonanceCost >= 1);
                abilityPip2Enabled.Add(ability.ResonanceCost >= 2);
                abilityPip3Enabled.Add(ability.ResonanceCost >= 3);
                abilityPip4Enabled.Add(ability.ResonanceCost >= 4);
            }

            AbilityIcons = abilityIcons;
            AbilityLevels = abilityLevels;
            AbilityNames = abilityNames;
            AbilityColors = abilityColors;
            AbilityToggles = abilityToggles;
            AbilityPip1Enabled = abilityPip1Enabled;
            AbilityPip2Enabled = abilityPip2Enabled;
            AbilityPip3Enabled = abilityPip3Enabled;
            AbilityPip4Enabled = abilityPip4Enabled;
        }

        private string BuildAbilityName(FeatType feat)
        {
            var ability = Ability.GetAbilityDetail(feat);
            var abilityName = $"{ability.Name.ToLocalizedString()}";

            if (_equippedAbilities.Contains(feat))
            {
                abilityName += $" [{LocaleString.EQUIPPED}]";
            }

            return abilityName;
        }

        private void RefreshSelectedAbility()
        {
            if (_selectedAbilityIndex <= -1)
                return;

            var feat = _availableAbilityFeats[_selectedAbilityIndex];
            AbilityNames[_selectedAbilityIndex] = BuildAbilityName(feat);
        }

        private void RefreshAbilityDetails()
        {
            if (_selectedAbilityIndex <= -1)
            {
                SelectedAbilityDescription = LocaleString.SelectAnAbilityFromTheList.ToLocalizedString();
                EquipUnequipButtonText = LocaleString.Equip.ToLocalizedString();
                IsEquipUnequipEnabled = false;
            }
            else
            {
                var feat = _availableAbilityFeats[_selectedAbilityIndex];
                var detail = Ability.GetAbilityDetail(feat);

                SelectedAbilityDescription = detail.Description.ToLocalizedString();

                EquipUnequipButtonText = _equippedAbilities.Contains(feat) 
                    ? LocaleString.Unequip.ToLocalizedString() 
                    : LocaleString.Equip.ToLocalizedString();

                IsEquipUnequipEnabled = CanEquipAbility(feat) || CanUnequipAbility(feat);
            }
        }

        private bool CanEquipAbility(FeatType feat)
        {
            var level = Job.GetJobLevel(Player, _selectedJob);
            var filterJob = Job.GetJobDefinition(_selectedJobFilter);
            var levelAcquired = filterJob.GetFeatAcquiredLevel(feat);
            var ability = Ability.GetAbilityDetail(feat);

            if (_equippedAbilities.Contains(feat))
                return false;

            if (level < levelAcquired)
                return false;

            var remainingNodes = _availableNodes - _spentNodes;
            if (remainingNodes < ability.ResonanceCost)
                return false;

            return true;
        }

        private bool CanUnequipAbility(FeatType feat)
        {
            if (!_equippedAbilities.Contains(feat))
                return false;

            return true;
        }

        public override void OnClose()
        {

        }

        private void ClearAllJobEncouragedFlags()
        {
            IsKeeperEncouraged = false;
            IsMenderEncouraged = false;
            IsBrawlerEncouraged = false;
            IsBeastmasterEncouraged = false;
            IsElementalistEncouraged = false;
            IsTechweaverEncouraged = false;
            IsHunterEncouraged = false;
            IsNightstalkerEncouraged = false;
        }
        private void RefreshAllJobFilterFlags()
        {
            _selectedJobFilter = JobType.Invalid;

            IsKeeperFilterEncouraged = false;
            IsKeeperFilterEnabled = _selectedJob != JobType.Keeper;

            IsMenderFilterEncouraged = false;
            IsMenderFilterEnabled = _selectedJob != JobType.Mender;

            IsBrawlerFilterEncouraged = false;
            IsBrawlerFilterEnabled = _selectedJob != JobType.Brawler;

            IsBeastmasterFilterEncouraged = false;
            IsBeastmasterFilterEnabled = _selectedJob != JobType.Beastmaster;

            IsElementalistFilterEncouraged = false;
            IsElementalistFilterEnabled = _selectedJob != JobType.Elementalist;

            IsTechweaverFilterEncouraged = false;
            IsTechweaverFilterEnabled = _selectedJob != JobType.Techweaver;

            IsHunterFilterEncouraged = false;
            IsHunterFilterEnabled = _selectedJob != JobType.Hunter;

            IsNightstalkerFilterEncouraged = false;
            IsNightstalkerFilterEnabled = _selectedJob != JobType.Nightstalker;
        }

        public Action OnChangeTab => () =>
        {
            switch (SelectedTab)
            {
                case 0: // 0 = Available Abilities
                    LoadAvailableAbilitiesView();
                    break;
                case 1: // 1 = Equipped Abilities
                    LoadEquippedAbilitiesView();
                    break;
            }
        };

        private void LoadAvailableAbilitiesView()
        {
            ChangePartialView(MainView, AvailableAbilitiesPartialId);

            WatchOnClient(model => model.AbilityToggles);
        }

        private void LoadEquippedAbilitiesView()
        {
            ChangePartialView(MainView, EquippedAbilitiesPartialId);
            LoadEquippedAbilitiesList();
            _selectedEquippedAbilityIndex = -1;
            IsUnequipAbilityEnabled = false;
        }

        private void LoadEquippedAbilitiesList()
        {
            var equippedAbilityToggles = new XMBindingList<bool>();
            var equippedAbilityNames = new XMBindingList<string>();
            var equippedAbilityIcons = new XMBindingList<string>();
            var equippedAbilityLevels = new XMBindingList<string>();
            var equippedAbilityColors = new XMBindingList<Color>();
            var equippedAbilityPip1Enabled = new XMBindingList<bool>();
            var equippedAbilityPip2Enabled = new XMBindingList<bool>();
            var equippedAbilityPip3Enabled = new XMBindingList<bool>();
            var equippedAbilityPip4Enabled = new XMBindingList<bool>();

            var job = Job.GetJobDefinition(_selectedJobFilter);
            foreach (var feat in _equippedAbilities)
            {
                var ability = Ability.GetAbilityDetail(feat);
                var levelAcquired = job.GetFeatAcquiredLevel(feat);

                equippedAbilityToggles.Add(false);
                equippedAbilityNames.Add(ability.Name.ToLocalizedString());
                equippedAbilityIcons.Add(ability.IconResref);
                equippedAbilityLevels.Add($"{LocaleString.Lv.ToLocalizedString()} {levelAcquired}");
                equippedAbilityColors.Add(_white);

                equippedAbilityPip1Enabled.Add(ability.ResonanceCost >= 1);
                equippedAbilityPip2Enabled.Add(ability.ResonanceCost >= 2);
                equippedAbilityPip3Enabled.Add(ability.ResonanceCost >= 3);
                equippedAbilityPip4Enabled.Add(ability.ResonanceCost >= 4);
            }

            EquippedAbilityToggles = equippedAbilityToggles;
            EquippedAbilityNames = equippedAbilityNames;
            EquippedAbilityIcons = equippedAbilityIcons;
            EquippedAbilityLevels = equippedAbilityLevels;
            EquippedAbilityColors = equippedAbilityColors;
            EquippedAbilityPip1Enabled = equippedAbilityPip1Enabled;
            EquippedAbilityPip2Enabled = equippedAbilityPip2Enabled;
            EquippedAbilityPip3Enabled = equippedAbilityPip3Enabled;
            EquippedAbilityPip4Enabled = equippedAbilityPip4Enabled;
        }

        private void ClearEquippedAbilities()
        {
            EquippedAbilityIcons.Clear();
            EquippedAbilityLevels.Clear();
            EquippedAbilityNames.Clear();
            EquippedAbilityPip1Enabled.Clear();
            EquippedAbilityPip2Enabled.Clear();
            EquippedAbilityPip3Enabled.Clear();
            EquippedAbilityPip4Enabled.Clear();
            _equippedAbilities.Clear();
        }

        private void SelectNewJob(JobType type)
        {
            _selectedJob = type;
            _selectedAbilityIndex = -1;
            ClearAllJobEncouragedFlags();
            RefreshAllJobFilterFlags();
            ClearAbilitiesList();
            RefreshAbilityDetails();
            ClearEquippedAbilities();
        }

        public Action OnClickKeeper => () =>
        {
            SelectNewJob(JobType.Keeper);
            IsKeeperEncouraged = true;
            IsKeeperFilterEnabled = false;
        };

        public Action OnClickMender => () =>
        {
            SelectNewJob(JobType.Mender);
            IsMenderEncouraged = true;
            IsMenderFilterEnabled = false;
        };

        public Action OnClickTechweaver => () =>
        {
            SelectNewJob(JobType.Techweaver);
            IsTechweaverEncouraged = true;
            IsTechweaverFilterEnabled = false;
        };

        public Action OnClickBeastmaster => () =>
        {
            SelectNewJob(JobType.Beastmaster);
            IsBeastmasterEncouraged = true;
            IsBeastmasterFilterEnabled = false;
        };

        public Action OnClickBrawler => () =>
        {
            SelectNewJob(JobType.Brawler);
            IsBrawlerEncouraged = true;
            IsBrawlerFilterEnabled = false;
        };

        public Action OnClickNightstalker => () =>
        {
            SelectNewJob(JobType.Nightstalker);
            IsNightstalkerEncouraged = true;
            IsNightstalkerFilterEnabled = false;
        };

        public Action OnClickHunter => () =>
        {
            SelectNewJob(JobType.Hunter);
            IsHunterEncouraged = true;
            IsHunterFilterEnabled = false;
        };

        public Action OnClickElementalist => () =>
        {
            SelectNewJob(JobType.Elementalist);
            IsElementalistEncouraged = true;
            IsElementalistFilterEnabled = false;
        };

        public Action OnClickFilterKeeper => () =>
        {
            RefreshAllJobFilterFlags();
            IsKeeperFilterEncouraged = true;
            _selectedJobFilter = JobType.Keeper;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterMender => () =>
        {
            RefreshAllJobFilterFlags();
            IsMenderFilterEncouraged = true;
            _selectedJobFilter = JobType.Mender;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterTechweaver => () =>
        {
            RefreshAllJobFilterFlags();
            IsTechweaverFilterEncouraged = true;
            _selectedJobFilter = JobType.Techweaver;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterBeastmaster => () =>
        {
            RefreshAllJobFilterFlags();
            IsBeastmasterFilterEncouraged = true;
            _selectedJobFilter = JobType.Beastmaster;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterBrawler => () =>
        {
            RefreshAllJobFilterFlags();
            IsBrawlerFilterEncouraged = true;
            _selectedJobFilter = JobType.Brawler;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterNightstalker => () =>
        {
            RefreshAllJobFilterFlags();
            IsNightstalkerFilterEncouraged = true;
            _selectedJobFilter = JobType.Nightstalker;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterHunter => () =>
        {
            RefreshAllJobFilterFlags();
            IsHunterFilterEncouraged = true;
            _selectedJobFilter = JobType.Hunter;
            RefreshAbilitiesList();
        };

        public Action OnClickFilterElementalist => () =>
        {
            RefreshAllJobFilterFlags();
            IsElementalistFilterEncouraged = true;
            _selectedJobFilter = JobType.Elementalist;
            RefreshAbilitiesList();
        };

        public Action OnClickAbility => () =>
        {
            var index = NuiGetEventArrayIndex();

            if (_selectedAbilityIndex > -1)
                AbilityToggles[_selectedAbilityIndex] = false;

            if (_selectedAbilityIndex == index)
                index = -1;

            _selectedAbilityIndex = index;
            RefreshAbilityDetails();
        };

        public Action OnClickEquippedAbility => () =>
        {
            var index = NuiGetEventArrayIndex();

            if (_selectedEquippedAbilityIndex > -1)
                EquippedAbilityToggles[_selectedEquippedAbilityIndex] = false;

            if (_selectedEquippedAbilityIndex == index)
                index = -1;

            _selectedEquippedAbilityIndex = index;
            IsUnequipAbilityEnabled = _selectedEquippedAbilityIndex > -1;
        };

        public Action OnEquipUnequipAbility => () =>
        {
            if (_selectedAbilityIndex <= -1)
                return;

            var feat = _availableAbilityFeats[_selectedAbilityIndex];

            // Unequip
            if (_equippedAbilities.Contains(feat))
            {
                UnequipAbility(feat);
            }
            // Equip
            else
            {
                EquipAbility(feat);
            }

            RefreshPips();
            RefreshAbilityDetails();
            RefreshSelectedAbility();
        };

        private void UnequipAbility(FeatType feat)
        {
            var ability = Ability.GetAbilityDetail(feat);
            var nodes = ability.ResonanceCost;

            _spentNodes -= nodes;
            _equippedAbilities.Remove(feat);
        }

        private void EquipAbility(FeatType feat)
        {
            if (!CanEquipAbility(feat))
                return;

            var ability = Ability.GetAbilityDetail(feat);
            var nodes = ability.ResonanceCost;
            _spentNodes += nodes;
            _equippedAbilities.Add(feat);
        }

        public Action OnClickUnequipAbility => () =>
        {
            if (_selectedEquippedAbilityIndex < 0)
                return;

            var feat = _equippedAbilities[_selectedEquippedAbilityIndex];
            UnequipAbility(feat);

            EquippedAbilityIcons.RemoveAt(_selectedEquippedAbilityIndex);
            EquippedAbilityLevels.RemoveAt(_selectedEquippedAbilityIndex);
            EquippedAbilityNames.RemoveAt(_selectedEquippedAbilityIndex);
            EquippedAbilityPip1Enabled.RemoveAt(_selectedEquippedAbilityIndex);
            EquippedAbilityPip2Enabled.RemoveAt(_selectedEquippedAbilityIndex);
            EquippedAbilityPip3Enabled.RemoveAt(_selectedEquippedAbilityIndex);
            EquippedAbilityPip4Enabled.RemoveAt(_selectedEquippedAbilityIndex);

            RefreshPips();
            RefreshAbilityDetails();
            RefreshSelectedAbility();
        };

        public Action OnClickChangeJob => () =>
        {
            
        };

    }
}
