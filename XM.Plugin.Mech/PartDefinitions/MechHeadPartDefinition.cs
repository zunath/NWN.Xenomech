using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechHeadPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPartStats> BuildMechParts()
        {
            FortressViiCombatArray();
            ApexTargetingMatrix();
            WraithEcmSuite();
            SentinelBasicArray();
            OracleCommandNode();
            PredatorKillSuite();
            PhantomCloakArray();
            BastionDefenseGrid();
            SynapticLinkCore();
            QuantumSightArray();
            ViperStrikeHead();
            HydraMultiSensor();
            CerberusGuardian();
            PhoenixRebirthCore();
            TitanFortressHead();
            ValkyrieWarCrown();
            ReaperSkullArray();
            SphinxEnigmaCore();
            AuroraLightMatrix();
            NovaBurstScanner();

            return _builder.Build();
        }

        private void FortressViiCombatArray()
        {
            _builder.Create("fortress_head", MechPartType.Head)
                .HPPercent(20)
                .DefensePercent(15)
                .EtherDefensePercent(5)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(5);
        }

        private void ApexTargetingMatrix()
        {
            _builder.Create("apex_head", MechPartType.Head)
                .HPPercent(-5)
                .FuelPercent(2)
                .AttackPercent(15)
                .EtherAttackPercent(10)
                .AccuracyPercent(20)
                .FuelConsumptionPercent(8);
        }

        private void WraithEcmSuite()
        {
            _builder.Create("wraith_head", MechPartType.Head)
                .FuelPercent(10)
                .EtherDefensePercent(15)
                .AccuracyPercent(-10)
                .EvasionPercent(25);
        }

        private void SentinelBasicArray()
        {
            _builder.Create("sentinel_head", MechPartType.Head)
                .FuelPercent(2)
                .AccuracyPercent(5)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-2);
        }

        private void OracleCommandNode()
        {
            _builder.Create("oracle_head", MechPartType.Head)
                .HPPercent(-10)
                .FuelPercent(25)
                .EtherAttackPercent(15)
                .EtherDefensePercent(10)
                .AccuracyPercent(15)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-10);
        }

        private void PredatorKillSuite()
        {
            _builder.Create("predator_head", MechPartType.Head)
                .HPPercent(2)
                .AttackPercent(20)
                .EtherAttackPercent(5)
                .DefensePercent(2)
                .AccuracyPercent(25)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(20);
        }

        private void PhantomCloakArray()
        {
            _builder.Create("phantom_head", MechPartType.Head)
                .HPPercent(-10)
                .FuelPercent(12)
                .DefensePercent(-5)
                .EtherDefensePercent(5)
                .AccuracyPercent(-15)
                .EvasionPercent(40)
                .FuelConsumptionPercent(-17);
        }

        private void BastionDefenseGrid()
        {
            _builder.Create("bastion_head", MechPartType.Head)
                .HPPercent(32)
                .FuelPercent(-5)
                .DefensePercent(27)
                .EtherDefensePercent(15)
                .AccuracyPercent(-10)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(25);
        }

        private void SynapticLinkCore()
        {
            _builder.Create("synaptic_head", MechPartType.Head)
                .HPPercent(-15)
                .FuelPercent(17)
                .AttackPercent(10)
                .EtherAttackPercent(25)
                .EtherDefensePercent(15)
                .AccuracyPercent(10)
                .EvasionPercent(10)
                .FuelConsumptionPercent(-2);
        }

        private void QuantumSightArray()
        {
            _builder.Create("quantum_head", MechPartType.Head)
                .FuelPercent(32)
                .EtherAttackPercent(20)
                .EtherDefensePercent(20)
                .AccuracyPercent(20)
                .FuelConsumptionPercent(-2);
        }

        private void ViperStrikeHead()
        {
            _builder.Create("viper_head", MechPartType.Head)
                .HPPercent(5)
                .AttackPercent(25)
                .AccuracyPercent(30)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(10);
        }

        private void HydraMultiSensor()
        {
            _builder.Create("hydra_head", MechPartType.Head)
                .HPPercent(-5)
                .FuelPercent(20)
                .AttackPercent(10)
                .EtherAttackPercent(10)
                .EtherDefensePercent(5)
                .AccuracyPercent(15)
                .EvasionPercent(10)
                .FuelConsumptionPercent(5);
        }

        private void CerberusGuardian()
        {
            _builder.Create("cerberus_head", MechPartType.Head)
                .HPPercent(25)
                .DefensePercent(20)
                .EtherDefensePercent(20)
                .AccuracyPercent(-5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(10);
        }

        private void PhoenixRebirthCore()
        {
            _builder.Create("phoenix_head", MechPartType.Head)
                .HPPercent(10)
                .FuelPercent(42)
                .AttackPercent(-5)
                .EtherAttackPercent(-5)
                .DefensePercent(5)
                .EtherDefensePercent(5)
                .FuelConsumptionPercent(-30);
        }

        private void TitanFortressHead()
        {
            _builder.Create("titan_head", MechPartType.Head)
                .HPPercent(40)
                .FuelPercent(-10)
                .DefensePercent(35)
                .EtherDefensePercent(10)
                .AccuracyPercent(-15)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(35);
        }

        private void ValkyrieWarCrown()
        {
            _builder.Create("valkyrie_head", MechPartType.Head)
                .HPPercent(2)
                .FuelPercent(5)
                .AttackPercent(30)
                .EtherAttackPercent(15)
                .DefensePercent(2)
                .AccuracyPercent(20)
                .EvasionPercent(15)
                .FuelConsumptionPercent(15);
        }

        private void ReaperSkullArray()
        {
            _builder.Create("reaper_head", MechPartType.Head)
                .HPPercent(-10)
                .AttackPercent(35)
                .EtherAttackPercent(20)
                .DefensePercent(5)
                .AccuracyPercent(15)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(35);
        }

        private void SphinxEnigmaCore()
        {
            _builder.Create("sphinx_head", MechPartType.Head)
                .FuelPercent(50)
                .EtherAttackPercent(30)
                .DefensePercent(-10)
                .EtherDefensePercent(25)
                .AccuracyPercent(25)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-5);
        }

        private void AuroraLightMatrix()
        {
            _builder.Create("aurora_head", MechPartType.Head)
                .HPPercent(-15)
                .FuelPercent(35)
                .EtherAttackPercent(25)
                .EtherDefensePercent(30)
                .AccuracyPercent(10)
                .EvasionPercent(20)
                .FuelConsumptionPercent(-25);
        }

        private void NovaBurstScanner()
        {
            _builder.Create("nova_head", MechPartType.Head)
                .HPPercent(-20)
                .FuelPercent(10)
                .AttackPercent(20)
                .EtherAttackPercent(40)
                .DefensePercent(10)
                .EtherDefensePercent(10)
                .AccuracyPercent(35)
                .FuelConsumptionPercent(55);
        }
    }
}