using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.API.Extension;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Plugin.Mech.UI.CustomizeMech
{
    [ServiceBinding(typeof(IViewModel))]
    internal class CustomizeMechViewModel :
        ViewModel<CustomizeMechViewModel>
    {
        internal static NuiRect HeadCoordinates { get; } = new(330f, 50f, 128f, 128f);
        internal static NuiRect LeftArmCoordinates { get; } = new(140f, 200f, 128f, 128f);
        internal static NuiRect RightArmCoordinates { get; } = new(520f, 200f, 128f, 128f);
        internal static NuiRect LeftWeaponCoordinates { get; } = new(140f, 350f, 128f, 128f);
        internal static NuiRect RightWeaponCoordinates { get; } = new(520f, 350f, 128f, 128f);
        internal static NuiRect CoreCoordinates { get; } = new(330f, 200f, 128f, 128f);
        internal static NuiRect LegsCoordinates { get; } = new(330f, 500f, 128f, 128f);
        internal static NuiRect GeneratorCoordinates { get; } = new(330f, 350f, 128f, 128f);

        private List<Guid> _mechIds = new();

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

        public string MechHeadResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechLeftArmResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechRightArmResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechCoreResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechLegsResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechGeneratorResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechLeftWeaponResref
        {
            get => Get<string>();
            set => Set(value);
        }
        public string MechRightWeaponResref
        {
            get => Get<string>();
            set => Set(value);
        }

        public int SelectedMechIndex
        {
            get => Get<int>();
            set => Set(value);
        }

        public string MechCountText
        {
            get => Get<string>();
            set => Set(value);
        }

        // Frame selection
        public string SelectedFrameResref
        {
            get => Get<string>();
            set => Set(value);
        }

        public string SelectedFrameName
        {
            get => Get<string>();
            set => Set(value);
        }


        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public MechService MechService { get; set; }

        public override void OnOpen()
        {
            MechHeadResref = MechService.GetMechPartType(MechPartType.Head).DefaultImageResref;
            MechLeftArmResref = MechService.GetMechPartType(MechPartType.LeftArm).DefaultImageResref;
            MechRightArmResref = MechService.GetMechPartType(MechPartType.RightArm).DefaultImageResref;
            MechCoreResref = MechService.GetMechPartType(MechPartType.Core).DefaultImageResref;
            MechLegsResref = MechService.GetMechPartType(MechPartType.Legs).DefaultImageResref;
            MechGeneratorResref = MechService.GetMechPartType(MechPartType.Generator).DefaultImageResref;
            MechLeftWeaponResref = MechService.GetMechPartType(MechPartType.LeftWeapon).DefaultImageResref;
            MechRightWeaponResref = MechService.GetMechPartType(MechPartType.RightWeapon).DefaultImageResref;

            SelectedMechIndex = -1;
            LoadMechList();
        }

        public override void OnClose()
        {
            
        }

        private Shared.Core.Entity.MechConfiguration GetMech()
        {
            if (SelectedMechIndex < 0)
                return null;

            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId);
            var mechId = _mechIds[SelectedMechIndex];

            return dbPlayerMech.SavedMechs[mechId];
        }

        private void LoadMechList()
        {
            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId) ?? new PlayerMech(playerId);

            _mechIds.Clear();
            var mechNames = new XMBindingList<string>();
            var mechToggles = new XMBindingList<bool>();

            foreach (var (mechId, mechConfig) in dbPlayerMech.SavedMechs)
            {
                _mechIds.Add(mechId);
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
            if (SelectedMechIndex > -1)
                MechToggles[SelectedMechIndex] = false;

            var index = NuiGetEventArrayIndex();
            SelectedMechIndex = index;
            MechToggles[index] = true;
            
            LoadSelectedMechConfiguration();
        };

        private void LoadSelectedMechConfiguration()
        {
            if (SelectedMechIndex < 0 || SelectedMechIndex >= MechNames.Count)
            {
                return;
            }

            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId);
            var mechId = _mechIds[SelectedMechIndex];
            var mechConfig = dbPlayerMech.SavedMechs[mechId];

            // Load frame
            SelectedFrameResref = mechConfig.FrameResref;
            SelectedFrameName = string.IsNullOrEmpty(mechConfig.FrameResref) 
                ? LocaleString.NoFrameSelected.ToLocalizedString() 
                : mechConfig.FrameResref;
            
        }

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
            
            var newMechConfig = new Shared.Core.Entity.MechConfiguration
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
            if (SelectedMechIndex < 0 || SelectedMechIndex >= MechNames.Count)
                return;

            var mechName = MechNames[SelectedMechIndex];
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
            if (SelectedMechIndex < 0 || SelectedMechIndex >= MechNames.Count)
                return;

            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId);
            
            if (dbPlayerMech == null)
                return;

            // Get the mech ID to delete
            var mechIds = dbPlayerMech.SavedMechs.Keys.ToList();
            if (SelectedMechIndex < mechIds.Count)
            {
                var mechIdToDelete = mechIds[SelectedMechIndex];
                dbPlayerMech.SavedMechs.Remove(mechIdToDelete);
                
                // If this was the active mech, clear it
                if (dbPlayerMech.ActiveMechId == mechIdToDelete)
                {
                    dbPlayerMech.ActiveMechId = Guid.Empty;
                }
                
                DB.Set(dbPlayerMech);
                
                // Reset selection and reload
                SelectedMechIndex = -1;
                LoadMechList();
            }
        }

        // Part attachment methods
        private void AttachPart(MechPartType partType)
        {
            if (SelectedMechIndex < 0)
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.PleaseSelectAMechFirst.ToLocalizedString()));
                return;
            }

            if (string.IsNullOrEmpty(SelectedFrameResref))
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.PleaseSelectAFrameFirst.ToLocalizedString()));
                return;
            }

            var mech = GetMech();
            var part = mech.Parts[(int)partType];

            if (string.IsNullOrEmpty(part))
            {
                // No part attached, enter targeting mode
                EnterTargetingMode(partType);
            }
            else
            {
                // Part already attached, show detachment confirmation
                ShowDetachPartConfirmation(partType);
            }
        }

        private void EnterTargetingMode(MechPartType partType)
        {
            var partTypeName = MechService.GetMechPartType(partType).Name.ToLocalizedString();

            Targeting.EnterTargetingMode(Player, ObjectType.Item, LocaleString.PleaseSelectMechItemFromInventory.ToLocalizedString(partTypeName),
                item =>
                {

                });
        }

        private void ShowDetachPartConfirmation(MechPartType partType)
        {
            var partTypeName = MechService.GetMechPartType(partType).Name.ToLocalizedString();
            var confirmMessage = LocaleString.AreYouSureYouWantToDetachPartX.ToLocalizedString(partTypeName);
            
            ShowModal(
                confirmMessage,
                () => DetachPart(partType),
                null,
                LocaleString.Detach,
                LocaleString.Cancel
            );
        }

        private void DetachPart(MechPartType partType)
        {
            // TODO: Implement part detachment logic
            // This should deserialize the part back to player inventory
        }

        public Action OnSelectFrame() => () =>
        {
            if (SelectedMechIndex < 0)
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.PleaseSelectAMechFirst.ToLocalizedString()));
                return;
            }

            // TODO: Implement frame selection logic
            // This should show available frames and allow selection
        };

        public Action OnClickMechOutline() => () =>
        {
            var data = NuiGetEventPayload();
            var coordinates = JsonObjectGet(data, "mouse_pos");
            var jX = JsonObjectGet(coordinates, "x");
            var jY = JsonObjectGet(coordinates, "y");
            var x = JsonGetFloat(jX);
            var y = JsonGetFloat(jY);

            Console.WriteLine($"x = {x}, y = {y}");

            if (HeadCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.Head);
            }
            else if (LeftArmCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.LeftArm);
            }
            else if (RightArmCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.RightArm);
            }
            else if (CoreCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.Core);
            }
            else if (LegsCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.Legs);
            }
            else if (GeneratorCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.Generator);
            }
            else if (LeftWeaponCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.LeftWeapon);
            }
            else if (RightWeaponCoordinates.Contains(x, y))
            {
                AttachPart(MechPartType.RightWeapon);
            }
        };
    }
}

