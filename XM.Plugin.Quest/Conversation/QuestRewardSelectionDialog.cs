using System.Linq;
using Anvil.Services;
using XM.Quest.Reward;
using XM.Shared.Core.Dialog;

namespace XM.Quest.Conversation
{
    [ServiceBinding(typeof(IConversation))]
    internal class QuestRewardSelectionDialog: DialogBase
    {
        private class Model
        {
            public string QuestId { get; set; }
        }

        [Inject]
        private QuestService Quest { get; set; }

        private const string MainPageId = "MAIN";

        public override PlayerDialog SetUp(uint player)
        {
            var builder = new DialogBuilder()
                .WithDataModel(new Model())
                .AddInitializationAction(Initialize)
                .AddPage(MainPageId, MainPageInit);

            return builder.Build();
        }

        private void Initialize()
        {
            const string RewardSelectionQuestIdVariable = "QST_REWARD_SELECTION_QUEST_ID";

            var player = GetPC();
            var questId = GetLocalString(player, RewardSelectionQuestIdVariable);
            var model = GetDataModel<Model>();

            model.QuestId = questId;
            DeleteLocalString(player, RewardSelectionQuestIdVariable);
        }

        private void MainPageInit(DialogPage page)
        {
            var model = GetDataModel<Model>();
            var quest = Quest.GetQuestById(model.QuestId);

            void HandleRewardSelection(IQuestReward reward)
            {
                Quest.CompleteQuest(GetPC(), model.QuestId, GetPC(), reward);
                EndConversation();
            }
            page.Header = "Please select a reward.";

            var rewardItems = quest.GetRewards().Where(x => x.IsSelectable);

            foreach (var reward in rewardItems)
            {
                page.AddResponse(reward.MenuName, () =>
                {
                    HandleRewardSelection(reward);
                });
            }
        }

    }
}
