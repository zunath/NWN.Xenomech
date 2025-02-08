using Anvil.API;
using XM.Shared.Core;

namespace XM.Plugin.Item.AppearanceEditor.ItemAppearance
{
    internal interface IArmorAppearanceDefinition
    {
        public int[] Helmet { get; }
        public int[] Cloak { get; }
        public int[] Neck { get; }
        public int[] Torso { get; }
        public int[] Belt { get; }
        public int[] Pelvis { get; }

        public int[] Shoulder { get; }
        public int[] Bicep { get; }
        public int[] Forearm { get; }
        public int[] Hand { get; }

        public int[] Thigh { get; }
        public int[] Shin { get; }
        public int[] Foot { get; }
        public int[] Robe { get; }

        public XMBindingList<NuiComboEntry> NeckOptions { get; }
        public XMBindingList<NuiComboEntry> TorsoOptions { get; }
        public XMBindingList<NuiComboEntry> BeltOptions { get; }
        public XMBindingList<NuiComboEntry> PelvisOptions { get; }
        public XMBindingList<NuiComboEntry> ShoulderOptions { get; }
        public XMBindingList<NuiComboEntry> BicepOptions { get; }
        public XMBindingList<NuiComboEntry> ForearmOptions { get; }
        public XMBindingList<NuiComboEntry> HandOptions { get; }
        public XMBindingList<NuiComboEntry> ThighOptions { get; }
        public XMBindingList<NuiComboEntry> ShinOptions { get; }
        public XMBindingList<NuiComboEntry> FootOptions { get; }
        public XMBindingList<NuiComboEntry> RobeOptions { get; }

    }
}
