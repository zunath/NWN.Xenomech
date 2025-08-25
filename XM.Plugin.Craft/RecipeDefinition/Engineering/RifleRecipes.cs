using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Engineering
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class RifleRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Rifles ordered by level (2-100)
            HakenbuechseRifle();        // Level 2
            VanguardMusket();           // Level 10
            MaraudersRifle();          // Level 20
            TanegashimaRifle();        // Level 30
            ArquebusRifle();           // Level 44
            CorsairsRifle();           // Level 57
            MarsHexRifle();            // Level 60
            DarksteelHexRifle();       // Level 74
            NegoroshikiRifle();        // Level 84
            SeadogRepeater();          // Level 100

            return _builder.Build();
        }

        private void HakenbuechseRifle()
        {
            _builder.Create(RecipeType.HakenbuechseRifle, SkillType.Engineering)
                .Level(2)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("hakenbuechse_rif", 1)
                .Component("brass_sheet", 3)
                .Component("circuit_matrix", 1)
                .Component("living_wood", 1)
                .Component("bond_agent", 1);
        }

        private void VanguardMusket()
        {
            _builder.Create(RecipeType.VanguardMusket, SkillType.Engineering)
                .Level(10)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("vanguard_muskt", 1)
                .HQItem("vanguard_muskt", 1)
                .Component("brass_sheet", 2)
                .Component("circuit_matrix", 1)
                .Component("living_wood", 1)
                .Component("bond_agent", 2);
        }

        private void MaraudersRifle()
        {
            _builder.Create(RecipeType.MaraudersRifle, SkillType.Engineering)
                .Level(20)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("marauders_rifl", 1)
                .HQItem("marauders_rifl", 1)
                .Component("brass_sheet", 3)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1)
                .Component("enhance_serum", 1);
        }

        private void TanegashimaRifle()
        {
            _builder.Create(RecipeType.TanegashimaRifle, SkillType.Engineering)
                .Level(30)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("tanegashima_rifl", 1)
                .HQItem("tanegashima_rifl", 1)
                .Component("darksteel_bar", 2)
                .Component("neural_inter", 1)
                .Component("plasma_conduit", 1)
                .Component("techno_fiber", 1)
                .Component("servo_motor", 1);
        }

        private void ArquebusRifle()
        {
            _builder.Create(RecipeType.ArquebusRifle, SkillType.Engineering)
                .Level(44)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("arquebus_rifle", 1)
                .HQItem("arquebus_rifle", 1)
                .Component("darksteel_bar", 3)
                .Component("neural_inter", 2)
                .Component("plasma_conduit", 1)
                .Component("techno_fiber", 2)
                .Component("servo_motor", 1);
        }

        private void CorsairsRifle()
        {
            _builder.Create(RecipeType.CorsairsRifle, SkillType.Engineering)
                .Level(57)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("corsairs_rifle", 1)
                .HQItem("corsairs_rifle", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 1)
                .Component("plasma_conduit", 2)
                .Component("gravit_stabil", 1)
                .Component("biosteel_comp", 1);
        }

        private void MarsHexRifle()
        {
            _builder.Create(RecipeType.MarsHexRifle, SkillType.Engineering)
                .Level(60)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("mars_hexrifle", 1)
                .HQItem("mars_hexrifle", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("phase_crystal", 1)
                .Component("gravit_stabil", 1)
                .Component("harmonic_alloy", 1);
        }

        private void DarksteelHexRifle()
        {
            _builder.Create(RecipeType.DarksteelHexRifle, SkillType.Engineering)
                .Level(74)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("darksteel_hexri", 1)
                .HQItem("darksteel_hexri", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("phase_crystal", 2)
                .Component("gravit_stabil", 1)
                .Component("biosteel_comp", 1);
        }

        private void NegoroshikiRifle()
        {
            _builder.Create(RecipeType.NegoroshikiRifle, SkillType.Engineering)
                .Level(84)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("negoroshiki_rifl", 1)
                .HQItem("negoroshiki_rifl", 1)
                .Component("quantum_steel", 3)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 2)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 1)
                .Component("sync_core", 1);
        }

        private void SeadogRepeater()
        {
            _builder.Create(RecipeType.SeadogRepeater, SkillType.Engineering)
                .Level(100)
                .Category(RecipeCategoryType.Rifle)
                .NormalItem("seadog_repeater", 1)
                .HQItem("seadog_repeater", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 2)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 1)
                .Component("sync_core", 1);
        }
    }
}