using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Beast.BeastDefinition;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast
{
    [ServiceBinding(typeof(BeastService))]
    [ServiceBinding(typeof(IInitializable))]
    public class BeastService: IInitializable
    {
        private const string BeastResref = "pc_beast";

        private const string BeastTypeVariable = "BEAST_TYPE";

        private readonly IList<IBeastDefinition> _definitions;
        private readonly Dictionary<BeastType, IBeastDefinition> _beasts = new();
        private readonly StatService _stat;

        public BeastService(
            IList<IBeastDefinition> definitions,
            StatService stat)
        {
            _definitions = definitions;
            _stat = stat;
        }

        public void Init()
        {
            foreach (var definition in _definitions)
            {
                _beasts[definition.Type] = definition;
            }
        }

        public bool HasSicAbilities(BeastType type)
        {
            var definition = _beasts[type];

            return definition.Feats.Count > 0;
        }

        public FeatType GetRandomSicAbility(BeastType type)
        {
            var definition = _beasts[type];

            if (definition.Feats.Count <= 0)
                return FeatType.Invalid;

            var index = XMRandom.Next(definition.Feats.Count);
            return definition.Feats[index];
        }

        public BeastType GetBeastType(uint beast)
        {
            return (BeastType)GetLocalInt(beast, BeastTypeVariable);
        }

        private void SetBeastType(uint beast, BeastType type)
        {
            SetLocalInt(beast, BeastTypeVariable, (int)type);
        }

        public uint GetBeast(uint activator)
        {
            var beast = GetHenchman(activator);
            if (!GetIsObjectValid(beast))
                return OBJECT_INVALID;

            var beastType = GetBeastType(beast);
            if (beastType == BeastType.Invalid)
                return OBJECT_INVALID;

            return beast;
        }

        public bool IsBeast(uint creature)
        {
            return GetBeastType(creature) != BeastType.Invalid;
        }

        public string CanSummonBeast(uint activator, BeastType beastType)
        {
            if (!GetHasFeat(FeatType.CallBeast, activator))
                return LocaleString.YouDoNotHaveTheCallBeastAbility.ToLocalizedString();

            var level = _stat.GetLevel(activator);
            var definition = _beasts[beastType];

            if (level < definition.LevelRequired)
                return LocaleString.InsufficientLevelRequiredX.ToLocalizedString(definition.LevelRequired);

            var currentBeast = GetBeast(activator);
            if (GetIsObjectValid(currentBeast) || GetIsObjectValid(GetHenchman(activator)))
                return LocaleString.OnlyOneCompanionMayBeActiveAtATime.ToLocalizedString();

            return string.Empty;
        }

        public void SummonBeast(uint activator, BeastType beastType)
        {
            if (GetIsObjectValid(GetAssociate(AssociateType.Henchman, activator)))
            {
                var message = LocaleString.OnlyOneCompanionMayBeActiveAtATime.ToLocalizedString();
                SendMessageToPC(activator, message);
                return;
            }

            var definition = _beasts[beastType];
            var beast = CreateObject(ObjectType.Creature, BeastResref, GetLocation(activator));

            SetName(beast, definition.Name.ToLocalizedString());
            SetBeastType(beast, beastType);

            ApplyAppearance(beast, definition);
            ApplyFeats(activator, beast, definition);
            ApplyStats(activator, beast, definition);

            AddHenchman(activator, beast);

            RestoreHP(beast);
            _stat.RestoreEP(beast, _stat.GetMaxEP(beast));
        }

        private void ApplyAppearance(uint beast, IBeastDefinition definition)
        {
            SetCreatureAppearanceType(beast, definition.Appearance);
            SetObjectVisualTransform(beast, ObjectVisualTransformType.Scale, definition.Scale);
            SetPortraitId(beast, definition.PortraitId);
            SetSoundset(beast, definition.SoundSetId);
        }

        private void ApplyStats(uint activator, uint beast, IBeastDefinition definition)
        {
            var level = _stat.GetLevel(activator);

            _stat.SetNPCStats(beast, level, definition.AttackDelay, definition.Grades);
        }

        private void ApplyFeats(uint activator, uint beast, IBeastDefinition definition)
        {
            foreach (var feat in definition.Feats)
            {
                CreaturePlugin.AddFeatByLevel(beast, feat, 1);
            }

            if (GetHasFeat(FeatType.BeastSpeed, activator))
            {
                CreaturePlugin.AddFeatByLevel(beast, FeatType.BeastSpeed, 1);
            }

            if (GetHasFeat(FeatType.EtherLink, activator))
            {
                CreaturePlugin.AddFeatByLevel(beast, FeatType.EtherLink, 1);
            }
        }

        private void RestoreHP(uint beast)
        {
            AssignCommand(GetModule(), () =>
            {
                DelayCommand(4f, () =>
                {
                    var healHP = GetMaxHitPoints(beast);
                    ApplyEffectToObject(DurationType.Instant, EffectHeal(healHP), beast);
                });
            });
        }
    }
}