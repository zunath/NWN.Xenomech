using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Shared.Core.EventManagement;

namespace XM.Progression.Event
{
    public class AbilityEvent
    {
        public struct OnQueueWeaponSkill : IXMEvent
        {
        }

        internal struct OnAbilitiesRegistered : IXMEvent
        {
            public List<AbilityDetail> Abilities { get; }

            public OnAbilitiesRegistered(List<AbilityDetail> abilities)
            {
                Abilities = abilities;
            }
        }
    }
}
