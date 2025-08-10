using NLog;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Shared.Core;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Plugin.Administration.StaffManagement
{
    internal class ManageStaffViewModel: ViewModel<ManageStaffViewModel>
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static readonly Color _green = new(0, 255, 0);
        private static readonly Color _red = new(255, 0, 0);

        [Inject]
        public DBService DB { get; set; }

        private int SelectedUserIndex { get; set; }
        private readonly List<string> _userIds = new List<string>();

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

        public XMBindingList<string> Names
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> UserToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public string ActiveUserName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ActiveUserCDKey
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsDeleteEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsUserSelected
        {
            get => Get<bool>();
            set => Set(value);
        }

        public int SelectedRoleId
        {
            get => Get<int>();
            set => Set(value);
        }

        public Action OnSelectUser() => () =>
        {
            if (SelectedUserIndex > -1)
                UserToggles[SelectedUserIndex] = false;

            var index = NuiGetEventArrayIndex();
            SelectedUserIndex = index;
            var userId = _userIds[index];
            var dbUser = DB.Get<AuthorizedDM>(userId);
            var userCDKey = GetPCPublicCDKey(Player);

            IsUserSelected = true;
            IsDeleteEnabled = userCDKey != dbUser.CDKey;
            ActiveUserName = dbUser.Name;
            ActiveUserCDKey = dbUser.CDKey;
            UserToggles[index] = true;
            SelectedRoleId = dbUser.Authorization == AuthorizationLevel.DM ? 0 : 1;

            StatusText = string.Empty;
        };

        public Action OnClickNewUser() => () =>
        {
            var newUser = new AuthorizedDM
            {
                Authorization = AuthorizationLevel.DM,
                CDKey = string.Empty,
                Name = LocaleString.NewUser.ToLocalizedString()
            };

            _userIds.Add(newUser.Id);
            Names.Add(newUser.Name);
            UserToggles.Add(false);

            DB.Set(newUser);

            StatusText = string.Empty;
        };

        public Action OnClickDeleteUser() => () =>
        {
            ShowModal(LocaleString.AreYouSureYouWantToDeleteThisStaffMember.ToLocalizedString(), () =>
            {
                var userCDKey = GetPCPublicCDKey(Player);
                var userId = _userIds[SelectedUserIndex];
                var dbUser = DB.Get<AuthorizedDM>(userId);

                if (dbUser.CDKey == userCDKey)
                {
                    StatusText = LocaleString.YouCantDeleteYourself.ToLocalizedString();
                    StatusColor = _red;
                    return;
                }

                DB.Delete<AuthorizedDM>(userId);

                IsUserSelected = false;
                IsDeleteEnabled = false;
                ActiveUserCDKey = string.Empty;
                ActiveUserName = string.Empty;

                _userIds.RemoveAt(SelectedUserIndex);
                Names.RemoveAt(SelectedUserIndex);
                UserToggles.RemoveAt(SelectedUserIndex);

                SelectedUserIndex = -1;
                StatusText = LocaleString.UserDeletedSuccessfully.ToLocalizedString();
                StatusColor = _green;

                _logger.Info(LocaleString.UserDeletedFromAuthorizedDMListNameXCDKeyYRoleZ.ToLocalizedString(dbUser.Name, dbUser.CDKey, dbUser.Authorization));
            });
        };

        public Action OnClickSave() => () =>
        {
            if (ActiveUserCDKey.Length != 8)
            {
                StatusText = LocaleString.CDKeysMustBeExactly8Digits.ToLocalizedString();
                StatusColor = _red;
                return;
            }

            var userId = _userIds[SelectedUserIndex];
            var dbUser = DB.Get<AuthorizedDM>(userId);
            var userCDKey = GetPCPublicCDKey(Player);

            dbUser.Name = ActiveUserName;
            dbUser.CDKey = ActiveUserCDKey;
            dbUser.Authorization = SelectedRoleId == 0 ? AuthorizationLevel.DM : AuthorizationLevel.Admin;

            IsDeleteEnabled = userCDKey != dbUser.CDKey;

            DB.Set(dbUser);

            Names[SelectedUserIndex] = dbUser.Name;

            StatusText = LocaleString.SavedSuccessfully.ToLocalizedString();
            StatusColor = _green;

            _logger.Info(LocaleString.UserUpdatedOnAuthorizedDMListNameXCDKeyYRoleZ.ToLocalizedString(dbUser.Name, dbUser.CDKey, dbUser.Authorization));
        };

        public Action OnClickDiscardChanges() => () =>
        {
            var userId = _userIds[SelectedUserIndex];
            var dbUser = DB.Get<AuthorizedDM>(userId);

            ActiveUserName = dbUser.Name;
            ActiveUserCDKey = dbUser.CDKey;

            StatusText = string.Empty;
        };
        public override void OnOpen()
        {
            SelectedUserIndex = -1;
            ActiveUserCDKey = string.Empty;
            ActiveUserName = string.Empty;
            SelectedRoleId = 0;

            _userIds.Clear();
            var query = new DBQuery();
            var users = DB.Search<AuthorizedDM>(query);

            var names = new XMBindingList<string>();
            var toggles = new XMBindingList<bool>();

            foreach (var user in users)
            {
                _userIds.Add(user.Id);
                names.Add(user.Name);
                toggles.Add(false);
            }

            Names = names;
            UserToggles = toggles;

            WatchOnClient(model => model.ActiveUserCDKey);
            WatchOnClient(model => model.ActiveUserName);
            WatchOnClient(model => model.SelectedRoleId);
        }

        public override void OnClose()
        {
            
        }
    }
}
