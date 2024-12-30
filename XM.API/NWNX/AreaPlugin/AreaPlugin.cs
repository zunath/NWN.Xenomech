using NWN.Core.NWNX;

namespace XM.API.NWNX.AreaPlugin
{
    public static class AreaPlugin
    {
        /// <summary>
        /// Gets the number of players in an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The player count for the area.</returns>
        public static int GetNumberOfPlayersInArea(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetNumberOfPlayersInArea(area);
        }

        /// <summary>
        /// Gets the creature that last entered the area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The most recent creature to enter the area.</returns>
        public static uint GetLastEntered(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetLastEntered(area);
        }

        /// <summary>
        /// Gets the creature that last left the area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The most recent creature to leave the area.</returns>
        public static uint GetLastLeft(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetLastLeft(area);
        }

        /// <summary>
        /// Gets the PVP setting of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The PVP setting for the area.</returns>
        public static AreaPVPSettingType GetPVPSetting(uint area)
        {
            return (AreaPVPSettingType)NWN.Core.NWNX.AreaPlugin.GetPVPSetting(area);
        }

        /// <summary>
        /// Sets the PVP setting of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="pvpSetting">The PVP setting to assign to the area.</param>
        public static void SetPVPSetting(uint area, AreaPVPSettingType pvpSetting)
        {
            NWN.Core.NWNX.AreaPlugin.SetPVPSetting(area, (int)pvpSetting);
        }

        /// <summary>
        /// Gets the Spot skill modifier for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The Spot skill modifier for the area.</returns>
        public static int GetAreaSpotModifier(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAreaSpotModifier(area);
        }

        /// <summary>
        /// Sets the Spot skill modifier for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="spotModifier">The modifier to set for the Spot skill.</param>
        public static void SetAreaSpotModifier(uint area, int spotModifier)
        {
            NWN.Core.NWNX.AreaPlugin.SetAreaSpotModifier(area, spotModifier);
        }

        /// <summary>
        /// Gets the Listen skill modifier for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The Listen skill modifier for the area.</returns>
        public static int GetAreaListenModifier(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAreaListenModifier(area);
        }

        /// <summary>
        /// Sets the Listen skill modifier for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="listenModifier">The modifier to set for the Listen skill.</param>
        public static void SetAreaListenModifier(uint area, int listenModifier)
        {
            NWN.Core.NWNX.AreaPlugin.SetAreaListenModifier(area, listenModifier);
        }

        /// <summary>
        /// Checks if the "No Resting" flag is set for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>True if resting is not allowed, otherwise false.</returns>
        public static bool GetNoRestingAllowed(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetNoRestingAllowed(area) == 1;
        }

        /// <summary>
        /// Sets the "No Resting" flag for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="noRestingAllowed">True to disable resting, otherwise false.</param>
        public static void SetNoRestingAllowed(uint area, bool noRestingAllowed)
        {
            NWN.Core.NWNX.AreaPlugin.SetNoRestingAllowed(area, noRestingAllowed ? 1 : 0);
        }

        /// <summary>
        /// Gets the wind power for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The wind power for the area (0-2).</returns>
        public static int GetWindPower(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetWindPower(area);
        }

        /// <summary>
        /// Sets the wind power for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="windPower">The wind power to set (0-2).</param>
        public static void SetWindPower(uint area, int windPower)
        {
            NWN.Core.NWNX.AreaPlugin.SetWindPower(area, windPower);
        }

        /// <summary>
        /// Gets the weather chance for a specified type in an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="type">The weather type.</param>
        /// <returns>The percentage chance for the weather type (0-100).</returns>
        public static int GetWeatherChance(uint area, int type)
        {
            return NWN.Core.NWNX.AreaPlugin.GetWeatherChance(area, type);
        }

        /// <summary>
        /// Sets the weather chance for a specified type in an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="type">The weather type.</param>
        /// <param name="chance">The percentage chance to set (0-100).</param>
        public static void SetWeatherChance(uint area, int type, int chance)
        {
            NWN.Core.NWNX.AreaPlugin.SetWeatherChance(area, type, chance);
        }

        /// <summary>
        /// Gets the fog clip distance for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The fog clip distance.</returns>
        public static float GetFogClipDistance(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetFogClipDistance(area);
        }

        /// <summary>
        /// Sets the fog clip distance for an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="distance">The new fog clip distance.</param>
        public static void SetFogClipDistance(uint area, float distance)
        {
            NWN.Core.NWNX.AreaPlugin.SetFogClipDistance(area, distance);
        }
        /// <summary>
        /// Gets the shadow opacity of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The shadow opacity for the area (0-100).</returns>
        public static int GetShadowOpacity(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetShadowOpacity(area);
        }

        /// <summary>
        /// Sets the shadow opacity of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="shadowOpacity">The shadow opacity to set (0-100).</param>
        public static void SetShadowOpacity(uint area, int shadowOpacity)
        {
            NWN.Core.NWNX.AreaPlugin.SetShadowOpacity(area, shadowOpacity);
        }

        /// <summary>
        /// Gets the day/night cycle setting of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <returns>The day/night cycle setting.</returns>
        public static int GetDayNightCycle(uint area)
        {
            return NWN.Core.NWNX.AreaPlugin.GetDayNightCycle(area);
        }

        /// <summary>
        /// Sets the day/night cycle setting of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="type">The day/night cycle setting.</param>
        public static void SetDayNightCycle(uint area, int type)
        {
            NWN.Core.NWNX.AreaPlugin.SetDayNightCycle(area, type);
        }

        /// <summary>
        /// Gets the Sun/Moon Ambient/Diffuse colors of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="type">The Sun/Moon color setting type.</param>
        /// <returns>The color setting value.</returns>
        public static int GetSunMoonColors(uint area, int type)
        {
            return NWN.Core.NWNX.AreaPlugin.GetSunMoonColors(area, type);
        }

        /// <summary>
        /// Sets the Sun/Moon Ambient/Diffuse colors of an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="type">The Sun/Moon color setting type.</param>
        /// <param name="color">The color value to set.</param>
        public static void SetSunMoonColors(uint area, int type, int color)
        {
            NWN.Core.NWNX.AreaPlugin.SetSunMoonColors(area, type, color);
        }

        /// <summary>
        /// Creates and returns a transition in an area.
        /// </summary>
        /// <param name="area">The area object.</param>
        /// <param name="target">The target door or waypoint object.</param>
        /// <param name="x">The X-coordinate.</param>
        /// <param name="y">The Y-coordinate.</param>
        /// <param name="z">The Z-coordinate.</param>
        /// <param name="size">The size of the transition.</param>
        /// <param name="tag">The tag for the transition.</param>
        /// <returns>The created transition object.</returns>
        public static uint CreateTransition(uint area, uint target, float x, float y, float z, float size = 2.0f, string tag = "")
        {
            return NWN.Core.NWNX.AreaPlugin.CreateTransition(area, target, x, y, z, size, tag);
        }

        /// <summary>
        /// Gets the state of a tile animation loop in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="fTileX">The X-coordinate of the tile.</param>
        /// <param name="fTileY">The Y-coordinate of the tile.</param>
        /// <param name="nAnimLoop">The animation loop to check.</param>
        /// <returns>True if the loop is enabled, otherwise false.</returns>
        public static bool GetTileAnimationLoop(uint oArea, float fTileX, float fTileY, int nAnimLoop)
        {
            return NWN.Core.NWNX.AreaPlugin.GetTileAnimationLoop(oArea, fTileX, fTileY, nAnimLoop) == 1;
        }

        /// <summary>
        /// Sets the state of a tile animation loop in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="fTileX">The X-coordinate of the tile.</param>
        /// <param name="fTileY">The Y-coordinate of the tile.</param>
        /// <param name="nAnimLoop">The animation loop to set.</param>
        /// <param name="bEnabled">True to enable the loop, otherwise false.</param>
        public static void SetTileAnimationLoop(uint oArea, float fTileX, float fTileY, int nAnimLoop, bool bEnabled)
        {
            NWN.Core.NWNX.AreaPlugin.SetTileAnimationLoop(oArea, fTileX, fTileY, nAnimLoop, bEnabled ? 1 : 0);
        }

        /// <summary>
        /// Gets the name of the tile model from any location in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="fTileX">The X-coordinate of the tile.</param>
        /// <param name="fTileY">The Y-coordinate of the tile.</param>
        /// <returns>The tile model name.</returns>
        public static string GetTileModelResRef(uint oArea, float fTileX, float fTileY)
        {
            return NWN.Core.NWNX.AreaPlugin.GetTileModelResRef(oArea, fTileX, fTileY);
        }

        /// <summary>
        /// Tests if there is a direct, walkable line between two points in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="fStartX">The starting X-coordinate.</param>
        /// <param name="fStartY">The starting Y-coordinate.</param>
        /// <param name="fEndX">The ending X-coordinate.</param>
        /// <param name="fEndY">The ending Y-coordinate.</param>
        /// <param name="fPerSpace">The personal space of a creature.</param>
        /// <param name="fHeight">The height of a creature.</param>
        /// <param name="bIgnoreDoors">Whether to ignore doors during the check.</param>
        /// <returns>
        /// 1 if there is a direct walkable line, -1 if blocked by terrain,
        /// -2 if blocked by a placeable, -3 if blocked by a creature.
        /// </returns>
        public static int TestDirectLine(uint oArea, float fStartX, float fStartY, float fEndX, float fEndY, float fPerSpace, float fHeight, bool bIgnoreDoors = false)
        {
            return NWN.Core.NWNX.AreaPlugin.TestDirectLine(oArea, fStartX, fStartY, fEndX, fEndY, fPerSpace, fHeight, bIgnoreDoors ? 1 : 0);
        }

        /// <summary>
        /// Gets if the area music is playing.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="bBattleMusic">True to check if battle music is playing, otherwise false.</param>
        /// <returns>True if music is playing, otherwise false.</returns>
        public static bool GetMusicIsPlaying(uint oArea, bool bBattleMusic = false)
        {
            return NWN.Core.NWNX.AreaPlugin.GetMusicIsPlaying(oArea, bBattleMusic ? 1 : 0) == 1;
        }

        /// <summary>
        /// Creates and returns a generic trigger in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="fX">The X-coordinate.</param>
        /// <param name="fY">The Y-coordinate.</param>
        /// <param name="fZ">The Z-coordinate.</param>
        /// <param name="sTag">The tag for the trigger.</param>
        /// <param name="fSize">The size of the trigger.</param>
        /// <returns>The created trigger object.</returns>
        public static uint CreateGenericTrigger(uint oArea, float fX, float fY, float fZ, string sTag = "", float fSize = 1.0f)
        {
            return NWN.Core.NWNX.AreaPlugin.CreateGenericTrigger(oArea, fX, fY, fZ, sTag, fSize);
        }

        /// <summary>
        /// Adds an object to the ExportGIT exclusion list for an area.
        /// </summary>
        /// <param name="oObject">The object to add.</param>
        public static void AddObjectToExclusionList(uint oObject)
        {
            NWN.Core.NWNX.AreaPlugin.AddObjectToExclusionList(oObject);
        }

        /// <summary>
        /// Removes an object from the ExportGIT exclusion list for an area.
        /// </summary>
        /// <param name="oObject">The object to remove.</param>
        public static void RemoveObjectFromExclusionList(uint oObject)
        {
            NWN.Core.NWNX.AreaPlugin.RemoveObjectFromExclusionList(oObject);
        }
        /// <summary>
        /// Exports the .git file of an area to the specified location.
        /// </summary>
        /// <param name="oArea">The area to export the .git file of.</param>
        /// <param name="sFileName">The filename to use (16 characters or less, lowercase). Defaults to the area resref.</param>
        /// <param name="bExportVarTable">Whether to export local variables set on the area.</param>
        /// <param name="bExportUUID">Whether to export the UUID of the area.</param>
        /// <param name="nObjectFilter">Object type filter for export. Use OBJECT_TYPE_* constants.</param>
        /// <param name="sAlias">The alias of the resource directory. Default: "NWNX".</param>
        /// <returns>True if exported successfully, otherwise false.</returns>
        public static bool ExportGIT(uint oArea, string sFileName = "", bool bExportVarTable = true, bool bExportUUID = true, int nObjectFilter = 0, string sAlias = "NWNX")
        {
            return NWN.Core.NWNX.AreaPlugin.ExportGIT(oArea, sFileName, bExportVarTable ? 1 : 0, bExportUUID ? 1 : 0, nObjectFilter, sAlias) == 1;
        }

        /// <summary>
        /// Gets the tile information for a tile in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="fTileX">The X-coordinate of the tile.</param>
        /// <param name="fTileY">The Y-coordinate of the tile.</param>
        /// <returns>The tile information.</returns>
        public static TileInfo GetTileInfo(uint oArea, float fTileX, float fTileY)
        {
            return NWN.Core.NWNX.AreaPlugin.GetTileInfo(oArea, fTileX, fTileY);
        }

        /// <summary>
        /// Exports the .are file of an area to the specified location.
        /// </summary>
        /// <param name="oArea">The area to export the .are file of.</param>
        /// <param name="sFileName">The filename to use (16 characters or less, lowercase).</param>
        /// <param name="sNewName">Optional new name for the area. Defaults to the current name.</param>
        /// <param name="sNewTag">Optional new tag for the area. Defaults to the current tag.</param>
        /// <param name="sAlias">The alias of the resource directory. Default: "NWNX".</param>
        /// <returns>True if exported successfully, otherwise false.</returns>
        public static bool ExportARE(uint oArea, string sFileName, string sNewName = "", string sNewTag = "", string sAlias = "NWNX")
        {
            return NWN.Core.NWNX.AreaPlugin.ExportARE(oArea, sFileName, sNewName, sNewTag, sAlias) == 1;
        }

        /// <summary>
        /// Gets the ambient sound playing during the day in an area.
        /// </summary>
        /// <param name="oArea">The area to query.</param>
        /// <returns>The ambient soundtrack ID.</returns>
        public static int GetAmbientSoundDay(uint oArea)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAmbientSoundDay(oArea);
        }

        /// <summary>
        /// Gets the ambient sound playing during the night in an area.
        /// </summary>
        /// <param name="oArea">The area to query.</param>
        /// <returns>The ambient soundtrack ID.</returns>
        public static int GetAmbientSoundNight(uint oArea)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAmbientSoundNight(oArea);
        }

        /// <summary>
        /// Gets the volume of the ambient sound playing during the day in an area.
        /// </summary>
        /// <param name="oArea">The area to query.</param>
        /// <returns>The volume level.</returns>
        public static int GetAmbientSoundDayVolume(uint oArea)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAmbientSoundDayVolume(oArea);
        }

        /// <summary>
        /// Gets the volume of the ambient sound playing during the night in an area.
        /// </summary>
        /// <param name="oArea">The area to query.</param>
        /// <returns>The volume level.</returns>
        public static int GetAmbientSoundNightVolume(uint oArea)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAmbientSoundNightVolume(oArea);
        }

        /// <summary>
        /// Creates a sound object at a specified position in an area.
        /// </summary>
        /// <param name="oArea">The area where the sound object will be created.</param>
        /// <param name="vPosition">The position for the sound object.</param>
        /// <param name="sResRef">The ResRef of the sound object.</param>
        /// <returns>The created sound object.</returns>
        public static uint CreateSoundObject(uint oArea, System.Numerics.Vector3 vPosition, string sResRef)
        {
            return NWN.Core.NWNX.AreaPlugin.CreateSoundObject(oArea, vPosition, sResRef);
        }

        /// <summary>
        /// Rotates an area, including all objects within (excluding PCs).
        /// </summary>
        /// <param name="oArea">The area to rotate.</param>
        /// <param name="nRotation">The rotation in 90-degree increments (1-3).</param>
        public static void RotateArea(uint oArea, int nRotation)
        {
            NWN.Core.NWNX.AreaPlugin.RotateArea(oArea, nRotation);
        }

        /// <summary>
        /// Gets tile information for a tile at a specific index in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="nIndex">The index of the tile.</param>
        /// <returns>The tile information.</returns>
        public static TileInfo GetTileInfoByTileIndex(uint oArea, int nIndex)
        {
            return NWN.Core.NWNX.AreaPlugin.GetTileInfoByTileIndex(oArea, nIndex);
        }

        /// <summary>
        /// Checks if there is a path between two positions in an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="vStartPosition">The starting position.</param>
        /// <param name="vEndPosition">The ending position.</param>
        /// <param name="nMaxDepth">The maximum depth of the DFS tree.</param>
        /// <returns>True if a path exists, otherwise false.</returns>
        public static bool GetPathExists(uint oArea, System.Numerics.Vector3 vStartPosition, System.Numerics.Vector3 vEndPosition, int nMaxDepth)
        {
            return NWN.Core.NWNX.AreaPlugin.GetPathExists(oArea, vStartPosition, vEndPosition, nMaxDepth) == 1;
        }

        /// <summary>
        /// Gets the flags of an area (e.g., interior, underground).
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <returns>The flags bitmask.</returns>
        public static int GetAreaFlags(uint oArea)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAreaFlags(oArea);
        }

        /// <summary>
        /// Sets the flags of an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <param name="nFlags">The flags bitmask to set.</param>
        public static void SetAreaFlags(uint oArea, int nFlags)
        {
            NWN.Core.NWNX.AreaPlugin.SetAreaFlags(oArea, nFlags);
        }

        /// <summary>
        /// Gets detailed wind data for an area.
        /// </summary>
        /// <param name="oArea">The area object.</param>
        /// <returns>The wind data.</returns>
        public static AreaWind GetAreaWind(uint oArea)
        {
            return NWN.Core.NWNX.AreaPlugin.GetAreaWind(oArea);
        }

        /// <summary>
        /// Sets the default object UI discovery mask for objects in an area.
        /// </summary>
        /// <param name="oArea">The area or OBJECT_INVALID to set a global mask.</param>
        /// <param name="nObjectTypes">Object type mask (e.g., OBJECT_TYPE_ALL).</param>
        /// <param name="nMask">The discovery mask to set.</param>
        /// <param name="bForceUpdate">Whether to update the discovery mask of all objects.</param>
        public static void SetDefaultObjectUiDiscoveryMask(uint oArea, int nObjectTypes, int nMask, bool bForceUpdate = false)
        {
            NWN.Core.NWNX.AreaPlugin.SetDefaultObjectUiDiscoveryMask(oArea, nObjectTypes, nMask, bForceUpdate ? 1 : 0);
        }
    }
}
