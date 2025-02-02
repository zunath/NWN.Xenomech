using Anvil.Services;
using NWN.Core.NWNX;
using XM.Shared.Core.EventManagement;

namespace XM.Shared.Core
{
    [ServiceBinding(typeof(SaveCharacterService))]
    internal class SaveCharacterService
    {
        private const string SaveCharactersVariable = "SAVE_CHARACTERS_TICK";
        private const string IsBarteringVariable = "IS_BARTERING";

        private readonly XMEventService _event;

        public SaveCharacterService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<PlayerEvent.OnHeartbeat>(SaveCharacter);
            _event.Subscribe<NWNXEvent.OnBarterStartBefore>(StartBarter);
            _event.Subscribe<NWNXEvent.OnBarterEndBefore>(EndBarter);
        }

        private void SaveCharacter(uint player)
        {
            var tick = GetLocalInt(player, SaveCharactersVariable) + 1;

            if (tick >= 10)
            {
                if (!GetLocalBool(player, IsBarteringVariable))
                {
                    ExportSingleCharacter(player);
                    tick = 0;
                }
            }

            SetLocalInt(player, SaveCharactersVariable, tick);
        }

        private void StartBarter(uint player1)
        {
            var player2 = StringToObject(EventsPlugin.GetEventData("BARTER_TARGET"));

            SetLocalBool(player1, IsBarteringVariable, true);
            SetLocalBool(player2, IsBarteringVariable, true);
        }

        private void EndBarter(uint player1)
        {
            var player2 = StringToObject(EventsPlugin.GetEventData("BARTER_TARGET"));

            DeleteLocalBool(player1, IsBarteringVariable);
            DeleteLocalBool(player2, IsBarteringVariable);
        }

    }
}
