using System.Collections.Generic;
using XM.Progression.Skill;

namespace XM.Progression.Craft
{
    public class RecipeDetail
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Resref { get; set; }
        public Dictionary<string, int> Components { get; set; }
        public SkillType Skill { get; set; }
        public RecipeCategoryType Category { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public bool MustBeUnlocked { get; set; }

        public RecipeDetail()
        {
            Quantity = 1;
            Category = RecipeCategoryType.Uncategorized;
            Components = new Dictionary<string, int>();
        }
    }
}
