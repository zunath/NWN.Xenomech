using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Administration.BanManagement;
using XM.Plugin.Administration.StaffManagement;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.EventManagement;
using XM.UI.Event;

namespace XM.Plugin.Administration
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class AdministrationChatCommands : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        [Inject]
        public XMEventService Event { get; set; }

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            ManageBans();
            ManageStaff();

            return _builder.Build();
        }

        private void ManageBans()
        {
            _builder.Create("managebans")
                .Description("Toggles the manage bans window to add/remove banned players.")
                .Permissions(AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    Event.PublishEvent(user, new UIEvent.OpenWindow(typeof(BanView)));
                });
        }

        private void ManageStaff()
        {
            _builder.Create("managestaff")
                .Description("Toggles the manage staff window to add/remove staff members.")
                .Permissions(AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    Event.PublishEvent(user, new UIEvent.OpenWindow(typeof(StaffView)));
                });
        }
    }
}
