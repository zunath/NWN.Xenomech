using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Shared.API.Constants;
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

        // Part slot states (internal resrefs)
        private string _headPartResref;
        private string _corePartResref;
        private string _leftArmPartResref;
        private string _rightArmPartResref;
        private string _legsPartResref;
        private string _generatorPartResref;
        private string _leftWeaponPartResref;
        private string _rightWeaponPartResref;

        // Display names for UI
        public string HeadPartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string CorePartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string LeftArmPartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string RightArmPartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string LegsPartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string GeneratorPartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string LeftWeaponPartDisplayName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string RightWeaponPartDisplayName
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
            
            LoadSelectedMechConfiguration();
        };

        private void LoadSelectedMechConfiguration()
        {
            if (SelectedMech < 0 || SelectedMech >= MechNames.Count)
            {
                ClearMechConfiguration();
                return;
            }

            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId);
            
            if (dbPlayerMech == null)
            {
                ClearMechConfiguration();
                return;
            }

            var mechIds = dbPlayerMech.SavedMechs.Keys.ToList();
            if (SelectedMech >= mechIds.Count)
            {
                ClearMechConfiguration();
                return;
            }

            var mechId = mechIds[SelectedMech];
            var mechConfig = dbPlayerMech.SavedMechs[mechId];

            // Load frame
            SelectedFrameResref = mechConfig.FrameResref;
            SelectedFrameName = string.IsNullOrEmpty(mechConfig.FrameResref) 
                ? LocaleString.NoFrameSelected.ToLocalizedString() 
                : mechConfig.FrameResref;

            // Load parts
            SetPartResref(MechPartType.Head, mechConfig.Parts.GetValueOrDefault((int)MechPartType.Head, string.Empty));
            SetPartResref(MechPartType.Core, mechConfig.Parts.GetValueOrDefault((int)MechPartType.Core, string.Empty));
            SetPartResref(MechPartType.LeftArm, mechConfig.Parts.GetValueOrDefault((int)MechPartType.LeftArm, string.Empty));
            SetPartResref(MechPartType.RightArm, mechConfig.Parts.GetValueOrDefault((int)MechPartType.RightArm, string.Empty));
            SetPartResref(MechPartType.Legs, mechConfig.Parts.GetValueOrDefault((int)MechPartType.Legs, string.Empty));
            SetPartResref(MechPartType.Generator, mechConfig.Parts.GetValueOrDefault((int)MechPartType.Generator, string.Empty));
            SetPartResref(MechPartType.LeftWeapon, mechConfig.Parts.GetValueOrDefault((int)MechPartType.LeftWeapon, string.Empty));
            SetPartResref(MechPartType.RightWeapon, mechConfig.Parts.GetValueOrDefault((int)MechPartType.RightWeapon, string.Empty));
        }

        private void ClearMechConfiguration()
        {
            SelectedFrameResref = string.Empty;
            SelectedFrameName = LocaleString.NoFrameSelected.ToLocalizedString();
            SetPartResref(MechPartType.Head, string.Empty);
            SetPartResref(MechPartType.Core, string.Empty);
            SetPartResref(MechPartType.LeftArm, string.Empty);
            SetPartResref(MechPartType.RightArm, string.Empty);
            SetPartResref(MechPartType.Legs, string.Empty);
            SetPartResref(MechPartType.Generator, string.Empty);
            SetPartResref(MechPartType.LeftWeapon, string.Empty);
            SetPartResref(MechPartType.RightWeapon, string.Empty);
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

        // Part attachment methods
        public Action OnAttachPart(MechPartType partType) => () =>
        {
            if (SelectedMech < 0)
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.PleaseSelectAMechFirst.ToLocalizedString()));
                return;
            }

            if (string.IsNullOrEmpty(SelectedFrameResref))
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.PleaseSelectAFrameFirst.ToLocalizedString()));
                return;
            }

            var currentPartResref = GetPartResref(partType);
            
            if (string.IsNullOrEmpty(currentPartResref))
            {
                // No part attached, enter targeting mode
                EnterTargetingMode(partType);
            }
            else
            {
                // Part already attached, show detachment confirmation
                ShowDetachPartConfirmation(partType);
            }
        };

        private string GetPartResref(MechPartType partType)
        {
            return partType switch
            {
                MechPartType.Head => _headPartResref,
                MechPartType.Core => _corePartResref,
                MechPartType.LeftArm => _leftArmPartResref,
                MechPartType.RightArm => _rightArmPartResref,
                MechPartType.Legs => _legsPartResref,
                MechPartType.Generator => _generatorPartResref,
                MechPartType.LeftWeapon => _leftWeaponPartResref,
                MechPartType.RightWeapon => _rightWeaponPartResref,
                _ => string.Empty
            };
        }

        private void SetPartResref(MechPartType partType, string resref)
        {
            switch (partType)
            {
                case MechPartType.Head:
                    _headPartResref = resref;
                    HeadPartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.Core:
                    _corePartResref = resref;
                    CorePartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.LeftArm:
                    _leftArmPartResref = resref;
                    LeftArmPartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.RightArm:
                    _rightArmPartResref = resref;
                    RightArmPartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.Legs:
                    _legsPartResref = resref;
                    LegsPartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.Generator:
                    _generatorPartResref = resref;
                    GeneratorPartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.LeftWeapon:
                    _leftWeaponPartResref = resref;
                    LeftWeaponPartDisplayName = GetPartDisplayName(resref);
                    break;
                case MechPartType.RightWeapon:
                    _rightWeaponPartResref = resref;
                    RightWeaponPartDisplayName = GetPartDisplayName(resref);
                    break;
            }
        }

        private string GetPartDisplayName(string resref)
        {
            var part = MechService.GetMechPart(resref);
            return part.Name;
        }

        private void EnterTargetingMode(MechPartType partType)
        {
            var partTypeName = GetPartTypeName(partType);

            Targeting.EnterTargetingMode(Player, ObjectType.Item, LocaleString.PleaseSelectMechItemFromInventory.ToLocalizedString(partTypeName),
                item =>
                {

                });
        }

        private string GetPartTypeName(MechPartType partType)
        {
            return partType switch
            {
                MechPartType.Head => LocaleString.MechHead.ToLocalizedString(),
                MechPartType.Core => LocaleString.MechCore.ToLocalizedString(),
                MechPartType.LeftArm => LocaleString.MechLeftArm.ToLocalizedString(),
                MechPartType.RightArm => LocaleString.MechRightArm.ToLocalizedString(),
                MechPartType.Legs => LocaleString.MechLegs.ToLocalizedString(),
                MechPartType.Generator => LocaleString.MechGenerator.ToLocalizedString(),
                MechPartType.LeftWeapon => LocaleString.MechLeftWeapon.ToLocalizedString(),
                MechPartType.RightWeapon => LocaleString.MechRightWeapon.ToLocalizedString(),
                _ => LocaleString.Unknown.ToLocalizedString()
            };
        }

        private void ShowDetachPartConfirmation(MechPartType partType)
        {
            var partTypeName = GetPartTypeName(partType);
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
            SetPartResref(partType, string.Empty);
            SaveMechConfiguration();
            
            var partTypeName = GetPartTypeName(partType);
        }

        private void SaveMechConfiguration()
        {
            if (SelectedMech < 0)
                return;

            var playerId = PlayerId.Get(Player);
            var dbPlayerMech = DB.Get<PlayerMech>(playerId);
            
            if (dbPlayerMech == null)
                return;

            var mechIds = dbPlayerMech.SavedMechs.Keys.ToList();
            if (SelectedMech >= mechIds.Count)
                return;

            var mechId = mechIds[SelectedMech];
            var mechConfig = dbPlayerMech.SavedMechs[mechId];

            // Update frame
            mechConfig.FrameResref = SelectedFrameResref;

            // Update parts
            mechConfig.Parts[(int)MechPartType.Head] = _headPartResref;
            mechConfig.Parts[(int)MechPartType.Core] = _corePartResref;
            mechConfig.Parts[(int)MechPartType.LeftArm] = _leftArmPartResref;
            mechConfig.Parts[(int)MechPartType.RightArm] = _rightArmPartResref;
            mechConfig.Parts[(int)MechPartType.Legs] = _legsPartResref;
            mechConfig.Parts[(int)MechPartType.Generator] = _generatorPartResref;
            mechConfig.Parts[(int)MechPartType.LeftWeapon] = _leftWeaponPartResref;
            mechConfig.Parts[(int)MechPartType.RightWeapon] = _rightWeaponPartResref;

            DB.Set(dbPlayerMech);
        }

        public Action OnSelectFrame() => () =>
        {
            if (SelectedMech < 0)
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.PleaseSelectAMechFirst.ToLocalizedString()));
                return;
            }

            // TODO: Implement frame selection logic
            // This should show available frames and allow selection
        };
    }
}

