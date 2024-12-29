using System.Numerics;
using NWN.Xenomech.API.Constants;

namespace NWN.Xenomech.API.NWNX.ObjectPlugin
{
    public static class ObjectPlugin
    {
        /// Set an object's position.
        /// <param name="oObject">The object.</param>
        /// <param name="vPosition">A vector position.</param>
        /// <param name="bUpdateSubareas">If TRUE and oObject is a creature, any triggers/traps at vPosition will fire their events.</param>
        public static void SetPosition(uint oObject, Vector3 vPosition, bool bUpdateSubareas = true)
        {
            Core.NWNX.ObjectPlugin.SetPosition(oObject, vPosition, bUpdateSubareas ? 1 : 0);
        }

        /// Get an object's hit points.
        /// <param name="creature">The object.</param>
        /// <returns>The hit points.</returns>
        public static int GetCurrentHitPoints(uint creature)
        {
            return Core.NWNX.ObjectPlugin.GetCurrentHitPoints(creature);
        }

        /// Set an object's hit points.
        /// <param name="creature">The object.</param>
        /// <param name="hp">The hit points.</param>
        public static void SetCurrentHitPoints(uint creature, int hp)
        {
            Core.NWNX.ObjectPlugin.SetCurrentHitPoints(creature, hp);
        }

        /// Adjust an object's maximum hit points.
        /// @note Will not work on PCs.
        /// <param name="creature">The object.</param>
        /// <param name="hp">The maximum hit points.</param>
        public static void SetMaxHitPoints(uint creature, int hp)
        {
            Core.NWNX.ObjectPlugin.SetMaxHitPoints(creature, hp);
        }

        /// Serialize a full object to a base64 string.
        /// <param name="obj">The object.</param>
        /// <returns>A base64 string representation of the object.</returns>
        public static string Serialize(uint obj)
        {
            return Core.NWNX.ObjectPlugin.Serialize(obj);
        }

        /// Deserialize the object.
        /// @note The object will be created outside of the world and needs to be manually positioned at a location/inventory.
        /// <param name="serialized">The base64 string.</param>
        /// <returns>The object.</returns>
        public static uint Deserialize(string serialized)
        {
            return Core.NWNX.ObjectPlugin.Deserialize(serialized);
        }

        /// Gets the dialog resref.
        /// <param name="obj">The object.</param>
        /// <returns>The name of the dialog resref.</returns>
        public static string GetDialogResref(uint obj)
        {
            return Core.NWNX.ObjectPlugin.GetDialogResref(obj);
        }

        /// Sets the dialog resref.
        /// <param name="obj">The object.</param>
        /// <param name="dialog">The name of the dialog resref.</param>
        public static void SetDialogResref(uint obj, string dialog)
        {
            Core.NWNX.ObjectPlugin.SetDialogResref(obj, dialog);
        }

        /// Set oPlaceable's appearance.
        /// @note Will not update for PCs until they re-enter the area.
        /// <param name="oPlaceable">The placeable.</param>
        /// <param name="nAppearance">The appearance id.</param>
        public static void SetAppearance(uint oPlaceable, int nAppearance)
        {
            Core.NWNX.ObjectPlugin.SetAppearance(oPlaceable, nAppearance);
        }

        /// Get oPlaceable's appearance.
        /// <param name="oPlaceable">The placeable.</param>
        /// <returns>The appearance id.</returns>
        public static int GetAppearance(uint oPlaceable)
        {
            return Core.NWNX.ObjectPlugin.GetAppearance(oPlaceable);
        }

        /// Determine if an object has a visual effect.
        /// <param name="obj">The object.</param>
        /// <param name="nVFX">The visual effect id.</param>
        /// <returns>TRUE if the object has the visual effect applied to it.</returns>
        public static bool GetHasVisualEffect(uint obj, VisualEffectType nVFX)
        {
            return Core.NWNX.ObjectPlugin.GetHasVisualEffect(obj, (int)nVFX) == 1;
        }

        /// Get an object's damage immunity.
        /// <param name="obj">The object.</param>
        /// <param name="damageType">The damage type to check for immunity. Use DAMAGE_TYPE_* constants.</param>
        /// <returns>Damage immunity as a percentage.</returns>
        public static int GetDamageImmunity(uint obj, DamageType damageType)
        {
            return Core.NWNX.ObjectPlugin.GetDamageImmunity(obj, (int)damageType);
        }

        /// Add or move an object.
        /// <param name="obj">The object.</param>
        /// <param name="area">The area.</param>
        /// <param name="pos">The position.</param>
        public static void AddToArea(uint obj, uint area, Vector3 pos)
        {
            Core.NWNX.ObjectPlugin.AddToArea(obj, area, pos);
        }

        /// Get placeable's static setting.
        /// <param name="obj">The object.</param>
        /// <returns>TRUE if placeable is static.</returns>
        public static int GetPlaceableIsStatic(uint obj)
        {
            return Core.NWNX.ObjectPlugin.GetPlaceableIsStatic(obj);
        }

        /// Set placeable as static or not.
        /// @note Will not update for PCs until they re-enter the area.
        /// <param name="obj">The object.</param>
        /// <param name="isStatic">TRUE/FALSE</param>
        public static void SetPlaceableIsStatic(uint obj, int isStatic)
        {
            Core.NWNX.ObjectPlugin.SetPlaceableIsStatic(obj, isStatic);
        }

        /// Gets if a door/placeable auto-removes the key after use.
        /// <param name="obj">The object.</param>
        /// <returns>TRUE/FALSE or -1 on error.</returns>
        public static int GetAutoRemoveKey(uint obj)
        {
            return Core.NWNX.ObjectPlugin.GetAutoRemoveKey(obj);
        }

        /// Sets if a door/placeable auto-removes the key after use.
        /// <param name="obj">The object.</param>
        /// <param name="bRemoveKey">TRUE/FALSE</param>
        public static void SetAutoRemoveKey(uint obj, int bRemoveKey)
        {
            Core.NWNX.ObjectPlugin.SetAutoRemoveKey(obj, bRemoveKey);
        }

        /// Get the geometry of a trigger.
        /// <param name="oTrigger">The trigger object.</param>
        /// <returns>A string of vertex positions.</returns>
        public static string GetTriggerGeometry(uint oTrigger)
        {
            return Core.NWNX.ObjectPlugin.GetTriggerGeometry(oTrigger);
        }

        /// Set the geometry of a trigger with a list of vertex positions.
        /// <param name="oTrigger">The trigger object.</param>
        /// <param name="sGeometry">Needs to be in the following format -> {x.x, y.y, z.z} or {x.x, y.y}</param>
        public static void SetTriggerGeometry(uint oTrigger, string sGeometry)
        {
            Core.NWNX.ObjectPlugin.SetTriggerGeometry(oTrigger, sGeometry);
        }

        /// Export an object to the UserDirectory/nwnx folder.
        /// <param name="sFileName">The filename without extension, 16 or less characters.</param>
        /// <param name="oObject">The object to export. Valid object types: Creature, Item, Placeable, Waypoint, Door, Store, Trigger</param>
        public static void Export(uint oObject, string sFileName, string sAlias = "NWNX")
        {
            Core.NWNX.ObjectPlugin.Export(oObject, sFileName, sAlias);
        }

        /// Get oObject's integer variable sVarName.
        /// <param name="oObject">The object to get the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <returns>The value or 0 on error.</returns>
        public static int GetInt(uint oObject, string sVarName)
        {
            return Core.NWNX.ObjectPlugin.GetInt(oObject, sVarName);
        }

        /// Set oObject's integer variable sVarName to nValue.
        /// <param name="oObject">The object to set the variable on.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <param name="nValue">The integer value to set.</param>
        public static void SetInt(uint oObject, string sVarName, int nValue, bool bPersist)
        {
            Core.NWNX.ObjectPlugin.SetInt(oObject, sVarName, nValue, bPersist ? 1 : 0);
        }

        /// Delete oObject's integer variable sVarName.
        /// <param name="oObject">The object to delete the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        public static void DeleteInt(uint oObject, string sVarName)
        {
            Core.NWNX.ObjectPlugin.DeleteInt(oObject, sVarName);
        }

        /// Get oObject's string variable sVarName.
        /// <param name="oObject">The object to get the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <returns>The value or "" on error.</returns>
        public static string GetString(uint oObject, string sVarName)
        {
            return Core.NWNX.ObjectPlugin.GetString(oObject, sVarName);
        }
        /// Delete oObject's string variable sVarName.
        /// <param name="oObject">The object to delete the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        public static void DeleteString(uint oObject, string sVarName)
        {
            Core.NWNX.ObjectPlugin.DeleteString(oObject, sVarName);
        }

        /// Get oObject's float variable sVarName.
        /// <param name="oObject">The object to get the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <returns>The value or 0.0f on error.</returns>
        public static float GetFloat(uint oObject, string sVarName)
        {
            return Core.NWNX.ObjectPlugin.GetFloat(oObject, sVarName);
        }

        /// Set oObject's float variable sVarName to fValue.
        /// <param name="oObject">The object to set the variable on.</param>
        /// <param name="sVarName">The variable name.</param>
        /// <param name="fValue">The float value to set</param>
        /// <param name="bPersist">When TRUE, the value is persisted to GFF, this means that it'll be saved in the .bic file of a player's character or when an object is serialized.</param>
        public static void SetFloat(uint oObject, string sVarName, float fValue, bool bPersist)
        {
            Core.NWNX.ObjectPlugin.SetFloat(oObject, sVarName, fValue, bPersist ? 1 : 0);
        }

        /// Delete oObject's persistent float variable sVarName.
        /// <param name="oObject">The object to delete the variable from.</param>
        /// <param name="sVarName">The variable name.</param>
        public static void DeleteFloat(uint oObject, string sVarName)
        {
            Core.NWNX.ObjectPlugin.DeleteFloat(oObject, sVarName);
        }

        /// Delete any variables that match sRegex
        /// @note It will only remove variables set by NWNX_Object_Set{Int|String|Float}()
        /// <param name="oObject">The object to delete the variables from.</param>
        /// <param name="sRegex">The regular expression, for example .*Test.* removes every variable that has Test in it.</param>
        public static void DeleteVarRegex(uint oObject, string sRegex)
        {
            Core.NWNX.ObjectPlugin.DeleteVarRegex(oObject, sRegex);
        }

        /// Get if vPosition is inside oTrigger's geometry.
        /// @note The Z value of vPosition is ignored.
        /// <param name="oTrigger">The trigger.</param>
        /// <param name="vPosition">The position.</param>
        /// <returns>TRUE if vPosition is inside oTrigger's geometry.</returns>
        public static bool GetPositionIsInTrigger(uint oTrigger, Vector3 vPosition)
        {
            return Core.NWNX.ObjectPlugin.GetPositionIsInTrigger(oTrigger, vPosition) == 1;
        }

        /// Gets the given object's internal type (NWNX_OBJECT_TYPE_INTERNAL_*).
        /// <param name="oObject">The object.</param>
        /// <returns>The object's type (NWNX_OBJECT_TYPE_INTERNAL_*)</returns>
        public static ObjectInternalType GetInternalObjectType(uint oObject)
        {
            return (ObjectInternalType)Core.NWNX.ObjectPlugin.GetInternalObjectType(oObject);
        }

        /// Have oObject acquire oItem.
        /// @note Useful to give deserialized items to an object, may not work if oItem is already possessed by an object.
        /// <param name="oObject">The object receiving oItem, must be a Creature, Placeable, Store or Item</param>
        /// <param name="oItem">The item.</param>
        /// <returns>TRUE on success.</returns>
        public static bool AcquireItem(uint oObject, uint oItem)
        {
            return Core.NWNX.ObjectPlugin.AcquireItem(oObject, oItem) == 1;
        }

        /// Clear all spell effects oObject has applied to others.
        /// <param name="oObject">The object that applied the spell effects.</param>
        public static void ClearSpellEffectsOnOthers(uint oObject)
        {
            Core.NWNX.ObjectPlugin.ClearSpellEffectsOnOthers(oObject);
        }

        /// Peek at the UUID of oObject without assigning one if it does not have one.
        /// <param name="oObject">The object</param>
        /// <returns>The UUID or "" when the object does not have or cannot have an UUID</returns>
        public static string PeekUUID(uint oObject)
        {
            return Core.NWNX.ObjectPlugin.PeekUUID(oObject);
        }

        /// Get if oDoor has a visible model.
        /// <param name="oDoor">The door</param>
        /// <returns>TRUE if oDoor has a visible model</returns>
        public static bool GetDoorHasVisibleModel(uint oDoor)
        {
            return Core.NWNX.ObjectPlugin.GetDoorHasVisibleModel(oDoor) == 1;
        }

        /// Get if oObject is destroyable.
        /// <param name="oObject">The object</param>
        /// <returns>TRUE if oObject is destroyable.</returns>
        public static bool GetIsDestroyable(uint oObject)
        {
            return Core.NWNX.ObjectPlugin.GetIsDestroyable(oObject) == 1;
        }

        /// Checks for specific spell immunity. Should only be called in spellscripts
        /// <param name="oDefender">The object defending against the spell.</param>
        /// <param name="oCaster">The object casting the spell.</param>
        /// <param name="nSpellId">The casted spell id. Default value is -1, which corresponds to the normal game behaviour.</param>
        /// <returns>-1 if defender has no immunity, 2 if the defender is immune</returns>
        public static int DoSpellImmunity(uint oDefender, uint oCaster, SpellType nSpellId = SpellType.AllSpells)
        {
            return Core.NWNX.ObjectPlugin.DoSpellImmunity(oDefender, oCaster, (int)nSpellId);
        }

        /// Checks for spell school/level immunities and mantles. Should only be called in spellscripts
        /// <param name="oDefender">The object defending against the spell.</param>
        /// <param name="oCaster">The object casting the spell.</param>
        /// <param name="nSpellId">The casted spell id. Default value is -1, which corresponds to the normal game behaviour.</param>
        /// <param name="nSpellLevel">The level of the casted spell. Default value is -1, which corresponds to the normal game behaviour.</param>
        /// <param name="nSpellSchool">The school of the casted spell (SPELL_SCHOOL_* constant). Default value is -1, which corresponds to the normal game behaviour.</param>
        /// <returns>-1 defender no immunity. 2 if immune. 3 if immune, but the immunity has a limit (example: mantles)</returns>
        public static int DoSpellLevelAbsorption(uint oDefender, uint oCaster, SpellType nSpellId = SpellType.AllSpells, int nSpellLevel = -1, SpellSchoolType nSpellSchool = SpellSchoolType.Invalid)
        {
            return Core.NWNX.ObjectPlugin.DoSpellLevelAbsorption(oDefender, oCaster, (int)nSpellId, nSpellLevel, (int)nSpellSchool);
        }
        /// Sets if a placeable has an inventory.
        /// <param name="obj">The placeable.</param>
        /// <param name="bHasInventory">TRUE/FALSE</param>
        /// @note Only works on placeables.
        public static void SetHasInventory(uint obj, bool bHasInventory)
        {
            Core.NWNX.ObjectPlugin.SetHasInventory(obj, bHasInventory ? 1 : 0);
        }

        /// Get the current animation of oObject
        /// @note The returned value will be an engine animation constant, not a NWScript ANIMATION_ constant.
        /// See: https://github.com/nwnxee/unified/blob/master/NWNXLib/API/Constants/Animation.hpp
        /// <param name="oObject">The object</param>
        /// <returns>-1 on error or the engine animation constant</returns>
        public static int GetCurrentAnimation(uint oObject)
        {
            return Core.NWNX.ObjectPlugin.GetCurrentAnimation(oObject);
        }

        /// Gets the AI level of an object.
        /// <param name="oObject">The object.</param>
        /// <returns>The AI level (AI_LEVEL_* -1 to 4).</returns>
        public static AILevelType GetAILevel(uint oObject)
        {
            return (AILevelType)Core.NWNX.ObjectPlugin.GetAILevel(oObject);
        }

        /// Sets the AI level of an object.
        /// <param name="oObject">The object.</param>
        /// <param name="nLevel">The level to set (AI_LEVEL_* -1 to 4).</param>
        public static void SetAILevel(uint oObject, AILevelType nLevel)
        {
            Core.NWNX.ObjectPlugin.SetAILevel(oObject, (int)nLevel);
        }

        /// Retrieves the Map Note (AKA Map Pin) from a waypoint - Returns even if currently disabled.
        /// <param name="oObject">The Waypoint object</param>
        /// <param name="nID">The Language ID (default English)</param>
        /// <param name="nGender">0 = Male, 1 = Female</param>
        public static string GetMapNote(uint oObject, PlayerLanguageType nID = 0, GenderType nGender = 0)
        {
            return Core.NWNX.ObjectPlugin.GetMapNote(oObject, (int)nID, (int)nGender);
        }

        /// Sets a Map Note (AKA Map Pin) to any waypoint, even if no previous map note. Only updates for clients on area-load. Use SetMapPinEnabled() as required.
        /// <param name="oObject">The Waypoint object</param>
        /// <param name="sMapNote">The contents to set as the Map Note.</param>
        /// <param name="nID">The Language ID (default English)</param>
        /// <param name="nGender">0 = Male, 1 = Female</param>
        public static void SetMapNote(uint oObject, string sMapNote, PlayerLanguageType nID = 0, GenderType nGender = 0)
        {
            Core.NWNX.ObjectPlugin.SetMapNote(oObject, sMapNote, (int)nID, (int)nGender);
        }

        /// Gets the last spell cast feat of oObject.
        /// @note Should be called in a spell script.
        /// <param name="oObject">The object.</param>
        /// <returns>The feat ID, or 65535 when not cast by a feat, or -1 on error.</returns>
        public static FeatType GetLastSpellCastFeat(uint oObject)
        {
            return (FeatType)Core.NWNX.ObjectPlugin.GetLastSpellCastFeat(oObject);
        }

        /// Sets the last object that triggered door or placeable trap.
        /// @note Should be retrieved with GetEnteringObject.
        /// <param name="oObject">Door or placeable object</param>
        /// <param name="oLast">Object that last triggered trap.</param>
        public static void SetLastTriggered(uint oObject, uint oLast)
        {
            Core.NWNX.ObjectPlugin.SetLastTriggered(oObject, oLast);
        }

        /// Gets the remaining duration of the AoE object.
        /// <param name="oAoE">The AreaOfEffect object.</param>
        /// <returns>The remaining duration, in seconds, or the zero on failure.</returns>
        public static float GetAoEObjectDurationRemaining(uint oAoE)
        {
            return Core.NWNX.ObjectPlugin.GetAoEObjectDurationRemaining(oAoE);
        }

        /// Sets conversations started by oObject to be private or not.
        /// @note ActionStartConversation()'s bPrivateConversation parameter will overwrite this flag.
        /// <param name="oObject">The object.</param>
        /// <param name="bPrivate">TRUE/FALSE.</param>
        public static void SetConversationPrivate(uint oObject, bool bPrivate)
        {
            Core.NWNX.ObjectPlugin.SetConversationPrivate(oObject, bPrivate ? 1 : 0);
        }

        /// Sets the radius of a circle AoE object.
        /// <param name="oAoE">The AreaOfEffect object.</param>
        /// <param name="fRadius">The radius, must be bigger than 0.0f.</param>
        public static void SetAoEObjectRadius(uint oAoE, float fRadius)
        {
            Core.NWNX.ObjectPlugin.SetAoEObjectRadius(oAoE, fRadius);
        }

        /// Gets the radius of a circle AoE object.
        /// <param name="oAoE">The AreaOfEffect object.</param>
        /// <returns>The radius or 0.0f on error</returns>
        public static float GetAoEObjectRadius(uint oAoE)
        {
            return Core.NWNX.ObjectPlugin.GetAoEObjectRadius(oAoE);
        }

        /// Gets whether the last spell cast of oObject was spontaneous.
        /// @note Should be called in a spell script.
        /// <param name="oObject">The object.</param>
        /// <returns>true if the last spell was cast spontaneously</returns>
        public static bool GetLastSpellCastSpontaneous(uint oObject)
        {
            return Core.NWNX.ObjectPlugin.GetLastSpellCastSpontaneous(oObject) == 1;
        }

        /// Gets the last spell cast domain level.
        /// @note Should be called in a spell script.
        /// <param name="oObject">The object.</param>
        /// <returns>Domain level of the cast spell, 0 if not a domain spell</returns>
        public static int GetLastSpellCastDomainLevel(uint oObject)
        {
            return Core.NWNX.ObjectPlugin.GetLastSpellCastDomainLevel(oObject);
        }

        /// Force the given object to carry the given UUID. Any other object currently owning the UUID is stripped of it.
        /// <param name="oObject">The object</param>
        /// <param name="sUUID">The UUID to force</param>
        public static void ForceAssignUUID(uint oObject, string sUUID)
        {
            Core.NWNX.ObjectPlugin.ForceAssignUUID(oObject, sUUID);
        }

        /// Returns how many items are in oObject's inventory.
        /// <param name="oObject">A creature, placeable, item or store.</param>
        /// <returns>Returns a count of how many items are in oObject's inventory.</returns>
        public static int GetInventoryItemCount(uint oObject)
        {
            return Core.NWNX.ObjectPlugin.GetInventoryItemCount(oObject);
        }

        /// Override the projectile visual effect of ranged/throwing weapons and spells.
        /// <param name="oCreature">The creature.</param>
        /// <param name="nProjectileType">A @ref projectile_types "NWNX_OBJECT_SPELL_PROJECTILE_TYPE_*" constant or -1 to remove the override.</param>
        /// <param name="nProjectilePathType">A "PROJECTILE_PATH_TYPE_*" constant or -1 to ignore.</param>
        /// <param name="nSpellID">A "SPELL_*" constant. -1 to ignore.</param>
        /// <param name="bPersist">Whether the override should persist to the .bic file (for PCs).</param>
        /// @note Persistence is enabled after a server reset by the first use of this function. Recommended to trigger on a dummy target OnModuleLoad to enable persistence.
        /// This will override all spell projectile VFX from oCreature until the override is removed.
        public static void OverrideSpellProjectileVFX(uint oCreature, int nProjectileType = -1, ProjectilePathType nProjectilePathType = ProjectilePathType.Invalid, SpellType nSpellID = SpellType.AllSpells, bool bPersist = false)
        {
            Core.NWNX.ObjectPlugin.OverrideSpellProjectileVFX(oCreature, nProjectileType, (int)nProjectilePathType, (int)nSpellID, bPersist ? 1 : 0);
        }
        /// Returns true if the last spell was cast instantly. This function should only be called in a spell script.
        /// @note To initialize the hooks used by this function it is recommended to call this function once in your module load script.
        /// <returns>true if the last spell was instant.</returns>
        public static bool GetLastSpellInstant()
        {
            return Core.NWNX.ObjectPlugin.GetLastSpellInstant() == 1;
        }

        /// Sets the creator of a trap on door, placeable, or trigger. Also changes trap Faction to that of the new Creator.
        /// @note Triggers (ground traps) will instantly update colour (Green/Red). Placeable/doors will not change if client has already seen them.
        /// <param name="oObject">Door, placeable or trigger (trap) object</param>
        /// <param name="oCreator">The new creator of the trap. Any non-creature creator will assign OBJECT_INVALID (similar to toolset-laid traps)</param>
        public static void SetTrapCreator(uint oObject, uint oCreator)
        {
            Core.NWNX.ObjectPlugin.SetTrapCreator(oObject, oCreator);
        }

        /// Return the name of the object for nLanguage.
        /// <param name="oObject">an object</param>
        /// <param name="nLanguage">A PLAYER_LANGUAGE constant.</param>
        /// <param name="nGender">Gender to use, 0 or 1.</param>
        /// <returns>The localized string.</returns>
        public static string GetLocalizedName(uint oObject, PlayerLanguageType nLanguage, GenderType nGender = 0)
        {
            return Core.NWNX.ObjectPlugin.GetLocalizedName(oObject, (int)nLanguage, (int)nGender);
        }

        /// Set the name of the object as set in the toolset for nLanguage.
        /// @note You may have to SetName(oObject, "") for the translated string to show.
        /// <param name="oObject">an object</param>
        /// <param name="sName">New value to set</param>
        /// <param name="nLanguage">A PLAYER_LANGUAGE constant.</param>
        /// <param name="nGender">Gender to use, 0 or 1.</param>
        public static void SetLocalizedName(uint oObject, string sName, PlayerLanguageType nLanguage, GenderType nGender = 0)
        {
            Core.NWNX.ObjectPlugin.SetLocalizedName(oObject, sName, (int)nLanguage, (int)nGender);
        }

    }
}
