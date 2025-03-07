using XM.Shared.Core.Localization;

namespace XM.Progression.Craft
{
    public enum RecipeCategoryType
    {
        [RecipeCategory(LocaleString.Invalid, false)]
        Invalid = 0,
        [RecipeCategory(LocaleString.Uncategorized, true)]
        Uncategorized = 1,
        [RecipeCategory(LocaleString.Longsword, true)]
        Longsword = 2,
        [RecipeCategory(LocaleString.GreatSword, true)]
        GreatSword = 3,
        [RecipeCategory(LocaleString.Pistol, true)]
        Pistol = 4,
        [RecipeCategory(LocaleString.Dagger, true)]
        Dagger = 5,
        [RecipeCategory(LocaleString.Throwing, true)]
        Throwing = 6,
        [RecipeCategory(LocaleString.GreatAxe, true)]
        GreatAxe = 7,
        [RecipeCategory(LocaleString.HandToHand, true)]
        HandToHand = 8,
        [RecipeCategory(LocaleString.Rifle, true)]
        Rifle = 9,
        [RecipeCategory(LocaleString.Club, true)]
        Club = 10,
        [RecipeCategory(LocaleString.Staff, true)]
        Staff = 11,
        [RecipeCategory(LocaleString.Axe, true)]
        Axe = 12,
        [RecipeCategory(LocaleString.ShortSword, true)]
        ShortSword = 13,
        [RecipeCategory(LocaleString.Polearm, true)]
        Polearm = 14,
        [RecipeCategory(LocaleString.Bow, true)]
        Bow = 15,
        
    }
}
