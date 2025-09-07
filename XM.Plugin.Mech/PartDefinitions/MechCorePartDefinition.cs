using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechCorePartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPart> BuildMechParts()
        {
            HeliosFusionCore();
            AtlasStabilityMatrix();
            PrometheusLogicEngine();
            StandardTitanCore();
            AbyssVoidEngine();
            GaiaCrystalMatrix();
            FortressCommandCore();
            BlitzOverdriveCore();
            DarwinAdaptiveCore();
            ParadoxQuantumCore();
            IcarusSolarCore();
            CerberusTripleCore();
            OuroborosLoopCore();
            PhoenixFlameCore();
            MjolnirThunderCore();
            PandoraChaosCore();
            ChronosTimeCore();
            MedusaPetrifyCore();
            KrakenDepthCore();
            NemesisWarCore();

            return _builder.Build();
        }

        private void HeliosFusionCore()
        {
            _builder.Create("helios_core", MechPartType.Core)
                .HPPercent(10)
                .FuelPercent(30)
                .DefensePercent(10)
                .FuelConsumptionPercent(25);
        }

        private void AtlasStabilityMatrix()
        {
            _builder.Create("atlas_core", MechPartType.Core)
                .HPPercent(20)
                .FuelPercent(2)
                .DefensePercent(15)
                .EtherDefensePercent(10)
                .AccuracyPercent(10)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(3);
        }

        private void PrometheusLogicEngine()
        {
            _builder.Create("prometheus_core", MechPartType.Core)
                .HPPercent(-10)
                .FuelPercent(22)
                .EtherAttackPercent(15)
                .EtherDefensePercent(10)
                .AccuracyPercent(5)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-17);
        }

        private void StandardTitanCore()
        {
            _builder.Create("standard_core", MechPartType.Core)
                .HPPercent(2)
                .FuelPercent(7)
                .DefensePercent(7)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(-2);
        }

        private void AbyssVoidEngine()
        {
            _builder.Create("abyss_core", MechPartType.Core)
                .HPPercent(-20)
                .FuelPercent(40)
                .EtherAttackPercent(30)
                .DefensePercent(30)
                .EtherDefensePercent(25)
                .FuelConsumptionPercent(40);
        }

        private void GaiaCrystalMatrix()
        {
            _builder.Create("gaia_core", MechPartType.Core)
                .HPPercent(12)
                .FuelPercent(35)
                .EtherAttackPercent(20)
                .DefensePercent(17)
                .EtherDefensePercent(20)
                .FuelConsumptionPercent(-25);
        }

        private void FortressCommandCore()
        {
            _builder.Create("fortress_core", MechPartType.Core)
                .HPPercent(35)
                .FuelPercent(-10)
                .DefensePercent(30)
                .EtherDefensePercent(10)
                .AccuracyPercent(-5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(15);
        }

        private void BlitzOverdriveCore()
        {
            _builder.Create("blitz_core", MechPartType.Core)
                .HPPercent(-15)
                .FuelPercent(35)
                .AttackPercent(15)
                .EtherAttackPercent(15)
                .DefensePercent(25)
                .AccuracyPercent(10)
                .EvasionPercent(10)
                .FuelConsumptionPercent(50);
        }

        private void DarwinAdaptiveCore()
        {
            _builder.Create("darwin_core", MechPartType.Core)
                .HPPercent(15)
                .FuelPercent(25)
                .AttackPercent(5)
                .EtherAttackPercent(5)
                .DefensePercent(10)
                .EtherDefensePercent(15)
                .AccuracyPercent(5)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-5);
        }

        private void ParadoxQuantumCore()
        {
            _builder.Create("paradox_core", MechPartType.Core)
                .HPPercent(-5)
                .FuelPercent(50)
                .EtherAttackPercent(35)
                .DefensePercent(35)
                .EtherDefensePercent(30)
                .AccuracyPercent(15)
                .EvasionPercent(5)
                .FuelConsumptionPercent(60);
        }

        private void IcarusSolarCore()
        {
            _builder.Create("icarus_core", MechPartType.Core)
                .HPPercent(20)
                .FuelPercent(47)
                .EtherAttackPercent(10)
                .DefensePercent(15)
                .EtherDefensePercent(15)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-32);
        }

        private void CerberusTripleCore()
        {
            _builder.Create("cerberus_core", MechPartType.Core)
                .HPPercent(22)
                .FuelPercent(10)
                .AttackPercent(10)
                .EtherAttackPercent(10)
                .DefensePercent(27)
                .EtherDefensePercent(20)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(15);
        }

        private void OuroborosLoopCore()
        {
            _builder.Create("ouroboros_core", MechPartType.Core)
                .HPPercent(5)
                .FuelPercent(62)
                .AttackPercent(-5)
                .EtherAttackPercent(-5)
                .DefensePercent(5)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(-52);
        }

        private void PhoenixFlameCore()
        {
            _builder.Create("phoenix_core", MechPartType.Core)
                .HPPercent(-5)
                .FuelPercent(40)
                .AttackPercent(20)
                .EtherAttackPercent(25)
                .DefensePercent(20)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(45);
        }

        private void MjolnirThunderCore()
        {
            _builder.Create("mjolnir_core", MechPartType.Core)
                .HPPercent(22)
                .FuelPercent(25)
                .AttackPercent(15)
                .EtherAttackPercent(35)
                .DefensePercent(17)
                .EtherDefensePercent(10)
                .AccuracyPercent(5)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(35);
        }

        private void PandoraChaosCore()
        {
            _builder.Create("pandora_core", MechPartType.Core)
                .HPPercent(-25)
                .FuelPercent(60)
                .AttackPercent(25)
                .EtherAttackPercent(45)
                .DefensePercent(30)
                .EtherDefensePercent(35)
                .AccuracyPercent(20)
                .EvasionPercent(15)
                .FuelConsumptionPercent(75);
        }

        private void ChronosTimeCore()
        {
            _builder.Create("chronos_core", MechPartType.Core)
                .HPPercent(7)
                .FuelPercent(37)
                .EtherAttackPercent(20)
                .DefensePercent(17)
                .EtherDefensePercent(25)
                .AccuracyPercent(25)
                .EvasionPercent(10)
                .FuelConsumptionPercent(3);
        }

        private void MedusaPetrifyCore()
        {
            _builder.Create("medusa_core", MechPartType.Core)
                .HPPercent(27)
                .FuelPercent(10)
                .EtherAttackPercent(15)
                .DefensePercent(32)
                .EtherDefensePercent(25)
                .AccuracyPercent(-10)
                .EvasionPercent(-25)
                .FuelConsumptionPercent(5);
        }

        private void KrakenDepthCore()
        {
            _builder.Create("kraken_core", MechPartType.Core)
                .HPPercent(27)
                .FuelPercent(30)
                .AttackPercent(10)
                .EtherAttackPercent(30)
                .DefensePercent(7)
                .EtherDefensePercent(40)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-20);
        }

        private void NemesisWarCore()
        {
            _builder.Create("nemesis_core", MechPartType.Core)
                .HPPercent(-10)
                .FuelPercent(25)
                .AttackPercent(30)
                .EtherAttackPercent(20)
                .DefensePercent(15)
                .AccuracyPercent(15)
                .EvasionPercent(10)
                .FuelConsumptionPercent(40);
        }
    }
}