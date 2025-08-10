using System.Collections.Generic;
using XM.Progression.Skill;

namespace XM.Plugin.Craft
{
    public class RecipeDetail
    {
        public Dictionary<RecipeQualityType, RecipeItem> Items { get; set; }
        public Dictionary<string, int> Components { get; set; }
        public SkillType Skill { get; set; }
        public RecipeCategoryType Category { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public bool MustBeUnlocked { get; set; }

        public RecipeDetail()
        {
            IsActive = true;
            Category = RecipeCategoryType.Uncategorized;
            Components = new Dictionary<string, int>();
            Items = new Dictionary<RecipeQualityType, RecipeItem>();
        }
    }
}
