using System.Collections.Generic;
using Anvil.Services;

namespace XM.Plugin.Mech.FrameDefinitions
{
    [ServiceBinding(typeof(IMechFrameListDefinition))]
    internal class MechFrameDefinition : IMechFrameListDefinition
    {
        private readonly MechFrameBuilder _builder = new();

        public Dictionary<string, MechFrameStats> BuildMechFrames()
        {
            // Tank Frames
            AegisMkI();
            AegisMkII();
            AegisMkIII();
            AegisMkIV();
            AegisMkV();

            // Damage Frames
            RavagerMkI();
            RavagerMkII();
            RavagerMkIII();
            RavagerMkIV();
            RavagerMkV();

            // Ether Frames
            NexusMkI();
            NexusMkII();
            NexusMkIII();
            NexusMkIV();
            NexusMkV();

            // Support Frames
            HeraldMkI();
            HeraldMkII();
            HeraldMkIII();
            HeraldMkIV();
            HeraldMkV();

            return _builder.Build();
        }

        // Tank Frames
        private void AegisMkI()
        {
            _builder.Create("aegis_mk1", MechFrameType.Tank)
                .LevelRequirement(10)
                .BaseHp(900)
                .BaseFuel(100)
                .BaseAttack(20)
                .BaseEtherAttack(15)
                .BaseDefense(45)
                .BaseEtherDefense(30)
                .BaseAccuracy(70)
                .BaseEvasion(10)
                .BaseFuelConsumption(10);
        }

        private void AegisMkII()
        {
            _builder.Create("aegis_mk2", MechFrameType.Tank)
                .LevelRequirement(20)
                .BaseHp(1350)
                .BaseFuel(150)
                .BaseAttack(30)
                .BaseEtherAttack(22)
                .BaseDefense(67)
                .BaseEtherDefense(45)
                .BaseAccuracy(75)
                .BaseEvasion(15)
                .BaseFuelConsumption(12);
        }

        private void AegisMkIII()
        {
            _builder.Create("aegis_mk3", MechFrameType.Tank)
                .LevelRequirement(30)
                .BaseHp(2025)
                .BaseFuel(225)
                .BaseAttack(45)
                .BaseEtherAttack(33)
                .BaseDefense(100)
                .BaseEtherDefense(67)
                .BaseAccuracy(80)
                .BaseEvasion(20)
                .BaseFuelConsumption(15);
        }

        private void AegisMkIV()
        {
            _builder.Create("aegis_mk4", MechFrameType.Tank)
                .LevelRequirement(40)
                .BaseHp(3037)
                .BaseFuel(337)
                .BaseAttack(67)
                .BaseEtherAttack(50)
                .BaseDefense(150)
                .BaseEtherDefense(100)
                .BaseAccuracy(85)
                .BaseEvasion(25)
                .BaseFuelConsumption(18);
        }

        private void AegisMkV()
        {
            _builder.Create("aegis_mk5", MechFrameType.Tank)
                .LevelRequirement(50)
                .BaseHp(4556)
                .BaseFuel(506)
                .BaseAttack(100)
                .BaseEtherAttack(75)
                .BaseDefense(225)
                .BaseEtherDefense(150)
                .BaseAccuracy(90)
                .BaseEvasion(30)
                .BaseFuelConsumption(22);
        }

        // Damage Frames
        private void RavagerMkI()
        {
            _builder.Create("ravager_mk1", MechFrameType.Damage)
                .LevelRequirement(10)
                .BaseHp(450)
                .BaseFuel(120)
                .BaseAttack(50)
                .BaseEtherAttack(25)
                .BaseDefense(20)
                .BaseEtherDefense(20)
                .BaseAccuracy(85)
                .BaseEvasion(20)
                .BaseFuelConsumption(12);
        }

        private void RavagerMkII()
        {
            _builder.Create("ravager_mk2", MechFrameType.Damage)
                .LevelRequirement(20)
                .BaseHp(675)
                .BaseFuel(180)
                .BaseAttack(75)
                .BaseEtherAttack(37)
                .BaseDefense(30)
                .BaseEtherDefense(30)
                .BaseAccuracy(90)
                .BaseEvasion(25)
                .BaseFuelConsumption(15);
        }

        private void RavagerMkIII()
        {
            _builder.Create("ravager_mk3", MechFrameType.Damage)
                .LevelRequirement(30)
                .BaseHp(1012)
                .BaseFuel(270)
                .BaseAttack(112)
                .BaseEtherAttack(56)
                .BaseDefense(45)
                .BaseEtherDefense(45)
                .BaseAccuracy(95)
                .BaseEvasion(30)
                .BaseFuelConsumption(18);
        }

        private void RavagerMkIV()
        {
            _builder.Create("ravager_mk4", MechFrameType.Damage)
                .LevelRequirement(40)
                .BaseHp(1518)
                .BaseFuel(405)
                .BaseAttack(168)
                .BaseEtherAttack(84)
                .BaseDefense(67)
                .BaseEtherDefense(67)
                .BaseAccuracy(100)
                .BaseEvasion(37)
                .BaseFuelConsumption(22);
        }

        private void RavagerMkV()
        {
            _builder.Create("ravager_mk5", MechFrameType.Damage)
                .LevelRequirement(50)
                .BaseHp(2277)
                .BaseFuel(607)
                .BaseAttack(252)
                .BaseEtherAttack(126)
                .BaseDefense(100)
                .BaseEtherDefense(100)
                .BaseAccuracy(105)
                .BaseEvasion(45)
                .BaseFuelConsumption(27);
        }

        // Ether Frames
        private void NexusMkI()
        {
            _builder.Create("nexus_mk1", MechFrameType.Ether)
                .LevelRequirement(10)
                .BaseHp(400)
                .BaseFuel(140)
                .BaseAttack(25)
                .BaseEtherAttack(50)
                .BaseDefense(15)
                .BaseEtherDefense(35)
                .BaseAccuracy(80)
                .BaseEvasion(25)
                .BaseFuelConsumption(8);
        }

        private void NexusMkII()
        {
            _builder.Create("nexus_mk2", MechFrameType.Ether)
                .LevelRequirement(20)
                .BaseHp(600)
                .BaseFuel(210)
                .BaseAttack(37)
                .BaseEtherAttack(75)
                .BaseDefense(22)
                .BaseEtherDefense(52)
                .BaseAccuracy(85)
                .BaseEvasion(30)
                .BaseFuelConsumption(10);
        }

        private void NexusMkIII()
        {
            _builder.Create("nexus_mk3", MechFrameType.Ether)
                .LevelRequirement(30)
                .BaseHp(900)
                .BaseFuel(315)
                .BaseAttack(56)
                .BaseEtherAttack(112)
                .BaseDefense(33)
                .BaseEtherDefense(78)
                .BaseAccuracy(90)
                .BaseEvasion(37)
                .BaseFuelConsumption(12);
        }

        private void NexusMkIV()
        {
            _builder.Create("nexus_mk4", MechFrameType.Ether)
                .LevelRequirement(40)
                .BaseHp(1350)
                .BaseFuel(472)
                .BaseAttack(84)
                .BaseEtherAttack(168)
                .BaseDefense(50)
                .BaseEtherDefense(117)
                .BaseAccuracy(95)
                .BaseEvasion(45)
                .BaseFuelConsumption(15);
        }

        private void NexusMkV()
        {
            _builder.Create("nexus_mk5", MechFrameType.Ether)
                .LevelRequirement(50)
                .BaseHp(2025)
                .BaseFuel(708)
                .BaseAttack(126)
                .BaseEtherAttack(252)
                .BaseDefense(75)
                .BaseEtherDefense(175)
                .BaseAccuracy(100)
                .BaseEvasion(55)
                .BaseFuelConsumption(18);
        }

        // Support Frames
        private void HeraldMkI()
        {
            _builder.Create("herald_mk1", MechFrameType.Support)
                .LevelRequirement(10)
                .BaseHp(600)
                .BaseFuel(160)
                .BaseAttack(30)
                .BaseEtherAttack(35)
                .BaseDefense(25)
                .BaseEtherDefense(30)
                .BaseAccuracy(80)
                .BaseEvasion(20)
                .BaseFuelConsumption(6);
        }

        private void HeraldMkII()
        {
            _builder.Create("herald_mk2", MechFrameType.Support)
                .LevelRequirement(20)
                .BaseHp(900)
                .BaseFuel(240)
                .BaseAttack(45)
                .BaseEtherAttack(52)
                .BaseDefense(37)
                .BaseEtherDefense(45)
                .BaseAccuracy(85)
                .BaseEvasion(25)
                .BaseFuelConsumption(8);
        }

        private void HeraldMkIII()
        {
            _builder.Create("herald_mk3", MechFrameType.Support)
                .LevelRequirement(30)
                .BaseHp(1350)
                .BaseFuel(360)
                .BaseAttack(67)
                .BaseEtherAttack(78)
                .BaseDefense(56)
                .BaseEtherDefense(67)
                .BaseAccuracy(90)
                .BaseEvasion(30)
                .BaseFuelConsumption(10);
        }

        private void HeraldMkIV()
        {
            _builder.Create("herald_mk4", MechFrameType.Support)
                .LevelRequirement(40)
                .BaseHp(2025)
                .BaseFuel(540)
                .BaseAttack(100)
                .BaseEtherAttack(117)
                .BaseDefense(84)
                .BaseEtherDefense(100)
                .BaseAccuracy(95)
                .BaseEvasion(37)
                .BaseFuelConsumption(12);
        }

        private void HeraldMkV()
        {
            _builder.Create("herald_mk5", MechFrameType.Support)
                .LevelRequirement(50)
                .BaseHp(3037)
                .BaseFuel(810)
                .BaseAttack(150)
                .BaseEtherAttack(175)
                .BaseDefense(126)
                .BaseEtherDefense(150)
                .BaseAccuracy(100)
                .BaseEvasion(45)
                .BaseFuelConsumption(15);
        }
    }
}