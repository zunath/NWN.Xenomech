using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Area.AreaNotes.UI;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.EventManagement;
using XM.UI.Event;

namespace XM.Plugin.Area.ChatCommand
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class AreaNotesCommand: IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();
        private readonly XMEventService _event;

        public AreaNotesCommand(XMEventService @event)
        {
            _event = @event;
        }

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            AreaNotes();

            return _builder.Build();
        }

        private void AreaNotes()
        {
            _builder.Create("notes", "note")
                .Description("Toggles the area notes window.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    _event.PublishEvent(user, new UIEvent.OpenWindow(typeof(AreaNotesView)));
                });
        }
    }
}
