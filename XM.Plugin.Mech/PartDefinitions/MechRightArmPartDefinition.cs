using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechRightArmPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPart> BuildMechParts()
        {
            DestroyerRAssaultArm();
            ScalpelRPrecisionArm();
            AegisRShieldArm();
            HermesRUtilityArm();
            ColossusRCrusherArm();
            ApolloRSurgicalArm();
            QuicksilverRReactiveArm();
            AtlasRTitanArm();
            WraithRPhantomArm();
            AresRBerserkerArm();
            ViperRStrikeArm();
            ThorRThunderArm();
            HadesRInfernoArm();
            PoseidonRHydroArm();
            ArtemisRHunterArm();
            DemeterRGrowthArm();
            HephaestusRForgeArm();
            LokiRTricksterArm();
            OdinRWisdomArm();
            FenrirRBeastArm();

            return _builder.Build();
        }

        private void DestroyerRAssaultArm()
        {
            _builder.Create("destroyer_rarm", MechPartType.RightArm)
                .HPPercent(12)
                .AttackPercent(20)
                .DefensePercent(12)
                .AccuracyPercent(5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(10);
        }

        private void ScalpelRPrecisionArm()
        {
            _builder.Create("scalpel_rarm", MechPartType.RightArm)
                .HPPercent(-5)
                .FuelPercent(5)
                .AttackPercent(10)
                .EtherAttackPercent(15)
                .AccuracyPercent(25)
                .EvasionPercent(10)
                .FuelConsumptionPercent(-5);
        }

        private void AegisRShieldArm()
        {
            _builder.Create("aegis_rarm", MechPartType.RightArm)
                .HPPercent(5)
                .AttackPercent(-10)
                .DefensePercent(25)
                .EtherDefensePercent(15)
                .AccuracyPercent(-5)
                .EvasionPercent(5)
                .FuelConsumptionPercent(5);
        }

        private void HermesRUtilityArm()
        {
            _builder.Create("hermes_rarm", MechPartType.RightArm)
                .FuelPercent(15)
                .EtherAttackPercent(5)
                .DefensePercent(5)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(-10);
        }

        private void ColossusRCrusherArm()
        {
            _builder.Create("colossus_rarm", MechPartType.RightArm)
                .HPPercent(20)
                .AttackPercent(30)
                .EtherAttackPercent(-5)
                .DefensePercent(10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(25);
        }

        private void ApolloRSurgicalArm()
        {
            _builder.Create("apollo_rarm", MechPartType.RightArm)
                .HPPercent(-10)
                .FuelPercent(12)
                .AttackPercent(5)
                .EtherAttackPercent(20)
                .EtherDefensePercent(10)
                .AccuracyPercent(30)
                .EvasionPercent(15)
                .FuelConsumptionPercent(-12);
        }

        private void QuicksilverRReactiveArm()
        {
            _builder.Create("quicksilver_rarm", MechPartType.RightArm)
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

        private void AtlasRTitanArm()
        {
            _builder.Create("atlas_rarm", MechPartType.RightArm)
                .HPPercent(27)
                .AttackPercent(25)
                .DefensePercent(22)
                .EtherDefensePercent(-5)
                .AccuracyPercent(-5)
                .EvasionPercent(-25)
                .FuelConsumptionPercent(35);
        }

        private void WraithRPhantomArm()
        {
            _builder.Create("wraith_rarm", MechPartType.RightArm)
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

        private void AresRBerserkerArm()
        {
            _builder.Create("ares_rarm", MechPartType.RightArm)
                .HPPercent(15)
                .AttackPercent(35)
                .EtherAttackPercent(5)
                .DefensePercent(10)
                .EtherDefensePercent(-10)
                .AccuracyPercent(5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(45);
        }

        private void ViperRStrikeArm()
        {
            _builder.Create("viper_rarm", MechPartType.RightArm)
                .FuelPercent(12)
                .AttackPercent(25)
                .EtherAttackPercent(10)
                .AccuracyPercent(20)
                .EvasionPercent(25)
                .FuelConsumptionPercent(-7);
        }

        private void ThorRThunderArm()
        {
            _builder.Create("thor_rarm", MechPartType.RightArm)
                .HPPercent(22)
                .AttackPercent(40)
                .EtherAttackPercent(20)
                .DefensePercent(17)
                .EtherDefensePercent(-5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(45);
        }

        private void HadesRInfernoArm()
        {
            _builder.Create("hades_rarm", MechPartType.RightArm)
                .HPPercent(-5)
                .AttackPercent(30)
                .EtherAttackPercent(25)
                .DefensePercent(25)
                .EtherDefensePercent(10)
                .AccuracyPercent(10)
                .FuelConsumptionPercent(45);
        }

        private void PoseidonRHydroArm()
        {
            _builder.Create("poseidon_rarm", MechPartType.RightArm)
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

        private void ArtemisRHunterArm()
        {
            _builder.Create("artemis_rarm", MechPartType.RightArm)
                .HPPercent(-10)
                .FuelPercent(15)
                .AttackPercent(35)
                .EtherAttackPercent(5)
                .AccuracyPercent(35)
                .EvasionPercent(20)
                .FuelConsumptionPercent(-3);
        }

        private void DemeterRGrowthArm()
        {
            _builder.Create("demeter_rarm", MechPartType.RightArm)
                .HPPercent(20)
                .FuelPercent(27)
                .EtherAttackPercent(15)
                .DefensePercent(15)
                .EtherDefensePercent(15)
                .AccuracyPercent(-5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(-22);
        }

        private void HephaestusRForgeArm()
        {
            _builder.Create("hephaestus_rarm", MechPartType.RightArm)
                .HPPercent(30)
                .FuelPercent(-5)
                .AttackPercent(20)
                .EtherAttackPercent(10)
                .DefensePercent(35)
                .AccuracyPercent(-10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(45);
        }

        private void LokiRTricksterArm()
        {
            _builder.Create("loki_rarm", MechPartType.RightArm)
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

        private void OdinRWisdomArm()
        {
            _builder.Create("odin_rarm", MechPartType.RightArm)
                .HPPercent(5)
                .FuelPercent(40)
                .AttackPercent(10)
                .EtherAttackPercent(35)
                .DefensePercent(5)
                .EtherDefensePercent(25)
                .AccuracyPercent(30)
                .EvasionPercent(5);
        }

        private void FenrirRBeastArm()
        {
            _builder.Create("fenrir_rarm", MechPartType.RightArm)
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