using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class PowerAttack : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            PowerAttack1();
            PowerAttack2();

            return _builder.Build();
        }

        private void PowerAttack1()
        {
            _builder.Create(FeatType.PowerAttack1)
                .Name(LocaleString.PowerAttackI)
                .Description(LocaleString.PowerAttackIDescription)
                .ResonanceCost(1)
                .HasEquipAction(creature =>
                {
                    CreaturePlugin.AddFeatByLevel(creature, FeatType.PowerAttack, 1);
                })
                .HasUnequipAction(creature =>
                {
                    CreaturePlugin.RemoveFeatByLevel(creature, FeatType.PowerAttack, 1);
                });
        }
        private void PowerAttack2()
        {
            _builder.Create(FeatType.PowerAttack2)
                .Name(LocaleString.PowerAttackII)
                .Description(LocaleString.PowerAttackIIDescription)
                .ResonanceCost(2)
                .HasEquipAction(creature =>
                {
                    CreaturePlugin.AddFeatByLevel(creature, FeatType.ImprovedPowerAttack, 1);
                })
                .HasUnequipAction(creature =>
                {
                    CreaturePlugin.RemoveFeatByLevel(creature, FeatType.ImprovedPowerAttack, 1);
                });
        }
    }
}