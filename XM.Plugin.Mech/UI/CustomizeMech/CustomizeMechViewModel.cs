using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Plugin.Mech;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Plugin.Mech.UI.CustomizeMech
{
    [ServiceBinding(typeof(IViewModel))]
    internal class CustomizeMechViewModel :
        ViewModel<CustomizeMechViewModel>
    {
        public XMBindingList<string> MechNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> MechToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public int SelectedMech
        {
            get => Get<int>();
            set => Set(value);
        }

        public string MechCountText
        {
            get => Get<string>();
            set => Set(value);
        }

        [Inject]
        public DBService DB { get; set; }

        public override void OnOpen()
        {
            SelectedMech = -1;
            LoadMechList();
        }

        public override void OnClose()
        {
            
        }

        private void LoadMechList()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId) ?? new PlayerMech(playerId);

            var mechNames = new XMBindingList<string>();
            var mechToggles = new XMBindingList<bool>();

            foreach (var (_, mechConfig) in dbPlayerMech.SavedMechs)
            {
                mechNames.Add(mechConfig.Name);
                mechToggles.Add(false);
            }

            MechNames = mechNames;
            MechToggles = mechToggles;
            
            // Update mech count display
            var currentCount = dbPlayerMech.SavedMechs.Count;
            var maxCount = dbPlayerMech.MechCap;
            MechCountText = $"{LocaleString.Mechs.ToLocalizedString()}: {currentCount}/{maxCount}";
        }

        public Action OnSelectMech() => () =>
        {
            if (SelectedMech > -1)
                MechToggles[SelectedMech] = false;

            var index = NuiGetEventArrayIndex();
            SelectedMech = index;
            MechToggles[index] = true;
        };

        public Action OnNewMech() => () =>
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId) ?? new PlayerMech(playerId);
            
            // Check if player can create more mechs
            if (dbPlayerMech.SavedMechs.Count >= dbPlayerMech.MechCap)
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.YourHangarCannotSupportAnyMoreMechs.ToLocalizedString()));
                return;
            }
            
            // Create new mech record
            var newMechId = Guid.NewGuid();
            var mechNumber = dbPlayerMech.SavedMechs.Count + 1;
            var mechName = $"{LocaleString.Mech.ToLocalizedString()} {mechNumber}";
            
            var newMechConfig = new MechConfiguration
            {
                FrameResref = string.Empty,
                Name = mechName,
                Parts = new Dictionary<int, string>()
            };
            
            dbPlayerMech.SavedMechs[newMechId] = newMechConfig;
            DB.Set(dbPlayerMech);
            
            LoadMechList();
        };

        public Action OnDeleteMech() => () =>
        {
            if (SelectedMech < 0 || SelectedMech >= MechNames.Count)
                return;

            var mechName = MechNames[SelectedMech];
            var confirmMessage = LocaleString.AreYouSureYouWantToDeleteMechX.ToLocalizedString(mechName);
            
            ShowModal(
                confirmMessage,
                ConfirmDeleteMech,
                null,
                LocaleString.Delete,
                LocaleString.Cancel
            );
        };

        private void ConfirmDeleteMech()
        {
            if (SelectedMech < 0 || SelectedMech >= MechNames.Count)
                return;

            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId);
            
            if (dbPlayerMech == null)
                return;

            // Get the mech ID to delete
            var mechIds = dbPlayerMech.SavedMechs.Keys.ToList();
            if (SelectedMech < mechIds.Count)
            {
                var mechIdToDelete = mechIds[SelectedMech];
                dbPlayerMech.SavedMechs.Remove(mechIdToDelete);
                
                // If this was the active mech, clear it
                if (dbPlayerMech.ActiveMechId == mechIdToDelete)
                {
                    dbPlayerMech.ActiveMechId = Guid.Empty;
                }
                
                DB.Set(dbPlayerMech);
                
                // Reset selection and reload
                SelectedMech = -1;
                LoadMechList();
            }
        }
    }
}

