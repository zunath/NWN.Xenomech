using System.Numerics;
using NWN.Core.NWNX;
using XM.Shared.API.BaseTypes;
using XM.Shared.API.Constants;

namespace XM.Shared.API.NWNX.CreaturePlugin
{
    public static class CreaturePlugin
    {
        /// <summary>
        /// Gives the creature a feat.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <remark>Consider also using NWNX_Creature_AddFeatByLevel() to properly allocate the feat to a level.</remark>
        public static void AddFeat(uint creature, FeatType feat)
        {
            NWN.Core.NWNX.CreaturePlugin.AddFeat(creature, (int)feat);
        }

        /// <summary>
        /// Gives the creature a feat assigned at a specific level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <param name="level">The level they gained the feat.</param>
        /// <remark>Adds the feat to the stat list at the provided level.</remark>
        public static void AddFeatByLevel(uint creature, FeatType feat, int level)
        {
            NWN.Core.NWNX.CreaturePlugin.AddFeatByLevel(creature, (int)feat, level);
        }

        /// <summary>
        /// Removes a feat from a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        public static void RemoveFeat(uint creature, FeatType feat)
        {
            NWN.Core.NWNX.CreaturePlugin.RemoveFeat(creature, (int)feat);
        }

        /// <summary>
        /// Removes the creature a feat assigned at a specific level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <param name="level">The level they gained the feat.</param>
        /// <remark>Removes the feat from the stat list at the provided level. Does not remove the feat from the creature, use NWNX_Creature_RemoveFeat for this.</remark>
        public static void RemoveFeatByLevel(uint creature, FeatType feat, int level)
        {
            NWN.Core.NWNX.CreaturePlugin.RemoveFeatByLevel(creature, (int)feat, level);
        }

        /// <summary>
        /// Determines if the creature knows a specific feat.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <returns>TRUE if the creature has the feat, regardless of whether they have any usages left or not.</returns>
        /// <note>This differs from native @nwn{GetHasFeat} which returns FALSE if the feat has no more uses per day.</note>
        public static bool GetKnowsFeat(uint creature, FeatType feat)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetKnowsFeat(creature, (int)feat) == 1;
        }

        /// <summary>
        /// Returns the count of feats learned at a specific level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <returns>The count of feats learned at the provided level.</returns>
        public static int GetFeatCountByLevel(uint creature, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetFeatCountByLevel(creature, level);
        }

        /// <summary>
        /// Returns the feat learned at the specified level and index.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <param name="index">The index of the feat. Index bounds: 0 <= index < NWNX_Creature_GetFeatCountByLevel().</param>
        /// <returns>The feat id at the specified index.</returns>
        public static FeatType GetFeatByLevel(uint creature, int level, int index)
        {
            return (FeatType)NWN.Core.NWNX.CreaturePlugin.GetFeatByLevel(creature, level, index);
        }

        /// <summary>
        /// Returns the level at which the specified feat was granted to the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <returns>The level at which the specified feat was granted, otherwise 0 if the creature does not have this feat.</returns>
        public static int GetFeatGrantLevel(uint creature, FeatType feat)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetFeatGrantLevel(creature, (int)feat);
        }

        /// <summary>
        /// Gets the total number of feats known by the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The total feat count for the creature.</returns>
        public static int GetFeatCount(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetFeatCount(creature);
        }

        /// <summary>
        /// Returns the creature's feat at a given index.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="index">The index. Index bounds: 0 <= index < NWNX_Creature_GetFeatCount();</param>
        /// <returns>The feat id at the index.</returns>
        public static FeatType GetFeatByIndex(uint creature, int index)
        {
            return (FeatType)NWN.Core.NWNX.CreaturePlugin.GetFeatByIndex(creature, index);
        }

        /// <summary>
        /// Gets if the creature meets feat requirements.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <returns>TRUE if the creature meets all requirements to take the given feat.</returns>
        public static bool GetMeetsFeatRequirements(uint creature, FeatType feat)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMeetsFeatRequirements(creature, (int)feat) == 1;
        }

        /// <summary>
        /// Gets the count of special abilities of the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The total special ability count.</returns>
        public static int GetSpecialAbilityCount(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetSpecialAbilityCount(creature);
        }

        /// <summary>
        /// Returns the creature's special ability at a given index.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="index">The index. Index bounds: 0 <= index < NWNX_Creature_GetSpecialAbilityCount().</param>
        /// <returns>An NWNX_Creature_SpecialAbility struct.</returns>
        public static SpecialAbility GetSpecialAbility(uint creature, int index)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetSpecialAbility(creature, index);
        }

        /// <summary>
        /// Adds a special ability to a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="ability">An NWNX_Creature_SpecialAbility struct.</param>
        public static void AddSpecialAbility(uint creature, SpecialAbility ability)
        {
            NWN.Core.NWNX.CreaturePlugin.AddSpecialAbility(creature, ability);
        }

        /// <summary>
        /// Removes a special ability from a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="index">The index. Index bounds: 0 <= index < NWNX_Creature_GetSpecialAbilityCount().</param>
        public static void RemoveSpecialAbility(uint creature, int index)
        {
            NWN.Core.NWNX.CreaturePlugin.RemoveSpecialAbility(creature, index);
        }

        /// <summary>
        /// Sets a special ability at the index for the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="index">The index. Index bounds: 0 <= index < NWNX_Creature_GetSpecialAbilityCount().</param>
        /// <param name="ability">An NWNX_Creature_SpecialAbility struct.</param>
        public static void SetSpecialAbility(uint creature, int index, SpecialAbility ability)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSpecialAbility(creature, index, ability);
        }

        /// <summary>
        /// Get the class taken by the creature at the provided level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <returns>The class id.</returns>
        public static int GetClassByLevel(uint creature, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetClassByLevel(creature, level);
        }

        /// <summary>
        /// Sets the base AC for the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="ac">The base AC to set for the creature.</param>
        public static void SetBaseAC(uint creature, int ac)
        {
            NWN.Core.NWNX.CreaturePlugin.SetBaseAC(creature, ac);
        }

        /// <summary>
        /// Get the base AC for the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The base AC.</returns>
        public static int GetBaseAC(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetBaseAC(creature);
        }

        /// <summary>
        /// Sets the ability score of the creature to the provided value.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="ability">The ability constant.</param>
        /// <param name="value">The value to set.</param>
        public static void SetRawAbilityScore(uint creature, AbilityType ability, int value)
        {
            NWN.Core.NWNX.CreaturePlugin.SetRawAbilityScore(creature, (int)ability, value);
        }

        /// <summary>
        /// Gets the ability score of the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="ability">The ability constant.</param>
        /// <returns>The ability score.</returns>
        public static int GetRawAbilityScore(uint creature, AbilityType ability)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetRawAbilityScore(creature, (int)ability);
        }

        /// <summary>
        /// Adjusts the ability score of a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="ability">The ability constant.</param>
        /// <param name="modifier">The modifier value.</param>
        public static void ModifyRawAbilityScore(uint creature, AbilityType ability, int modifier)
        {
            NWN.Core.NWNX.CreaturePlugin.ModifyRawAbilityScore(creature, (int)ability, modifier);
        }

        /// <summary>
        /// Gets the raw ability score a polymorphed creature had prior to polymorphing.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="ability">The ability constant.</param>
        /// <returns>The raw ability score.</returns>
        public static int GetPrePolymorphAbilityScore(uint creature, AbilityType ability)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetPrePolymorphAbilityScore(creature, (int)ability);
        }

        /// <summary>
        /// Gets the remaining spell slots (innate casting) at a class level's index.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="level">The spell level.</param>
        /// <returns>The remaining spell slot count.</returns>
        public static int GetRemainingSpellSlots(uint creature, ClassType @class, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetRemainingSpellSlots(creature, (int)@class, level);
        }

        /// <summary>
        /// Sets the remaining spell slots (innate casting) at a class level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="level">The spell level.</param>
        /// <param name="slots">The remaining spell slots to set.</param>
        public static void SetRemainingSpellSlots(uint creature, ClassType @class, int level, int slots)
        {
            NWN.Core.NWNX.CreaturePlugin.SetRemainingSpellSlots(creature, (int)@class, level, slots);
        }

        /// <summary>
        /// Gets the maximum spell slots (innate casting) at a class level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="level">The spell level.</param>
        /// <returns>The maximum spell slot count.</returns>
        public static int GetMaxSpellSlots(uint creature, ClassType @class, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMaxSpellSlots(creature, (int)@class, level);
        }

        /// <summary>
        /// Add a spell to a creature's spellbook for class.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="level">The spell level.</param>
        /// <param name="spellId">The spell to remove.</param>
        public static void AddKnownSpell(uint creature, ClassType @class, int level, int spellId)
        {
            NWN.Core.NWNX.CreaturePlugin.AddKnownSpell(creature, (int)@class, level, spellId);
        }

        /// <summary>
        /// Remove a spell from creature's spellbook for class.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="level">The spell level.</param>
        /// <param name="spellId">The spell to remove.</param>
        public static void RemoveKnownSpell(uint creature, ClassType @class, int level, int spellId)
        {
            NWN.Core.NWNX.CreaturePlugin.RemoveKnownSpell(creature, (int)@class, level, spellId);
        }

        /// <summary>
        /// Gets the maximum hit points for creature for level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <returns>The maximum hit points a creature can have for the class at the provided level.</returns>
        public static int GetMaxHitPointsByLevel(uint creature, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMaxHitPointsByLevel(creature, level);
        }

        /// <summary>
        /// Sets the maximum hit points for creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <param name="value">The amount to set the max hit points.</param>
        public static void SetMaxHitPointsByLevel(uint creature, int level, int value)
        {
            NWN.Core.NWNX.CreaturePlugin.SetMaxHitPointsByLevel(creature, level, value);
        }

        /// <summary>
        /// Set creature's movement rate.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="rate">The movement rate.</param>
        public static void SetMovementRate(uint creature, MovementRateType rate)
        {
            NWN.Core.NWNX.CreaturePlugin.SetMovementRate(creature, (int)rate);
        }

        /// <summary>
        /// Returns the creature's current movement rate factor.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The current movement rate factor.</returns>
        public static float GetMovementRateFactor(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMovementRateFactor(creature);
        }

        /// <summary>
        /// Sets the creature's current movement rate factor.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="factor">The rate to set.</param>
        public static void SetMovementRateFactor(uint creature, float factor)
        {
            NWN.Core.NWNX.CreaturePlugin.SetMovementRateFactor(creature, factor);
        }

        /// <summary>
        /// Returns the creature's maximum movement rate cap.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The maximum movement rate cap.</returns>
        public static float GetMovementRateFactorCap(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMovementRateFactorCap(creature);
        }

        /// <summary>
        /// Sets the creature's maximum movement rate cap.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="cap">The cap to set.</param>
        /// <note>Default movement rate cap is 1.5.</note>
        public static void SetMovementRateFactorCap(uint creature, float cap)
        {
            NWN.Core.NWNX.CreaturePlugin.SetMovementRateFactorCap(creature, cap);
        }

        /// <summary>
        /// Returns the creature's current movement type.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>An NWNX_CREATURE_MOVEMENT_TYPE_* constant.</returns>
        public static int GetMovementType(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMovementType(creature);
        }

        /// <summary>
        /// Sets the maximum movement rate a creature can have while walking (not running).
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="fWalkRate">The walk rate to apply. Setting the value to -1.0 will remove the cap. Default value is 2000.0, which is the base human walk speed.</param>
        /// <remark>This allows a creature with movement speed enhancements to walk at a normal rate.</remark>
        public static void SetWalkRateCap(uint creature, float fWalkRate = 2000.0f)
        {
            NWN.Core.NWNX.CreaturePlugin.SetWalkRateCap(creature, fWalkRate);
        }

        /// <summary>
        /// Set creature's raw good/evil alignment value.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="value">The value to set.</param>
        public static void SetAlignmentGoodEvil(uint creature, int value)
        {
            NWN.Core.NWNX.CreaturePlugin.SetAlignmentGoodEvil(creature, value);
        }

        /// <summary>
        /// Set creature's raw law/chaos alignment value.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="value">The value to set.</param>
        public static void SetAlignmentLawChaos(uint creature, int value)
        {
            NWN.Core.NWNX.CreaturePlugin.SetAlignmentLawChaos(creature, value);
        }

        /// <summary>
        /// Set the base ranks in a skill for the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="skill">The skill id.</param>
        /// <param name="rank">The value to set as the skill rank.</param>
        public static void SetSkillRank(uint creature, SkillType skill, int rank)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSkillRank(creature, (int)skill, rank);
        }

        /// <summary>
        /// Get the ranks in a skill for the creature assigned at a level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="skill">The skill id.</param>
        /// <param name="level">The level they gained skill ranks.</param>
        /// <returns>The rank in a skill assigned at a level (-1 on error).</returns>
        public static int GetSkillRankByLevel(uint creature, SkillType skill, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetSkillRankByLevel(creature, (int)skill, level);
        }

        /// <summary>
        /// Set the ranks in a skill for the creature assigned at a level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="skill">The skill id.</param>
        /// <param name="level">The level they gained skill ranks.</param>
        /// <param name="rank">The value to set as the skill rank.</param>
        /// <note>It only affects the leveling array. To effectively change the skill rank on the current level, NWNX_Creature_SetSkillRank is also needed.</note>
        public static void SetSkillRankByLevel(uint creature, SkillType skill, int rank, int level)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSkillRankByLevel(creature, (int)skill, rank, level);
        }

        /// <summary>
        /// Set the class ID in a particular position for a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="position">Should be 0, 1, or 2 depending on how many classes the creature has and which is to be modified.</param>
        /// <param name="classID">A valid ID number in classes.2da and between 0 and 255.</param>
        /// <param name="bUpdateLevels">Determines whether the method will replace all occurrences of the old class in CNWLevelStats with the new classID.</param>
        public static void SetClassByPosition(uint creature, int position, ClassType classID, int bUpdateLevels = 1)
        {
            NWN.Core.NWNX.CreaturePlugin.SetClassByPosition(creature, position, (int)classID, bUpdateLevels);
        }

        /// <summary>
        /// Set the level at the given position for a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="position">Should be 0, 1, or 2 depending on how many classes the creature has and which is to be modified.</param>
        /// <param name="level">The level to set.</param>
        public static void SetLevelByPosition(uint creature, int position, int level)
        {
            NWN.Core.NWNX.CreaturePlugin.SetLevelByPosition(creature, position, level);
        }

        /// <summary>
        /// Set creature's base attack bonus (BAB).
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="bab">The BAB value. Should be between 0 and 254. Setting BAB to 0 will cause the creature to revert to its original BAB based on its classes and levels.</param>
        /// <note>Modifying the BAB will also affect the creature's attacks per round and its eligibility for feats, prestige classes, etc.</note>
        public static void SetBaseAttackBonus(uint creature, int bab)
        {
            NWN.Core.NWNX.CreaturePlugin.SetBaseAttackBonus(creature, bab);
        }

        /// <summary>
        /// Gets the creature's current attacks per round (using equipped weapon).
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="bBaseAPR">If TRUE, will return the base attacks per round, based on BAB and equipped weapons, regardless of overrides set by calls to SetBaseAttackBonus.</param>
        /// <returns>The attacks per round.</returns>
        public static int GetAttacksPerRound(uint creature, int bBaseAPR = 0)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetAttacksPerRound(creature, bBaseAPR);
        }

        /// <summary>
        /// Restore all creature feat uses.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        public static void RestoreFeats(uint creature)
        {
            NWN.Core.NWNX.CreaturePlugin.RestoreFeats(creature);
        }

        /// <summary>
        /// Restore all creature special ability uses.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        public static void RestoreSpecialAbilities(uint creature)
        {
            NWN.Core.NWNX.CreaturePlugin.RestoreSpecialAbilities(creature);
        }

        /// <summary>
        /// Restore uses for all items carried by the creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        public static void RestoreItems(uint creature)
        {
            NWN.Core.NWNX.CreaturePlugin.RestoreItems(creature);
        }

        /// <summary>
        /// Sets the creature's size.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="size">Use CREATURE_SIZE_* constants.</param>
        public static void SetSize(uint creature, int size)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSize(creature, size);
        }

        /// <summary>
        /// Gets the creature's remaining unspent skill points.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The remaining unspent skill points.</returns>
        public static int GetSkillPointsRemaining(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetSkillPointsRemaining(creature);
        }

        /// <summary>
        /// Sets the creature's remaining unspent skill points.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="skillPoints">The value to set.</param>
        public static void SetSkillPointsRemaining(uint creature, int skillPoints)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSkillPointsRemaining(creature, skillPoints);
        }

        /// <summary>
        /// Gets the creature's remaining unspent skill points for a specific level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <returns>The remaining unspent skill points for the level.</returns>
        public static int GetSkillPointsRemainingByLevel(uint creature, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetSkillPointsRemainingByLevel(creature, level);
        }

        /// <summary>
        /// Sets the creature's remaining unspent skill points for a specific level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <param name="value">The value to set for the level.</param>
        public static void SetSkillPointsRemainingByLevel(uint creature, int level, int value)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSkillPointsRemainingByLevel(creature, level, value);
        }

        /// <summary>
        /// Sets the creature's racial type.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="racialType">The racial type to set.</param>
        public static void SetRacialType(uint creature, RacialType racialType)
        {
            NWN.Core.NWNX.CreaturePlugin.SetRacialType(creature, (int)racialType);
        }

        /// <summary>
        /// Sets the creature's gold without sending a feedback message.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="gold">The amount of gold to set for the creature.</param>
        public static void SetGold(uint creature, int gold)
        {
            NWN.Core.NWNX.CreaturePlugin.SetGold(creature, gold);
        }

        /// <summary>
        /// Sets corpse decay time in milliseconds.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="nDecayTime">The corpse decay time.</param>
        public static void SetCorpseDecayTime(uint creature, int nDecayTime)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCorpseDecayTime(creature, nDecayTime);
        }

        /// <summary>
        /// Gets the creature's base save.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="which">One of SAVING_THROW_FORT, SAVING_THROW_REFLEX or SAVING_THROW_WILL.</param>
        /// <returns>The base save value.</returns>
        /// <note>This will include any modifiers set in the toolset.</note>
        public static int GetBaseSavingThrow(uint creature, SavingThrowCategoryType which)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetBaseSavingThrow(creature, (int)which);
        }
        /// <summary>
        /// Sets the creature's base save.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="which">One of SAVING_THROW_FORT, SAVING_THROW_REFLEX or SAVING_THROW_WILL.</param>
        /// <param name="value">The base save value.</param>
        public static void SetBaseSavingThrow(uint creature, SavingThrowCategoryType which, int value)
        {
            NWN.Core.NWNX.CreaturePlugin.SetBaseSavingThrow(creature, (int)which, value);
        }

        /// <summary>
        /// Add levels of class to the creature, bypassing all validation.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id.</param>
        /// <param name="count">The amount of levels of class to add.</param>
        /// <param name="package">The class package to use for leveling up (PACKAGE_INVALID = starting package).</param>
        /// <note>This will not work on player characters.</note>
        public static void LevelUp(uint creature, ClassType @class, int count = 1, PackageType package = 0)
        {
            NWN.Core.NWNX.CreaturePlugin.LevelUp(creature, (int)@class, count, (int)package);
        }

        /// <summary>
        /// Remove last levels from a creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="count">The amount of levels to decrement.</param>
        /// <note>This will not work on player characters.</note>
        public static void LevelDown(uint creature, int count = 1)
        {
            NWN.Core.NWNX.CreaturePlugin.LevelDown(creature, count);
        }

        /// <summary>
        /// Sets the creature's challenge rating.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="fCR">The challenge rating.</param>
        public static void SetChallengeRating(uint creature, float fCR)
        {
            NWN.Core.NWNX.CreaturePlugin.SetChallengeRating(creature, fCR);
        }

        /// <summary>
        /// Returns the creature's highest attack bonus based on its own stats.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="isMelee">TRUE: Get Melee/Unarmed Attack Bonus, FALSE: Get Ranged Attack Bonus, -1: Get Attack Bonus depending on the weapon creature has equipped in its right hand.</param>
        /// <param name="isTouchAttack">If the attack was a touch attack.</param>
        /// <param name="isOffhand">If the attack was with the offhand.</param>
        /// <param name="includeBaseAttackBonus">Should the result include the base attack bonus.</param>
        /// <returns>The highest attack bonus.</returns>
        public static int GetAttackBonus(uint creature, int isMelee = -1, bool isTouchAttack = false, bool isOffhand = false, bool includeBaseAttackBonus = true)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetAttackBonus(creature, isMelee, isTouchAttack ? 1 : 0, isOffhand ? 1 : 0, includeBaseAttackBonus ? 1 : 0);
        }

        /// <summary>
        /// Get highest level version of feat possessed by creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <returns>The highest level version of the feat.</returns>
        public static int GetHighestLevelOfFeat(uint creature, FeatType feat)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetHighestLevelOfFeat(creature, (int)feat);
        }

        /// <summary>
        /// Get feat remaining uses.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <returns>The amount of remaining uses.</returns>
        public static int GetFeatRemainingUses(uint creature, FeatType feat)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetFeatRemainingUses(creature, (int)feat);
        }

        /// <summary>
        /// Get feat total uses.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <returns>The total uses.</returns>
        public static int GetFeatTotalUses(uint creature, FeatType feat)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetFeatTotalUses(creature, (int)feat);
        }

        /// <summary>
        /// Set feat remaining uses.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="feat">The feat id.</param>
        /// <param name="uses">The amount of remaining uses.</param>
        public static void SetFeatRemainingUses(uint creature, FeatType feat, int uses)
        {
            NWN.Core.NWNX.CreaturePlugin.SetFeatRemainingUses(creature, (int)feat, uses);
        }

        /// <summary>
        /// Get total effect bonus beyond a player's base scores to attack, damage bonus, saves, skills, ability scores, and touch attack provided by spells, equipment, potions etc.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="bonusType">A Bonus Type constant.</param>
        /// <param name="target">A target object. Used to calculate bonuses versus specific races, alignments, etc.</param>
        /// <param name="isElemental">If a damage bonus includes elemental damage.</param>
        /// <param name="isForceMax">If the bonus should return the maximum possible.</param>
        /// <param name="saveType">A SAVING_THROW_* constant.</param>
        /// <param name="saveSpecificType">A SAVING_THROW_TYPE_* constant.</param>
        /// <param name="skill">A skill id.</param>
        /// <param name="abilityScore">An ABILITY_* constant.</param>
        /// <param name="isOffhand">Whether the attack is an offhand attack.</param>
        /// <returns>The bonus value.</returns>
        public static int GetTotalEffectBonus(
            uint creature,
            int bonusType = 0,
            uint target = 0,
            bool isElemental = false,
            bool isForceMax = false,
            SavingThrowCategoryType saveType = SavingThrowCategoryType.Invalid,
            SavingThrowType saveSpecificType = SavingThrowType.Invalid,
            SkillType skill = SkillType.Invalid,
            AbilityType abilityScore = AbilityType.Invalid,
            bool isOffhand = false)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetTotalEffectBonus(
                creature,
                bonusType,
                target,
                isElemental ? 1 : 0,
                isForceMax ? 1 : 0,
                (int)saveType,
                (int)saveSpecificType,
                (int)skill,
                (int)abilityScore,
                isOffhand ? 1 : 0);
        }

        /// <summary>
        /// Set the original first or last name of creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="name">The name to give the creature.</param>
        /// <param name="isLastName">TRUE to change their last name, FALSE for first.</param>
        /// <note>For PCs this will persist to the .bic file if saved. Requires a relog to update.</note>
        public static void SetOriginalName(uint creature, string name, bool isLastName)
        {
            NWN.Core.NWNX.CreaturePlugin.SetOriginalName(creature, name, isLastName ? 1 : 0);
        }

        /// <summary>
        /// Get the original first or last name of creature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="isLastName">TRUE to get last name, FALSE for first name.</param>
        /// <returns>The original first or last name of the creature.</returns>
        public static string GetOriginalName(uint creature, bool isLastName)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetOriginalName(creature, isLastName ? 1 : 0);
        }

        /// <summary>
        /// Set creature's spell resistance.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="sr">The spell resistance.</param>
        /// <note>This setting will be overwritten by effects and once those effects fade the old setting (typically 0) will be set.</note>
        public static void SetSpellResistance(uint creature, int sr)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSpellResistance(creature, sr);
        }

        /// <summary>
        /// Set creature's animal companion creature type.
        /// </summary>
        /// <param name="creature">The master creature object.</param>
        /// <param name="type">The type from ANIMAL_COMPANION_CREATURE_TYPE_*.</param>
        public static void SetAnimalCompanionCreatureType(uint creature, AnimalCompanionCreatureType type)
        {
            NWN.Core.NWNX.CreaturePlugin.SetAnimalCompanionCreatureType(creature, (int)type);
        }

        /// <summary>
        /// Set creature's familiar creature type.
        /// </summary>
        /// <param name="creature">The master creature object.</param>
        /// <param name="type">The type from FAMILIAR_CREATURE_TYPE_*.</param>
        public static void SetFamiliarCreatureType(uint creature, FamiliarCreatureType type)
        {
            NWN.Core.NWNX.CreaturePlugin.SetFamiliarCreatureType(creature, (int)type);
        }

        /// <summary>
        /// Set creature's animal companion's name.
        /// </summary>
        /// <param name="creature">The master creature object.</param>
        /// <param name="name">The name to give their animal companion.</param>
        public static void SetAnimalCompanionName(uint creature, string name)
        {
            NWN.Core.NWNX.CreaturePlugin.SetAnimalCompanionName(creature, name);
        }

        /// <summary>
        /// Set creature's familiar's name.
        /// </summary>
        /// <param name="creature">The master creature object.</param>
        /// <param name="name">The name to give their familiar.</param>
        public static void SetFamiliarName(uint creature, string name)
        {
            NWN.Core.NWNX.CreaturePlugin.SetFamiliarName(creature, name);
        }

        /// <summary>
        /// Get whether the creature can be disarmed.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>TRUE if the creature can be disarmed.</returns>
        public static bool GetDisarmable(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetDisarmable(creature) == 1;
        }

        /// <summary>
        /// Set whether a creature can be disarmed.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="disarmable">Set to TRUE if the creature can be disarmed.</param>
        public static void SetDisarmable(uint creature, bool disarmable)
        {
            NWN.Core.NWNX.CreaturePlugin.SetDisarmable(creature, disarmable ? 1 : 0);
        }

        /// <summary>
        /// Sets one of creature's domains.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="index">The first or second domain.</param>
        /// <param name="domain">The domain constant to set.</param>
        public static void SetDomain(uint creature, ClassType @class, int index, int domain)
        {
            NWN.Core.NWNX.CreaturePlugin.SetDomain(creature, (int)@class, index, domain);
        }

        /// <summary>
        /// Sets creature's specialist school.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="class">The class id from classes.2da. (Not class index 0-2)</param>
        /// <param name="school">The school constant.</param>
        public static void SetSpecialization(uint creature, ClassType @class, int school)
        {
            NWN.Core.NWNX.CreaturePlugin.SetSpecialization(creature, (int)@class, school);
        }

        /// <summary>
        /// Sets oCreature's faction to be the faction with id nFactionId.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="nFactionId">The faction id we want the creature to join.</param>
        public static void SetFaction(uint creature, StandardFactionType nFactionId)
        {
            NWN.Core.NWNX.CreaturePlugin.SetFaction(creature, (int)nFactionId);
        }

        /// <summary>
        /// Gets the faction id from oCreature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The faction id as an integer, -1 when used against invalid creature or invalid object.</returns>
        public static StandardFactionType GetFaction(uint creature)
        {
            return (StandardFactionType)NWN.Core.NWNX.CreaturePlugin.GetFaction(creature);
        }

        /// <summary>
        /// Get whether a creature is flat-footed.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>TRUE if the creature is flat-footed.</returns>
        public static bool GetFlatFooted(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetFlatFooted(creature) == 1;
        }

        /// <summary>
        /// Serialize oCreature's quickbar to a base64 string.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>A base64 string representation of oCreature's quickbar.</returns>
        public static string SerializeQuickbar(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.SerializeQuickbar(creature);
        }

        /// <summary>
        /// Deserialize a serialized quickbar for oCreature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="sSerializedQuickbar">A base64 string of a quickbar.</param>
        /// <returns>TRUE on success.</returns>
        public static int DeserializeQuickbar(uint creature, string sSerializedQuickbar)
        {
            return NWN.Core.NWNX.CreaturePlugin.DeserializeQuickbar(creature, sSerializedQuickbar);
        }

        /// <summary>
        /// Sets a caster level modifier for oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="classID">The class that this modifier will apply to.</param>
        /// <param name="modifier">The modifier to apply.</param>
        /// <param name="persist">Whether the modifier should be persisted to the .bic file if applicable.</param>
        public static void SetCasterLevelModifier(uint creature, ClassType classID, int modifier, int persist = 0)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCasterLevelModifier(creature, (int)classID, modifier, persist);
        }

        /// <summary>
        /// Gets the current caster level modifier for oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="classID">The creature's caster class.</param>
        /// <returns>The current caster level modifier for the creature.</returns>
        public static int GetCasterLevelModifier(uint creature, ClassType classID)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCasterLevelModifier(creature, (int)classID);
        }

        /// <summary>
        /// Sets a caster level override for oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="classID">The class that this modifier will apply to.</param>
        /// <param name="casterLevel">The caster level override to apply.</param>
        /// <param name="persist">Whether the override should be persisted to the .bic file if applicable.</param>
        public static void SetCasterLevelOverride(uint creature, ClassType classID, int casterLevel, int persist = 0)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCasterLevelOverride(creature, (int)classID, casterLevel, persist);
        }
        /// <summary>
        /// Gets the current caster level override for oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="classID">The creature caster class.</param>
        /// <returns>The current caster level override for the creature or -1 if not set.</returns>
        public static int GetCasterLevelOverride(uint creature, ClassType classID)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCasterLevelOverride(creature, (int)classID);
        }

        /// <summary>
        /// Move a creature to limbo.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        public static void JumpToLimbo(uint creature)
        {
            NWN.Core.NWNX.CreaturePlugin.JumpToLimbo(creature);
        }

        /// <summary>
        /// Sets the critical hit multiplier modifier for the Creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="modifier">The modifier to apply.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="persist">Whether the modifier should persist to .bic file if applicable.</param>
        /// <param name="baseItem">Applies the modifier only when the attack used this baseitem. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        public static void SetCriticalMultiplierModifier(uint creature, int modifier, int hand = 0, bool persist = false, BaseItemType baseItem = BaseItemType.All)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCriticalMultiplierModifier(creature, modifier, hand, persist ? 1 : 0, (int)baseItem);
        }

        /// <summary>
        /// Gets the critical hit multiplier modifier for the Creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="baseItem">The baseitem modifier to retrieve. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        /// <returns>The current critical hit multiplier modifier for the creature.</returns>
        public static int GetCriticalMultiplierModifier(uint creature, int hand = 0, BaseItemType baseItem = BaseItemType.All)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCriticalMultiplierModifier(creature, hand, (int)baseItem);
        }

        /// <summary>
        /// Sets the critical hit multiplier override for the Creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="overrideValue">The override value to apply. -1 to clear override.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="persist">Whether the modifier should persist to .bic file if applicable.</param>
        /// <param name="baseItem">Applies the override only when the attack used this baseitem. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        public static void SetCriticalMultiplierOverride(uint creature, int overrideValue, int hand = 0, bool persist = false, BaseItemType baseItem = BaseItemType.All)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCriticalMultiplierOverride(creature, overrideValue, hand, persist ? 1 : 0, (int)baseItem);
        }

        /// <summary>
        /// Gets the critical hit multiplier override for the Creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="baseItem">The baseitem override to retrieve. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        /// <returns>The current critical hit multiplier override for the creature. No override == -1.</returns>
        public static int GetCriticalMultiplierOverride(uint creature, int hand = 0, BaseItemType baseItem = BaseItemType.All)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCriticalMultiplierOverride(creature, hand, (int)baseItem);
        }

        /// <summary>
        /// Sets the critical hit range modifier for the creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="modifier">The modifier to apply. Positive modifiers reduce critical chance. (I.e. From 18-20, a +1 results in crit range of 19-20)</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="persist">Whether the modifier should persist to .bic file if applicable.</param>
        /// <param name="baseItem">Applies the modifier only when the attack used this baseitem. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        public static void SetCriticalRangeModifier(uint creature, int modifier, int hand = 0, bool persist = false, BaseItemType baseItem = BaseItemType.All)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCriticalRangeModifier(creature, modifier, hand, persist ? 1 : 0, (int)baseItem);
        }

        /// <summary>
        /// Gets the critical hit range modifier for the creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="baseItem">The baseitem modifier to retrieve. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        /// <returns>The current critical hit range modifier for the creature.</returns>
        public static int GetCriticalRangeModifier(uint creature, int hand = 0, BaseItemType baseItem = BaseItemType.All)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCriticalRangeModifier(creature, hand, (int)baseItem);
        }

        /// <summary>
        /// Sets the critical hit range override for the creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="overrideValue">The new minimum roll to crit. i.e overrideValue of 15 results in crit range of 15-20. -1 to clear override.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="persist">Whether the modifier should persist to .bic file if applicable.</param>
        /// <param name="baseItem">Applies the override only when the attack used this baseitem. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        public static void SetCriticalRangeOverride(uint creature, int overrideValue, int hand = 0, bool persist = false, BaseItemType baseItem = BaseItemType.All)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCriticalRangeOverride(creature, overrideValue, hand, persist ? 1 : 0, (int)baseItem);
        }

        /// <summary>
        /// Gets the critical hit range override for the creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="hand">0 for all attacks, 1 for Mainhand, 2 for Offhand.</param>
        /// <param name="baseItem">The baseitem override to retrieve. BASE_ITEM_GLOVES for Unarmed, '-1' for all.</param>
        /// <returns>The current critical hit range override for the creature. No override == -1.</returns>
        public static int GetCriticalRangeOverride(uint creature, int hand = 0, BaseItemType baseItem = BaseItemType.All)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCriticalRangeOverride(creature, hand, (int)baseItem);
        }

        /// <summary>
        /// Add oAssociate as nAssociateType to oCreature.
        /// </summary>
        /// <param name="creature">The creature to add the associate to.</param>
        /// <param name="associate">The associate, must be a NPC.</param>
        /// <param name="associateType">The associate type, one of ASSOCIATE_TYPE_* constants, except _NONE.</param>
        public static void AddAssociate(uint creature, uint associate, AssociateType associateType)
        {
            NWN.Core.NWNX.CreaturePlugin.AddAssociate(creature, associate, (int)associateType);
        }

        /// <summary>
        /// Override the damage level of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="damageLevel">A damage level, see damagelevels.2da. Allowed values: 0-255 or -1 to remove the override.</param>
        public static void OverrideDamageLevel(uint creature, int damageLevel)
        {
            NWN.Core.NWNX.CreaturePlugin.OverrideDamageLevel(creature, damageLevel);
        }

        /// <summary>
        /// Set the encounter source of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="encounter">The source encounter.</param>
        public static void SetEncounter(uint creature, uint encounter)
        {
            NWN.Core.NWNX.CreaturePlugin.SetEncounter(creature, encounter);
        }

        /// <summary>
        /// Get the encounter source of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <returns>The encounter, OBJECT_INVALID if not part of an encounter or on error.</returns>
        public static uint GetEncounter(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetEncounter(creature);
        }

        /// <summary>
        /// Get if oCreature is currently bartering.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <returns>TRUE if oCreature is bartering, FALSE if not or on error.</returns>
        public static bool GetIsBartering(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetIsBartering(creature) == 1;
        }

        /// <summary>
        /// Sets caster level for the last item used.
        /// </summary>
        /// <param name="creature">The creature who used the item.</param>
        /// <param name="casterLevel">The desired caster level.</param>
        public static void SetLastItemCasterLevel(uint creature, int casterLevel)
        {
            NWN.Core.NWNX.CreaturePlugin.SetLastItemCasterLevel(creature, casterLevel);
        }

        /// <summary>
        /// Gets the caster level of the last item used.
        /// </summary>
        /// <param name="creature">The creature who used the item.</param>
        /// <returns>The creature's last used item's level.</returns>
        public static int GetLastItemCasterLevel(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetLastItemCasterLevel(creature);
        }

        /// <summary>
        /// Gets the Armor class of attacked against versus.
        /// </summary>
        /// <param name="attacked">The one being attacked.</param>
        /// <param name="versus">The one doing the attacking.</param>
        /// <param name="touch">TRUE for touch attacks.</param>
        /// <returns>-255 on Error, Flat footed AC if oVersus is invalid or the Attacked AC versus oVersus.</returns>
        public static int GetArmorClassVersus(uint attacked, uint versus, int touch = 0)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetArmorClassVersus(attacked, versus, touch);
        }

        /// <summary>
        /// Gets the current walk animation of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <returns>-1 on Error, otherwise the walk animation number.</returns>
        public static int GetWalkAnimation(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetWalkAnimation(creature);
        }

        /// <summary>
        /// Sets the current walk animation of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="animation">The walk animation number.</param>
        public static void SetWalkAnimation(uint creature, int animation)
        {
            NWN.Core.NWNX.CreaturePlugin.SetWalkAnimation(creature, animation);
        }

        /// <summary>
        /// Changes the attack modifier depending on the dice roll.
        /// </summary>
        /// <param name="creature">The attacking creature, use OBJECT_INVALID for all.</param>
        /// <param name="roll">The dice roll to modify.</param>
        /// <param name="modifier">The modifier to the attack, use 0 to turn off autofail for 1/autosucceed for 20 with no attack modifier value.</param>
        public static void SetAttackRollOverride(uint creature, int roll, int modifier)
        {
            NWN.Core.NWNX.CreaturePlugin.SetAttackRollOverride(creature, roll, modifier);
        }
        /// <summary>
        /// Works like the tweak but can be turned on and off for all creatures or single ones.
        /// </summary>
        /// <param name="creature">The parrying creature, use OBJECT_INVALID for all.</param>
        /// <param name="parry">TRUE to parry all attacks.</param>
        /// <note>Use this command on_module_load instead of the NWNX_TWEAKS_PARRY_ALL_ATTACKS tweak if using NWNX_Creature_SetAttackRollOverride()</note>
        public static void SetParryAllAttacks(uint creature, bool parry)
        {
            NWN.Core.NWNX.CreaturePlugin.SetParryAllAttacks(creature, parry ? 1 : 0);
        }

        /// <summary>
        /// Gets the NoPermanentDeath flag of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <returns>TRUE/FALSE or false on error.</returns>
        public static bool GetNoPermanentDeath(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetNoPermanentDeath(creature) == 1;
        }

        /// <summary>
        /// Sets the NoPermanentDeath flag of oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="noPermanentDeath">TRUE/FALSE.</param>
        public static void SetNoPermanentDeath(uint creature, bool noPermanentDeath)
        {
            NWN.Core.NWNX.CreaturePlugin.SetNoPermanentDeath(creature, noPermanentDeath ? 1 : 0);
        }

        /// <summary>
        /// Compute a safe location for oCreature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="position">The starting position.</param>
        /// <param name="radius">The search radius around position.</param>
        /// <param name="walkStraightLineRequired">Whether the creature must be able to walk in a straight line to the position.</param>
        /// <returns>A safe location as vector, will return position if one wasn't found. Returns {0.0, 0.0, 0.0} on error.</returns>
        public static Vector3 ComputeSafeLocation(uint creature, Vector3 position, float radius = 20.0f, bool walkStraightLineRequired = true)
        {
            return NWN.Core.NWNX.CreaturePlugin.ComputeSafeLocation(creature, position, radius, walkStraightLineRequired ? 1 : 0);
        }

        /// <summary>
        /// Update oCreature's perception of oTargetCreature.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="targetCreature">The target creature.</param>
        public static void DoPerceptionUpdateOnCreature(uint creature, uint targetCreature)
        {
            NWN.Core.NWNX.CreaturePlugin.DoPerceptionUpdateOnCreature(creature, targetCreature);
        }

        /// <summary>
        /// Get a creatures personal space (meters from center to non-creature objects).
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The creatures personal space.</returns>
        public static float GetPersonalSpace(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetPersonalSpace(creature);
        }

        /// <summary>
        /// Set a creatures personal space (meters from center to non-creature objects).
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="perspace">The creatures personal space.</param>
        public static void SetPersonalSpace(uint creature, float perspace)
        {
            NWN.Core.NWNX.CreaturePlugin.SetPersonalSpace(creature, perspace);
        }

        /// <summary>
        /// Get a creatures creature personal space (meters from center to other creatures).
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The creatures creature personal space.</returns>
        public static float GetCreaturePersonalSpace(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetCreaturePersonalSpace(creature);
        }

        /// <summary>
        /// Set a creatures creature personal space (meters from center to other creatures).
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="creaturePerspace">The creatures creature personal space.</param>
        public static void SetCreaturePersonalSpace(uint creature, float creaturePerspace)
        {
            NWN.Core.NWNX.CreaturePlugin.SetCreaturePersonalSpace(creature, creaturePerspace);
        }

        /// <summary>
        /// Get a creatures height.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The creatures height.</returns>
        public static float GetHeight(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetHeight(creature);
        }

        /// <summary>
        /// Set a creatures height.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="height">The creatures height.</param>
        public static void SetHeight(uint creature, float height)
        {
            NWN.Core.NWNX.CreaturePlugin.SetHeight(creature, height);
        }

        /// <summary>
        /// Get a creatures hit distance.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The creatures hit distance.</returns>
        public static float GetHitDistance(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetHitDistance(creature);
        }

        /// <summary>
        /// Set a creatures hit distance.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="hitDist">The creatures hit distance.</param>
        public static void SetHitDistance(uint creature, float hitDist)
        {
            NWN.Core.NWNX.CreaturePlugin.SetHitDistance(creature, hitDist);
        }

        /// <summary>
        /// Get a creatures preferred attack distance.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The creatures preferred attack distance.</returns>
        public static float GetPreferredAttackDistance(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetPreferredAttackDistance(creature);
        }

        /// <summary>
        /// Set a creatures preferred attack distance.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="prefAtckDist">The creatures preferred attack distance.</param>
        public static void SetPreferredAttackDistance(uint creature, float prefAtckDist)
        {
            NWN.Core.NWNX.CreaturePlugin.SetPreferredAttackDistance(creature, prefAtckDist);
        }

        /// <summary>
        /// Get the skill penalty from wearing armor.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The skill penalty from wearing armor.</returns>
        public static int GetArmorCheckPenalty(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetArmorCheckPenalty(creature);
        }

        /// <summary>
        /// Get the skill penalty from wearing a shield.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The skill penalty from wearing a shield.</returns>
        public static int GetShieldCheckPenalty(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetShieldCheckPenalty(creature);
        }

        /// <summary>
        /// Sets a chance for normal Effect Immunities to be bypassed.
        /// </summary>
        /// <param name="creature">The affected creature.</param>
        /// <param name="immunityType">'IMMUNITY_TYPE_*' to bypass.</param>
        /// <param name="chance">The chance (of 100%) to bypass the immunity check.</param>
        /// <param name="persist">Whether the modifier should persist to .bic file (for PCs).</param>
        public static void SetBypassEffectImmunity(uint creature, ImmunityType immunityType, int chance = 100, bool persist = false)
        {
            NWN.Core.NWNX.CreaturePlugin.SetBypassEffectImmunity(creature, (int)immunityType, chance, persist ? 1 : 0);
        }

        /// <summary>
        /// Gets a chance for normal Effect Immunities to be bypassed.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="immunityType">'IMMUNITY_TYPE_*' to retrieve the current chance for bypass.</param>
        /// <returns>The current critical hit multiplier modifier for the creature.</returns>
        public static int GetBypassEffectImmunity(uint creature, ImmunityType immunityType)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetBypassEffectImmunity(creature, (int)immunityType);
        }

        /// <summary>
        /// Sets the killer of oCreature to oKiller.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <param name="killer">The killer.</param>
        public static void SetLastKiller(uint creature, uint killer)
        {
            NWN.Core.NWNX.CreaturePlugin.SetLastKiller(creature, killer);
        }

        /// <summary>
        /// Instantly cast a spell at a target or location.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="target">The target, use OBJECT_INVALID to cast at a location.</param>
        /// <param name="location">The location, only used when target is OBJECT_INVALID.</param>
        /// <param name="spellID">The spell ID.</param>
        /// <param name="casterLevel">The caster level of the spell.</param>
        /// <param name="projectileTime">The time in seconds for the projectile to reach the target. 0.0f for no projectile.</param>
        /// <param name="projectilePathType">A PROJECTILE_PATH_TYPE_* constant.</param>
        /// <param name="projectileSpellID">An optional spell ID which to use the projectile vfx of. -1 to use spellID's projectile vfx.</param>
        /// <param name="spellCastItem">The spell cast item retrieved by GetSpellCastItem().</param>
        /// <param name="impactScript">The spell impact script. Set to "****" to not run any impact script.</param>
        public static void DoItemCastSpell(uint caster, uint target, Location location, SpellType spellID, int casterLevel, float projectileTime, ProjectilePathType projectilePathType = ProjectilePathType.Default, SpellType projectileSpellID = SpellType.AllSpells, uint spellCastItem = OBJECT_INVALID, string impactScript = "")
        {
            NWN.Core.NWNX.CreaturePlugin.DoItemCastSpell(caster, target, location, (int)spellID, casterLevel, projectileTime, (int)projectilePathType, (int)projectileSpellID, spellCastItem, impactScript);
        }

        /// <summary>
        /// Have oCreature instantly equip oItem to nInventorySlot.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="item">The item, must be possessed by creature.</param>
        /// <param name="inventorySlot">An INVENTORY_SLOT_* constant.</param>
        /// <returns>TRUE on success, FALSE on failure.</returns>
        public static int RunEquip(uint creature, uint item, InventorySlotType inventorySlot)
        {
            return NWN.Core.NWNX.CreaturePlugin.RunEquip(creature, item, (int)inventorySlot);
        }

        /// <summary>
        /// Have oCreature instantly unequip oItem.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="item">The item, must be possessed by creature.</param>
        /// <returns>TRUE on success, FALSE on failure.</returns>
        public static int RunUnequip(uint creature, uint item)
        {
            return NWN.Core.NWNX.CreaturePlugin.RunUnequip(creature, item);
        }

        /// <summary>
        /// Override the elemental projectile visual effect of ranged/throwing weapons.
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="projectileVFX">A ranged_projectile_vfx "NWNX_CREATURE_PROJECTILE_VFX_*" constant or -1 to remove the override.</param>
        /// <param name="persist">Whether the vfx should persist to the .bic file (for PCs).</param>
        public static void OverrideRangedProjectileVFX(uint creature, VisualEffectType projectileVFX, bool persist = false)
        {
            NWN.Core.NWNX.CreaturePlugin.OverrideRangedProjectileVFX(creature, (int)projectileVFX, persist ? 1 : 0);
        }

        /// <summary>
        /// Sets a custom Initiative modifier.
        /// </summary>
        /// <param name="creature">The affected creature.</param>
        /// <param name="modifier">The amount to adjust their initiative (+/-).</param>
        /// <param name="persist">Whether the modifier should persist to .bic file (for PCs).</param>
        /// <note>Persistence is enabled after a server reset by the first use of this function. Recommended to trigger on a dummy target OnModuleLoad to enable persistence.</note>
        public static void SetInitiativeModifier(uint creature, int modifier, bool persist = false)
        {
            NWN.Core.NWNX.CreaturePlugin.SetInitiativeModifier(creature, modifier, persist ? 1 : 0);
        }

        /// <summary>
        /// Gets the custom Initiative modifier.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <returns>The current custom initiative modifier for the creature.</returns>
        public static int GetInitiativeModifier(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetInitiativeModifier(creature);
        }

        /// <summary>
        /// Gets the Body Bag of a creature.
        /// </summary>
        /// <param name="creature">The target creature.</param>
        /// <returns>The creature's assigned Body Bag.</returns>
        public static uint GetBodyBag(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetBodyBag(creature);
        }

        /// <summary>
        /// Add a cast spell action to oCreature's action queue.
        /// </summary>
        /// <param name="creature">The creature casting the spell.</param>
        /// <param name="target">The target, to cast at a location use the area as target.</param>
        /// <param name="targetLocation">The target location.</param>
        /// <param name="spellID">The spell ID.</param>
        /// <param name="multiClass">The character class position to cast the spell as.</param>
        /// <param name="metaMagic">A METAMAGIC_* constant, except METAMAGIC_ANY.</param>
        /// <param name="domainLevel">The domain level if casting a domain spell.</param>
        /// <param name="projectilePathType">A PROJECTILE_PATH_TYPE_* constant.</param>
        /// <param name="instant">TRUE to instantly cast the spell.</param>
        /// <param name="clearActions">TRUE to clear all actions.</param>
        /// <param name="addToFront">TRUE to add the cast spell action to the front of the action queue.</param>
        /// <returns>TRUE if the action was successfully added to creature's action queue.</returns>
        public static int AddCastSpellActions(uint creature, uint target, Vector3 targetLocation, SpellType spellID, int multiClass, MetamagicType metaMagic = MetamagicType.None, int domainLevel = 0, ProjectilePathType projectilePathType = ProjectilePathType.Default, bool instant = false, bool clearActions = false, bool addToFront = false)
        {
            return NWN.Core.NWNX.CreaturePlugin.AddCastSpellActions(creature, target, targetLocation, (int)spellID, multiClass, (int)metaMagic, domainLevel, (int)projectilePathType, instant ? 1 : 0, clearActions ? 1 : 0, addToFront ? 1 : 0);
        }

        /// <summary>
        /// Get whether creature is flanking targetCreature.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="targetCreature">The target creature object.</param>
        /// <returns>TRUE if creature is flanking targetCreature.</returns>
        public static bool GetIsFlanking(uint creature, uint targetCreature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetIsFlanking(creature, targetCreature) == 1;
        }

        /// <summary>
        /// Decrements the remaining spell slots (innate casting) at a class level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="classID">The class id from classes.2da.</param>
        /// <param name="spellLevel">The spell level.</param>
        public static void DecrementRemainingSpellSlots(uint creature, ClassType classID, int spellLevel)
        {
            NWN.Core.NWNX.CreaturePlugin.DecrementRemainingSpellSlots(creature, (int)classID, spellLevel);
        }

        /// <summary>
        /// Increments the remaining spell slots (innate casting) at a class level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="classID">The class id from classes.2da.</param>
        /// <param name="spellLevel">The spell level.</param>
        public static void IncrementRemainingSpellSlots(uint creature, ClassType classID, int spellLevel)
        {
            NWN.Core.NWNX.CreaturePlugin.IncrementRemainingSpellSlots(creature, (int)classID, spellLevel);
        }

        /// <summary>
        /// Gets the maximum number of bonus attacks a creature can have from EffectModifyAttacks().
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <returns>The maximum number of bonus attacks or 0 on error.</returns>
        public static int GetMaximumBonusAttacks(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMaximumBonusAttacks(creature);
        }

        /// <summary>
        /// Sets the maximum number of bonus attacks a creature can have from EffectModifyAttacks().
        /// </summary>
        /// <param name="creature">The creature.</param>
        /// <param name="maxBonusAttacks">The maximum number of bonus attacks.</param>
        /// <param name="persist">Whether the modifier should persist to .bic file (for PCs).</param>
        /// <note>Persistence is enabled after a server reset by the first use of this function.</note>
        public static void SetMaximumBonusAttacks(uint creature, int maxBonusAttacks, bool persist = false)
        {
            NWN.Core.NWNX.CreaturePlugin.SetMaximumBonusAttacks(creature, maxBonusAttacks, persist ? 1 : 0);
        }

        /// <summary>
        /// Inserts a cleave or great cleave attack into creature's current attack round against the nearest enemy within melee reach.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <note>creature must have the cleave or great cleave feats, be in combat, and have available attacks remaining in their combat round.</note>
        public static void DoCleaveAttack(uint creature)
        {
            NWN.Core.NWNX.CreaturePlugin.DoCleaveAttack(creature);
        }

        /// <summary>
        /// Gets the current object creature's orientation is locked to.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <returns>The object creature's orientation is locked to, or OBJECT_INVALID if creature's orientation is not locked.</returns>
        public static uint GetLockOrientationToObject(uint creature)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetLockOrientationToObject(creature);
        }

        /// <summary>
        /// Locks creature's orientation to always face target.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="target">The target to lock creature's orientation to. Use OBJECT_INVALID to remove the orientation lock.</param>
        public static void SetLockOrientationToObject(uint creature, uint target)
        {
            NWN.Core.NWNX.CreaturePlugin.SetLockOrientationToObject(creature, target);
        }

        /// <summary>
        /// Causes creature to broadcast an Attack of Opportunity against themself.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="singleCreature">A single creature to broadcast the Attack of Opportunity to. Use OBJECT_INVALID to broadcast to all nearby enemies.</param>
        /// <param name="movement">Whether the Attack of Opportunity was caused by movement.</param>
        public static void BroadcastAttackOfOpportunity(uint creature, uint singleCreature = OBJECT_INVALID, bool movement = false)
        {
            NWN.Core.NWNX.CreaturePlugin.BroadcastAttackOfOpportunity(creature, singleCreature, movement ? 1 : 0);
        }

        /// <summary>
        /// Returns the maximum price store will buy items from creature for.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="store">The store object.</param>
        /// <returns>The max buy price override. -1 = No maximum buy price, -2 = No override set.</returns>
        public static int GetMaxSellToStorePriceOverride(uint creature, uint store)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMaxSellToStorePriceOverride(creature, store);
        }

        /// <summary>
        /// Overrides the maximum price store will buy items from creature for.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="store">The store object.</param>
        /// <param name="maxSellToPrice">The maximum buy price override. -1 = No maximum buy price, -2 = Remove the override.</param>
        public static void SetMaxSellToStorePriceOverride(uint creature, uint store, int maxSellToPrice)
        {
            NWN.Core.NWNX.CreaturePlugin.SetMaxSellToStorePriceOverride(creature, store, maxSellToPrice);
        }

        /// <summary>
        /// Returns the creature's ability increase for level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <returns>An ABILITY_* constant, NWNX_CREATURE_ABILITY_NONE or -1 on error.</returns>
        public static int GetAbilityIncreaseByLevel(uint creature, int level)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetAbilityIncreaseByLevel(creature, level);
        }

        /// <summary>
        /// Sets the creature's ability increase for level.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="level">The level.</param>
        /// <param name="ability">ABILITY_* constant or NWNX_CREATURE_ABILITY_NONE.</param>
        public static void SetAbilityIncreaseByLevel(uint creature, int level, AbilityType ability)
        {
            NWN.Core.NWNX.CreaturePlugin.SetAbilityIncreaseByLevel(creature, level, (int)ability);
        }

        /// <summary>
        /// Returns the creature's maximum attack range to a target.
        /// </summary>
        /// <param name="creature">The creature object.</param>
        /// <param name="target">The target to get the maximum attack range to.</param>
        /// <returns>The maximum attack range for creature to target.</returns>
        public static float GetMaxAttackRange(uint creature, uint target)
        {
            return NWN.Core.NWNX.CreaturePlugin.GetMaxAttackRange(creature, target);
        }

    }
}
