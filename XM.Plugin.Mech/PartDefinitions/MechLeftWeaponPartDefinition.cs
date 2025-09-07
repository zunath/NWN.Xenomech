using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechLeftWeaponPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPart> BuildMechParts()
        {
            PlasmaDevastator();
            ZeusRailgun();
            ApolloBeamSaber();
            AegisShieldMatrix();
            OdinGaussLance();
            PrometheusFlamethrower();
            AresChaingun();
            LokiRealityRipper();
            HeliosLaserArray();
            HermesStunRod();
            KrakenHarpoonGun();
            ZeusTeslaCoil();
            PoseidonSonicCannon();
            AtlasMeteorHammer();
            FenrirClawCannon();
            NemesisVengeanceGun();
            PhoenixRebirthRay();

            return _builder.Build();
        }

        private void PlasmaDevastator()
        {
            _builder.Create("plasma_lweap", MechPartType.LeftWeapon)
                .HPPercent(7)
                .FuelPercent(-5)
                .AttackPercent(30)
                .EtherAttackPercent(15)
                .DefensePercent(7)
                .AccuracyPercent(5)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(35);
        }

        private void ZeusRailgun()
        {
            _builder.Create("zeusr_lweap", MechPartType.LeftWeapon)
                .HPPercent(5)
                .AttackPercent(25)
                .DefensePercent(5)
                .AccuracyPercent(15)
                .FuelConsumptionPercent(20);
        }

        private void ApolloBeamSaber()
        {
            _builder.Create("apollo_lweap", MechPartType.LeftWeapon)
                .FuelPercent(10)
                .AttackPercent(20)
                .EtherAttackPercent(10)
                .AccuracyPercent(5)
                .EvasionPercent(15)
                .FuelConsumptionPercent(-13);
        }

        private void AegisShieldMatrix()
        {
            _builder.Create("aegis_lweap", MechPartType.LeftWeapon)
                .HPPercent(-5)
                .FuelPercent(12)
                .DefensePercent(30)
                .EtherDefensePercent(25)
                .AccuracyPercent(-10)
                .FuelConsumptionPercent(-2);
        }

        private void OdinGaussLance()
        {
            _builder.Create("odin_lweap", MechPartType.LeftWeapon)
                .HPPercent(2)
                .AttackPercent(20)
                .DefensePercent(2)
                .AccuracyPercent(25)
                .FuelConsumptionPercent(10);
        }

        private void PrometheusFlamethrower()
        {
            _builder.Create("prometheus_lweap", MechPartType.LeftWeapon)
                .HPPercent(15)
                .AttackPercent(25)
                .EtherAttackPercent(5)
                .DefensePercent(15)
                .AccuracyPercent(-5)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(45);
        }

        private void AresChaingun()
        {
            _builder.Create("ares_lweap", MechPartType.LeftWeapon)
                .HPPercent(7)
                .AttackPercent(40)
                .EtherAttackPercent(-5)
                .DefensePercent(7)
                .AccuracyPercent(5)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(35);
        }

        private void LokiRealityRipper()
        {
            _builder.Create("loki_lweap", MechPartType.LeftWeapon)
                .HPPercent(5)
                .FuelPercent(10)
                .AttackPercent(5)
                .EtherAttackPercent(30)
                .DefensePercent(-5)
                .EtherDefensePercent(20)
                .AccuracyPercent(10)
                .EvasionPercent(10)
                .FuelConsumptionPercent(5);
        }

        private void HeliosLaserArray()
        {
            _builder.Create("helios_lweap", MechPartType.LeftWeapon)
                .HPPercent(10)
                .AttackPercent(20)
                .EtherAttackPercent(25)
                .DefensePercent(10)
                .AccuracyPercent(15)
                .EvasionPercent(5)
                .FuelConsumptionPercent(30);
        }

        private void HermesStunRod()
        {
            _builder.Create("hermes_lweap", MechPartType.LeftWeapon)
                .HPPercent(10)
                .FuelPercent(20)
                .AttackPercent(10)
                .EtherAttackPercent(15)
                .DefensePercent(10)
                .EtherDefensePercent(15)
                .AccuracyPercent(10)
                .EvasionPercent(20)
                .FuelConsumptionPercent(-13);
        }

        private void KrakenHarpoonGun()
        {
            _builder.Create("kraken_lweap", MechPartType.LeftWeapon)
                .HPPercent(5)
                .FuelPercent(7)
                .AttackPercent(20)
                .DefensePercent(5)
                .AccuracyPercent(20)
                .FuelConsumptionPercent(-2);
        }

        private void ZeusTeslaCoil()
        {
            _builder.Create("zeus_lweap", MechPartType.LeftWeapon)
                .HPPercent(17)
                .FuelPercent(20)
                .AttackPercent(25)
                .EtherAttackPercent(30)
                .DefensePercent(17)
                .EtherDefensePercent(5)
                .AccuracyPercent(5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(45);
        }

        private void PoseidonSonicCannon()
        {
            _builder.Create("poseidon_lweap", MechPartType.LeftWeapon)
                .HPPercent(5)
                .FuelPercent(15)
                .AttackPercent(20)
                .EtherAttackPercent(15)
                .EtherDefensePercent(10)
                .AccuracyPercent(15)
                .EvasionPercent(5)
                .FuelConsumptionPercent(2);
        }

        private void AtlasMeteorHammer()
        {
            _builder.Create("atlas_lweap", MechPartType.LeftWeapon)
                .HPPercent(17)
                .FuelPercent(-10)
                .AttackPercent(35)
                .EtherAttackPercent(10)
                .DefensePercent(12)
                .EtherDefensePercent(-10)
                .AccuracyPercent(-5)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(25);
        }

        private void FenrirClawCannon()
        {
            _builder.Create("fenrir_lweap", MechPartType.LeftWeapon)
                .HPPercent(20)
                .AttackPercent(40)
                .EtherAttackPercent(15)
                .DefensePercent(10)
                .EtherDefensePercent(-5)
                .AccuracyPercent(15)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(35);
        }

        private void NemesisVengeanceGun()
        {
            _builder.Create("nemesis_lweap", MechPartType.LeftWeapon)
                .HPPercent(-5)
                .AttackPercent(45)
                .EtherAttackPercent(25)
                .DefensePercent(15)
                .AccuracyPercent(30)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(45);
        }

        private void PhoenixRebirthRay()
        {
            _builder.Create("phoenix_lweap", MechPartType.LeftWeapon)
                .HPPercent(20)
                .FuelPercent(40)
                .AttackPercent(10)
                .EtherAttackPercent(25)
                .DefensePercent(15)
                .EtherDefensePercent(20)
                .FuelConsumptionPercent(-30);
        }
    }
}