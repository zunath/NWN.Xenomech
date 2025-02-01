using System;
using Anvil.Services;
using XM.Progression.Job;
using XM.UI;

namespace XM.Progression.UI.JobUI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class JobViewModel: ViewModel<JobViewModel>
    {
        [Inject]
        public JobService Job { get; set; }

        private const string PipLit = "icon_pip_lit";
        private const string PipUnlit = "icon_pip_unlit";
        private const string PipGrey = "icon_pip_grey";

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

        public override void OnOpen()
        {
            LoadActiveJob();
            RefreshPips();
        }

        private void LoadActiveJob()
        {
            var job = Job.GetActiveJob(Player);
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
            ResonancePip1 = PipLit;
            ResonancePip2 = PipLit;
            ResonancePip3 = PipLit;
            ResonancePip4 = PipLit;
            ResonancePip5 = PipLit;
            ResonancePip6 = PipUnlit;
            ResonancePip7 = PipUnlit;
            ResonancePip8 = PipUnlit;
            ResonancePip9 = PipGrey;
            ResonancePip10 = PipGrey;
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
        };

        public Action OnClickMender => () =>
        {
            ClearAllJobEncouragedFlags();
            IsMenderEncouraged = true;
        };

        public Action OnClickTechweaver => () =>
        {
            ClearAllJobEncouragedFlags();
            IsTechweaverEncouraged = true;

        };

        public Action OnClickBeastmaster => () =>
        {
            ClearAllJobEncouragedFlags();
            IsBeastmasterEncouraged = true;
        };

        public Action OnClickBrawler => () =>
        {
            ClearAllJobEncouragedFlags();
            IsBrawlerEncouraged = true;
        };

        public Action OnClickNightstalker => () =>
        {
            ClearAllJobEncouragedFlags();
            IsNightstalkerEncouraged = true;
        };

        public Action OnClickHunter => () =>
        {
            ClearAllJobEncouragedFlags();
            IsHunterEncouraged = true;
        };

        public Action OnClickElementalist => () =>
        {
            ClearAllJobEncouragedFlags();
            IsElementalistEncouraged = true;
        };

        public Action OnClickFilterKeeper => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsKeeperFilterEncouraged = true;
        };

        public Action OnClickFilterMender => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsMenderFilterEncouraged = true;
        };

        public Action OnClickFilterTechweaver => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsTechweaverFilterEncouraged = true;
        };

        public Action OnClickFilterBeastmaster => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsBeastmasterFilterEncouraged = true;
        };

        public Action OnClickFilterBrawler => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsBrawlerFilterEncouraged = true;
        };

        public Action OnClickFilterNightstalker => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsNightstalkerFilterEncouraged = true;
        };

        public Action OnClickFilterHunter => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsHunterFilterEncouraged = true;
        };

        public Action OnClickFilterElementalist => () =>
        {
            ClearAllJobFilterEncouragedFlags();
            IsElementalistFilterEncouraged = true;
        };

    }
}
