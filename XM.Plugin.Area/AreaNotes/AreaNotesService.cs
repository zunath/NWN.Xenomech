using System.Linq;
using Anvil.Services;
using XM.Shared.Core.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Area.AreaNotes
{
    [ServiceBinding(typeof(AreaNotesService))]
    internal class AreaNotesService
    {
        private readonly XMEventService _event;
        private readonly DBService _db;

        public AreaNotesService(
            XMEventService @event,
            DBService db)
        {
            _event = @event;
            _db = db;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<AreaEvent.OnAreaEnter>(SendAreaNote);
        }

        private void SendAreaNote(uint area)
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player))
                return;

            // Handle DM created Area Notes
            var query = new DBQuery()
                .AddFieldSearch(nameof(AreaNote.AreaResref), GetResRef(area), false)
                .OrderBy(nameof(AreaNote.AreaResref));
            var notes = _db.Search<AreaNote>(query)
                .ToList();

            if (notes.Count > 0)
            {
                var prefix = GetName(area) + ": ";
                var message = string.Empty;
                foreach (var note in notes)
                {
                    message += note.PublicText;
                }

                if (!string.IsNullOrWhiteSpace(message.Trim()))
                {
                    SendMessageToPC(player, ColorToken.Purple(prefix + message));
                }
            }
        }
    }
}
