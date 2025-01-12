using System.Collections.Generic;
using Anvil.Services;

namespace XM.Quest.QuestDefinition
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
            GoblinTest2();
            GoblinTest3();

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
        private void GoblinTest2()
        {
            Builder.Create("goblin_test2", "Goblin Test Quest 2")

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
        private void GoblinTest3()
        {
            Builder.Create("goblin_test3", "Goblin Test Quest 3")

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