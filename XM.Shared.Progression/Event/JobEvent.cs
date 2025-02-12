using XM.Progression.Job.JobDefinition;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.Progression.Event
{
    public class JobEvent
    {
        public struct PlayerChangedJobEvent : IXMEvent
        {
            public IJobDefinition Definition { get; set; }
            public int Level { get; set; }

            public PlayerChangedJobEvent(IJobDefinition definition, int level)
            {
                Definition = definition;
                Level = level;
            }
        }

        public struct PlayerLeveledUpEvent : IXMEvent
        {
            public IJobDefinition Definition { get; set; }
            public int Level { get; set; }

            public PlayerLeveledUpEvent(IJobDefinition definition, int level)
            {
                Definition = definition;
                Level = level;
            }
        }

        public struct JobFeatRemovedEvent : IXMEvent
        {
            public FeatType Feat { get; set; }
            public JobFeatRemovedEvent(FeatType feat)
            {
                Feat = feat;
            }
        }

        public struct JobFeatAddedEvent : IXMEvent
        {
            public FeatType Feat { get; set; }

            public JobFeatAddedEvent(FeatType feat)
            {
                Feat = feat;
            }
        }
    }
}
