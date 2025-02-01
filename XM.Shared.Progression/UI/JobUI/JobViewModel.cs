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

        private const string PipLit = "icon_pip_lit";
        private const string PipUnlit = "icon_pip_unlit";
        private const string PipGrey = "icon_pip_grey";

        private readonly Color _green = new(0, 255, 0);
        private readonly Color _red = new(255, 0, 0);

        private JobType _selectedJob;
        private JobType _selectedJobFilter;
        private List<FeatType> _equippedAbilities = new();

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

        public XMBindingList<string> AbilityEquipUnequipTexts
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        private int _selectedToggle = -1;
        public XMBindingList<bool> AbilityToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public string SelectedAbilityDescription
        {
            get => Get<string>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            LoadJobLevels();
            LoadActiveJob();
            RefreshPips();
            RefreshAbilities();
            RefreshAbilityDetails();

            WatchOnClient(model => model.AbilityToggles);
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
            ClearAllJobFilterEncouragedFlags();

            switch (job.Type)
            {
                case JobType.Keeper:
                    IsKeeperEncouraged = true;
                    break;
                case JobType.Mender:
                    IsMenderEncouraged = true;
                    break;
                case JobType.Techweaver:
                    IsTechweaverEncouraged = true;
                    break;
                case JobType.Beastmaster:
                    IsBeastmasterEncouraged = true;
                    break;
                case JobType.Brawler:
                    IsBrawlerEncouraged = true;
                    break;
                case JobType.Nightstalker:
                    IsNightstalkerEncouraged = true;
                    break;
                case JobType.Hunter:
                    IsHunterEncouraged = true;
                    break;
                case JobType.Elementalist:
                    IsElementalistEncouraged = true;
                    break;
            }
        }

        private void RefreshPips()
        {
            var level = Stat.GetLevel(Player);
            var availablePips = level / JobService.ResonanceNodeLevelAcquisitionRate;

            if (availablePips < 1)
            {
                ResonancePip1 = PipGrey;
                ResonancePipTooltip1 = LocaleString.AvailableAtLevel5.ToLocalizedString();
            }
            else
            {
                ResonancePip1 = PipLit;
            }

            if (availablePips < 2)
            {
                ResonancePip2 = PipGrey;
                ResonancePipTooltip2 = LocaleString.AvailableAtLevel10.ToLocalizedString();
            }
            else
            {
                ResonancePip2 = PipLit;
            }

            if (availablePips < 3)
            {
                ResonancePip3 = PipGrey;
                ResonancePipTooltip3 = LocaleString.AvailableAtLevel15.ToLocalizedString();
            }
            else
            {
                ResonancePip3 = PipLit;
            }

            if (availablePips < 4)
            {
                ResonancePip4 = PipGrey;
                ResonancePipTooltip4 = LocaleString.AvailableAtLevel20.ToLocalizedString();
            }
            else
            {
                ResonancePip4 = PipLit;
            }

            if (availablePips < 5)
            {
                ResonancePip5 = PipGrey;
                ResonancePipTooltip5 = LocaleString.AvailableAtLevel25.ToLocalizedString();
            }
            else
            {
                ResonancePip5 = PipLit;
            }

            if (availablePips < 6)
            {
                ResonancePip6 = PipGrey;
                ResonancePipTooltip6 = LocaleString.AvailableAtLevel30.ToLocalizedString();
            }
            else
            {
                ResonancePip6 = PipLit;
            }

            if (availablePips < 7)
            {
                ResonancePip7 = PipGrey;
                ResonancePipTooltip7 = LocaleString.AvailableAtLevel35.ToLocalizedString();
            }
            else
            {
                ResonancePip7 = PipLit;
            }

            if (availablePips < 8)
            {
                ResonancePip8 = PipGrey;
                ResonancePipTooltip8 = LocaleString.AvailableAtLevel40.ToLocalizedString();
            }
            else
            {
                ResonancePip8 = PipLit;
            }

            if (availablePips < 9)
            {
                ResonancePip9 = PipGrey;
                ResonancePipTooltip9 = LocaleString.AvailableAtLevel45.ToLocalizedString();
            }
            else
            {
                ResonancePip9 = PipLit;
            }

            if (availablePips < 10)
            {
                ResonancePip10 = PipGrey;
                ResonancePipTooltip10 = LocaleString.AvailableAtLevel50.ToLocalizedString();
            }
            else
            {
                ResonancePip10 = PipLit;
            }
        }

        private void RefreshAbilities()
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
            var abilityEquipUnequipTexts = new XMBindingList<string>();
            var abilityToggles = new XMBindingList<bool>();

            foreach (var feat in feats)
            {
                var ability = Ability.GetAbilityDetail(feat);
                var levelAcquired = job.GetFeatAcquiredLevel(feat);

                abilityIcons.Add(ability.IconResref);
                abilityLevels.Add($"{LocaleString.Lv.ToLocalizedString()} {levelAcquired}");
                abilityNames.Add(ability.Name.ToLocalizedString());
                abilityColors.Add(level >= levelAcquired ? _green : _red);
                abilityEquipUnequipTexts.Add(_equippedAbilities.Contains(feat)
                    ? LocaleString.Unequip.ToLocalizedString()
                    : LocaleString.Equip.ToLocalizedString());
                abilityToggles.Add(false);
            }

            AbilityIcons = abilityIcons;
            AbilityLevels = abilityLevels;
            AbilityNames = abilityNames;
            AbilityColors = abilityColors;
            AbilityEquipUnequipTexts = abilityEquipUnequipTexts;
            AbilityToggles = abilityToggles;
        }

        private void RefreshAbilityDetails()
        {
            if (_selectedToggle <= -1)
                SelectedAbilityDescription = LocaleString.SelectAnAbilityFromTheList.ToLocalizedString();



        }

        private bool CanEquipAbility(FeatType type)
        {
            return false;
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
        private void ClearAllJobFilterEncouragedFlags()
        {
            IsKeeperFilterEncouraged = false;
            IsMenderFilterEncouraged = false;
            IsBrawlerFilterEncouraged = false;
            IsBeastmasterFilterEncouraged = false;
            IsElementalistFilterEncouraged = false;
            IsTechweaverFilterEncouraged = false;
            IsHunterFilterEncouraged = false;
            IsNightstalkerFilterEncouraged = false;
        }

        public Action OnClickKeeper => () =>
        {
            ClearAllJobEncouragedFlags();
            IsKeeperEncouraged = true;
            _selectedJob = JobType.Keeper;
        };

        public Action OnClickMender => () =>
        {
            ClearAllJobEncouragedFlags();
            IsMenderEncouraged = true;
            _selectedJob = JobType.Mender;
        };

        public Action OnClickTechweaver => () =>
        {
            ClearAllJobEncouragedFlags();
            IsTechweaverEncouraged = true;
            _selectedJob = JobType.Techweaver;

        };

        public Action OnClickBeastmaster => () =>
        {
            ClearAllJobEncouragedFlags();
            IsBeastmasterEncouraged = true;
            _selectedJob = JobType.Beastmaster;
        };

        public Action OnClickBrawler => () =>
        {
            ClearAllJobEncouragedFlags();
            IsBrawlerEncouraged = true;
            _selectedJob = JobType.Brawler;
        };

        public Action OnClickNightstalker => () =>
        {
            ClearAllJobEncouragedFlags();
            IsNightstalkerEncouraged = true;
            _selectedJob = JobType.Nightstalker;
        };

        public Action OnClickHunter => () =>
        {
            ClearAllJobEncouragedFlags();
            IsHunterEncouraged = true;
            _selectedJob = JobType.Hunter;
        };

        public Action OnClickElementalist => () =>
        {
            ClearAllJobEncouragedFlags();
            IsElementalistEncouraged = true;
            _selectedJob = JobType.Elementalist;
        };

        public Action OnClickFilterKeeper => () =>
        {
            var wasEncouraged = IsKeeperFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if(!wasEncouraged)
                IsKeeperFilterEncouraged = true;
            _selectedJobFilter = JobType.Keeper;
            RefreshAbilities();
        };

        public Action OnClickFilterMender => () =>
        {
            var wasEncouraged = IsMenderFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if (!wasEncouraged)
                IsMenderFilterEncouraged = true;
            _selectedJobFilter = JobType.Mender;
            RefreshAbilities();
        };

        public Action OnClickFilterTechweaver => () =>
        {
            var wasEncouraged = IsTechweaverFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if (!wasEncouraged)
                IsTechweaverFilterEncouraged = true;
            _selectedJobFilter = JobType.Techweaver;
            RefreshAbilities();
        };

        public Action OnClickFilterBeastmaster => () =>
        {
            var wasEncouraged = IsBeastmasterFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if (!wasEncouraged)
                IsBeastmasterFilterEncouraged = true;
            _selectedJobFilter = JobType.Beastmaster;
            RefreshAbilities();
        };

        public Action OnClickFilterBrawler => () =>
        {
            var wasEncouraged = IsBrawlerFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if (!wasEncouraged)
                IsBrawlerFilterEncouraged = true;
            _selectedJobFilter = JobType.Brawler;
            RefreshAbilities();
        };

        public Action OnClickFilterNightstalker => () =>
        {
            var wasEncouraged = IsNightstalkerFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if (!wasEncouraged)
                IsNightstalkerFilterEncouraged = true;
            _selectedJobFilter = JobType.Nightstalker;
            RefreshAbilities();
        };

        public Action OnClickFilterHunter => () =>
        {
            var wasEncouraged = IsHunterFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if (!wasEncouraged)
                IsHunterFilterEncouraged = true;
            _selectedJobFilter = JobType.Hunter;
            RefreshAbilities();
        };

        public Action OnClickFilterElementalist => () =>
        {
            var wasEncouraged = IsElementalistFilterEncouraged;
            ClearAllJobFilterEncouragedFlags();
            if(!wasEncouraged)
                IsElementalistFilterEncouraged = true;
            _selectedJobFilter = JobType.Elementalist;
            RefreshAbilities();
        };

        public Action OnClickAbility => () =>
        {
            if (_selectedToggle > -1)
                AbilityToggles[_selectedToggle] = false;

            _selectedToggle = NuiGetEventArrayIndex();
            RefreshAbilityDetails();
        };

        public Action OnEquipUnequipAbility => () =>
        {

        };

    }
}
