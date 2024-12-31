using System.Collections.Generic;
using Anvil.Services;

namespace XM.Quest.QuestDefinitions
{
    [ServiceBinding(typeof(GoblinTestQuest))]
    internal class GoblinTestQuest: QuestListDefinitionBase
    {
        public GoblinTestQuest(QuestBuilder builder) 
            : base(builder)
        {
        }


        public override Dictionary<string, QuestDetail> BuildQuests()
        {
            GoblinTest();

            return Builder.Build();
        }

        private void GoblinTest()
        {
            Builder.Create("goblin_test", "Goblin Test Quest")

                .AddState()
                .SetStateJournalText(
                    "Kill goblins bitch")
                .AddKillObjective(QuestNPCGroupType.Goblin, 1)

                .AddState()
                .SetStateJournalText(
                    "You've killed 1 goblin go finish the job")

                .AddXPReward(200)
                .AddGoldReward(100);
        }

    }
}