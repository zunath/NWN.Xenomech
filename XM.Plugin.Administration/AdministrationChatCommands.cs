using System;
using System.Collections.Generic;
using System.Globalization;
using Anvil.Services;
using XM.Plugin.Administration.BanManagement;
using XM.Plugin.Administration.BugReport;
using XM.Plugin.Administration.StaffManagement;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using XM.UI.Event;

namespace XM.Plugin.Administration
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class AdministrationChatCommands : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        [Inject]
        public XMEventService Event { get; set; }

        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands()
        {
            ManageBans();
            ManageStaff();
            BugReport();

            return _builder.Build();
        }

        private void ManageBans()
        {
            _builder.Create(LocaleString.managebans)
                .Description(LocaleString.TogglesTheManageBansWindowToAddRemoveBannedPlayers)
                .Permissions(AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    Event.PublishEvent(user, new UIEvent.OpenWindow(typeof(ManageBanView)));
                });
        }

        private void ManageStaff()
        {
            _builder.Create(LocaleString.managestaff)
                .Description(LocaleString.TogglesTheManageStaffWindowToAddRemoveStaffMembers)
                .Permissions(AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    Event.PublishEvent(user, new UIEvent.OpenWindow(typeof(ManageStaffView)));
                });
        }

        private void BugReport()
        {
            _builder.Create(LocaleString.bug)
                .Description(LocaleString.TogglesTheBugReportWindowToSubmitBugs)
                .Permissions(AuthorizationLevel.All)
                .Validate((user, args) =>
                {
                    var lastSubmission = GetLocalString(user, "BUG_REPORT_LAST_SUBMISSION");
                    if (!string.IsNullOrWhiteSpace(lastSubmission))
                    {
                        var dateTime = DateTime.ParseExact(lastSubmission, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                        if (DateTime.UtcNow <= dateTime)
                        {
                            return LocaleString.YouMayOnlySubmitOneBugReportPerMinute.ToLocalizedString();
                        }
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    Event.PublishEvent(user, new UIEvent.OpenWindow(typeof(BugReportView)));
                });
        }
    }
}
