using System.Collections.Generic;
using Anvil.API;
using XM.Shared.API.Constants;

namespace XM.Plugin.Item.AppearanceEditor.UI
{
    internal partial class AppearanceEditorViewModel
    {
        private class ColorRegion
        {
            public string PropertyName { get; set; }
            public NuiRect Region { get; set; }

            public ColorRegion(string propertyName, NuiRect region)
            {
                PropertyName = propertyName;
                Region = region;
            }
        }

        private Dictionary<ColorTarget, Dictionary<ItemAppearanceArmorColorType, ColorRegion>> _colorMappings;

        private void RegisterColorMappings()
        {
            InitializeRegions();

            _colorMappings = new Dictionary<ColorTarget, Dictionary<ItemAppearanceArmorColorType, ColorRegion>>
            {
                [ColorTarget.Global] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(GlobalLeather1Region), GlobalLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(GlobalLeather2Region), GlobalLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(GlobalCloth1Region), GlobalCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(GlobalCloth2Region), GlobalCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(GlobalMetal1Region), GlobalMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(GlobalMetal2Region), GlobalMetal2Region)
                },
                [ColorTarget.LeftShoulder] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftShoulderLeather1Region), LeftShoulderLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftShoulderLeather2Region), LeftShoulderLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftShoulderCloth1Region), LeftShoulderCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftShoulderCloth2Region), LeftShoulderCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftShoulderMetal1Region), LeftShoulderMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftShoulderMetal2Region), LeftShoulderMetal2Region)
                },
                [ColorTarget.LeftBicep] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftBicepLeather1Region), LeftBicepLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftBicepLeather2Region), LeftBicepLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftBicepCloth1Region), LeftBicepCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftBicepCloth2Region), LeftBicepCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftBicepMetal1Region), LeftBicepMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftBicepMetal2Region), LeftBicepMetal2Region)
                },
                [ColorTarget.LeftForearm] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftForearmLeather1Region), LeftForearmLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftForearmLeather2Region), LeftForearmLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftForearmCloth1Region), LeftForearmCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftForearmCloth2Region), LeftForearmCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftForearmMetal1Region), LeftForearmMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftForearmMetal2Region), LeftForearmMetal2Region)
                },
                [ColorTarget.LeftHand] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftHandLeather1Region), LeftHandLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftHandLeather2Region), LeftHandLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftHandCloth1Region), LeftHandCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftHandCloth2Region), LeftHandCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftHandMetal1Region), LeftHandMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftHandMetal2Region), LeftHandMetal2Region)
                },
                [ColorTarget.LeftThigh] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftThighLeather1Region), LeftThighLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftThighLeather2Region), LeftThighLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftThighCloth1Region), LeftThighCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftThighCloth2Region), LeftThighCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftThighMetal1Region), LeftThighMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftThighMetal2Region), LeftThighMetal2Region)
                },
                [ColorTarget.LeftShin] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftShinLeather1Region), LeftShinLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftShinLeather2Region), LeftShinLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftShinCloth1Region), LeftShinCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftShinCloth2Region), LeftShinCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftShinMetal1Region), LeftShinMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftShinMetal2Region), LeftShinMetal2Region)
                },
                [ColorTarget.LeftFoot] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(LeftFootLeather1Region), LeftFootLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(LeftFootLeather2Region), LeftFootLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(LeftFootCloth1Region), LeftFootCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(LeftFootCloth2Region), LeftFootCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(LeftFootMetal1Region), LeftFootMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(LeftFootMetal2Region), LeftFootMetal2Region)
                },
                [ColorTarget.RightShoulder] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightShoulderLeather1Region), RightShoulderLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightShoulderLeather2Region), RightShoulderLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightShoulderCloth1Region), RightShoulderCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightShoulderCloth2Region), RightShoulderCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightShoulderMetal1Region), RightShoulderMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightShoulderMetal2Region), RightShoulderMetal2Region)
                },
                [ColorTarget.RightBicep] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightBicepLeather1Region), RightBicepLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightBicepLeather2Region), RightBicepLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightBicepCloth1Region), RightBicepCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightBicepCloth2Region), RightBicepCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightBicepMetal1Region), RightBicepMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightBicepMetal2Region), RightBicepMetal2Region)
                },
                [ColorTarget.RightForearm] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightForearmLeather1Region), RightForearmLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightForearmLeather2Region), RightForearmLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightForearmCloth1Region), RightForearmCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightForearmCloth2Region), RightForearmCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightForearmMetal1Region), RightForearmMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightForearmMetal2Region), RightForearmMetal2Region)
                },
                [ColorTarget.RightHand] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightHandLeather1Region), RightHandLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightHandLeather2Region), RightHandLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightHandCloth1Region), RightHandCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightHandCloth2Region), RightHandCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightHandMetal1Region), RightHandMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightHandMetal2Region), RightHandMetal2Region)
                },
                [ColorTarget.RightThigh] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightThighLeather1Region), RightThighLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightThighLeather2Region), RightThighLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightThighCloth1Region), RightThighCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightThighCloth2Region), RightThighCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightThighMetal1Region), RightThighMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightThighMetal2Region), RightThighMetal2Region)
                },
                [ColorTarget.RightShin] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightShinLeather1Region), RightShinLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightShinLeather2Region), RightShinLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightShinCloth1Region), RightShinCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightShinCloth2Region), RightShinCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightShinMetal1Region), RightShinMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightShinMetal2Region), RightShinMetal2Region)
                },
                [ColorTarget.RightFoot] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RightFootLeather1Region), RightFootLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RightFootLeather2Region), RightFootLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RightFootCloth1Region), RightFootCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RightFootCloth2Region), RightFootCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RightFootMetal1Region), RightFootMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RightFootMetal2Region), RightFootMetal2Region)
                },
                [ColorTarget.Neck] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(NeckLeather1Region), NeckLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(NeckLeather2Region), NeckLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(NeckCloth1Region), NeckCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(NeckCloth2Region), NeckCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(NeckMetal1Region), NeckMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(NeckMetal2Region), NeckMetal2Region)
                },
                [ColorTarget.Chest] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(ChestLeather1Region), ChestLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(ChestLeather2Region), ChestLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(ChestCloth1Region), ChestCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(ChestCloth2Region), ChestCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(ChestMetal1Region), ChestMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(ChestMetal2Region), ChestMetal2Region)
                },
                [ColorTarget.Belt] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(BeltLeather1Region), BeltLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(BeltLeather2Region), BeltLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(BeltCloth1Region), BeltCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(BeltCloth2Region), BeltCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(BeltMetal1Region), BeltMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(BeltMetal2Region), BeltMetal2Region)
                },
                [ColorTarget.Pelvis] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(PelvisLeather1Region), PelvisLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(PelvisLeather2Region), PelvisLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(PelvisCloth1Region), PelvisCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(PelvisCloth2Region), PelvisCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(PelvisMetal1Region), PelvisMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(PelvisMetal2Region), PelvisMetal2Region)
                },
                [ColorTarget.Robe] = new()
                {
                    [ItemAppearanceArmorColorType.Leather1] = new ColorRegion(nameof(RobeLeather1Region), RobeLeather1Region),
                    [ItemAppearanceArmorColorType.Leather2] = new ColorRegion(nameof(RobeLeather2Region), RobeLeather2Region),
                    [ItemAppearanceArmorColorType.Cloth1] = new ColorRegion(nameof(RobeCloth1Region), RobeCloth1Region),
                    [ItemAppearanceArmorColorType.Cloth2] = new ColorRegion(nameof(RobeCloth2Region), RobeCloth2Region),
                    [ItemAppearanceArmorColorType.Metal1] = new ColorRegion(nameof(RobeMetal1Region), RobeMetal1Region),
                    [ItemAppearanceArmorColorType.Metal2] = new ColorRegion(nameof(RobeMetal2Region), RobeMetal2Region)
                },
            };
        }

        private void InitializeRegions()
        {
            const int X = 247;
            const int Y = 55;
            const int Width = 1;
            const int Height = 1;

            // Global
            GlobalLeather1Region = new NuiRect(X, Y, Width, Height);
            GlobalLeather2Region = new NuiRect(X, Y, Width, Height);
            GlobalCloth1Region = new NuiRect(X, Y, Width, Height);
            GlobalCloth2Region = new NuiRect(X, Y, Width, Height);
            GlobalMetal1Region = new NuiRect(X, Y, Width, Height);
            GlobalMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Shoulder
            LeftShoulderLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftShoulderLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftShoulderCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftShoulderCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftShoulderMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftShoulderMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Bicep
            LeftBicepLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftBicepLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftBicepCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftBicepCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftBicepMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftBicepMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Forearm
            LeftForearmLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftForearmLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftForearmCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftForearmCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftForearmMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftForearmMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Hand
            LeftHandLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftHandLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftHandCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftHandCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftHandMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftHandMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Thigh
            LeftThighLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftThighLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftThighCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftThighCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftThighMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftThighMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Shin
            LeftShinLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftShinLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftShinCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftShinCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftShinMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftShinMetal2Region = new NuiRect(X, Y, Width, Height);

            // Left Foot
            LeftFootLeather1Region = new NuiRect(X, Y, Width, Height);
            LeftFootLeather2Region = new NuiRect(X, Y, Width, Height);
            LeftFootCloth1Region = new NuiRect(X, Y, Width, Height);
            LeftFootCloth2Region = new NuiRect(X, Y, Width, Height);
            LeftFootMetal1Region = new NuiRect(X, Y, Width, Height);
            LeftFootMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Shoulder
            RightShoulderLeather1Region = new NuiRect(X, Y, Width, Height);
            RightShoulderLeather2Region = new NuiRect(X, Y, Width, Height);
            RightShoulderCloth1Region = new NuiRect(X, Y, Width, Height);
            RightShoulderCloth2Region = new NuiRect(X, Y, Width, Height);
            RightShoulderMetal1Region = new NuiRect(X, Y, Width, Height);
            RightShoulderMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Bicep
            RightBicepLeather1Region = new NuiRect(X, Y, Width, Height);
            RightBicepLeather2Region = new NuiRect(X, Y, Width, Height);
            RightBicepCloth1Region = new NuiRect(X, Y, Width, Height);
            RightBicepCloth2Region = new NuiRect(X, Y, Width, Height);
            RightBicepMetal1Region = new NuiRect(X, Y, Width, Height);
            RightBicepMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Forearm
            RightForearmLeather1Region = new NuiRect(X, Y, Width, Height);
            RightForearmLeather2Region = new NuiRect(X, Y, Width, Height);
            RightForearmCloth1Region = new NuiRect(X, Y, Width, Height);
            RightForearmCloth2Region = new NuiRect(X, Y, Width, Height);
            RightForearmMetal1Region = new NuiRect(X, Y, Width, Height);
            RightForearmMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Hand
            RightHandLeather1Region = new NuiRect(X, Y, Width, Height);
            RightHandLeather2Region = new NuiRect(X, Y, Width, Height);
            RightHandCloth1Region = new NuiRect(X, Y, Width, Height);
            RightHandCloth2Region = new NuiRect(X, Y, Width, Height);
            RightHandMetal1Region = new NuiRect(X, Y, Width, Height);
            RightHandMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Thigh
            RightThighLeather1Region = new NuiRect(X, Y, Width, Height);
            RightThighLeather2Region = new NuiRect(X, Y, Width, Height);
            RightThighCloth1Region = new NuiRect(X, Y, Width, Height);
            RightThighCloth2Region = new NuiRect(X, Y, Width, Height);
            RightThighMetal1Region = new NuiRect(X, Y, Width, Height);
            RightThighMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Shin
            RightShinLeather1Region = new NuiRect(X, Y, Width, Height);
            RightShinLeather2Region = new NuiRect(X, Y, Width, Height);
            RightShinCloth1Region = new NuiRect(X, Y, Width, Height);
            RightShinCloth2Region = new NuiRect(X, Y, Width, Height);
            RightShinMetal1Region = new NuiRect(X, Y, Width, Height);
            RightShinMetal2Region = new NuiRect(X, Y, Width, Height);

            // Right Foot
            RightFootLeather1Region = new NuiRect(X, Y, Width, Height);
            RightFootLeather2Region = new NuiRect(X, Y, Width, Height);
            RightFootCloth1Region = new NuiRect(X, Y, Width, Height);
            RightFootCloth2Region = new NuiRect(X, Y, Width, Height);
            RightFootMetal1Region = new NuiRect(X, Y, Width, Height);
            RightFootMetal2Region = new NuiRect(X, Y, Width, Height);

            // Neck
            NeckLeather1Region = new NuiRect(X, Y, Width, Height);
            NeckLeather2Region = new NuiRect(X, Y, Width, Height);
            NeckCloth1Region = new NuiRect(X, Y, Width, Height);
            NeckCloth2Region = new NuiRect(X, Y, Width, Height);
            NeckMetal1Region = new NuiRect(X, Y, Width, Height);
            NeckMetal2Region = new NuiRect(X, Y, Width, Height);

            // Chest
            ChestLeather1Region = new NuiRect(X, Y, Width, Height);
            ChestLeather2Region = new NuiRect(X, Y, Width, Height);
            ChestCloth1Region = new NuiRect(X, Y, Width, Height);
            ChestCloth2Region = new NuiRect(X, Y, Width, Height);
            ChestMetal1Region = new NuiRect(X, Y, Width, Height);
            ChestMetal2Region = new NuiRect(X, Y, Width, Height);

            // Belt
            BeltLeather1Region = new NuiRect(X, Y, Width, Height);
            BeltLeather2Region = new NuiRect(X, Y, Width, Height);
            BeltCloth1Region = new NuiRect(X, Y, Width, Height);
            BeltCloth2Region = new NuiRect(X, Y, Width, Height);
            BeltMetal1Region = new NuiRect(X, Y, Width, Height);
            BeltMetal2Region = new NuiRect(X, Y, Width, Height);

            // Pelvis
            PelvisLeather1Region = new NuiRect(X, Y, Width, Height);
            PelvisLeather2Region = new NuiRect(X, Y, Width, Height);
            PelvisCloth1Region = new NuiRect(X, Y, Width, Height);
            PelvisCloth2Region = new NuiRect(X, Y, Width, Height);
            PelvisMetal1Region = new NuiRect(X, Y, Width, Height);
            PelvisMetal2Region = new NuiRect(X, Y, Width, Height);

            // Robe
            RobeLeather1Region = new NuiRect(X, Y, Width, Height);
            RobeLeather2Region = new NuiRect(X, Y, Width, Height);
            RobeCloth1Region = new NuiRect(X, Y, Width, Height);
            RobeCloth2Region = new NuiRect(X, Y, Width, Height);
            RobeMetal1Region = new NuiRect(X, Y, Width, Height);
            RobeMetal2Region = new NuiRect(X, Y, Width, Height);
        }

        private (int, int) ColorIdToCoordinates(int colorId)
        {
            var x = colorId % TextureColorsPerRow;
            var y = colorId / TextureColorsPerRow;

            return (x, y);
        }
        private void ChangeColor(ColorTarget target, ItemAppearanceArmorColorType channel, int colorId)
        {
            if (colorId >= 255)
            {
                _colorMappings[target][channel].Region = new NuiRect(247, 55, 1, 1);
            }
            else
            {
                var (x, y) = ColorIdToCoordinates(colorId);
                _colorMappings[target][channel].Region = new NuiRect(x * ColorSize, y * ColorSize, ColorSize, ColorSize);
            }

            GetType().GetProperty(_colorMappings[target][channel].PropertyName)?.SetValue(this, _colorMappings[target][channel].Region);
        }

        // Global
        public NuiRect GlobalLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect GlobalLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect GlobalCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect GlobalCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect GlobalMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect GlobalMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Shoulder
        public NuiRect LeftShoulderLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftShoulderLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShoulderCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShoulderCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShoulderMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShoulderMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Bicep
        public NuiRect LeftBicepLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftBicepLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftBicepCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftBicepCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftBicepMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftBicepMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Forearm
        public NuiRect LeftForearmLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftForearmLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftForearmCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftForearmCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftForearmMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftForearmMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Hand
        public NuiRect LeftHandLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftHandLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftHandCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftHandCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftHandMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftHandMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Thigh
        public NuiRect LeftThighLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftThighLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftThighCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftThighCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftThighMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftThighMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Shin
        public NuiRect LeftShinLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftShinLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShinCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShinCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShinMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftShinMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Left Foot
        public NuiRect LeftFootLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect LeftFootLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftFootCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftFootCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftFootMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect LeftFootMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Shoulder
        public NuiRect RightShoulderLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightShoulderLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShoulderCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShoulderCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShoulderMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShoulderMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Bicep
        public NuiRect RightBicepLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightBicepLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightBicepCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightBicepCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightBicepMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightBicepMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Forearm
        public NuiRect RightForearmLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightForearmLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightForearmCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightForearmCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightForearmMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightForearmMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Hand
        public NuiRect RightHandLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightHandLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightHandCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightHandCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightHandMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightHandMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Thigh
        public NuiRect RightThighLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightThighLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightThighCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightThighCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightThighMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightThighMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Shin
        public NuiRect RightShinLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightShinLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShinCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShinCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShinMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightShinMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Right Foot
        public NuiRect RightFootLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RightFootLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightFootCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightFootCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightFootMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RightFootMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Neck
        public NuiRect NeckLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect NeckLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect NeckCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect NeckCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect NeckMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect NeckMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Chest
        public NuiRect ChestLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect ChestLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect ChestCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect ChestCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect ChestMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect ChestMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Belt
        public NuiRect BeltLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect BeltLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect BeltCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect BeltCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect BeltMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect BeltMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Pelvis
        public NuiRect PelvisLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect PelvisLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect PelvisCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect PelvisCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect PelvisMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect PelvisMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        // Robe
        public NuiRect RobeLeather1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public NuiRect RobeLeather2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RobeCloth1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RobeCloth2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RobeMetal1Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
        public NuiRect RobeMetal2Region
        {
            get => Get<NuiRect>();
            set => Set(value);
        }
    }
}
