using System.Numerics;
using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWScript.Enum;
using NWN.Xenomech.Core.NWScript.Enum.Area;
using ObjectType = NWN.Xenomech.Core.NWScript.Enum.ObjectType;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        ///   Get the area that oTarget is currently in
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetArea(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(24);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   The value returned by this function depends on the object type of the caller:
        ///   1) If the caller is a door it returns the object that last
        ///   triggered it.
        ///   2) If the caller is a trigger, area of effect, module, area or encounter it
        ///   returns the object that last entered it.
        ///   * Return value on error: OBJECT_INVALID
        ///   When used for doors, this should only be called from the OnAreaTransitionClick
        ///   event.  Otherwise, it should only be called in OnEnter scripts.
        /// </summary>
        public static uint GetEnteringObject()
        {
            NWNXPInvoke.CallBuiltIn(25);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the object that last left the caller.  This function works on triggers,
        ///   areas of effect, modules, areas and encounters.
        ///   * Return value on error: OBJECT_INVALID
        ///   Should only be called in OnExit scripts.
        /// </summary>
        public static uint GetExitingObject()
        {
            NWNXPInvoke.CallBuiltIn(26);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the position of oTarget
        ///   * Return value on error: vector (0.0f, 0.0f, 0.0f)
        /// </summary>
        public static Vector3 GetPosition(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(27);
            return NWNXPInvoke.StackPopVector();
        }
        /// <summary>
        ///   Play the background music for oArea.
        /// </summary>
        public static void MusicBackgroundPlay(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(425);
        }

        /// <summary>
        ///   Stop the background music for oArea.
        /// </summary>
        public static void MusicBackgroundStop(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(426);
        }

        /// <summary>
        ///   Set the delay for the background music for oArea.
        ///   - oArea
        ///   - nDelay: delay in milliseconds
        /// </summary>
        public static void MusicBackgroundSetDelay(uint oArea, int nDelay)
        {
            NWNXPInvoke.StackPushInteger(nDelay);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(427);
        }

        /// <summary>
        ///   Change the background day track for oArea to nTrack.
        ///   - oArea
        ///   - nTrack
        /// </summary>
        public static void MusicBackgroundChangeDay(uint oArea, int nTrack)
        {
            NWNXPInvoke.StackPushInteger(nTrack);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(428);
        }

        /// <summary>
        ///   Change the background night track for oArea to nTrack.
        ///   - oArea
        ///   - nTrack
        /// </summary>
        public static void MusicBackgroundChangeNight(uint oArea, int nTrack)
        {
            NWNXPInvoke.StackPushInteger(nTrack);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(429);
        }

        /// <summary>
        ///   Play the battle music for oArea.
        /// </summary>
        public static void MusicBattlePlay(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(430);
        }

        /// <summary>
        ///   Stop the battle music for oArea.
        /// </summary>
        public static void MusicBattleStop(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(431);
        }

        /// <summary>
        ///   Change the battle track for oArea.
        ///   - oArea
        ///   - nTrack
        /// </summary>
        public static void MusicBattleChange(uint oArea, int nTrack)
        {
            NWNXPInvoke.StackPushInteger(nTrack);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(432);
        }

        /// <summary>
        ///   Play the ambient sound for oArea.
        /// </summary>
        public static void AmbientSoundPlay(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(433);
        }

        /// <summary>
        ///   Stop the ambient sound for oArea.
        /// </summary>
        public static void AmbientSoundStop(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(434);
        }
        /// <summary>
        ///   Change the ambient day track for oArea to nTrack.
        ///   - oArea
        ///   - nTrack
        /// </summary>
        public static void AmbientSoundChangeDay(uint oArea, int nTrack)
        {
            NWNXPInvoke.StackPushInteger(nTrack);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(435);
        }

        /// <summary>
        ///   Change the ambient night track for oArea to nTrack.
        ///   - oArea
        ///   - nTrack
        /// </summary>
        public static void AmbientSoundChangeNight(uint oArea, int nTrack)
        {
            NWNXPInvoke.StackPushInteger(nTrack);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(436);
        }

        /// <summary>
        ///   All clients in oArea will recompute the static lighting.
        ///   This can be used to update the lighting after changing any tile lights or if
        ///   placeables with lights have been added/deleted.
        /// </summary>
        public static void RecomputeStaticLighting(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(516);
        }

        /// <summary>
        ///   Get the Day Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetDayTrack(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(558);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the Night Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetNightTrack(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(559);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the ambient day volume for oArea to nVolume.
        ///   - oArea
        ///   - nVolume: 0 - 100
        /// </summary>
        public static void AmbientSoundSetDayVolume(uint oArea, int nVolume)
        {
            NWNXPInvoke.StackPushInteger(nVolume);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(567);
        }

        /// <summary>
        ///   Set the ambient night volume for oArea to nVolume.
        ///   - oArea
        ///   - nVolume: 0 - 100
        /// </summary>
        public static void AmbientSoundSetNightVolume(uint oArea, int nVolume)
        {
            NWNXPInvoke.StackPushInteger(nVolume);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(568);
        }

        /// <summary>
        ///   Get the Battle Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetBattleTrack(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(569);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   This will return TRUE if the area is flagged as either interior or underground.
        /// </summary>
        public static bool GetIsAreaInterior(uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(716);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Gets the current weather conditions for the area oArea.
        ///   Returns: WEATHER_CLEAR, WEATHER_RAIN, WEATHER_SNOW, WEATHER_INVALID
        ///   Note: If called on an Interior area, this will always return WEATHER_CLEAR.
        /// </summary>
        public static Weather GetWeather(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(724);
            return (Weather)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns AREA_NATURAL if the area oArea is natural, AREA_ARTIFICIAL otherwise.
        ///   Returns AREA_INVALID, on an error.
        /// </summary>
        public static Natural GetIsAreaNatural(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(725);
            return (Natural)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns AREA_ABOVEGROUND if the area oArea is above ground, AREA_UNDERGROUND otherwise.
        ///   Returns AREA_INVALID, on an error.
        /// </summary>
        public static bool GetIsAreaAboveGround(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(726);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Changes the sky that is displayed in the specified area.
        ///   nSkyBox = SKYBOX_* constants (associated with skyboxes.2da)
        ///   If no valid area (or object) is specified, it uses the area of caller.
        ///   If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetSkyBox(Skybox nSkyBox, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nSkyBox);
            NWNXPInvoke.CallBuiltIn(777);
        }

        /// <summary>
        ///   Sets the fog color in the area specified.
        ///   nFogType = FOG_TYPE_* specifies wether the Sun, Moon, or both fog types are set.
        ///   nFogColor = FOG_COLOR_* specifies the color the fog is being set to.
        ///   The fog color can also be represented as a hex RGB number if specific color shades
        ///   are desired.
        ///   The format of a hex specified color would be 0xFFEEDD where
        ///   FF would represent the amount of red in the color
        ///   EE would represent the amount of green in the color
        ///   DD would represent the amount of blue in the color.
        ///   If no valid area (or object) is specified, it uses the area of caller.
        ///   If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetFogColor(FogType nFogType, FogColor nFogColor, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nFogColor);
            NWNXPInvoke.StackPushInteger((int)nFogType);
            NWNXPInvoke.CallBuiltIn(780);
        }

        /// <summary>
        ///   Gets the skybox that is currently displayed in the specified area.
        ///   Returns:
        ///   SKYBOX_* constant
        ///   If no valid area (or object) is specified, it uses the area of caller.
        ///   If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static Skybox GetSkyBox(uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(782);
            return (Skybox)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the fog color in the area specified.
        ///   nFogType specifies wether the Sun, or Moon fog type is returned.
        ///   Valid values for nFogType are FOG_TYPE_SUN or FOG_TYPE_MOON.
        ///   If no valid area (or object) is specified, it uses the area of caller.
        ///   If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static FogColor GetFogColor(FogType nFogType, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nFogType);
            NWNXPInvoke.CallBuiltIn(783);
            return (FogColor)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the fog amount in the area specified.
        ///   nFogType = FOG_TYPE_* specifies wether the Sun, Moon, or both fog types are set.
        ///   nFogAmount = specifies the density that the fog is being set to.
        ///   If no valid area (or object) is specified, it uses the area of caller.
        ///   If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetFogAmount(FogType nFogType, int nFogAmount, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger(nFogAmount);
            NWNXPInvoke.StackPushInteger((int)nFogType);
            NWNXPInvoke.CallBuiltIn(784);
        }
        /// <summary>
        ///   Gets the fog amount in the area specified.
        ///   nFogType = nFogType specifies wether the Sun, or Moon fog type is returned.
        ///   Valid values for nFogType are FOG_TYPE_SUN or FOG_TYPE_MOON.
        ///   If no valid area (or object) is specified, it uses the area of caller.
        ///   If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static int GetFogAmount(FogType nFogType, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nFogType);
            NWNXPInvoke.CallBuiltIn(785);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the resref (TILESET_RESREF_*) of the tileset used to create area oArea.
        ///   * returns an empty string on an error.
        /// </summary>
        public static string GetTilesetResRef(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(814);
            return NWNCore.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///   Gets the size of the area.
        ///   - nAreaDimension: The area dimension that you wish to determine.
        ///   AREA_HEIGHT
        ///   AREA_WIDTH
        ///   - oArea: The area that you wish to get the size of.
        ///   Returns: The number of tiles that the area is wide/high, or zero on an error.
        /// </summary>
        public static int GetAreaSize(Dimension nAreaDimension, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nAreaDimension);
            NWNXPInvoke.CallBuiltIn(829);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Destroys the given area object and everything in it.
        /// </summary>
        public static int DestroyArea(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(859);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Instances a new area from the given sSourceResRef.
        ///   Returns the new area, or OBJECT_INVALID on failure.
        /// </summary>
        public static uint CreateArea(string sSourceResRef, string sNewTag = "", string sNewName = "")
        {
            NWNXPInvoke.StackPushString(sNewName);
            NWNXPInvoke.StackPushString(sNewTag);
            NWNXPInvoke.StackPushString(sSourceResRef);
            NWNXPInvoke.CallBuiltIn(858);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Creates a copy of an existing area, including everything inside of it (except players).
        /// </summary>
        public static uint CopyArea(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(860);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Returns the first area in the module.
        /// </summary>
        public static uint GetFirstArea()
        {
            NWNXPInvoke.CallBuiltIn(861);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Returns the next area in the module (after GetFirstArea), or OBJECT_INVALID if no more areas are loaded.
        /// </summary>
        public static uint GetNextArea()
        {
            NWNXPInvoke.CallBuiltIn(862);
            return NWNXPInvoke.StackPopObject();
        }
        /// <summary>
        /// Get the first object in oArea.
        /// If no valid area is specified, it will use the caller's area.
        /// - nObjectFilter: This allows you to filter out undesired object types, using bitwise "or".
        /// * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetFirstObjectInArea(uint oArea = OBJECT_INVALID, ObjectType nObjectFilter = ObjectType.All)
        {
            NWNXPInvoke.StackPushInteger((int)nObjectFilter);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(93);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Get the next object in oArea.
        /// If no valid area is specified, it will use the caller's area.
        /// - nObjectFilter: This allows you to filter out undesired object types, using bitwise "or".
        /// * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNextObjectInArea(uint oArea = OBJECT_INVALID, ObjectType nObjectFilter = ObjectType.All)
        {
            NWNXPInvoke.StackPushInteger((int)nObjectFilter);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(94);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the location of oObject.
        /// </summary>
        public static Location GetLocation(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(213);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Location);
        }

        /// <summary>
        ///   The subject will jump to lLocation instantly (even between areas).
        ///   If lLocation is invalid, nothing will happen.
        /// </summary>
        public static void ActionJumpToLocation(Location lLocation)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.CallBuiltIn(214);
        }

        /// <summary>
        ///   Create a location.
        /// </summary>
        public static Location Location(uint oArea, Vector3 vPosition, float fOrientation)
        {
            NWNXPInvoke.StackPushFloat(fOrientation);
            NWNXPInvoke.StackPushVector(vPosition);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(215);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Location);
        }

        /// <summary>
        ///   Apply eEffect at lLocation.
        /// </summary>
        public static void ApplyEffectAtLocation(DurationType nDurationType, Effect eEffect, Location lLocation, float fDuration = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fDuration);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.StackPushInteger((int)nDurationType);
            NWNXPInvoke.CallBuiltIn(216);
        }

        /// <summary>
        ///   Expose/Hide the entire map of oArea for oPlayer.
        ///   - oArea: The area that the map will be exposed/hidden for.
        ///   - oPlayer: The player the map will be exposed/hidden for.
        ///   - bExplored: TRUE/FALSE. Whether the map should be completely explored or hidden.
        /// </summary>
        public static void ExploreAreaForPlayer(uint oArea, uint oPlayer, bool bExplored = true)
        {
            NWNXPInvoke.StackPushInteger(bExplored ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(403);
        }

        /// <summary>
        ///   Sets the transition target for oTransition.
        /// </summary>
        public static void SetTransitionTarget(uint oTransition, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oTransition);
            NWNXPInvoke.CallBuiltIn(863);
        }

        /// <summary>
        ///   Set the weather for oTarget.
        /// </summary>
        public static void SetWeather(uint oTarget, WeatherType nWeather)
        {
            NWNXPInvoke.StackPushInteger((int)nWeather);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(507);
        }

        /// <summary>
        ///   Sets if the given creature has explored tile at x, y of the given area.
        /// </summary>
        public static int SetTileExplored(uint creature, uint area, int x, int y, bool newState)
        {
            NWNXPInvoke.StackPushInteger(newState ? 1 : 0);
            NWNXPInvoke.StackPushInteger(y);
            NWNXPInvoke.StackPushInteger(x);
            NWNXPInvoke.StackPushObject(area);
            NWNXPInvoke.StackPushObject(creature);
            NWNXPInvoke.CallBuiltIn(866);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns whether the given tile at x, y, for the given creature in the stated area is visible on the map.
        /// </summary>
        public static int GetTileExplored(uint creature, uint area, int x, int y)
        {
            NWNXPInvoke.StackPushInteger(y);
            NWNXPInvoke.StackPushInteger(x);
            NWNXPInvoke.StackPushObject(area);
            NWNXPInvoke.StackPushObject(creature);
            NWNXPInvoke.CallBuiltIn(867);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        ///   Sets the creature to auto-explore the map as it walks around.
        ///   Valid arguments: TRUE and FALSE.
        ///   Returns the previous state (or -1 if non-creature).
        /// </summary>
        public static int SetCreatureExploresMinimap(uint creature, bool newState)
        {
            NWNXPInvoke.StackPushInteger(newState ? 1 : 0);
            NWNXPInvoke.StackPushObject(creature);
            NWNXPInvoke.CallBuiltIn(868);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns TRUE if the creature is set to auto-explore the map as it walks around (on by default).
        ///   Returns FALSE if creature is not actually a creature.
        /// </summary>
        public static int GetCreatureExploresMinimap(uint creature)
        {
            NWNXPInvoke.StackPushObject(creature);
            NWNXPInvoke.CallBuiltIn(869);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the surface material at the given location. (This is equivalent to the walkmesh type).
        ///   Returns 0 if the location is invalid or has no surface type.
        /// </summary>
        public static int GetSurfaceMaterial(Location at)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, at);
            NWNXPInvoke.CallBuiltIn(870);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the z-offset at which the walkmesh is at the given location.
        ///   Returns -6.0 for invalid locations.
        /// </summary>
        public static float GetGroundHeight(Location at)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, at);
            NWNXPInvoke.CallBuiltIn(871);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Is this creature in the given subarea? (trigger, area of effect object, etc..)
        ///   Returns TRUE if the creature has triggered an onEnter event.
        /// </summary>
        public static bool GetIsInSubArea(uint oCreature, uint oSubArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSubArea);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(768);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Set the main light color on the tile at lTileLocation.
        /// </summary>
        public static void SetTileMainLightColor(Location lTileLocation, int nMainLight1Color, int nMainLight2Color)
        {
            NWNXPInvoke.StackPushInteger(nMainLight2Color);
            NWNXPInvoke.StackPushInteger(nMainLight1Color);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTileLocation);
            NWNXPInvoke.CallBuiltIn(514);
        }

        /// <summary>
        ///   Set the source light color on the tile at lTileLocation.
        /// </summary>
        public static void SetTileSourceLightColor(Location lTileLocation, int nSourceLight1Color, int nSourceLight2Color)
        {
            NWNXPInvoke.StackPushInteger(nSourceLight2Color);
            NWNXPInvoke.StackPushInteger(nSourceLight1Color);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTileLocation);
            NWNXPInvoke.CallBuiltIn(515);
        }

        /// <summary>
        ///   Get the color for the main light 1 of the tile at lTile.
        /// </summary>
        public static int GetTileMainLight1Color(Location lTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTile);
            NWNXPInvoke.CallBuiltIn(517);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the color for the main light 2 of the tile at lTile.
        /// </summary>
        public static int GetTileMainLight2Color(Location lTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTile);
            NWNXPInvoke.CallBuiltIn(518);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the color for the source light 1 of the tile at lTile.
        /// </summary>
        public static int GetTileSourceLight1Color(Location lTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTile);
            NWNXPInvoke.CallBuiltIn(519);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the color for the source light 2 of the tile at lTile.
        /// </summary>
        public static int GetTileSourceLight2Color(Location lTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTile);
            NWNXPInvoke.CallBuiltIn(520);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set whether oMapPin is enabled.
        ///   - oMapPin
        ///   - nEnabled: 0=Off, 1=On
        /// </summary>
        public static void SetMapPinEnabled(uint oMapPin, bool bEnabled = true)
        {
            NWNXPInvoke.StackPushInteger(bEnabled ? 1 : 0);
            NWNXPInvoke.StackPushObject(oMapPin);
            NWNXPInvoke.CallBuiltIn(386);
        }

        /// <summary>
        ///   Get the area's object ID from lLocation.
        /// </summary>
        public static uint GetAreaFromLocation(Location lLocation)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.CallBuiltIn(224);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the position vector from lLocation.
        /// </summary>
        public static Vector3 GetPositionFromLocation(Location lLocation)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.CallBuiltIn(223);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        ///   Set the transition bitmap of a player.
        /// </summary>
        public static void SetAreaTransitionBMP(AreaTransition nPredefinedAreaTransition, string sCustomAreaTransitionBMP = "")
        {
            NWNXPInvoke.StackPushString(sCustomAreaTransitionBMP);
            NWNXPInvoke.StackPushInteger((int)nPredefinedAreaTransition);
            NWNXPInvoke.CallBuiltIn(203);
        }

        /// <summary>
        ///   Sets the detailed wind data for oArea.
        /// </summary>
        public static void SetAreaWind(uint oArea, Vector3 vDirection, float fMagnitude, float fYaw, float fPitch)
        {
            NWNXPInvoke.StackPushFloat(fPitch);
            NWNXPInvoke.StackPushFloat(fYaw);
            NWNXPInvoke.StackPushFloat(fMagnitude);
            NWNXPInvoke.StackPushVector(vDirection);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(919);
        }

        /// <summary>
        ///   Gets the light color in the area specified.
        /// </summary>
        public static int GetAreaLightColor(AreaLightColorType nColorType, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nColorType);
            NWNXPInvoke.CallBuiltIn(1080);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the light color in the area specified.
        /// </summary>
        public static void SetAreaLightColor(AreaLightColorType nColorType, FogColor nColor, uint oArea = OBJECT_INVALID, float fFadeTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fFadeTime);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger((int)nColor);
            NWNXPInvoke.StackPushInteger((int)nColorType);
            NWNXPInvoke.CallBuiltIn(1081);
        }

        /// <summary>
        ///   Gets the light direction of origin in the area specified.
        /// </summary>
        public static Vector3 GetAreaLightDirection(AreaLightDirectionType nLightType, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushInteger((int)nLightType);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1082);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        ///   Sets the light direction of origin in the area specified.
        /// </summary>
        public static void SetAreaLightDirection(AreaLightDirectionType nLightType, Vector3 vDirection, uint oArea = OBJECT_INVALID, float fFadeTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fFadeTime);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushVector(vDirection);
            NWNXPInvoke.StackPushInteger((int)nLightType);
            NWNXPInvoke.CallBuiltIn(1083);
        }

        /// <summary>
        ///   Sets a grass override for nMaterialId in oArea.
        /// </summary>
        public static void SetAreaGrassOverride(uint oArea, int nMaterialId, string sTexture, float fDensity, float fHeight, Vector3 vAmbientColor, Vector3 vDiffuseColor)
        {
            NWNXPInvoke.StackPushVector(vDiffuseColor);
            NWNXPInvoke.StackPushVector(vAmbientColor);
            NWNXPInvoke.StackPushFloat(fHeight);
            NWNXPInvoke.StackPushFloat(fDensity);
            NWNXPInvoke.StackPushString(sTexture);
            NWNXPInvoke.StackPushInteger(nMaterialId);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1139);
        }

        /// <summary>
        ///   Remove a grass override from oArea for nMaterialId.
        /// </summary>
        public static void RemoveAreaGrassOverride(uint oArea, int nMaterialId)
        {
            NWNXPInvoke.StackPushInteger(nMaterialId);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1140);
        }

        /// <summary>
        ///   Set to TRUE to disable the default grass of oArea.
        /// </summary>
        public static void SetAreaDefaultGrassDisabled(uint oArea, bool bDisabled)
        {
            NWNXPInvoke.StackPushInteger(bDisabled ? 1 : 0);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1141);
        }

        /// <summary>
        ///   Gets the NoRest area flag.
        /// </summary>
        public static bool GetAreaNoRestFlag(uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1142);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   Sets the NoRest flag on an area.
        /// </summary>
        public static void SetAreaNoRestFlag(bool bNoRestFlag, uint oArea = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.StackPushInteger(bNoRestFlag ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(1143);
        }

        /// <summary>
        ///   Set to TRUE to disable the inaccessible tile border of oArea.
        /// </summary>
        public static void SetAreaTileBorderDisabled(uint oArea, bool bDisabled)
        {
            NWNXPInvoke.StackPushInteger(bDisabled ? 1 : 0);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1147);
        }

    }
}