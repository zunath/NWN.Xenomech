using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Beast.BeastDefinition
{
    public interface IBeastDefinition
    {
        public BeastType Type { get; }
        public int LevelRequired { get; }
        public AppearanceType Appearance { get; }
        public float Scale { get; }
        public int PortraitId { get; }
        public int SoundSetId { get; }
        public LocaleString Name { get; }
        public StatGrade Grades { get; }
        public List<FeatType> Feats { get; }
    }
}
