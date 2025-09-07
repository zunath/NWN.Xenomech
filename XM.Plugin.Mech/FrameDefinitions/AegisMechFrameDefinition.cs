using Anvil.Services;
using System.Collections.Generic;

namespace XM.Plugin.Mech.FrameDefinitions
{
    [ServiceBinding(typeof(IMechFrameListDefinition))]
    internal class AegisMechFrameDefinition : IMechFrameListDefinition
    {
        private readonly MechFrameBuilder _builder = new();

        public Dictionary<string, MechFrame> BuildMechFrames()
        {
            AegisMkI();
            AegisMkII();
            AegisMkIII();
            AegisMkIV();
            AegisMkV();

            return _builder.Build();
        }

        private void AegisMkI()
        {
            _builder.Create("aegis_mk1")
                .LevelRequirement(10)
                .BaseHP(900)
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
            _builder.Create("aegis_mk2")
                .LevelRequirement(20)
                .BaseHP(1350)
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
            _builder.Create("aegis_mk3")
                .LevelRequirement(30)
                .BaseHP(2025)
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
            _builder.Create("aegis_mk4")
                .LevelRequirement(40)
                .BaseHP(3037)
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
            _builder.Create("aegis_mk5")
                .LevelRequirement(50)
                .BaseHP(4556)
                .BaseFuel(506)
                .BaseAttack(100)
                .BaseEtherAttack(75)
                .BaseDefense(225)
                .BaseEtherDefense(150)
                .BaseAccuracy(90)
                .BaseEvasion(30)
                .BaseFuelConsumption(22);
        }
    }
}
