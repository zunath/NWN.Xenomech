using Anvil.Services;
using System.Collections.Generic;

namespace XM.Plugin.Mech.FrameDefinitions
{
    [ServiceBinding(typeof(IMechFrameListDefinition))]
    internal class NexusMechFrameDefinition : IMechFrameListDefinition
    {
        private readonly MechFrameBuilder _builder = new();

        public Dictionary<string, MechFrameStats> BuildMechFrames()
        {
            NexusMkI();
            NexusMkII();
            NexusMkIII();
            NexusMkIV();
            NexusMkV();

            return _builder.Build();
        }

        private void NexusMkI()
        {
            _builder.Create("nexus_mk1")
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
            _builder.Create("nexus_mk2")
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
            _builder.Create("nexus_mk3")
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
            _builder.Create("nexus_mk4")
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
            _builder.Create("nexus_mk5")
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
    }
}
