using System.Numerics;
using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWScript.Enum;
using NWN.Xenomech.Core.NWScript.Enum.Item.Property;
using NWN.Xenomech.Core.NWScript.Enum.VisualEffect;
using Alignment = NWN.Xenomech.Core.NWScript.Enum.Alignment;
using DamageType = NWN.Xenomech.Core.NWScript.Enum.DamageType;
using RacialType = NWN.Xenomech.Core.NWScript.Enum.RacialType;
using SpellSchool = NWN.Xenomech.Core.NWScript.Enum.SpellSchool;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {/// <summary>
     ///   Returns the string tag set for the provided effect.
     ///   - If no tag has been set, returns an empty string.
     /// </summary>
        public static string GetEffectTag(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(849);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Tags the effect with the provided string.
        ///   - Any other tags in the link will be overwritten.
        /// </summary>
        public static Effect TagEffect(Effect eEffect, string sNewTag)
        {
            NWNXPInvoke.StackPushString(sNewTag);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(850);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Returns the caster level of the creature who created the effect.
        ///   - If not created by a creature, returns 0.
        ///   - If created by a spell-like ability, returns 0.
        /// </summary>
        public static int GetEffectCasterLevel(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(851);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the total duration of the effect in seconds.
        ///   - Returns 0 if the duration type of the effect is not DURATION_TYPE_TEMPORARY.
        /// </summary>
        public static int GetEffectDuration(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(852);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the remaining duration of the effect in seconds.
        ///   - Returns 0 if the duration type of the effect is not DURATION_TYPE_TEMPORARY.
        /// </summary>
        public static int GetEffectDurationRemaining(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(853);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns an effect that when applied will paralyze the target's legs, rendering
        ///   them unable to walk but otherwise unpenalized. This effect cannot be resisted.
        /// </summary>
        public static Effect EffectCutsceneImmobilize()
        {
            NWNXPInvoke.CallBuiltIn(767);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Creates a cutscene ghost effect, this will allow creatures
        ///   to pathfind through other creatures without bumping into them
        ///   for the duration of the effect.
        /// </summary>
        public static Effect EffectCutsceneGhost()
        {
            NWNXPInvoke.CallBuiltIn(757);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Returns TRUE if the item is cursed and cannot be dropped
        /// </summary>
        public static bool GetItemCursedFlag(uint oItem)
        {
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(744);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   When cursed, items cannot be dropped
        /// </summary>
        public static void SetItemCursedFlag(uint oItem, bool nCursed)
        {
            NWNXPInvoke.StackPushInteger(nCursed ? 1 : 0);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(745);
        }

        /// <summary>
        ///   Get the possessor of oItem
        ///     bReturnBags: If TRUE will potentially return a bag container item the item is in, instead of
        ///         the object holding the bag. Make sure to check the returning item object type with this flag.
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetItemPossessor(uint oItem, bool bReturnBags = false)
        {
            NWNXPInvoke.StackPushInteger(bReturnBags ? 1 : 0);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(29);
            return NWNXPInvoke.StackPopObject();
        }
        /// <summary>
        ///   Get the object possessed by oCreature with the tag sItemTag
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetItemPossessedBy(uint oCreature, string sItemTag)
        {
            NWNXPInvoke.StackPushString(sItemTag);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(30);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Create an item with the template sItemTemplate in oTarget's inventory.
        ///   - nStackSize: This is the stack size of the item to be created
        ///   - sNewTag: If this string is not empty, it will replace the default tag from the template
        ///   * Return value: The object that has been created. On error, this returns
        ///   OBJECT_INVALID.
        ///   If the item created was merged into an existing stack of similar items,
        ///   the function will return the merged stack object. If the merged stack
        ///   overflowed, the function will return the overflowed stack that was created.
        /// </summary>
        public static uint CreateItemOnObject(string sResRef, uint oTarget = OBJECT_INVALID, int nStackSize = 1, string sNewTag = "")
        {
            NWNXPInvoke.StackPushString(sNewTag);
            NWNXPInvoke.StackPushInteger(nStackSize);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.CallBuiltIn(31);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Equip oItem into nInventorySlot.
        ///   - nInventorySlot: INVENTORY_SLOT_*
        ///   * No return value, but if an error occurs the log file will contain
        ///   "ActionEquipItem failed."
        ///   Note:
        ///   If the creature already has an item equipped in the slot specified, it will be
        ///   unequipped automatically by the call to ActionEquipItem.
        ///   In order for ActionEquipItem to succeed the creature must be able to equip the
        ///   item oItem normally. This means that:
        ///   1) The item is in the creature's inventory.
        ///   2) The item must already be identified (if magical).
        ///   3) The creature has the level required to equip the item (if magical and ILR is on).
        ///   4) The creature possesses the required feats to equip the item (such as weapon proficiencies).
        /// </summary>
        public static void ActionEquipItem(uint oItem, InventorySlot nInventorySlot)
        {
            NWNXPInvoke.StackPushInteger((int)nInventorySlot);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(32);
        }

        /// <summary>
        ///   Unequip oItem from whatever slot it is currently in.
        /// </summary>
        public static void ActionUnequipItem(uint oItem)
        {
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(33);
        }

        /// <summary>
        ///   Pick up oItem from the ground.
        ///   * No return value, but if an error occurs the log file will contain
        ///   "ActionPickUpItem failed."
        /// </summary>
        public static void ActionPickUpItem(uint oItem)
        {
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(34);
        }

        /// <summary>
        ///   Put down oItem on the ground.
        ///   * No return value, but if an error occurs the log file will contain
        ///   "ActionPutDownItem failed."
        /// </summary>
        public static void ActionPutDownItem(uint oItem)
        {
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(35);
        }

        /// <summary>
        ///   Give oItem to oGiveTo
        ///   If oItem is not a valid item, or oGiveTo is not a valid object, nothing will
        ///   happen.
        /// </summary>
        public static void ActionGiveItem(uint oItem, uint oGiveTo)
        {
            NWNXPInvoke.StackPushObject(oGiveTo);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(135);
        }

        /// <summary>
        ///   Take oItem from oTakeFrom
        ///   If oItem is not a valid item, or oTakeFrom is not a valid object, nothing
        ///   will happen.
        /// </summary>
        public static void ActionTakeItem(uint oItem, uint oTakeFrom)
        {
            NWNXPInvoke.StackPushObject(oTakeFrom);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(136);
        }

        /// <summary>
        ///   Create a Death effect
        ///   - nSpectacularDeath: if this is TRUE, the creature to which this effect is
        ///   applied will die in an extraordinary fashion
        ///   - nDisplayFeedback
        /// </summary>
        public static Effect EffectDeath(bool nSpectacularDeath = false, bool nDisplayFeedback = true)
        {
            NWNXPInvoke.StackPushInteger(nDisplayFeedback ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nSpectacularDeath ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(133);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Knockdown effect
        ///   This effect knocks creatures off their feet, they will sit until the effect
        ///   is removed. This should be applied as a temporary effect with a 3 second
        ///   duration minimum (1 second to fall, 1 second sitting, 1 second to get up).
        /// </summary>
        public static Effect EffectKnockdown()
        {
            NWNXPInvoke.CallBuiltIn(134);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }
        /// <summary>
        ///   Create a Curse effect.
        ///   - nStrMod: strength modifier
        ///   - nDexMod: dexterity modifier
        ///   - nConMod: constitution modifier
        ///   - nIntMod: intelligence modifier
        ///   - nWisMod: wisdom modifier
        ///   - nChaMod: charisma modifier
        /// </summary>
        public static Effect EffectCurse(int nStrMod = 1, int nDexMod = 1, int nConMod = 1, int nIntMod = 1, int nWisMod = 1, int nChaMod = 1)
        {
            NWNXPInvoke.StackPushInteger(nChaMod);
            NWNXPInvoke.StackPushInteger(nWisMod);
            NWNXPInvoke.StackPushInteger(nIntMod);
            NWNXPInvoke.StackPushInteger(nConMod);
            NWNXPInvoke.StackPushInteger(nDexMod);
            NWNXPInvoke.StackPushInteger(nStrMod);
            NWNXPInvoke.CallBuiltIn(138);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Entangle effect.
        /// </summary>
        public static Effect EffectEntangle()
        {
            NWNXPInvoke.CallBuiltIn(130);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Saving Throw Increase effect.
        /// </summary>
        public static Effect EffectSavingThrowIncrease(int nSave, int nValue, SavingThrowType nSaveType = SavingThrowType.All)
        {
            NWNXPInvoke.StackPushInteger((int)nSaveType);
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushInteger(nSave);
            NWNXPInvoke.CallBuiltIn(117);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Attack Increase effect.
        /// </summary>
        public static Effect EffectAccuracyIncrease(int nBonus, AttackBonus nModifierType = AttackBonus.Misc)
        {
            NWNXPInvoke.StackPushInteger((int)nModifierType);
            NWNXPInvoke.StackPushInteger(nBonus);
            NWNXPInvoke.CallBuiltIn(118);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage Reduction effect.
        /// </summary>
        public static Effect EffectDamageReduction(int nAmount, DamagePower nDamagePower, int nLimit = 0, bool bRangedOnly = false)
        {
            NWNXPInvoke.StackPushInteger(bRangedOnly ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nLimit);
            NWNXPInvoke.StackPushInteger((int)nDamagePower);
            NWNXPInvoke.StackPushInteger(nAmount);
            NWNXPInvoke.CallBuiltIn(119);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage Increase effect.
        /// </summary>
        public static Effect EffectDamageIncrease(int nBonus, DamageType nDamageType = DamageType.Force)
        {
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.StackPushInteger(nBonus);
            NWNXPInvoke.CallBuiltIn(120);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Set the subtype of eEffect to Magical.
        /// </summary>
        public static Effect MagicalEffect(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(112);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Set the subtype of eEffect to Supernatural.
        /// </summary>
        public static Effect SupernaturalEffect(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(113);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Set the subtype of eEffect to Extraordinary.
        /// </summary>
        public static Effect ExtraordinaryEffect(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(114);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an AC Increase effect.
        /// </summary>
        public static Effect EffectACIncrease(int nValue, ArmorClassModiferType nModifyType = ArmorClassModiferType.Dodge, AC nDamageType = AC.VsDamageTypeAll)
        {
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.StackPushInteger((int)nModifyType);
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.CallBuiltIn(115);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Get the first in-game effect on oCreature.
        /// </summary>
        public static Effect GetFirstEffect(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(85);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Get the next in-game effect on oCreature.
        /// </summary>
        public static Effect GetNextEffect(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(86);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }
        /// <summary>
        ///   Remove eEffect from oCreature.
        /// </summary>
        public static void RemoveEffect(uint oCreature, Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(87);
        }

        /// <summary>
        ///   Returns TRUE if eEffect is a valid effect. The effect must have been applied to an object.
        /// </summary>
        public static bool GetIsEffectValid(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(88);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   Get the duration type (DURATION_TYPE_*) of eEffect.
        /// </summary>
        public static int GetEffectDurationType(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(89);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the subtype (SUBTYPE_*) of eEffect.
        /// </summary>
        public static int GetEffectSubType(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(90);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the object that created eEffect.
        /// </summary>
        public static uint GetEffectCreator(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(91);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Create a Heal effect.
        /// </summary>
        public static Effect EffectHeal(int nDamageToHeal)
        {
            NWNXPInvoke.StackPushInteger(nDamageToHeal);
            NWNXPInvoke.CallBuiltIn(78);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage effect.
        /// </summary>
        public static Effect EffectDamage(int nDamageAmount, DamageType nDamageType = DamageType.Force, DamagePower nDamagePower = DamagePower.Normal)
        {
            NWNXPInvoke.StackPushInteger((int)nDamagePower);
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.StackPushInteger(nDamageAmount);
            NWNXPInvoke.CallBuiltIn(79);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Ability Increase effect.
        /// </summary>
        public static Effect EffectAbilityIncrease(AbilityType nAbilityToIncrease, int nModifyBy)
        {
            NWNXPInvoke.StackPushInteger(nModifyBy);
            NWNXPInvoke.StackPushInteger((int)nAbilityToIncrease);
            NWNXPInvoke.CallBuiltIn(80);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage Resistance effect.
        /// </summary>
        public static Effect EffectDamageResistance(DamageType nDamageType, int nAmount, int nLimit = 0, bool bRangedOnly = false)
        {
            NWNXPInvoke.StackPushInteger(bRangedOnly ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nLimit);
            NWNXPInvoke.StackPushInteger(nAmount);
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.CallBuiltIn(81);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Resurrection effect.
        /// </summary>
        public static Effect EffectResurrection()
        {
            NWNXPInvoke.CallBuiltIn(82);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Summon Creature effect.
        /// </summary>
        public static Effect EffectSummonCreature(string sCreatureResref, VisualEffect nVisualEffectId = VisualEffect.Vfx_Com_Sparks_Parry, float fDelaySeconds = 0.0f, bool nUseAppearAnimation = false)
        {
            NWNXPInvoke.StackPushInteger(nUseAppearAnimation ? 1 : 0);
            NWNXPInvoke.StackPushFloat(fDelaySeconds);
            NWNXPInvoke.StackPushInteger((int)nVisualEffectId);
            NWNXPInvoke.StackPushString(sCreatureResref);
            NWNXPInvoke.CallBuiltIn(83);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Returns an effect of type EFFECT_TYPE_ETHEREAL.
        /// </summary>
        public static Effect EffectEthereal()
        {
            NWNXPInvoke.CallBuiltIn(711);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Creates an effect that inhibits spells.
        /// </summary>
        public static Effect EffectSpellFailure(int nPercent = 100, SpellSchool nSpellSchool = SpellSchool.General)
        {
            NWNXPInvoke.StackPushInteger((int)nSpellSchool);
            NWNXPInvoke.StackPushInteger(nPercent);
            NWNXPInvoke.CallBuiltIn(690);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Returns an effect that is guaranteed to dominate a creature.
        /// </summary>
        public static Effect EffectCutsceneDominated()
        {
            NWNXPInvoke.CallBuiltIn(604);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Returns an effect that will petrify the target.
        /// </summary>
        public static Effect EffectPetrify()
        {
            NWNXPInvoke.CallBuiltIn(583);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Returns an effect that is guaranteed to paralyze a creature.
        /// </summary>
        public static Effect EffectCutsceneParalyze()
        {
            NWNXPInvoke.CallBuiltIn(585);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }
        /// <summary>
        ///   Create a Turn Resistance Decrease effect.
        /// </summary>
        public static Effect EffectTurnResistanceDecrease(int nHitDice)
        {
            NWNXPInvoke.StackPushInteger(nHitDice);
            NWNXPInvoke.CallBuiltIn(552);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Turn Resistance Increase effect.
        /// </summary>
        public static Effect EffectTurnResistanceIncrease(int nHitDice)
        {
            NWNXPInvoke.StackPushInteger(nHitDice);
            NWNXPInvoke.CallBuiltIn(553);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Swarm effect.
        /// </summary>
        public static Effect EffectSwarm(int nLooping, string sCreatureTemplate1, string sCreatureTemplate2 = "",
            string sCreatureTemplate3 = "", string sCreatureTemplate4 = "")
        {
            NWNXPInvoke.StackPushString(sCreatureTemplate4);
            NWNXPInvoke.StackPushString(sCreatureTemplate3);
            NWNXPInvoke.StackPushString(sCreatureTemplate2);
            NWNXPInvoke.StackPushString(sCreatureTemplate1);
            NWNXPInvoke.StackPushInteger(nLooping);
            NWNXPInvoke.CallBuiltIn(510);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Disappear/Appear effect.
        /// </summary>
        public static Effect EffectDisappearAppear(Location lLocation, int nAnimation = 1)
        {
            NWNXPInvoke.StackPushInteger(nAnimation);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.CallBuiltIn(480);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Disappear effect.
        /// </summary>
        public static Effect EffectDisappear(int nAnimation = 1)
        {
            NWNXPInvoke.StackPushInteger(nAnimation);
            NWNXPInvoke.CallBuiltIn(481);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Appear effect.
        /// </summary>
        public static Effect EffectAppear(int nAnimation = 1)
        {
            NWNXPInvoke.StackPushInteger(nAnimation);
            NWNXPInvoke.CallBuiltIn(482);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Modify Attacks effect.
        /// </summary>
        public static Effect EffectModifyAttacks(int nAttacks)
        {
            NWNXPInvoke.StackPushInteger(nAttacks);
            NWNXPInvoke.CallBuiltIn(485);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage Shield effect.
        /// </summary>
        public static Effect EffectDamageShield(int nDamageAmount, DamageBonus nRandomAmount, DamageType nDamageType)
        {
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.StackPushInteger((int)nRandomAmount);
            NWNXPInvoke.StackPushInteger(nDamageAmount);
            NWNXPInvoke.CallBuiltIn(487);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Miss Chance effect.
        /// </summary>
        public static Effect EffectMissChance(int nPercentage, MissChanceType nMissChanceType = MissChanceType.Normal)
        {
            NWNXPInvoke.StackPushInteger((int)nMissChanceType);
            NWNXPInvoke.StackPushInteger(nPercentage);
            NWNXPInvoke.CallBuiltIn(477);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Spell Level Absorption effect.
        /// </summary>
        public static Effect EffectSpellLevelAbsorption(int nMaxSpellLevelAbsorbed, int nTotalSpellLevelsAbsorbed = 0,
            SpellSchool nSpellSchool = SpellSchool.General)
        {
            NWNXPInvoke.StackPushInteger((int)nSpellSchool);
            NWNXPInvoke.StackPushInteger(nTotalSpellLevelsAbsorbed);
            NWNXPInvoke.StackPushInteger(nMaxSpellLevelAbsorbed);
            NWNXPInvoke.CallBuiltIn(472);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Dispel Magic Best effect.
        /// </summary>
        public static Effect EffectDispelMagicBest(int nCasterLevel = USE_CREATURE_LEVEL)
        {
            NWNXPInvoke.StackPushInteger(nCasterLevel);
            NWNXPInvoke.CallBuiltIn(473);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Invisibility effect.
        /// </summary>
        public static Effect EffectInvisibility(InvisibilityType nInvisibilityType)
        {
            NWNXPInvoke.StackPushInteger((int)nInvisibilityType);
            NWNXPInvoke.CallBuiltIn(457);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Concealment effect.
        /// </summary>
        public static Effect EffectConcealment(int nPercentage, MissChanceType nMissType = MissChanceType.Normal)
        {
            NWNXPInvoke.StackPushInteger((int)nMissType);
            NWNXPInvoke.StackPushInteger(nPercentage);
            NWNXPInvoke.CallBuiltIn(458);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Darkness effect.
        /// </summary>
        public static Effect EffectDarkness()
        {
            NWNXPInvoke.CallBuiltIn(459);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Dispel Magic All effect.
        /// </summary>
        public static Effect EffectDispelMagicAll(int nCasterLevel = USE_CREATURE_LEVEL)
        {
            NWNXPInvoke.StackPushInteger(nCasterLevel);
            NWNXPInvoke.CallBuiltIn(460);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Ultravision effect.
        /// </summary>
        public static Effect EffectUltravision()
        {
            NWNXPInvoke.CallBuiltIn(461);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Negative Level effect.
        /// </summary>
        public static Effect EffectNegativeLevel(int nNumLevels, bool bHPBonus = false)
        {
            NWNXPInvoke.StackPushInteger(bHPBonus ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nNumLevels);
            NWNXPInvoke.CallBuiltIn(462);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }
        /// <summary>
        ///   Create a Polymorph effect.
        /// </summary>
        public static Effect EffectPolymorph(int nPolymorphSelection, bool nLocked = false)
        {
            NWNXPInvoke.StackPushInteger(nLocked ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nPolymorphSelection);
            NWNXPInvoke.CallBuiltIn(463);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Sanctuary effect.
        /// </summary>
        public static Effect EffectSanctuary(int nDifficultyClass)
        {
            NWNXPInvoke.StackPushInteger(nDifficultyClass);
            NWNXPInvoke.CallBuiltIn(464);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a True Seeing effect.
        /// </summary>
        public static Effect EffectTrueSeeing()
        {
            NWNXPInvoke.CallBuiltIn(465);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a See Invisible effect.
        /// </summary>
        public static Effect EffectSeeInvisible()
        {
            NWNXPInvoke.CallBuiltIn(466);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Time Stop effect.
        /// </summary>
        public static Effect EffectTimeStop()
        {
            NWNXPInvoke.CallBuiltIn(467);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Blindness effect.
        /// </summary>
        public static Effect EffectBlindness()
        {
            NWNXPInvoke.CallBuiltIn(468);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Ability Decrease effect.
        /// </summary>
        public static Effect EffectAbilityDecrease(AbilityType nAbility, int nModifyBy)
        {
            NWNXPInvoke.StackPushInteger(nModifyBy);
            NWNXPInvoke.StackPushInteger((int)nAbility);
            NWNXPInvoke.CallBuiltIn(446);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Attack Decrease effect.
        /// </summary>
        public static Effect EffectAccuracyDecrease(int nPenalty, AttackBonus nModifierType = AttackBonus.Misc)
        {
            NWNXPInvoke.StackPushInteger((int)nModifierType);
            NWNXPInvoke.StackPushInteger(nPenalty);
            NWNXPInvoke.CallBuiltIn(447);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage Decrease effect.
        /// </summary>
        public static Effect EffectDamageDecrease(int nPenalty, DamageType nDamageType = DamageType.Force)
        {
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.StackPushInteger(nPenalty);
            NWNXPInvoke.CallBuiltIn(448);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Damage Immunity Decrease effect.
        /// </summary>
        public static Effect EffectDamageImmunityDecrease(int nDamageType, int nPercentImmunity)
        {
            NWNXPInvoke.StackPushInteger(nPercentImmunity);
            NWNXPInvoke.StackPushInteger(nDamageType);
            NWNXPInvoke.CallBuiltIn(449);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an AC Decrease effect.
        /// </summary>
        public static Effect EffectACDecrease(int nValue,
            ArmorClassModiferType nModifyType = ArmorClassModiferType.Dodge,
            AC nDamageType = AC.VsDamageTypeAll)
        {
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.StackPushInteger((int)nModifyType);
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.CallBuiltIn(450);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Movement Speed Decrease effect.
        /// </summary>
        public static Effect EffectMovementSpeedDecrease(int nPercentChange)
        {
            NWNXPInvoke.StackPushInteger(nPercentChange);
            NWNXPInvoke.CallBuiltIn(451);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Saving Throw Decrease effect.
        /// </summary>
        public static Effect EffectSavingThrowDecrease(int nSave, int nValue,
            SavingThrowType nSaveType = SavingThrowType.All)
        {
            NWNXPInvoke.StackPushInteger((int)nSaveType);
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushInteger(nSave);
            NWNXPInvoke.CallBuiltIn(452);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Skill Decrease effect.
        /// </summary>
        public static Effect EffectSkillDecrease(int nSkill, int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushInteger(nSkill);
            NWNXPInvoke.CallBuiltIn(453);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Spell Resistance Decrease effect.
        /// </summary>
        public static Effect EffectSpellResistanceDecrease(int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.CallBuiltIn(454);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Activate oItem.
        /// </summary>
        public static Event EventActivateItem(uint oItem, Location lTarget, uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTarget);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(424);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Event);
        }

        /// <summary>
        ///   Create a Hit Point Change When Dying effect.
        /// </summary>
        public static Effect EffectHitPointChangeWhenDying(float fHitPointChangePerRound)
        {
            NWNXPInvoke.StackPushFloat(fHitPointChangePerRound);
            NWNXPInvoke.CallBuiltIn(387);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Turned effect.
        /// </summary>
        public static Effect EffectTurned()
        {
            NWNXPInvoke.CallBuiltIn(379);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Set eEffect to be versus a specific alignment.
        /// </summary>
        public static Effect VersusAlignmentEffect(Effect eEffect,
            Alignment nLawChaos = Alignment.All,
            Alignment nGoodEvil = Alignment.All)
        {
            NWNXPInvoke.StackPushInteger((int)nGoodEvil);
            NWNXPInvoke.StackPushInteger((int)nLawChaos);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(355);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Set eEffect to be versus nRacialType.
        ///   - eEffect
        ///   - nRacialType: RACIAL_TYPE_*
        /// </summary>
        public static Effect VersusRacialTypeEffect(Effect eEffect, RacialType nRacialType)
        {
            NWNXPInvoke.StackPushInteger((int)nRacialType);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(356);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Set eEffect to be versus traps.
        /// </summary>
        public static Effect VersusTrapEffect(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(357);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Skill Increase effect.
        /// </summary>
        public static Effect EffectSkillIncrease(NWNSkillType nSkill, int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushInteger((int)nSkill);
            NWNXPInvoke.CallBuiltIn(351);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Temporary Hitpoints effect.
        /// </summary>
        public static Effect EffectTemporaryHitpoints(int nHitPoints)
        {
            NWNXPInvoke.StackPushInteger(nHitPoints);
            NWNXPInvoke.CallBuiltIn(314);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Creates a conversation event.
        /// </summary>
        public static Event EventConversation()
        {
            NWNXPInvoke.CallBuiltIn(295);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Event);
        }

        /// <summary>
        ///   Creates a Damage Immunity Increase effect.
        /// </summary>
        public static Effect EffectDamageImmunityIncrease(DamageType nDamageType, int nPercentImmunity)
        {
            NWNXPInvoke.StackPushInteger(nPercentImmunity);
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.CallBuiltIn(275);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create an Immunity effect.
        /// </summary>
        public static Effect EffectImmunity(ImmunityType nImmunityType)
        {
            NWNXPInvoke.StackPushInteger((int)nImmunityType);
            NWNXPInvoke.CallBuiltIn(273);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Haste effect.
        /// </summary>
        public static Effect EffectHaste()
        {
            NWNXPInvoke.CallBuiltIn(270);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Slow effect.
        /// </summary>
        public static Effect EffectSlow()
        {
            NWNXPInvoke.CallBuiltIn(271);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Poison effect.
        /// </summary>
        public static Effect EffectPoison(Poison nPoisonType)
        {
            NWNXPInvoke.StackPushInteger((int)nPoisonType);
            NWNXPInvoke.CallBuiltIn(250);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Disease effect.
        /// </summary>
        public static Effect EffectDisease(Disease nDiseaseType)
        {
            NWNXPInvoke.StackPushInteger((int)nDiseaseType);
            NWNXPInvoke.CallBuiltIn(251);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Silence effect.
        /// </summary>
        public static Effect EffectSilence()
        {
            NWNXPInvoke.CallBuiltIn(252);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Spell Resistance Increase effect.
        /// </summary>
        public static Effect EffectSpellResistanceIncrease(int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.CallBuiltIn(212);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Create a Beam effect.
        /// </summary>
        public static Effect EffectBeam(VisualEffect nBeamVisualEffect, uint oEffector, BodyNode nBodyPart, bool bMissEffect = false)
        {
            NWNXPInvoke.StackPushInteger(bMissEffect ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nBodyPart);
            NWNXPInvoke.StackPushObject(oEffector);
            NWNXPInvoke.StackPushInteger((int)nBeamVisualEffect);
            NWNXPInvoke.CallBuiltIn(207);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Link the two supplied effects.
        /// </summary>
        public static Effect EffectLinkEffects(Effect eChildEffect, Effect eParentEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eParentEffect);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eChildEffect);
            NWNXPInvoke.CallBuiltIn(199);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }
        /// <summary>
        ///   * Create a Visual Effect that can be applied to an object.
        /// </summary>
        public static Effect EffectVisualEffect(VisualEffect visualEffectID, bool nMissEffect = false, float fScale = 1.0f, Vector3 vTranslate = new Vector3(), Vector3 vRotate = new Vector3())
        {
            NWNXPInvoke.StackPushVector(vRotate);
            NWNXPInvoke.StackPushVector(vTranslate);
            NWNXPInvoke.StackPushFloat(fScale);
            NWNXPInvoke.StackPushInteger(nMissEffect ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)visualEffectID);

            NWNXPInvoke.CallBuiltIn(180);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Apply eEffect to oTarget.
        /// </summary>
        public static void ApplyEffectToObject(DurationType nDurationType, Effect eEffect, uint oTarget, float fDuration = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fDuration);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.StackPushInteger((int)nDurationType);
            NWNXPInvoke.CallBuiltIn(220);
        }

        /// <summary>
        ///   Get the effect type (EFFECT_TYPE_*) of eEffect.
        /// </summary>
        public static EffectTypeScript GetEffectType(Effect eEffect, bool bAllTypes = false)
        {
            NWNXPInvoke.StackPushInteger(bAllTypes ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(170);
            return (EffectTypeScript)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Create an Area Of Effect effect in the area of the creature it is applied to.
        /// </summary>
        public static Effect EffectAreaOfEffect(AreaOfEffect nAreaEffect, string sOnEnterScript = "", string sHeartbeatScript = "", string sOnExitScript = "")
        {
            NWNXPInvoke.StackPushString(sOnExitScript);
            NWNXPInvoke.StackPushString(sHeartbeatScript);
            NWNXPInvoke.StackPushString(sOnEnterScript);
            NWNXPInvoke.StackPushInteger((int)nAreaEffect);
            NWNXPInvoke.CallBuiltIn(171);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        ///   Get the integer parameter of eEffect at nIndex.
        /// </summary>
        public static int GetEffectInteger(Effect eEffect, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(939);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Get the float parameter of eEffect at nIndex.
        /// </summary>
        public static float GetEffectFloat(Effect eEffect, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(940);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Get the string parameter of eEffect at nIndex.
        /// </summary>
        public static string GetEffectString(Effect eEffect, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(941);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Get the object parameter of eEffect at nIndex.
        /// </summary>
        public static uint GetEffectObject(Effect eEffect, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(942);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Get the vector parameter of eEffect at nIndex.
        /// * nIndex bounds: 0 to 1 inclusive
        /// </summary>
        public static Vector3 GetEffectVector(Effect eEffect, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(943);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Create a RunScript effect.
        /// </summary>
        public static Effect EffectRunScript(string sOnAppliedScript = "", string sOnRemovedScript = "", string sOnIntervalScript = "", float fInterval = 0.0f, string sData = "")
        {
            NWNXPInvoke.StackPushString(sData);
            NWNXPInvoke.StackPushFloat(fInterval);
            NWNXPInvoke.StackPushString(sOnIntervalScript);
            NWNXPInvoke.StackPushString(sOnRemovedScript);
            NWNXPInvoke.StackPushString(sOnAppliedScript);
            NWNXPInvoke.CallBuiltIn(955);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Get the effect that last triggered an EffectRunScript() script.
        /// </summary>
        public static Effect GetLastRunScriptEffect()
        {
            NWNXPInvoke.CallBuiltIn(956);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Hides the effect icon of eEffect.
        /// </summary>
        public static Effect HideEffectIcon(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(958);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Create an Icon effect.
        /// </summary>
        public static Effect EffectIcon(EffectIconType nIconId)
        {
            NWNXPInvoke.StackPushInteger((int)nIconId);
            NWNXPInvoke.CallBuiltIn(959);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Set the subtype of eEffect to Unyielding and return eEffect.
        /// </summary>
        public static Effect UnyieldingEffect(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(1036);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Set eEffect to ignore immunities and return eEffect.
        /// </summary>
        public static Effect IgnoreEffectImmunity(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(1037);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Create a Pacified effect.
        /// </summary>
        public static Effect EffectPacified()
        {
            NWNXPInvoke.CallBuiltIn(1089);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Get the Link ID of the given effect.
        /// </summary>
        public static string GetEffectLinkId(Effect eEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(1096);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Creates a bonus feat effect.
        /// </summary>
        public static Effect EffectBonusFeat(int nFeat)
        {
            NWNXPInvoke.StackPushInteger(nFeat);
            NWNXPInvoke.CallBuiltIn(1107);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Provides immunity to the effects of EffectTimeStop.
        /// </summary>
        public static Effect EffectTimeStopImmunity()
        {
            NWNXPInvoke.CallBuiltIn(1112);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Forces the creature to always walk.
        /// </summary>
        public static Effect EffectForceWalk()
        {
            NWNXPInvoke.CallBuiltIn(1117);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Sets the effect creator.
        /// </summary>
        public static Effect SetEffectCreator(Effect eEffect, uint oCreator)
        {
            NWNXPInvoke.StackPushObject(oCreator);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(1123);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Sets the effect caster level.
        /// </summary>
        public static Effect SetEffectCasterLevel(Effect eEffect, int nCasterLevel)
        {
            NWNXPInvoke.StackPushInteger(nCasterLevel);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(1124);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Sets the effect spell ID.
        /// </summary>
        public static Effect SetEffectSpellId(Effect eEffect, Spell nSpellId)
        {
            NWNXPInvoke.StackPushInteger((int)nSpellId);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eEffect);
            NWNXPInvoke.CallBuiltIn(1125);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

        /// <summary>
        /// Create an Enemy Attack Bonus effect.
        /// </summary>
        public static Effect EffectEnemyAttackBonus(int nBonus)
        {
            NWNXPInvoke.StackPushInteger(nBonus);
            NWNXPInvoke.CallBuiltIn(1146);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Effect);
        }

    }
}