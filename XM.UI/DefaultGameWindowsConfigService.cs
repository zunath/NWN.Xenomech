using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.UI
{
    [ServiceBinding(typeof(DefaultGameWindowsConfigService))]
    internal class DefaultGameWindowsConfigService
    {
        private readonly XMEventService _event;

        public DefaultGameWindowsConfigService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnModuleEnter);
        }

        private void OnModuleEnter()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            // Spell Book - Completely unused
            SetGuiPanelDisabled(player, GuiPanelType.SpellBook, true);

            // Character Sheet - A NUI replacement is used
            SetGuiPanelDisabled(player, GuiPanelType.CharacterSheet, true);

            // Journal - A NUI replacement is used
            SetGuiPanelDisabled(player, GuiPanelType.Journal, true);

            // Compass - Space is used by HP/FP/STM bars.
            SetGuiPanelDisabled(player, GuiPanelType.Compass, true);
        }
    }
}
