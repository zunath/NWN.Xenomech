using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.ChatCommand
{
    [ServiceBinding(typeof(TypingIndicatorService))]
    internal class TypingIndicatorService
    {
        private const string TypingIndicatorTag = "TYPING_INDICATOR";

        private readonly XMEventService _event;

        public TypingIndicatorService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerGui>(OnChatBarFocusChanged);
        }

        private void OnChatBarFocusChanged(uint module)
        {
            var player = GetLastGuiEventPlayer();
            var type = GetLastGuiEventType();
            if (!GetIsPC(player))
                return;

            if (type == GuiEventType.ChatbarFocus)
            {
                var effect = EffectVisualEffect(VisualEffectType.ChatBubble, false, 0.5f);
                effect = TagEffect(effect, TypingIndicatorTag);
                ApplyEffectToObject(DurationType.Temporary, effect, player, 120f);
            }
            else if (type == GuiEventType.ChatbarUnfocus)
            {
                RemoveEffectByTag(player, TypingIndicatorTag);
            }
        }
    }
}
