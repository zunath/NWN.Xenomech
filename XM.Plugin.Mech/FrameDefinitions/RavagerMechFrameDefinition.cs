using Anvil.Services;
using System.Collections.Generic;

namespace XM.Plugin.Mech.FrameDefinitions
{
    [ServiceBinding(typeof(IMechFrameListDefinition))]
    internal class RavagerMechFrameDefinition: IMechFrameListDefinition
    {
        private readonly MechFrameBuilder _builder = new();

        public Dictionary<string, MechFrameStats> BuildMechFrames()
        {
            RavagerMkI();
            RavagerMkII();
            RavagerMkIII();
            RavagerMkIV();
            RavagerMkV();

            return _builder.Build();
        }

        private void RavagerMkI()
        {
            _builder.Create("ravager_mk1")
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
            _builder.Create("ravager_mk2")
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
            _builder.Create("ravager_mk3")
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
            _builder.Create("ravager_mk4")
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
            _builder.Create("ravager_mk5")
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
    }
}
