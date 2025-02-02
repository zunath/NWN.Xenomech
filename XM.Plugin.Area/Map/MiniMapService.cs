﻿using Anvil.Services;
using Pipelines.Sockets.Unofficial.Arenas;
using XM.Inventory.Event;
using XM.Inventory.KeyItem;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Area.Map
{
    [ServiceBinding(typeof(MiniMapService))]
    internal class MiniMapService
    {
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
            _event.Subscribe<InventoryEvent.GiveKeyItemEvent>(OnPlayerReceivesKeyItem);
        }

        private void OnPlayerReceivesKeyItem(uint player)
        {
            var data = _event.GetEventData<InventoryEvent.GiveKeyItemEvent>();
            var area = GetArea(player);
            var keyItemId = GetLocalInt(area, MapConstants.AreaMapKeyItemIdVariable);

            if (keyItemId == (int)data.KeyItem)
            {
                ExploreAreaForPlayer(area, player);
                SetGuiPanelDisabled(player, GuiPanelType.Minimap, false);
            }
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

            var isMiniMapDisabled = GetLocalBool(area, MapConstants.AreaMiniMapDisabledVariable);
            if (isMiniMapDisabled)
            {
                SetGuiPanelDisabled(player, GuiPanelType.Minimap, true);
            }

            var keyItemId = GetLocalInt(area, MapConstants.AreaMapKeyItemIdVariable);
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
            if (GetLocalBool(area, MapConstants.AreaMiniMapDisabledVariable))
                return;

            var message = ColorToken.Red(LocaleString.YouDoNotHaveAMapOfThisArea.ToLocalizedString());
            SendMessageToPC(player, message);
        }
    }
}
