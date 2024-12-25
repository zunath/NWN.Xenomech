using System.Numerics;
using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.API.Enum.Area;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.NWNX
{
    public struct TileInfo
    {
        public int nID; /// The tile's ID
        public int nHeight; /// The tile's height
        public int nOrientation; /// The tile's orientation
        public int nGridX; /// The tile's grid x position
        public int nGridY; /// The tile's grid y position
    };

    public static class AreaPlugin
    {
        private const string PLUGIN_NAME = "NWNX_Area";
        // Gets the number of players in area
        public static int GetNumberOfPlayersInArea(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetNumberOfPlayersInArea");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Gets the creature that last entered area
        public static uint GetLastEntered(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetLastEntered");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopObject();
        }

        // Gets the creature that last left area
        public static uint GetLastLeft(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetLastLeft");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopObject();
        }

        // Get the PVP setting of area
        // Returns NWNX_AREA_PVP_SETTING_*
        public static PvPSetting GetPVPSetting(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetPVPSetting");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return (PvPSetting)NWNXPInvoke.NWNXPopInt();
        }

        // Set the PVP setting of area
        // pvpSetting = NWNX_AREA_PVP_SETTING_*
        public static void SetPVPSetting(uint area, PvPSetting pvpSetting)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetPVPSetting");
            NWNXPInvoke.NWNXPushInt((int)pvpSetting);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the spot modifier of area
        public static int GetAreaSpotModifier(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAreaSpotModifier");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set the spot modifier of area
        public static void SetAreaSpotModifier(uint area, int spotModifier)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetAreaSpotModifier");
            NWNXPInvoke.NWNXPushInt(spotModifier);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the listen modifer of area
        public static int GetAreaListenModifier(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAreaListenModifier");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set the listen modifier of area
        public static void SetAreaListenModifier(uint area, int listenModifier)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetAreaListenModifier");
            NWNXPInvoke.NWNXPushInt(listenModifier);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Returns TRUE if resting is not allowed in area
        public static bool GetNoRestingAllowed(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetNoRestingAllowed");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt() == 1;
        }

        // Set whether resting is allowed in area
        // TRUE: Resting not allowed
        // FALSE: Resting allowed
        public static void SetNoRestingAllowed(uint area, bool bNoRestingAllowed)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetNoRestingAllowed");
            NWNXPInvoke.NWNXPushInt(bNoRestingAllowed ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the wind power in area
        public static int GetWindPower(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetWindPower");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set the wind power in area
        // windPower = 0-2
        public static void SetWindPower(uint area, int windPower)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetWindPower");
            NWNXPInvoke.NWNXPushInt(windPower);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the weather chance of type in area
        // type = NWNX_AREA_WEATHER_CHANCE_*
        public static int GetWeatherChance(uint area, WeatherEffectType type)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetWeatherChance");
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set the weather chance of type in area
        // type = NWNX_AREA_WEATHER_CHANCE_*
        // chance = 0-100
        public static void SetWeatherChance(uint area, WeatherEffectType type, int chance)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetWeatherChance");
            NWNXPInvoke.NWNXPushInt(chance);
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the fog clip distance in area
        public static float GetFogClipDistance(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetFogClipDistance");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopFloat();
        }

        // Set the fog clip distance in area
        public static void SetFogClipDistance(uint area, float distance)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetFogClipDistance");
            NWNXPInvoke.NWNXPushFloat(distance);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the shadow opacity of area
        public static int GetShadowOpacity(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetShadowOpacity");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set the shadow opacity of area
        // shadowOpacity = 0-100
        public static void SetShadowOpacity(uint area, int shadowOpacity)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetShadowOpacity");
            NWNXPInvoke.NWNXPushInt(shadowOpacity);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the day/night cycle of area
        // Returns NWNX_AREA_DAYNIGHTCYCLE_*
        public static DayNightCycle GetDayNightCycle(uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetDayNightCycle");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return (DayNightCycle)NWNXPInvoke.NWNXPopInt();
        }

        // Set the day/night cycle of area
        // type = NWNX_AREA_DAYNIGHTCYCLE_*
        public static void SetDayNightCycle(uint area, DayNightCycle type)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetDayNightCycle");
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the Sun/Moon Ambient/Diffuse colors of area
        // type = NWNX_AREA_COLOR_TYPE_*
        //
        // Returns FOG_COLOR_* or a custom value, -1 on error
        public static int GetSunMoonColors(uint area, AreaLightColorType type)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetSunMoonColors");
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }
        // Set the Sun/Moon Ambient/Diffuse colors of area
        // type = NWNX_AREA_COLOR_TYPE_*
        // color = FOG_COLOR_*
        //
        // The color can also be represented as a hex RGB number if specific color shades are desired.
        // The format of a hex specified color would be 0xFFEEDD where
        // FF would represent the amount of red in the color
        // EE would represent the amount of green in the color
        // DD would represent the amount of blue in the color.
        public static void SetSunMoonColors(uint area, AreaLightColorType type, int color)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetSunMoonColors");
            NWNXPInvoke.NWNXPushInt(color);
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Create and returns a transition (square shaped of specified size) at a location
        // Valid object types for the target are DOOR or WAYPOINT.
        // If a tag is specified the returning object will have that tag
        public static uint CreateTransition(uint area, uint target, float x, float y, float z, float size = 2.0f, string tag = "")
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "CreateTransition");
            NWNXPInvoke.NWNXPushString(tag);
            NWNXPInvoke.NWNXPushFloat(size);
            NWNXPInvoke.NWNXPushFloat(z);
            NWNXPInvoke.NWNXPushFloat(y);
            NWNXPInvoke.NWNXPushFloat(x);
            NWNXPInvoke.NWNXPushObject(target);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopObject();
        }

        // Get the state of a tile animation loop
        // nAnimLoop = 1-3
        public static int GetTileAnimationLoop(uint area, float tileX, float tileY, int animLoop)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetTileAnimationLoop");
            NWNXPInvoke.NWNXPushInt(animLoop);
            NWNXPInvoke.NWNXPushFloat(tileY);
            NWNXPInvoke.NWNXPushFloat(tileX);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set the state of a tile animation loop
        // nAnimLoop = 1-3
        //
        // NOTE: Requires clients to re-enter the area for it to take effect
        public static void SetTileAnimationLoop(uint area, float tileX, float tileY, int animLoop, bool enabled)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetTileAnimationLoop");
            NWNXPInvoke.NWNXPushInt(enabled ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(animLoop);
            NWNXPInvoke.NWNXPushFloat(tileY);
            NWNXPInvoke.NWNXPushFloat(tileX);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Create and return a generic trigger (square shaped of specified size) at a location.
        // oArea The area object.
        // fX, fY, fZ The position to create the trigger.
        // sTag If specified, the returned trigger will have this tag.
        // fSize The size of the square.
        // NWNX_Object_SetTriggerGeometry() if you wish to draw the trigger as something other than a square.
        public static uint CreateGenericTrigger(uint area, float x, float y, float z, string tag = "", float size = 1.0f)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "CreateGenericTrigger");
            NWNXPInvoke.NWNXPushFloat(size);
            NWNXPInvoke.NWNXPushString(tag);
            NWNXPInvoke.NWNXPushFloat(z);
            NWNXPInvoke.NWNXPushFloat(y);
            NWNXPInvoke.NWNXPushFloat(x);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopObject();
        }

        // Add oObject to the ExportGIT exclusion list, objects on this list won't be exported when NWNX_Area_ExportGIT() is called.
        public static void AddObjectToExclusionList(uint oObject)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "AddObjectToExclusionList");
            NWNXPInvoke.NWNXPushObject(oObject);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Remove oObject from the ExportGIT exclusion list.
        public static void RemoveObjectFromExclusionList(uint oObject)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "RemoveObjectFromExclusionList");
            NWNXPInvoke.NWNXPushObject(oObject);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Export the GIT of oArea to the UserDirectory/nwnx folder.
        public static int ExportGIT(uint oArea, string sFileName = "", bool bExportVarTable = true, bool bExportUUID = true, int nObjectFilter = 0)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ExportGIT");
            NWNXPInvoke.NWNXPushInt(nObjectFilter);
            NWNXPInvoke.NWNXPushInt(bExportUUID ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(bExportVarTable ? 1 : 0);
            NWNXPInvoke.NWNXPushString(sFileName);
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Get the tile info of the tile at [fTileX, fTileY] in oArea.
        public static TileInfo GetTileInfo(uint oArea, float fTileX, float fTileY)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetTileInfo");
            NWNXPInvoke.NWNXPushFloat(fTileY);
            NWNXPInvoke.NWNXPushFloat(fTileX);
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();

            var str = new TileInfo
            {
                nGridY = NWNXPInvoke.NWNXPopInt(),
                nGridX = NWNXPInvoke.NWNXPopInt(),
                nOrientation = NWNXPInvoke.NWNXPopInt(),
                nHeight = NWNXPInvoke.NWNXPopInt(),
                nID = NWNXPInvoke.NWNXPopInt()
            };

            return str;
        }

        // Export the .are file of oArea to the UserDirectory/nwnx folder, or to the location of sAlias.
        public static bool ExportARE(uint oArea, string sFileName, string sNewName = "", string sNewTag = "", string sAlias = "NWNX")
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ExportARE");
            NWNXPInvoke.NWNXPushString(sAlias);
            NWNXPInvoke.NWNXPushString(sNewTag);
            NWNXPInvoke.NWNXPushString(sNewName);
            NWNXPInvoke.NWNXPushString(sFileName);
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt() == 1;
        }

        // Get the ambient sound playing in an area during the day.
        public static int GetAmbientSoundDay(uint oArea)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAmbientSoundDay");
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Get the ambient sound playing in an area during the night.
        public static int GetAmbientSoundNight(uint oArea)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAmbientSoundNight");
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Get the volume of the ambient sound playing in an area during the day.
        public static int GetAmbientSoundDayVolume(uint oArea)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAmbientSoundDayVolume");
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Get the volume of the ambient sound playing in an area during the night.
        public static int GetAmbientSoundNightVolume(uint oArea)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAmbientSoundNightVolume");
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Create a sound object.
        public static uint CreateSoundObject(uint oArea, Vector3 vPosition, string sResRef)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "CreateSoundObject");
            NWNXPInvoke.NWNXPushString(sResRef);
            NWNXPInvoke.NWNXPushFloat(vPosition.Z);
            NWNXPInvoke.NWNXPushFloat(vPosition.Y);
            NWNXPInvoke.NWNXPushFloat(vPosition.X);
            NWNXPInvoke.NWNXPushObject(oArea);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopObject();
        }

    }
}