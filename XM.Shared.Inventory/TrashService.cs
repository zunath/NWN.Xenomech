using Anvil.Services;
using NWN.Core.NWNX;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;

namespace XM.Inventory
{
    [ServiceBinding(typeof(TrashService))]
    internal class TrashService
    {
        public TrashService(XMEventService @event)
        {
            @event.Subscribe<NWNXEvent.OnInputDropItemBefore>(PreventItemDrops);
        }
        
        private void PreventItemDrops()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            EventsPlugin.SkipEvent();

            SendMessageToPC(player, ColorToken.Red("Please use the trash can option in your character menu to discard items."));
        }

        [ScriptHandler("trash_opened")]
        public void AlertPlayer()
        {
            var player = GetLastOpenedBy();
            FloatingTextStringOnCreature("Any item placed inside this trash can will be destroyed permanently.", player, false);
        }

        [ScriptHandler("trash_closed")]
        public void CleanUp()
        {
            var container = OBJECT_SELF;
            for (var item = GetFirstItemInInventory(container); GetIsObjectValid(item); item = GetNextItemInInventory(container))
            {
                DestroyObject(item);
            }

            DestroyObject(container);
        }

        [ScriptHandler("trash_disturbed")]
        public void DestroyItem()
        {
            var item = GetInventoryDisturbItem();
            var type = GetInventoryDisturbType();

            if (type != InventoryDisturbType.Added) return;

            DestroyObject(item);
        }
    }
}
