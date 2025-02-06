using Anvil.API;
using XM.Shared.Core;

namespace XM.Plugin.Item.AppearanceEditor.ItemAppearance
{
    internal abstract class BaseArmorAppearanceDefinition : IArmorAppearanceDefinition
    {
        public abstract int[] Helmet { get; }
        public abstract int[] Cloak { get; }
        public abstract int[] Neck { get; }
        public abstract int[] Torso { get; }
        public abstract int[] Belt { get; }
        public abstract int[] Pelvis { get; }
        public abstract int[] Shoulder { get; }
        public abstract int[] Bicep { get; }
        public abstract int[] Forearm { get; }
        public abstract int[] Hand { get; }
        public abstract int[] Thigh { get; }
        public abstract int[] Shin { get; }
        public abstract int[] Foot { get; }
        public abstract int[] Robe { get; }
        public XMBindingList<NuiComboEntry> NeckOptions { get; } = new();
        public XMBindingList<NuiComboEntry> TorsoOptions { get; } = new();
        public XMBindingList<NuiComboEntry> BeltOptions { get; } = new();
        public XMBindingList<NuiComboEntry> PelvisOptions { get; } = new();
        public XMBindingList<NuiComboEntry> ShoulderOptions { get; } = new();
        public XMBindingList<NuiComboEntry> BicepOptions { get; } = new();
        public XMBindingList<NuiComboEntry> ForearmOptions { get; } = new();
        public XMBindingList<NuiComboEntry> HandOptions { get; } = new();
        public XMBindingList<NuiComboEntry> ThighOptions { get; } = new();
        public XMBindingList<NuiComboEntry> ShinOptions { get; } = new();
        public XMBindingList<NuiComboEntry> FootOptions { get; } = new();
        public XMBindingList<NuiComboEntry> RobeOptions { get; } = new();

        protected BaseArmorAppearanceDefinition()
        {
            foreach (var neck in Neck)
            {
                NeckOptions.Add(new NuiComboEntry(neck.ToString(), neck));
            }
            foreach (var torso in Torso)
            {
                TorsoOptions.Add(new NuiComboEntry(torso.ToString(), torso));
            }
            foreach (var belt in Belt)
            {
                BeltOptions.Add(new NuiComboEntry(belt.ToString(), belt));
            }
            foreach (var pelvis in Pelvis)
            {
                PelvisOptions.Add(new NuiComboEntry(pelvis.ToString(), pelvis));
            }
            foreach (var shoulder in Shoulder)
            {
                ShoulderOptions.Add(new NuiComboEntry(shoulder.ToString(), shoulder));
            }
            foreach (var bicep in Bicep)
            {
                BicepOptions.Add(new NuiComboEntry(bicep.ToString(), bicep));
            }
            foreach (var forearm in Forearm)
            {
                ForearmOptions.Add(new NuiComboEntry(forearm.ToString(), forearm));
            }
            foreach (var hand in Hand)
            {
                HandOptions.Add(new NuiComboEntry(hand.ToString(), hand));
            }
            foreach (var thigh in Thigh)
            {
                ThighOptions.Add(new NuiComboEntry(thigh.ToString(), thigh));
            }
            foreach (var shin in Shin)
            {
                ShinOptions.Add(new NuiComboEntry(shin.ToString(), shin));
            }
            foreach (var foot in Foot)
            {
                FootOptions.Add(new NuiComboEntry(foot.ToString(), foot));
            }
            foreach (var robe in Robe)
            {
                RobeOptions.Add(new NuiComboEntry(robe.ToString(), robe));
            }
        }
    }
}
