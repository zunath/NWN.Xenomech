using Anvil.Services;
using XM.Migration.Entity;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;

namespace XM.Migration
{
    [ServiceBinding(typeof(PCInitializationService))]
    internal class PCInitializationService
    {
        private readonly DBService _db;
        private readonly MigrationService _migration;
        private readonly XMEventService _event;

        public PCInitializationService(
            DBService db, 
            MigrationService migration,
            XMEventService @event)
        {
            _db = db;
            _migration = migration;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnModuleEnter);
        }

        private void OnModuleEnter(uint objectSelf)
        {
            var player = GetEnteringObject();

            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerMigrationStatus>(playerId) ?? new PlayerMigrationStatus(playerId);

            // Already been initialized. Don't do it again.
            if (dbPlayer.MigrationVersion >= 1)
            {
                ExecuteScript(EventScript.OnXMPlayerMigrationBeforeScript, player);
                return;
            }

            ClearInventory(player);
            AutoLevelPlayer(player);
            InitializeSkills(player);
            RemoveNWNSpells(player);
            ClearFeats(player);
            GrantBasicFeats(player);
            SetBaseAttackBonus(player);
            AdjustAlignment(player);
            InitializeSavingThrows(player);
            GiveStartingItems(player);
            ApplyMovementRate(player);

            dbPlayer.MigrationVersion = _migration.GetLatestPlayerVersion();
            _db.Set(dbPlayer);

            ExecuteScript(EventScript.OnXMPCInitializedScript, player);
            ExecuteScript(EventScript.OnXMPlayerMigrationBeforeScript, player);
        }

        private void AutoLevelPlayer(uint player)
        {
            // Capture original stats before we level up the player.
            var mgt = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            var vit = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            var per = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception);
            var agi = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Agility);
            var wil = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            var soc = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Social);

            GiveXPToCreature(player, 800000);
            var @class = GetClassByPosition(1, player);

            for (var level = 1; level <= 40; level++)
            {
                LevelUpHenchman(player, @class);
            }

            // Set stats back to how they were on entry.
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Might, mgt);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Vitality, vit);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Perception, per);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Agility, agi);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Willpower, wil);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Social, soc);
        }

        /// <summary>
        /// Wipes a player's equipped items and inventory.
        /// </summary>
        /// <param name="player">The player to wipe an inventory for.</param>
        private void ClearInventory(uint player)
        {
            for (var slot = 0; slot < GeneralConstants.NumberOfInventorySlots; slot++)
            {
                var item = GetItemInSlot((InventorySlotType)slot, player);
                if (!GetIsObjectValid(item)) continue;

                DestroyObject(item);
            }

            var inventory = GetFirstItemInInventory(player);
            while (GetIsObjectValid(inventory))
            {
                DestroyObject(inventory);
                inventory = GetNextItemInInventory(player);
            }

            TakeGoldFromCreature(GetGold(player), player, true);
        }

        /// <summary>
        /// Initializes all player NWN skills to zero.
        /// </summary>
        /// <param name="player">The player to modify</param>
        public void InitializeSkills(uint player)
        {
            for (var iCurSkill = 1; iCurSkill <= 27; iCurSkill++)
            {
                var skill = (SkillType)(iCurSkill - 1);
                CreaturePlugin.SetSkillRank(player, skill, 0);
            }
        }

        /// <summary>
        /// Initializes all player saving throws to zero.
        /// </summary>
        /// <param name="player">The player to modify</param>
        public void InitializeSavingThrows(uint player)
        {
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrowCategoryType.Fortitude, 0);
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrowCategoryType.Will, 0);
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrowCategoryType.Reflex, 0);
        }

        /// <summary>
        /// Removes all NWN spells from a player.
        /// </summary>
        /// <param name="player">The player to modify.</param>
        private void RemoveNWNSpells(uint player)
        {
            var @class = GetClassByPosition(1, player);
            for (var index = 0; index <= 255; index++)
            {
                CreaturePlugin.RemoveKnownSpell(player, @class, 0, index);
            }
        }

        public void ClearFeats(uint player)
        {
            var numberOfFeats = CreaturePlugin.GetFeatCount(player);
            for (var currentFeat = numberOfFeats; currentFeat >= 0; currentFeat--)
            {
                CreaturePlugin.RemoveFeat(player, CreaturePlugin.GetFeatByIndex(player, currentFeat - 1));
            }
        }

        public void GrantBasicFeats(uint player)
        {
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyLight, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyMedium, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyHeavy, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ShieldProficiency, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencyExotic, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencyMartial, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencySimple, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.UncannyDodge1, 1);
        }

        /// <summary>
        /// Adjusts the player's base attack bonus
        /// </summary>
        /// <param name="player">The player object</param>
        private void SetBaseAttackBonus(uint player)
        {
            CreaturePlugin.SetBaseAttackBonus(player, 1);
        }

        /// <summary>
        /// Modifies the player's alignment to Neutral/Neutral since we don't use alignment at all here.
        /// </summary>
        /// <param name="player">The player to object.</param>
        public void AdjustAlignment(uint player)
        {
            CreaturePlugin.SetAlignmentLawChaos(player, 50);
            CreaturePlugin.SetAlignmentGoodEvil(player, 50);
        }


        /// <summary>
        /// Gives the starting items to the player.
        /// </summary>
        /// <param name="player">The player to receive the starting items.</param>
        private void GiveStartingItems(uint player)
        {
            var race = GetRacialType(player);
            var item = CreateItemOnObject("survival_knife", player);
            SetName(item, GetName(player) + "'s Survival Knife");
            SetItemCursedFlag(item, true);

            item = CreateItemOnObject("fresh_bread", player);
            SetItemCursedFlag(item, true);

            var clothes = "travelers_clothes";
            item = CreateItemOnObject(clothes, player);
            AssignCommand(player, () =>
            {
                ClearAllActions();
                ActionEquipItem(item, InventorySlotType.Chest);
            });

            GiveGoldToCreature(player, 200);
        }

        private void ApplyMovementRate(uint player)
        {
            CreaturePlugin.SetMovementRate(player, MovementRateType.PC);
        }
    }
}
