using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechLegsPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPartStats> BuildMechParts()
        {
            OlympusSiegePlatform();
            HermesSwiftLegs();
            ArtemisStabilizedLegs();
            StandardMobilityLegs();
            IcarusJumpLegs();
            AtlasFortressLegs();
            ArachneSpiderLegs();
            ApolloRocketLegs();
            PoseidonShockLegs();
            HadesWraithLegs();
            ZeusThunderLegs();
            KrakenTentacleLegs();
            CerberusGuardianLegs();
            PhoenixWingLegs();
            MedusaStoneLegs();
            ChronosPhaseLegs();
            GaiaRootLegs();
            NemesisHuntLegs();
            ValkyrieFlightLegs();
            TitanMarchLegs();

            return _builder.Build();
        }

        private void OlympusSiegePlatform()
        {
            _builder.Create("olympus_legs", MechPartType.Legs)
                .HPPercent(32)
                .AttackPercent(5)
                .DefensePercent(27)
                .EtherDefensePercent(-5)
                .AccuracyPercent(-10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(25);
        }

        private void HermesSwiftLegs()
        {
            _builder.Create("hermes_legs", MechPartType.Legs)
                .HPPercent(-15)
                .FuelPercent(15)
                .AccuracyPercent(10)
                .EvasionPercent(35)
                .FuelConsumptionPercent(-20);
        }

        private void ArtemisStabilizedLegs()
        {
            _builder.Create("artemis_legs", MechPartType.Legs)
                .HPPercent(5)
                .FuelPercent(2)
                .DefensePercent(10)
                .EtherDefensePercent(10)
                .AccuracyPercent(15)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(-2);
        }

        private void StandardMobilityLegs()
        {
            _builder.Create("standard_legs", MechPartType.Legs)
                .DefensePercent(5)
                .EtherDefensePercent(5)
                .EvasionPercent(5);
        }

        private void IcarusJumpLegs()
        {
            _builder.Create("icarus_legs", MechPartType.Legs)
                .HPPercent(-10)
                .FuelPercent(12)
                .EtherDefensePercent(5)
                .AccuracyPercent(5)
                .EvasionPercent(25)
                .FuelConsumptionPercent(-15);
        }

        private void AtlasFortressLegs()
        {
            _builder.Create("atlas_legs", MechPartType.Legs)
                .HPPercent(45)
                .DefensePercent(40)
                .EtherDefensePercent(15)
                .AccuracyPercent(-15)
                .EvasionPercent(-30)
                .FuelConsumptionPercent(35);
        }

        private void ArachneSpiderLegs()
        {
            _builder.Create("arachne_legs", MechPartType.Legs)
                .HPPercent(-5)
                .FuelPercent(17)
                .EtherAttackPercent(10)
                .EtherDefensePercent(10)
                .AccuracyPercent(20)
                .EvasionPercent(20)
                .FuelConsumptionPercent(-2);
        }

        private void ApolloRocketLegs()
        {
            _builder.Create("apollo_legs", MechPartType.Legs)
                .HPPercent(5)
                .FuelPercent(20)
                .AttackPercent(10)
                .DefensePercent(5)
                .AccuracyPercent(5)
                .EvasionPercent(15)
                .FuelConsumptionPercent(15);
        }

        private void PoseidonShockLegs()
        {
            _builder.Create("poseidon_legs", MechPartType.Legs)
                .HPPercent(17)
                .FuelPercent(2)
                .AttackPercent(15)
                .EtherAttackPercent(20)
                .DefensePercent(2)
                .EtherDefensePercent(5)
                .EvasionPercent(10)
                .FuelConsumptionPercent(8);
        }

        private void HadesWraithLegs()
        {
            _builder.Create("hades_legs", MechPartType.Legs)
                .HPPercent(-20)
                .FuelPercent(35)
                .EtherAttackPercent(15)
                .DefensePercent(-15)
                .EtherDefensePercent(20)
                .AccuracyPercent(10)
                .EvasionPercent(40)
                .FuelConsumptionPercent(-23);
        }

        private void ZeusThunderLegs()
        {
            _builder.Create("zeus_legs", MechPartType.Legs)
                .HPPercent(15)
                .FuelPercent(15)
                .AttackPercent(20)
                .EtherAttackPercent(25)
                .DefensePercent(5)
                .EtherDefensePercent(10)
                .AccuracyPercent(10)
                .EvasionPercent(15)
                .FuelConsumptionPercent(35);
        }

        private void KrakenTentacleLegs()
        {
            _builder.Create("kraken_legs", MechPartType.Legs)
                .HPPercent(5)
                .FuelPercent(15)
                .AttackPercent(10)
                .EtherAttackPercent(15)
                .DefensePercent(5)
                .EtherDefensePercent(15)
                .AccuracyPercent(5)
                .EvasionPercent(25)
                .FuelConsumptionPercent(2);
        }

        private void CerberusGuardianLegs()
        {
            _builder.Create("cerberus_legs", MechPartType.Legs)
                .HPPercent(25)
                .AttackPercent(5)
                .DefensePercent(30)
                .EtherDefensePercent(20)
                .AccuracyPercent(-5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(10);
        }

        private void PhoenixWingLegs()
        {
            _builder.Create("phoenix_legs", MechPartType.Legs)
                .HPPercent(-15)
                .FuelPercent(42)
                .EtherAttackPercent(20)
                .DefensePercent(-5)
                .EtherDefensePercent(25)
                .AccuracyPercent(5)
                .EvasionPercent(30)
                .FuelConsumptionPercent(-32);
        }

        private void MedusaStoneLegs()
        {
            _builder.Create("medusa_legs", MechPartType.Legs)
                .HPPercent(37)
                .FuelPercent(-15)
                .AttackPercent(10)
                .DefensePercent(42)
                .EtherDefensePercent(30)
                .AccuracyPercent(-20)
                .EvasionPercent(-35)
                .FuelConsumptionPercent(30);
        }

        private void ChronosPhaseLegs()
        {
            _builder.Create("chronos_legs", MechPartType.Legs)
                .HPPercent(-10)
                .FuelPercent(42)
                .AttackPercent(15)
                .EtherAttackPercent(30)
                .DefensePercent(-15)
                .EtherDefensePercent(35)
                .AccuracyPercent(20)
                .EvasionPercent(45)
                .FuelConsumptionPercent(-17);
        }

        private void GaiaRootLegs()
        {
            _builder.Create("gaia_legs", MechPartType.Legs)
                .HPPercent(17)
                .FuelPercent(40)
                .EtherAttackPercent(25)
                .DefensePercent(22)
                .EtherDefensePercent(35)
                .AccuracyPercent(-10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(-40);
        }

        private void NemesisHuntLegs()
        {
            _builder.Create("nemesis_legs", MechPartType.Legs)
                .HPPercent(5)
                .FuelPercent(5)
                .AttackPercent(30)
                .EtherAttackPercent(10)
                .DefensePercent(5)
                .AccuracyPercent(25)
                .EvasionPercent(20)
                .FuelConsumptionPercent(15);
        }

        private void ValkyrieFlightLegs()
        {
            _builder.Create("valkyrie_legs", MechPartType.Legs)
                .HPPercent(-20)
                .FuelPercent(50)
                .AttackPercent(5)
                .EtherAttackPercent(35)
                .DefensePercent(-20)
                .EtherDefensePercent(30)
                .AccuracyPercent(15)
                .EvasionPercent(50)
                .FuelConsumptionPercent(-33);
        }

        private void TitanMarchLegs()
        {
            _builder.Create("titan_legs", MechPartType.Legs)
                .HPPercent(52)
                .FuelPercent(-20)
                .AttackPercent(15)
                .DefensePercent(52)
                .EtherDefensePercent(20)
                .AccuracyPercent(-25)
                .EvasionPercent(-40)
                .FuelConsumptionPercent(45);
        }
    }
}