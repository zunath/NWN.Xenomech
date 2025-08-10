using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Shared.Core.Entity;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.UI;

namespace XM.Plugin.Item.OutfitEditor.UI
{
    internal class OutfitEditorViewModel: ViewModel<OutfitEditorViewModel>
    {
        [Inject]
        public DBService DB { get; set; }

        private const int MaxOutfits = 25;

        private readonly List<string> _outfitIds = new();

        public XMBindingList<string> SlotNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> SlotToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public int SelectedSlotIndex
        {
            get => Get<int>();
            set => Set(value);
        }

        public bool IsSaveEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsLoadEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsSlotLoaded
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsDeleteEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string Name
        {
            get => Get<string>();
            set
            {
                Set(value);
                IsSaveEnabled = true;
            }
        }

        public string Details
        {
            get => Get<string>();
            set => Set(value);
        }

        private List<PlayerOutfit> GetOutfits()
        {
            var playerId = GetObjectUUID(Player);
            var dbOutfits = DB.Search<PlayerOutfit>(new DBQuery()
                .AddFieldSearch(nameof(PlayerOutfit.PlayerId), playerId, false));

            return dbOutfits.ToList();
        }

        public Action OnClickSlot() => () =>
        {
            if (SelectedSlotIndex > -1)
            {
                SlotToggles[SelectedSlotIndex] = false;
            }
            SelectedSlotIndex = NuiGetEventArrayIndex();

            var dbOutfit = DB.Get<PlayerOutfit>(_outfitIds[SelectedSlotIndex]);
            IsDeleteEnabled = true;
            IsSlotLoaded = true;
            IsLoadEnabled = !string.IsNullOrWhiteSpace(dbOutfit.Data);

            Name = dbOutfit.Name;
            SlotToggles[SelectedSlotIndex] = true;
            UpdateDetails(dbOutfit);
        };

        private void UpdateDetails(PlayerOutfit detail)
        {
            if (string.IsNullOrWhiteSpace(detail.Data))
            {
                Details = "No outfit is saved to this slot.";
            }
            else
            {
                var details = $"Neck: #{detail.NeckId}\n" +
                              $"Torso: #{detail.TorsoId}\n" +
                              $"Belt: #{detail.BeltId}\n" +
                              $"Pelvis: #{detail.PelvisId}\n" +
                              $"Robe: #{detail.RobeId}\n" +

                              $"Left Bicep: #{detail.LeftBicepId}\n" +
                              $"Left Foot: #{detail.LeftFootId}\n" +
                              $"Left Forearm: #{detail.LeftForearmId}\n" +
                              $"Left Hand: #{detail.LeftHandId}\n" +
                              $"Left Shin: #{detail.LeftShinId}\n" +
                              $"Left Shoulder: #{detail.LeftShoulderId}\n" +
                              $"Left Thigh: #{detail.LeftThighId}\n" +

                              $"Right Bicep: #{detail.RightBicepId}\n" +
                              $"Right Foot: #{detail.RightFootId}\n" +
                              $"Right Forearm: #{detail.RightForearmId}\n" +
                              $"Right Hand: #{detail.RightHandId}\n" +
                              $"Right Shin: #{detail.RightShinId}\n" +
                              $"Right Shoulder: #{detail.RightShoulderId}\n" +
                              $"Right Thigh: #{detail.RightThighId}\n";

                Details = details;
            }
        }

        public Action OnClickSave() => () =>
        {
            var dbOutfit = DB.Get<PlayerOutfit>(_outfitIds[SelectedSlotIndex]);

            if (Name.Length > 32)
                Name = Name.Substring(0, 32);

            dbOutfit.Name = Name;

            DB.Set(dbOutfit);
            IsSaveEnabled = false;
            SlotNames[SelectedSlotIndex] = Name;
        };

        public Action OnClickStoreOutfit() => () =>
        {
            var dbOutfit = DB.Get<PlayerOutfit>(_outfitIds[SelectedSlotIndex]);

            void DoSave()
            {
                var outfit = GetItemInSlot(InventorySlotType.Chest, Player);
                if (!GetIsObjectValid(outfit))
                {
                    FloatingTextStringOnCreature("You do not have any clothes equipped.", Player, false);
                    return;
                }

                dbOutfit.Data = ObjectPlugin.Serialize(outfit);


                dbOutfit.NeckId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.Neck);
                dbOutfit.TorsoId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.Torso);
                dbOutfit.BeltId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.Belt);
                dbOutfit.PelvisId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.Pelvis);
                dbOutfit.RobeId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.Robe);

                dbOutfit.LeftBicepId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftBicep);
                dbOutfit.LeftFootId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftFoot);
                dbOutfit.LeftForearmId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftForearm);
                dbOutfit.LeftHandId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftHand);
                dbOutfit.LeftShinId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftShin);
                dbOutfit.LeftShoulderId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftShoulder);
                dbOutfit.LeftThighId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.LeftThigh);

                dbOutfit.RightBicepId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightBicep);
                dbOutfit.RightFootId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightFoot);
                dbOutfit.RightForearmId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightForearm);
                dbOutfit.RightHandId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightHand);
                dbOutfit.RightShinId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightShin);
                dbOutfit.RightShoulderId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightShoulder);
                dbOutfit.RightThighId = GetItemAppearance(outfit, ItemAppearanceType.ArmorModel, (int)ItemAppearanceArmorModelType.RightThigh);

                DB.Set(dbOutfit);

                IsLoadEnabled = true;
                UpdateDetails(dbOutfit);
            }

            // Nothing is saved to this slot yet.
            if (string.IsNullOrWhiteSpace(dbOutfit.Data))
            {
                DoSave();
            }
            // Something else is here. Prompt the user first.
            else
            {
                ShowModal($"Another outfit exists in this slot already. Are you sure you want to overwrite it?", DoSave);
            }

        };

        public Action OnClickLoadOutfit() => () =>
        {
            ShowModal($"Loading this outfit will overwrite the appearance of your currently equipped clothes. Are you sure you want to do this?",
                () =>
                {
                    var outfit = GetItemInSlot(InventorySlotType.Chest, Player);
                    if (!GetIsObjectValid(outfit))
                    {
                        FloatingTextStringOnCreature("You do not have any clothes equipped.", Player, false);
                        return;
                    }

                    LoadOutfit();
                });
        };

        private int CalculatePerPartColorIndex(ItemAppearanceArmorModelType armorModel, ItemAppearanceArmorColorType colorChannel)
        {
            return (int)ItemAppearanceArmorColorType.NumColors + (int)armorModel * (int)ItemAppearanceArmorColorType.NumColors + (int)colorChannel;
        }

        private void LoadOutfit()
        {
            var armor = GetItemInSlot(InventorySlotType.Chest, Player);
            var dbOutfit = DB.Get<PlayerOutfit>(_outfitIds[SelectedSlotIndex]);

            // Get the temporary storage placeable and deserialize the outfit into it.
            var tempStorage = GetObjectByTag("OUTFIT_BARREL");
            var deserialized = ObjectPlugin.Deserialize(dbOutfit.Data);
            var copy = CopyItem(armor, tempStorage, true);

            uint CopyColors(ItemAppearanceArmorModelType part)
            {
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorModel, (int)part, GetItemAppearance(deserialized, ItemAppearanceType.ArmorModel, (int)part), true);
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, (int)part, GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, (int)part), true);
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Cloth1), GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Cloth1)));
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Cloth2), GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Cloth2)));
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Leather1), GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Leather1)));
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Leather2), GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Leather2)));
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Metal1), GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Metal1)));
                DestroyObject(copy);
                copy = CopyItemAndModify(copy, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Metal2), GetItemAppearance(deserialized, ItemAppearanceType.ArmorColor, CalculatePerPartColorIndex(part, ItemAppearanceArmorColorType.Metal2)));
                DestroyObject(copy);

                return copy;
            }

            copy = CopyColors(ItemAppearanceArmorModelType.LeftBicep);
            copy = CopyColors(ItemAppearanceArmorModelType.Belt);
            copy = CopyColors(ItemAppearanceArmorModelType.LeftFoot);
            copy = CopyColors(ItemAppearanceArmorModelType.LeftForearm);
            copy = CopyColors(ItemAppearanceArmorModelType.LeftHand);
            copy = CopyColors(ItemAppearanceArmorModelType.LeftShin);
            copy = CopyColors(ItemAppearanceArmorModelType.LeftShoulder);
            copy = CopyColors(ItemAppearanceArmorModelType.LeftThigh);
            copy = CopyColors(ItemAppearanceArmorModelType.Neck);
            copy = CopyColors(ItemAppearanceArmorModelType.Pelvis);
            copy = CopyColors(ItemAppearanceArmorModelType.RightBicep);
            copy = CopyColors(ItemAppearanceArmorModelType.RightFoot);
            copy = CopyColors(ItemAppearanceArmorModelType.RightForearm);
            copy = CopyColors(ItemAppearanceArmorModelType.RightHand);
            copy = CopyColors(ItemAppearanceArmorModelType.Robe);
            copy = CopyColors(ItemAppearanceArmorModelType.RightShin);
            copy = CopyColors(ItemAppearanceArmorModelType.RightShoulder);
            copy = CopyColors(ItemAppearanceArmorModelType.RightThigh);
            copy = CopyColors(ItemAppearanceArmorModelType.Torso);

            var final = CopyItem(copy, Player, true);
            DestroyObject(armor);
            DestroyObject(copy);
            DestroyObject(deserialized);

            AssignCommand(Player, () =>
            {
                ActionEquipItem(final, InventorySlotType.Chest);
            });
        }

        public Action OnClickNew() => () =>
        {
            var playerId = GetObjectUUID(Player);
            var outfitCount = DB.SearchCount<PlayerOutfit>(new DBQuery()
                .AddFieldSearch(nameof(PlayerOutfit.PlayerId), playerId, false));

            if (outfitCount >= MaxOutfits)
            {
                FloatingTextStringOnCreature($"You may only create {MaxOutfits} outfits.", Player, false);
                return;
            }

            var newOutfit = new PlayerOutfit
            {
                Name = $"Outfit #{outfitCount + 1}",
                PlayerId = playerId
            };

            DB.Set(newOutfit);
            _outfitIds.Add(newOutfit.Id);
            SlotNames.Add(newOutfit.Name);
            SlotToggles.Add(false);
        };

        public Action OnClickDelete() => () =>
        {
            ShowModal("Are you sure you want to delete this stored outfit?", () =>
            {
                if (SelectedSlotIndex < 0)
                    return;

                var outfitId = _outfitIds[SelectedSlotIndex];
                var dbOutfit = DB.Get<PlayerOutfit>(outfitId);

                DB.Delete<PlayerOutfit>(dbOutfit.Id);

                _outfitIds.RemoveAt(SelectedSlotIndex);
                SlotNames.RemoveAt(SelectedSlotIndex);
                SlotToggles.RemoveAt(SelectedSlotIndex);

                SelectedSlotIndex = -1;
                IsSlotLoaded = false;
            });
        };

        public override void OnOpen()
        {
            SelectedSlotIndex = -1;
            IsDeleteEnabled = false;
            Name = string.Empty;

            var dbOutfits = GetOutfits();
            var slotNames = new XMBindingList<string>();
            var slotToggles = new XMBindingList<bool>();
            _outfitIds.Clear();

            foreach (var outfit in dbOutfits)
            {
                _outfitIds.Add(outfit.Id);
                slotNames.Add(outfit.Name);
                slotToggles.Add(false);
            }

            SlotNames = slotNames;
            SlotToggles = slotToggles;
            IsSlotLoaded = false;

            WatchOnClient(model => model.Name);
        }

        public override void OnClose()
        {
            
        }
    }
}
