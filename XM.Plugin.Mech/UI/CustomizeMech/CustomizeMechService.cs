using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Plugin.Mech.UI.CustomizeMech
{
    [ServiceBinding(typeof(CustomizeMechService))]
    internal class CustomizeMechService
    {
        private GuiService _gui;

        public CustomizeMechService(GuiService gui)
        {
            _gui = gui;
        }

        [ScriptHandler("mech_customize")]
        public void UseMechCustomizationTerminal()
        {
            var player = GetLastUsedBy();

            if (!GetIsPC(player) || GetIsDM(player))
            {
                SendMessageToPC(player, LocaleString.OnlyPlayersMayUseThisItem.ToLocalizedString());
                return;
            }

            _gui.ShowWindow<CustomizeMechView>(player, null, OBJECT_SELF);
        }
    }
}
