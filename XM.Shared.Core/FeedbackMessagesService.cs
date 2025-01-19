using Anvil.Services;
using XM.Shared.API.NWNX.FeedbackPlugin;

namespace XM.Shared.Core
{
    [ServiceBinding(typeof(FeedbackMessagesService))]
    internal class FeedbackMessagesService: IInitializable
    {
        public void Init()
        {
            FeedbackPlugin.SetFeedbackMessageHidden(FeedbackMessageType.UseItemCantUse, true);
            FeedbackPlugin.SetFeedbackMessageHidden(FeedbackMessageType.CombatRunningOutOfAmmo, true);
            FeedbackPlugin.SetFeedbackMessageHidden(FeedbackMessageType.RestBeginningRest, true);
            FeedbackPlugin.SetFeedbackMessageHidden(FeedbackMessageType.RestFinishedRest, true);
            FeedbackPlugin.SetFeedbackMessageHidden(FeedbackMessageType.RestCancelRest, true);
        }
    }
}
