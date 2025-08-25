using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Fabrication
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class RingRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Ring recipes ordered by level (1-97)
            CopperforgeRing();                          // Level 1
            BrassguardRing();                           // Level 7
            EremitesFocusRing();                        // Level 17
            SentinelsSafeguardRing();                   // Level 20
            SanctifiedBand();                           // Level 20
            ReflexResonanceRing();                      // Level 25
            AmberResonanceRing();                       // Level 26
            EquilibriumRing();                          // Level 26
            ValorRing();                                // Level 26
            LapisArcaneRing();                          // Level 26
            EtherflowRing();                            // Level 27
            EnduranceRing();                            // Level 27
            ObsidianOnyxRing();                         // Level 29
            AmethystArcRing();                          // Level 30
            BeaconofHopeRing();                         // Level 30
            LeatherboundRing();                         // Level 31
            SilverguardRing();                          // Level 31
            BoneforgedRing();                           // Level 33
            ChitinPlatedRing();                         // Level 44
            MythriteBand();                             // Level 45
            MarksmansRing();                            // Level 57
            SmilodonFangRing();                         // Level 62
            DeftstrikeRing();                           // Level 69
            VerveEtherflowRing();                       // Level 70
            AlacrityResonanceRing();                    // Level 71
            HornedCrestRing();                          // Level 72
            OathkeepersLoyaltyRing();                   // Level 72
            SolaceGuardianRing();                       // Level 72
            PuissanceBattleRing();                      // Level 75
            ElectrumArcRing();                          // Level 77
            SharpshootersRing();                        // Level 83
            WoodsmansTrackerRing();                     // Level 85
            AuricGoldRing();                            // Level 87
            PhalanxDefenderRing();                      // Level 97

            return _builder.Build();
        }

        private void CopperforgeRing()
        {
            _builder.Create(RecipeType.CopperforgeRing, SkillType.Fabrication)
                .Level(1)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("copperforge_rng", 1)
                .HQItem("copperforge_p1", 1)
                .Component("aurion_ingot", 1)
                .Component("ether_crystal", 1);
        }

        private void BrassguardRing()
        {
            _builder.Create(RecipeType.BrassguardRing, SkillType.Fabrication)
                .Level(7)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("brassguard_rng", 1)
                .HQItem("brassguard_p1", 1)
                .Component("aurion_ingot", 1)
                .Component("ether_crystal", 1)
                .Component("bond_agent", 1);
        }

        private void EremitesFocusRing()
        {
            _builder.Create(RecipeType.EremitesFocusRing, SkillType.Fabrication)
                .Level(17)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("erem_focus_rng", 1)
                .HQItem("erem_focus_p1", 1)
                .Component("ferrite_core", 1)
                .Component("resonance_ston", 1)
                .Component("enhance_serum", 1);
        }

        private void SentinelsSafeguardRing()
        {
            _builder.Create(RecipeType.SentinelsSafeguardRing, SkillType.Fabrication)
                .Level(20)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("sentinel_safrng", 1)
                .Component("ferrite_core", 1)
                .Component("resonance_ston", 1)
                .Component("circuit_matrix", 1);
        }

        private void SanctifiedBand()
        {
            _builder.Create(RecipeType.SanctifiedBand, SkillType.Fabrication)
                .Level(20)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("sanctified_band", 1)
                .HQItem("sanctified_bnd_p1", 1)
                .Component("ferrite_core", 1)
                .Component("resonance_ston", 1)
                .Component("enhance_serum", 1);
        }

        private void ReflexResonanceRing()
        {
            _builder.Create(RecipeType.ReflexResonanceRing, SkillType.Fabrication)
                .Level(25)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("reflex_reso_rng", 1)
                .HQItem("reflex_reso_p1", 1)
                .Component("ferrite_core", 1)
                .Component("resonance_ston", 2)
                .Component("circuit_matrix", 1);
        }

        private void AmberResonanceRing()
        {
            _builder.Create(RecipeType.AmberResonanceRing, SkillType.Fabrication)
                .Level(26)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("amber_reso_rng", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1);
        }

        private void EquilibriumRing()
        {
            _builder.Create(RecipeType.EquilibriumRing, SkillType.Fabrication)
                .Level(26)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("equilibrium_rng", 1)
                .HQItem("equilibrium_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void ValorRing()
        {
            _builder.Create(RecipeType.ValorRing, SkillType.Fabrication)
                .Level(26)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("valor_ring", 1)
                .HQItem("valor_ring_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void LapisArcaneRing()
        {
            _builder.Create(RecipeType.LapisArcaneRing, SkillType.Fabrication)
                .Level(26)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("lapis_arc_rng", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("mythrite_frag", 1);
        }

        private void EtherflowRing()
        {
            _builder.Create(RecipeType.EtherflowRing, SkillType.Fabrication)
                .Level(27)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("etherflow_ring", 1)
                .HQItem("etherflow_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void EnduranceRing()
        {
            _builder.Create(RecipeType.EnduranceRing, SkillType.Fabrication)
                .Level(27)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("endurance_ring", 1)
                .HQItem("endurance_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void ObsidianOnyxRing()
        {
            _builder.Create(RecipeType.ObsidianOnyxRing, SkillType.Fabrication)
                .Level(29)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("obsidian_onyxrn", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("mythrite_frag", 1);
        }

        private void AmethystArcRing()
        {
            _builder.Create(RecipeType.AmethystArcRing, SkillType.Fabrication)
                .Level(30)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("amethyst_arc_rng", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("mythrite_frag", 1);
        }

        private void BeaconofHopeRing()
        {
            _builder.Create(RecipeType.BeaconofHopeRing, SkillType.Fabrication)
                .Level(30)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("beacon_hope_rng", 1)
                .HQItem("beacon_hope_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void LeatherboundRing()
        {
            _builder.Create(RecipeType.LeatherboundRing, SkillType.Fabrication)
                .Level(31)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("leatherb_ring", 1)
                .HQItem("leatherb_ring_p1", 1)
                .Component("beast_hide", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void SilverguardRing()
        {
            _builder.Create(RecipeType.SilverguardRing, SkillType.Fabrication)
                .Level(31)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("silverg_ring", 1)
                .HQItem("silverg_ring_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void BoneforgedRing()
        {
            _builder.Create(RecipeType.BoneforgedRing, SkillType.Fabrication)
                .Level(33)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("boneforged_ring", 1)
                .HQItem("boneforged_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("mythrite_frag", 1);
        }

        private void ChitinPlatedRing()
        {
            _builder.Create(RecipeType.ChitinPlatedRing, SkillType.Fabrication)
                .Level(44)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("chitin_ring", 1)
                .HQItem("chitin_ring_p1", 1)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("mythrite_frag", 1);
        }

        private void MythriteBand()
        {
            _builder.Create(RecipeType.MythriteBand, SkillType.Fabrication)
                .Level(45)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("mythrite_band", 1)
                .HQItem("mythrite_band_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void MarksmansRing()
        {
            _builder.Create(RecipeType.MarksmansRing, SkillType.Fabrication)
                .Level(57)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("marksman_ring", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void SmilodonFangRing()
        {
            _builder.Create(RecipeType.SmilodonFangRing, SkillType.Fabrication)
                .Level(62)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("smilodon_fangrn", 1)
                .HQItem("smilodon_fang_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("spirit_ess", 1);
        }

        private void DeftstrikeRing()
        {
            _builder.Create(RecipeType.DeftstrikeRing, SkillType.Fabrication)
                .Level(69)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("deftstrike_ring", 1)
                .HQItem("deftstrike_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void VerveEtherflowRing()
        {
            _builder.Create(RecipeType.VerveEtherflowRing, SkillType.Fabrication)
                .Level(70)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("verve_ether_rng", 1)
                .HQItem("verve_ether_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 1);
        }

        private void AlacrityResonanceRing()
        {
            _builder.Create(RecipeType.AlacrityResonanceRing, SkillType.Fabrication)
                .Level(71)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("alacrity_res_rng", 1)
                .HQItem("alacrity_res_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void HornedCrestRing()
        {
            _builder.Create(RecipeType.HornedCrestRing, SkillType.Fabrication)
                .Level(72)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("horned_crest_rng", 1)
                .HQItem("horned_crest_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void OathkeepersLoyaltyRing()
        {
            _builder.Create(RecipeType.OathkeepersLoyaltyRing, SkillType.Fabrication)
                .Level(72)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("oathkeeper_loyal", 1)
                .HQItem("oathkeep_loyal_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("spirit_ess", 2);
        }

        private void SolaceGuardianRing()
        {
            _builder.Create(RecipeType.SolaceGuardianRing, SkillType.Fabrication)
                .Level(72)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("solace_guard_rng", 1)
                .HQItem("solace_guard_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("spirit_ess", 1)
                .Component("purify_filter", 1);
        }

        private void PuissanceBattleRing()
        {
            _builder.Create(RecipeType.PuissanceBattleRing, SkillType.Fabrication)
                .Level(75)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("puissance_btlrng", 1)
                .HQItem("puissance_btl_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 1)
                .Component("purify_filter", 1);
        }

        private void ElectrumArcRing()
        {
            _builder.Create(RecipeType.ElectrumArcRing, SkillType.Fabrication)
                .Level(77)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("electrum_arc_rng", 1)
                .HQItem("electrum_arc_p1", 1)
                .Component("titan_plate", 1)
                .Component("quantum_proc", 1)
                .Component("nano_enchant", 1);
        }

        private void SharpshootersRing()
        {
            _builder.Create(RecipeType.SharpshootersRing, SkillType.Fabrication)
                .Level(83)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("sharpshooter_rng", 1)
                .HQItem("sharpshoot_p1", 1)
                .Component("titan_plate", 1)
                .Component("quantum_proc", 2)
                .Component("nano_enchant", 1);
        }

        private void WoodsmansTrackerRing()
        {
            _builder.Create(RecipeType.WoodsmansTrackerRing, SkillType.Fabrication)
                .Level(85)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("woodsman_trackrn", 1)
                .HQItem("woods_track_p1", 1)
                .Component("titan_plate", 1)
                .Component("quantum_proc", 1)
                .Component("nano_enchant", 1)
                .Component("harmonic_alloy", 1);
        }

        private void AuricGoldRing()
        {
            _builder.Create(RecipeType.AuricGoldRing, SkillType.Fabrication)
                .Level(87)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("auric_gold_ring", 1)
                .HQItem("auric_goldrng_p1", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 2)
                .Component("nano_enchant", 1);
        }

        private void PhalanxDefenderRing()
        {
            _builder.Create(RecipeType.PhalanxDefenderRing, SkillType.Fabrication)
                .Level(97)
                .Category(RecipeCategoryType.Ring)
                .NormalItem("phalanx_def_rng", 1)
                .HQItem("phalanx_def_p1", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 2)
                .Component("nano_enchant", 1)
                .Component("sync_core", 1);
        }
    }
}