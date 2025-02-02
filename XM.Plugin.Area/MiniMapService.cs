using Anvil.Services;
using XM.Inventory.KeyItem;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Area
{
    [ServiceBinding(typeof(MiniMapService))]
    internal class MiniMapService
    {
        private const string AreaMiniMapDisabledVariable = "MINI_MAP_DISABLED";
        private const string AreaMapKeyItemIdVariable = "MAP_KEY_ITEM_ID";

        private readonly XMEventService _event;
        private readonly KeyItemService _keyItem;

        public MiniMapService(
            XMEventService @event,
            KeyItemService keyItem)
        {
            _event = @event;
            _keyItem = keyItem;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<AreaEvent.OnAreaEnter>(DisableMiniMap);
            _event.Subscribe<AreaEvent.OnAreaExit>(EnableMiniMap);
            _event.Subscribe<ModuleEvent.OnPlayerGui>(OpenMiniMap);
        }


        private void EnableMiniMap(uint area)
        {
            var player = GetExitingObject();
            if (!GetIsPC(player))
                return;

            SetGuiPanelDisabled(player, GuiPanelType.Minimap, false);
        }

        private void DisableMiniMap(uint area)
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var isMiniMapDisabled = GetLocalBool(area, AreaMiniMapDisabledVariable);
            if (isMiniMapDisabled)
            {
                SetGuiPanelDisabled(player, GuiPanelType.Minimap, true);
            }

            var keyItemId = GetLocalInt(area, AreaMapKeyItemIdVariable);
            if (keyItemId > 0)
            {
                var keyItemType = (KeyItemType)keyItemId;
                var hasKeyItem = _keyItem.HasKeyItem(player, keyItemType);

                if (hasKeyItem)
                {
                    SetGuiPanelDisabled(player, GuiPanelType.Minimap, false);
                    ExploreAreaForPlayer(area, player);
                }
            }
        }
        private void OpenMiniMap(uint module)
        {
            var player = GetLastGuiEventPlayer();
            var type = GetLastGuiEventType();
            var panelType = (GuiPanelType)GetLastGuiEventInteger();
            if (!GetIsPC(player) || GetIsDM(player))
                return;
            if (type != GuiEventType.DisabledPanelAttemptOpen)
                return;
            if (panelType != GuiPanelType.Minimap)
                return;

            var area = GetArea(player);
            if (GetLocalBool(area, AreaMiniMapDisabledVariable))
                return;

            var message = ColorToken.Red(LocaleString.YouDoNotHaveAMapOfThisArea.ToLocalizedString());
            SendMessageToPC(player, message);
        }
    }
}
