using Anvil.Services;
using System.Collections.Generic;

namespace XM.Plugin.Mech.FrameDefinitions
{
    [ServiceBinding(typeof(IMechFrameListDefinition))]
    internal class HeraldMechFrameDefinition : IMechFrameListDefinition
    {
        private readonly MechFrameBuilder _builder = new();

        public Dictionary<string, MechFrame> BuildMechFrames()
        {
            HeraldMkI();
            HeraldMkII();
            HeraldMkIII();
            HeraldMkIV();
            HeraldMkV();

            return _builder.Build();
        }
        private void HeraldMkI()
        {
            _builder.Create("herald_mk1")
                .LevelRequirement(10)
                .BaseHP(600)
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
            _builder.Create("herald_mk2")
                .LevelRequirement(20)
                .BaseHP(900)
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
            _builder.Create("herald_mk3")
                .LevelRequirement(30)
                .BaseHP(1350)
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
            _builder.Create("herald_mk4")
                .LevelRequirement(40)
                .BaseHP(2025)
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
            _builder.Create("herald_mk5")
                .LevelRequirement(50)
                .BaseHP(3037)
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
