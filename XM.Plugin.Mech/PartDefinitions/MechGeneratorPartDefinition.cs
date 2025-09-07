using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechGeneratorPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPartStats> BuildMechParts()
        {
            HeliosFusionReactor();
            GaiaEfficientCell();
            PhoenixEmergencyCore();
            StandardPowerCell();
            ZeusArcReactor();
            ApolloSolarArray();
            AbyssVoidReactor();
            DarwinHybridCell();
            AresOverchargeCore();
            AtlasStableReactor();
            PrometheusFireCore();
            PoseidonHydroCell();
            ChronosTemporalCore();
            HadesInfernoCore();
            ThorStormGenerator();
            OdinWisdomCore();
            ArtemisMoonCell();
            DemeterGrowthCore();
            LokiChaosGenerator();
            FenrirBeastCore();
            HeraDivineReactor();
            HephaestusForgeCore();
            HermesQuickCell();

            return _builder.Build();
        }

        private void HeliosFusionReactor()
        {
            _builder.Create("helios_gen", MechPartType.Generator)
                .HPPercent(12)
                .FuelPercent(40)
                .EtherAttackPercent(10)
                .DefensePercent(12)
                .FuelConsumptionPercent(40);
        }

        private void GaiaEfficientCell()
        {
            _builder.Create("gaia_gen", MechPartType.Generator)
                .HPPercent(2)
                .FuelPercent(32)
                .DefensePercent(2)
                .EtherDefensePercent(10)
                .FuelConsumptionPercent(-32);
        }

        private void PhoenixEmergencyCore()
        {
            _builder.Create("phoenix_gen", MechPartType.Generator)
                .HPPercent(10)
                .FuelPercent(12)
                .DefensePercent(5)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(8);
        }

        private void StandardPowerCell()
        {
            _builder.Create("standard_gen", MechPartType.Generator)
                .HPPercent(5)
                .FuelPercent(20)
                .DefensePercent(5)
                .FuelConsumptionPercent(-10);
        }

        private void ZeusArcReactor()
        {
            _builder.Create("zeus_gen", MechPartType.Generator)
                .HPPercent(15)
                .FuelPercent(35)
                .EtherAttackPercent(25)
                .DefensePercent(15)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(55);
        }

        private void ApolloSolarArray()
        {
            _builder.Create("apollo_gen", MechPartType.Generator)
                .HPPercent(10)
                .FuelPercent(40)
                .EtherAttackPercent(5)
                .DefensePercent(10)
                .EtherDefensePercent(15)
                .FuelConsumptionPercent(-40);
        }

        private void AbyssVoidReactor()
        {
            _builder.Create("abyss_gen", MechPartType.Generator)
                .HPPercent(-10)
                .FuelPercent(50)
                .EtherAttackPercent(20)
                .DefensePercent(40)
                .EtherDefensePercent(25)
                .EvasionPercent(5)
                .FuelConsumptionPercent(70);
        }

        private void DarwinHybridCell()
        {
            _builder.Create("darwin_gen", MechPartType.Generator)
                .HPPercent(12)
                .FuelPercent(32)
                .AttackPercent(5)
                .EtherAttackPercent(5)
                .DefensePercent(12)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(3);
        }

        private void AresOverchargeCore()
        {
            _builder.Create("ares_gen", MechPartType.Generator)
                .HPPercent(-15)
                .FuelPercent(45)
                .AttackPercent(15)
                .EtherAttackPercent(15)
                .DefensePercent(35)
                .FuelConsumptionPercent(70);
        }

        private void AtlasStableReactor()
        {
            _builder.Create("atlas_gen", MechPartType.Generator)
                .HPPercent(17)
                .FuelPercent(27)
                .DefensePercent(12)
                .EtherDefensePercent(20)
                .FuelConsumptionPercent(-47);
        }

        private void PrometheusFireCore()
        {
            _builder.Create("prometheus_gen", MechPartType.Generator)
                .HPPercent(17)
                .FuelPercent(30)
                .AttackPercent(10)
                .EtherAttackPercent(15)
                .DefensePercent(17)
                .FuelConsumptionPercent(40);
        }

        private void PoseidonHydroCell()
        {
            _builder.Create("poseidon_gen", MechPartType.Generator)
                .HPPercent(12)
                .FuelPercent(45)
                .EtherAttackPercent(10)
                .DefensePercent(7)
                .EtherDefensePercent(15)
                .FuelConsumptionPercent(-50);
        }

        private void ChronosTemporalCore()
        {
            _builder.Create("chronos_gen", MechPartType.Generator)
                .HPPercent(-5)
                .FuelPercent(55)
                .AttackPercent(5)
                .EtherAttackPercent(25)
                .DefensePercent(45)
                .EtherDefensePercent(30)
                .AccuracyPercent(10)
                .EvasionPercent(10)
                .FuelConsumptionPercent(75);
        }

        private void HadesInfernoCore()
        {
            _builder.Create("hades_gen", MechPartType.Generator)
                .HPPercent(-10)
                .FuelPercent(40)
                .AttackPercent(20)
                .EtherAttackPercent(30)
                .DefensePercent(35)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(70);
        }

        private void ThorStormGenerator()
        {
            _builder.Create("thor_gen", MechPartType.Generator)
                .HPPercent(20)
                .FuelPercent(35)
                .AttackPercent(15)
                .EtherAttackPercent(20)
                .DefensePercent(20)
                .EtherDefensePercent(10)
                .AccuracyPercent(5)
                .FuelConsumptionPercent(50);
        }

        private void OdinWisdomCore()
        {
            _builder.Create("odin_gen", MechPartType.Generator)
                .HPPercent(17)
                .FuelPercent(52)
                .EtherAttackPercent(30)
                .DefensePercent(22)
                .EtherDefensePercent(25)
                .AccuracyPercent(15)
                .EvasionPercent(5)
                .FuelConsumptionPercent(8);
        }

        private void ArtemisMoonCell()
        {
            _builder.Create("artemis_gen", MechPartType.Generator)
                .HPPercent(7)
                .FuelPercent(47)
                .EtherAttackPercent(15)
                .DefensePercent(12)
                .EtherDefensePercent(25)
                .AccuracyPercent(10)
                .EvasionPercent(15)
                .FuelConsumptionPercent(-42);
        }

        private void DemeterGrowthCore()
        {
            _builder.Create("demeter_gen", MechPartType.Generator)
                .HPPercent(25)
                .FuelPercent(32)
                .EtherAttackPercent(20)
                .DefensePercent(20)
                .EtherDefensePercent(20)
                .AccuracyPercent(-5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(-32);
        }

        private void LokiChaosGenerator()
        {
            _builder.Create("loki_gen", MechPartType.Generator)
                .HPPercent(-20)
                .FuelPercent(60)
                .AttackPercent(25)
                .EtherAttackPercent(35)
                .DefensePercent(45)
                .EtherDefensePercent(15)
                .AccuracyPercent(20)
                .EvasionPercent(25)
                .FuelConsumptionPercent(95);
        }

        private void FenrirBeastCore()
        {
            _builder.Create("fenrir_gen", MechPartType.Generator)
                .HPPercent(25)
                .FuelPercent(35)
                .AttackPercent(25)
                .EtherAttackPercent(10)
                .DefensePercent(20)
                .EtherDefensePercent(-10)
                .FuelConsumptionPercent(45);
        }

        private void HeraDivineReactor()
        {
            _builder.Create("hera_gen", MechPartType.Generator)
                .HPPercent(15)
                .FuelPercent(50)
                .EtherAttackPercent(25)
                .DefensePercent(30)
                .EtherDefensePercent(30)
                .AccuracyPercent(10)
                .EvasionPercent(5);
        }

        private void HephaestusForgeCore()
        {
            _builder.Create("hephaestus_gen", MechPartType.Generator)
                .HPPercent(37)
                .FuelPercent(25)
                .AttackPercent(15)
                .EtherAttackPercent(5)
                .DefensePercent(32)
                .AccuracyPercent(-5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(45);
        }

        private void HermesQuickCell()
        {
            _builder.Create("hermes_gen", MechPartType.Generator)
                .HPPercent(-10)
                .FuelPercent(40)
                .AttackPercent(5)
                .EtherAttackPercent(15)
                .DefensePercent(15)
                .EtherDefensePercent(10)
                .AccuracyPercent(15)
                .EvasionPercent(25)
                .FuelConsumptionPercent(-20);
        }
    }
}