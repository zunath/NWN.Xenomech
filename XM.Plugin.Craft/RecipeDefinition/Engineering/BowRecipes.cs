using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Engineering
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class BowRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Bows ordered by level (5-99)
            StrikerShortbow();          // Level 5
            VanguardLongbow();          // Level 13
            ArcwoodBow();              // Level 17
            RegalArchersWarbow();      // Level 23
            SandusRecurve();           // Level 33
            PowerstrikeBow();          // Level 35
            WrappedWarbow();           // Level 51
            TitanGreatbow();           // Level 63
            ShadowpiercerBow();        // Level 71
            CompositeRecurve();        // Level 75
            BattleforgedBow();         // Level 83
            KamanStriker();            // Level 90
            WarbornBow();              // Level 99

            return _builder.Build();
        }

        private void StrikerShortbow()
        {
            _builder.Create(RecipeType.StrikerShortbow, SkillType.Engineering)
                .Level(5)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("striker_shortbw", 1)
                .HQItem("striker_shortbw", 1)
                .Component("living_wood", 2)
                .Component("circuit_matrix", 1)
                .Component("bond_agent", 1);
        }

        private void VanguardLongbow()
        {
            _builder.Create(RecipeType.VanguardLongbow, SkillType.Engineering)
                .Level(13)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("vanguard_longbw", 1)
                .HQItem("vanguard_longbw", 1)
                .Component("living_wood", 3)
                .Component("circuit_matrix", 1)
                .Component("bond_agent", 2);
        }

        private void ArcwoodBow()
        {
            _builder.Create(RecipeType.ArcwoodBow, SkillType.Engineering)
                .Level(17)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("arcwood_bow", 1)
                .HQItem("arcwood_bow", 1)
                .Component("living_wood", 2)
                .Component("circuit_matrix", 1)
                .Component("enhance_serum", 1)
                .Component("bond_agent", 1);
        }

        private void RegalArchersWarbow()
        {
            _builder.Create(RecipeType.RegalArchersWarbow, SkillType.Engineering)
                .Level(23)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("regal_archers_wa", 1)
                .Component("living_wood", 2)
                .Component("ferrite_core", 1)
                .Component("circuit_matrix", 1)
                .Component("enhance_serum", 1);
        }

        private void SandusRecurve()
        {
            _builder.Create(RecipeType.SandusRecurve, SkillType.Engineering)
                .Level(33)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("sandus_recurve", 1)
                .Component("living_wood", 2)
                .Component("darksteel_bar", 1)
                .Component("neural_inter", 1)
                .Component("techno_fiber", 1);
        }

        private void PowerstrikeBow()
        {
            _builder.Create(RecipeType.PowerstrikeBow, SkillType.Engineering)
                .Level(35)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("powerstrike_bow", 1)
                .HQItem("powerstrike_bow", 1)
                .Component("living_wood", 3)
                .Component("darksteel_bar", 1)
                .Component("neural_inter", 1)
                .Component("plasma_conduit", 1);
        }

        private void WrappedWarbow()
        {
            _builder.Create(RecipeType.WrappedWarbow, SkillType.Engineering)
                .Level(51)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("wrapped_warbow", 1)
                .HQItem("wrapped_warbow", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 1)
                .Component("phase_crystal", 1)
                .Component("gravit_stabil", 1);
        }

        private void TitanGreatbow()
        {
            _builder.Create(RecipeType.TitanGreatbow, SkillType.Engineering)
                .Level(63)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("titan_greatbow", 1)
                .HQItem("titan_greatbow", 1)
                .Component("living_wood", 2)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 1)
                .Component("phase_crystal", 1)
                .Component("gravit_stabil", 1);
        }

        private void ShadowpiercerBow()
        {
            _builder.Create(RecipeType.ShadowpiercerBow, SkillType.Engineering)
                .Level(71)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("shadowpiercer_bo", 1)
                .HQItem("shadowpiercer_bo", 1)
                .Component("living_wood", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 2)
                .Component("phase_crystal", 1)
                .Component("gravit_stabil", 1)
                .Component("biosteel_comp", 1);
        }

        private void CompositeRecurve()
        {
            _builder.Create(RecipeType.CompositeRecurve, SkillType.Engineering)
                .Level(75)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("composite_recurv", 1)
                .HQItem("composite_recurv", 1)
                .Component("living_wood", 2)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 2)
                .Component("phase_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void BattleforgedBow()
        {
            _builder.Create(RecipeType.BattleforgedBow, SkillType.Engineering)
                .Level(83)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("battleforged_bow", 1)
                .HQItem("battleforged_bow", 1)
                .Component("living_wood", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 2)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 1);
        }

        private void KamanStriker()
        {
            _builder.Create(RecipeType.KamanStriker, SkillType.Engineering)
                .Level(90)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("kaman_striker", 1)
                .HQItem("kaman_striker", 1)
                .Component("living_wood", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 2)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 1)
                .Component("sync_core", 1);
        }

        private void WarbornBow()
        {
            _builder.Create(RecipeType.WarbornBow, SkillType.Engineering)
                .Level(99)
                .Category(RecipeCategoryType.Bow)
                .NormalItem("warborn_bow", 1)
                .HQItem("warborn_bow", 1)
                .Component("living_wood", 1)
                .Component("quantum_steel", 3)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 2)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 1)
                .Component("sync_core", 1);
        }
    }
}