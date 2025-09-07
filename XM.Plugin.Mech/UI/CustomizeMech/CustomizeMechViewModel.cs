using System;
using Anvil.Services;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
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
        }

        public Action OnSelectMech() => () =>
        {
            if (SelectedMech > -1)
                MechToggles[SelectedMech] = false;

            var index = NuiGetEventArrayIndex();
            SelectedMech = index;
            MechToggles[index] = true;
        };
    }
}

