using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechLeftArmPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPart> BuildMechParts()
        {
            DestroyerLAssaultArm();
            ScalpelLPrecisionArm();
            AegisLShieldArm();
            HermesLUtilityArm();
            ColossusLCrusherArm();
            ApolloLSurgicalArm();
            QuicksilverLReactiveArm();
            AtlasLTitanArm();
            WraithLPhantomArm();
            AresLBerserkerArm();
            ViperLStrikeArm();
            ThorLThunderArm();
            HadesLInfernoArm();
            PoseidonLHydroArm();
            ArtemisLHunterArm();
            DemeterLGrowthArm();
            HephaestusLForgeArm();
            LokiLTricksterArm();
            OdinLWisdomArm();
            FenrirLBeastArm();

            return _builder.Build();
        }

        private void DestroyerLAssaultArm()
        {
            _builder.Create("destroyer_larm", MechPartType.LeftArm)
                .HPPercent(12)
                .AttackPercent(20)
                .DefensePercent(12)
                .AccuracyPercent(5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(10);
        }

        private void ScalpelLPrecisionArm()
        {
            _builder.Create("scalpel_larm", MechPartType.LeftArm)
                .HPPercent(-5)
                .FuelPercent(5)
                .AttackPercent(10)
                .EtherAttackPercent(15)
                .AccuracyPercent(25)
                .EvasionPercent(10)
                .FuelConsumptionPercent(-5);
        }

        private void AegisLShieldArm()
        {
            _builder.Create("aegis_larm", MechPartType.LeftArm)
                .HPPercent(5)
                .AttackPercent(-10)
                .DefensePercent(25)
                .EtherDefensePercent(15)
                .AccuracyPercent(-5)
                .EvasionPercent(5)
                .FuelConsumptionPercent(5);
        }

        private void HermesLUtilityArm()
        {
            _builder.Create("hermes_larm", MechPartType.LeftArm)
                .FuelPercent(15)
                .EtherAttackPercent(5)
                .DefensePercent(5)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(-10);
        }

        private void ColossusLCrusherArm()
        {
            _builder.Create("colossus_larm", MechPartType.LeftArm)
                .HPPercent(20)
                .AttackPercent(30)
                .EtherAttackPercent(-5)
                .DefensePercent(10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(25);
        }

        private void ApolloLSurgicalArm()
        {
            _builder.Create("apollo_larm", MechPartType.LeftArm)
                .HPPercent(-10)
                .FuelPercent(12)
                .AttackPercent(5)
                .EtherAttackPercent(20)
                .EtherDefensePercent(10)
                .AccuracyPercent(30)
                .EvasionPercent(15)
                .FuelConsumptionPercent(-12);
        }

        private void QuicksilverLReactiveArm()
        {
            _builder.Create("quicksilver_larm", MechPartType.LeftArm)
                .HPPercent(2)
                .FuelPercent(2)
                .AttackPercent(15)
                .EtherAttackPercent(10)
                .DefensePercent(17)
                .EtherDefensePercent(10)
                .AccuracyPercent(10)
                .EvasionPercent(20)
                .FuelConsumptionPercent(3);
        }

        private void AtlasLTitanArm()
        {
            _builder.Create("atlas_larm", MechPartType.LeftArm)
                .HPPercent(27)
                .AttackPercent(25)
                .DefensePercent(22)
                .EtherDefensePercent(-5)
                .AccuracyPercent(-5)
                .EvasionPercent(-25)
                .FuelConsumptionPercent(35);
        }

        private void WraithLPhantomArm()
        {
            _builder.Create("wraith_larm", MechPartType.LeftArm)
                .HPPercent(-15)
                .FuelPercent(15)
                .AttackPercent(10)
                .EtherAttackPercent(15)
                .DefensePercent(-10)
                .EtherDefensePercent(5)
                .AccuracyPercent(15)
                .EvasionPercent(30)
                .FuelConsumptionPercent(-13);
        }

        private void AresLBerserkerArm()
        {
            _builder.Create("ares_larm", MechPartType.LeftArm)
                .HPPercent(15)
                .AttackPercent(35)
                .EtherAttackPercent(5)
                .DefensePercent(10)
                .EtherDefensePercent(-10)
                .AccuracyPercent(5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(45);
        }

        private void ViperLStrikeArm()
        {
            _builder.Create("viper_larm", MechPartType.LeftArm)
                .FuelPercent(12)
                .AttackPercent(25)
                .EtherAttackPercent(10)
                .AccuracyPercent(20)
                .EvasionPercent(25)
                .FuelConsumptionPercent(-7);
        }

        private void ThorLThunderArm()
        {
            _builder.Create("thor_larm", MechPartType.LeftArm)
                .HPPercent(22)
                .AttackPercent(40)
                .EtherAttackPercent(20)
                .DefensePercent(17)
                .EtherDefensePercent(-5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(45);
        }

        private void HadesLInfernoArm()
        {
            _builder.Create("hades_larm", MechPartType.LeftArm)
                .HPPercent(-5)
                .AttackPercent(30)
                .EtherAttackPercent(25)
                .DefensePercent(25)
                .EtherDefensePercent(10)
                .AccuracyPercent(10)
                .FuelConsumptionPercent(45);
        }

        private void PoseidonLHydroArm()
        {
            _builder.Create("poseidon_larm", MechPartType.LeftArm)
                .HPPercent(5)
                .FuelPercent(27)
                .AttackPercent(15)
                .EtherAttackPercent(30)
                .DefensePercent(-5)
                .EtherDefensePercent(20)
                .AccuracyPercent(15)
                .EvasionPercent(10)
                .FuelConsumptionPercent(-7);
        }

        private void ArtemisLHunterArm()
        {
            _builder.Create("artemis_larm", MechPartType.LeftArm)
                .HPPercent(-10)
                .FuelPercent(15)
                .AttackPercent(35)
                .EtherAttackPercent(5)
                .AccuracyPercent(35)
                .EvasionPercent(20)
                .FuelConsumptionPercent(-3);
        }

        private void DemeterLGrowthArm()
        {
            _builder.Create("demeter_larm", MechPartType.LeftArm)
                .HPPercent(20)
                .FuelPercent(27)
                .EtherAttackPercent(15)
                .DefensePercent(15)
                .EtherDefensePercent(15)
                .AccuracyPercent(-5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(-22);
        }

        private void HephaestusLForgeArm()
        {
            _builder.Create("hephaestus_larm", MechPartType.LeftArm)
                .HPPercent(30)
                .FuelPercent(-5)
                .AttackPercent(20)
                .EtherAttackPercent(10)
                .DefensePercent(35)
                .AccuracyPercent(-10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(45);
        }

        private void LokiLTricksterArm()
        {
            _builder.Create("loki_larm", MechPartType.LeftArm)
                .HPPercent(-10)
                .FuelPercent(27)
                .AttackPercent(15)
                .EtherAttackPercent(20)
                .DefensePercent(-15)
                .EtherDefensePercent(15)
                .AccuracyPercent(25)
                .EvasionPercent(35)
                .FuelConsumptionPercent(-7);
        }

        private void OdinLWisdomArm()
        {
            _builder.Create("odin_larm", MechPartType.LeftArm)
                .HPPercent(5)
                .FuelPercent(40)
                .AttackPercent(10)
                .EtherAttackPercent(35)
                .DefensePercent(5)
                .EtherDefensePercent(25)
                .AccuracyPercent(30)
                .EvasionPercent(5);
        }

        private void FenrirLBeastArm()
        {
            _builder.Create("fenrir_larm", MechPartType.LeftArm)
                .HPPercent(32)
                .AttackPercent(45)
                .EtherAttackPercent(10)
                .DefensePercent(17)
                .EtherDefensePercent(-15)
                .AccuracyPercent(-5)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(30);
        }
    }
}