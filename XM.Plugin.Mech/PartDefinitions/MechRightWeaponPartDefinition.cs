using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.PartDefinitions
{
    [ServiceBinding(typeof(IMechPartListDefinition))]
    internal class MechRightWeaponPartDefinition : IMechPartListDefinition
    {
        private readonly MechPartBuilder _builder = new();

        public Dictionary<string, MechPartStats> BuildMechParts()
        {
            HermesPulseRifle();
            AresMissileBattery();
            ThorAutocannon();
            GaiaRepairSwarm();
            HadesIonDestructor();
            ArtemisCryoLance();
            ApolloSniperCannon();
            AtlasMinigun();
            PoseidonGravityWell();
            AbyssVoidCannon();
            MedusaAcidSprayer();
            HeliosPhotonTorpedo();
            ArtemisNeedleGun();
            ChronosPhaseRifle();
            HeraDivineLance();
            DemeterThornLauncher();
            ValkyrieHonorBlade();

            return _builder.Build();
        }

        private void HermesPulseRifle()
        {
            _builder.Create("hermes_rweap", MechPartType.RightWeapon)
                .HPPercent(2)
                .AttackPercent(15)
                .EtherAttackPercent(20)
                .DefensePercent(2)
                .AccuracyPercent(10)
                .EvasionPercent(5)
                .FuelConsumptionPercent(5);
        }

        private void AresMissileBattery()
        {
            _builder.Create("ares_rweap", MechPartType.RightWeapon)
                .HPPercent(10)
                .AttackPercent(35)
                .DefensePercent(10)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(35);
        }

        private void ThorAutocannon()
        {
            _builder.Create("thor_rweap", MechPartType.RightWeapon)
                .HPPercent(5)
                .AttackPercent(30)
                .DefensePercent(5)
                .AccuracyPercent(10)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(15);
        }

        private void GaiaRepairSwarm()
        {
            _builder.Create("gaia_rweap", MechPartType.RightWeapon)
                .HPPercent(15)
                .FuelPercent(15)
                .DefensePercent(10)
                .EtherDefensePercent(10)
                .FuelConsumptionPercent(-15);
        }

        private void HadesIonDestructor()
        {
            _builder.Create("hades_rweap", MechPartType.RightWeapon)
                .HPPercent(12)
                .FuelPercent(-10)
                .AttackPercent(10)
                .EtherAttackPercent(35)
                .DefensePercent(12)
                .EtherDefensePercent(5)
                .AccuracyPercent(15)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(50);
        }

        private void ArtemisCryoLance()
        {
            _builder.Create("artemisc_rweap", MechPartType.RightWeapon)
                .FuelPercent(22)
                .AttackPercent(5)
                .EtherAttackPercent(25)
                .EtherDefensePercent(15)
                .AccuracyPercent(20)
                .FuelConsumptionPercent(-2);
        }

        private void ApolloSniperCannon()
        {
            _builder.Create("apollo_rweap", MechPartType.RightWeapon)
                .HPPercent(2)
                .FuelPercent(-3)
                .AttackPercent(35)
                .EtherAttackPercent(5)
                .DefensePercent(2)
                .AccuracyPercent(35)
                .EvasionPercent(-10)
                .FuelConsumptionPercent(8);
        }

        private void AtlasMinigun()
        {
            _builder.Create("atlas_rweap", MechPartType.RightWeapon)
                .HPPercent(10)
                .AttackPercent(45)
                .EtherAttackPercent(-10)
                .DefensePercent(10)
                .AccuracyPercent(-5)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(45);
        }

        private void PoseidonGravityWell()
        {
            _builder.Create("poseidon_rweap", MechPartType.RightWeapon)
                .HPPercent(7)
                .FuelPercent(17)
                .AttackPercent(15)
                .EtherAttackPercent(20)
                .DefensePercent(2)
                .EtherDefensePercent(10)
                .AccuracyPercent(5)
                .EvasionPercent(15)
                .FuelConsumptionPercent(18);
        }

        private void AbyssVoidCannon()
        {
            _builder.Create("abyss_rweap", MechPartType.RightWeapon)
                .HPPercent(-10)
                .FuelPercent(-15)
                .AttackPercent(50)
                .EtherAttackPercent(40)
                .DefensePercent(30)
                .EtherDefensePercent(-10)
                .AccuracyPercent(20)
                .EvasionPercent(-15)
                .FuelConsumptionPercent(90);
        }

        private void MedusaAcidSprayer()
        {
            _builder.Create("medusa_rweap", MechPartType.RightWeapon)
                .HPPercent(2)
                .FuelPercent(17)
                .AttackPercent(15)
                .EtherAttackPercent(10)
                .DefensePercent(-3)
                .EtherDefensePercent(-10)
                .EvasionPercent(-5)
                .FuelConsumptionPercent(3);
        }

        private void HeliosPhotonTorpedo()
        {
            _builder.Create("helios_rweap", MechPartType.RightWeapon)
                .HPPercent(12)
                .FuelPercent(-20)
                .AttackPercent(40)
                .EtherAttackPercent(25)
                .DefensePercent(12)
                .AccuracyPercent(10)
                .EvasionPercent(-20)
                .FuelConsumptionPercent(60);
        }

        private void ArtemisNeedleGun()
        {
            _builder.Create("artemis_rweap", MechPartType.RightWeapon)
                .FuelPercent(10)
                .AttackPercent(15)
                .EtherAttackPercent(5)
                .AccuracyPercent(30)
                .EvasionPercent(10)
                .FuelConsumptionPercent(-5);
        }

        private void ChronosPhaseRifle()
        {
            _builder.Create("chronos_rweap", MechPartType.RightWeapon)
                .HPPercent(-5)
                .FuelPercent(32)
                .AttackPercent(10)
                .EtherAttackPercent(35)
                .EtherDefensePercent(25)
                .AccuracyPercent(20)
                .EvasionPercent(25)
                .FuelConsumptionPercent(3);
        }

        private void HeraDivineLance()
        {
            _builder.Create("hera_rweap", MechPartType.RightWeapon)
                .HPPercent(7)
                .FuelPercent(20)
                .AttackPercent(20)
                .EtherAttackPercent(30)
                .DefensePercent(12)
                .EtherDefensePercent(20)
                .AccuracyPercent(25)
                .EvasionPercent(10)
                .FuelConsumptionPercent(10);
        }

        private void DemeterThornLauncher()
        {
            _builder.Create("demeter_rweap", MechPartType.RightWeapon)
                .HPPercent(10)
                .FuelPercent(27)
                .AttackPercent(15)
                .EtherAttackPercent(20)
                .DefensePercent(5)
                .EtherDefensePercent(15)
                .AccuracyPercent(10)
                .EvasionPercent(5)
                .FuelConsumptionPercent(-17);
        }

        private void ValkyrieHonorBlade()
        {
            _builder.Create("valkyrie_rweap", MechPartType.RightWeapon)
                .HPPercent(7)
                .FuelPercent(12)
                .AttackPercent(25)
                .EtherAttackPercent(20)
                .DefensePercent(12)
                .EtherDefensePercent(10)
                .AccuracyPercent(20)
                .EvasionPercent(15)
                .FuelConsumptionPercent(-2);
        }
    }
}