using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using XM.API.Constants;

namespace XM.UI
{
    [ServiceBinding(typeof(DisableDefaultGameWindowsService))]
    internal class DisableDefaultGameWindowsService
    {
        public DisableDefaultGameWindowsService()
        {
            NwModule.Instance.OnClientEnter += OnModuleEnter;
        }

        private void OnModuleEnter(ModuleEvents.OnClientEnter obj)
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
