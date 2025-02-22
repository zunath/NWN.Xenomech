using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Beast.BeastDefinition;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core.Localization;

namespace XM.Progression.Beast
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

        private BeastType GetBeastType(uint beast)
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
            ApplyStats(activator, beast, definition);
            ApplyFeats(activator, beast, definition);

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
            var skin = GetItemInSlot(InventorySlotType.CreatureArmor, beast);
            var claw = GetItemInSlot(InventorySlotType.CreatureWeaponLeft, beast);

            var level = _stat.GetLevel(activator);
            var dmg = _stat.CalculateDMG(level, definition.Grades.DMG);
            var hp = _stat.CalculateHP(level, definition.Grades.MaxHP);
            var ep = _stat.CalculateHP(level, definition.Grades.MaxEP);
            var might = _stat.CalculateStat(level, definition.Grades.Might);
            var perception = _stat.CalculateStat(level, definition.Grades.Perception);
            var vitality = _stat.CalculateStat(level, definition.Grades.Vitality);
            var willpower = _stat.CalculateStat(level, definition.Grades.Willpower);
            var agility = _stat.CalculateStat(level, definition.Grades.Agility);
            var social = _stat.CalculateStat(level, definition.Grades.Social);

            BiowareXP2.IPSafeAddItemProperty(skin, ItemPropertyCustom(ItemPropertyType.NPCLevel, -1, level), 0f, AddItemPropertyPolicy.ReplaceExisting, false, false);
            BiowareXP2.IPSafeAddItemProperty(skin, ItemPropertyCustom(ItemPropertyType.EP, -1, ep), 0f, AddItemPropertyPolicy.ReplaceExisting, false, false);

            BiowareXP2.IPSafeAddItemProperty(claw, ItemPropertyCustom(ItemPropertyType.DMG, -1, dmg), 0f, AddItemPropertyPolicy.ReplaceExisting, false, false);

            ObjectPlugin.SetMaxHitPoints(beast, hp);
            CreaturePlugin.SetRawAbilityScore(beast, AbilityType.Might, might);
            CreaturePlugin.SetRawAbilityScore(beast, AbilityType.Perception, perception);
            CreaturePlugin.SetRawAbilityScore(beast, AbilityType.Vitality, vitality);
            CreaturePlugin.SetRawAbilityScore(beast, AbilityType.Willpower, willpower);
            CreaturePlugin.SetRawAbilityScore(beast, AbilityType.Agility, agility);
            CreaturePlugin.SetRawAbilityScore(beast, AbilityType.Social, social);
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