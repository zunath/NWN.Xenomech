using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using NLog;
using NLog.Fluent;
using XM.Plugin.Administration.Entity;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Plugin.Administration.BanManagement
{
    internal class ManageBanViewModel: ViewModel<ManageBanViewModel>
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static readonly Color _green = new(0, 255, 0);
        private static readonly Color _red = new(255, 0, 0);

        private int SelectedUserIndex { get; set; }
        private readonly List<string> _userIds = new();

        [Inject]
        public DBService DB { get; set; }

        public string StatusText
        {
            get => Get<string>();
            set => Set(value);
        }

        public Color StatusColor
        {
            get => Get<Color>();
            set => Set(value);
        }

        public XMBindingList<string> CDKeys
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> UserToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public string ActiveBanReason
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ActiveUserCDKey
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsUserSelected
        {
            get => Get<bool>();
            set => Set(value);
        }

        public Action OnSelectUser => () =>
        {
            if (SelectedUserIndex > -1)
                UserToggles[SelectedUserIndex] = false;

            var index = NuiGetEventArrayIndex();
            SelectedUserIndex = index;
            var userId = _userIds[index];
            var dbUser = DB.Get<PlayerBan>(userId);

            IsUserSelected = true;
            ActiveBanReason = dbUser.Reason;
            ActiveUserCDKey = dbUser.CDKey;
            UserToggles[index] = true;

            StatusText = string.Empty;
        };

        public Action OnClickNewUser => () =>
        {
            var newBan = new PlayerBan
            {
                CDKey = string.Empty,
                Reason = string.Empty
            };

            _userIds.Add(newBan.Id);
            CDKeys.Add(newBan.CDKey);
            UserToggles.Add(false);

            DB.Set(newBan);

            StatusText = string.Empty;
        };

        public Action OnClickDeleteUser => () =>
        {
            ShowModal(LocaleString.AreYouSureYouWantToDeleteThisBan.ToLocalizedString(), () =>
            {
                var userId = _userIds[SelectedUserIndex];
                var dbUser = DB.Get<PlayerBan>(userId);
                if (dbUser == null)
                    return;

                DB.Delete<PlayerBan>(userId);

                IsUserSelected = false;
                ActiveUserCDKey = string.Empty;
                ActiveBanReason = string.Empty;

                _userIds.RemoveAt(SelectedUserIndex);
                CDKeys.RemoveAt(SelectedUserIndex);
                UserToggles.RemoveAt(SelectedUserIndex);

                SelectedUserIndex = -1;
                StatusText = LocaleString.UserBanDeletedSuccessfully.ToLocalizedString();
                StatusColor = _green;

                AdminPlugin.RemoveBannedCDKey(dbUser.CDKey);

                _logger.Info(LocaleString.UserDeletedFromBanListCDKeyXReasonY.ToLocalizedString(dbUser.CDKey, dbUser.Reason));
            });
        };

        public Action OnClickSave => () =>
        {
            if (ActiveUserCDKey.Length != 8)
            {
                StatusText = LocaleString.CDKeysMustBeExactly8Digits.ToLocalizedString();
                StatusColor = _red;
                return;
            }

            var userId = _userIds[SelectedUserIndex];
            var dbUser = DB.Get<PlayerBan>(userId);

            AdminPlugin.RemoveBannedCDKey(dbUser.CDKey);

            dbUser.Reason = ActiveBanReason;
            dbUser.CDKey = ActiveUserCDKey;

            DB.Set(dbUser);

            AdminPlugin.AddBannedCDKey(dbUser.CDKey);

            CDKeys[SelectedUserIndex] = dbUser.CDKey;

            StatusText = LocaleString.SavedSuccessfully.ToLocalizedString();
            StatusColor = _green;

            _logger.Info(LocaleString.UserAddedToBanListCDKeyXReasonY.ToLocalizedString(dbUser.CDKey, dbUser.Reason));
        };

        public Action OnClickDiscardChanges => () =>
        {
            var userId = _userIds[SelectedUserIndex];
            var dbUser = DB.Get<PlayerBan>(userId);

            ActiveBanReason = dbUser.Reason;
            ActiveUserCDKey = dbUser.CDKey;

            StatusText = string.Empty;
        };
        public override void OnOpen()
        {
            SelectedUserIndex = -1;
            ActiveUserCDKey = string.Empty;
            ActiveBanReason = string.Empty;

            _userIds.Clear();
            var query = new DBQuery();
            var users = DB.Search<PlayerBan>(query);

            var cdKeys = new XMBindingList<string>();
            var toggles = new XMBindingList<bool>();

            foreach (var user in users)
            {
                _userIds.Add(user.Id);
                toggles.Add(false);
                cdKeys.Add(user.CDKey);
            }

            CDKeys = cdKeys;
            UserToggles = toggles;

            WatchOnClient(model => model.ActiveUserCDKey);
            WatchOnClient(model => model.ActiveBanReason);
        }

        public override void OnClose()
        {
            
        }
    }
}
