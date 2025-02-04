using System;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Item.Market
{
    public enum MarketCategoryType
    {
        [MarketCategory(LocaleString.Invalid,  false)]
        Invalid = 0,
        [MarketCategory(LocaleString.Longsword, true)]
        Longsword = 1,
        [MarketCategory(LocaleString.GreatSword, true)]
        GreatSword = 2,
        [MarketCategory(LocaleString.Pistol, true)]
        Pistol = 3,
        [MarketCategory(LocaleString.Dagger, true)]
        Dagger = 4,
        [MarketCategory(LocaleString.Throwing, true)]
        Throwing = 5,
        [MarketCategory(LocaleString.GreatAxe, true)]
        GreatAxe = 6,
        [MarketCategory(LocaleString.HandToHand, true)]
        HandToHand = 7,
        [MarketCategory(LocaleString.Rifle, true)]
        Rifle = 8,
        [MarketCategory(LocaleString.Club, true)]
        Club = 9,
        [MarketCategory(LocaleString.Staff, true)]
        Staff = 10,
        [MarketCategory(LocaleString.Axe, true)]
        Axe = 11,
        [MarketCategory(LocaleString.ShortSword, true)]
        ShortSword = 12,
        [MarketCategory(LocaleString.Polearm, true)]
        Polearm = 13,
        [MarketCategory(LocaleString.Bow, true)]
        Bow = 14,
        [MarketCategory(LocaleString.Shield, true)]
        Shield = 15,
        [MarketCategory(LocaleString.Back, true)]
        Back = 16,
        [MarketCategory(LocaleString.Waist, true)]
        Waist = 17,
        [MarketCategory(LocaleString.Finger, true)]
        Finger = 18,
        [MarketCategory(LocaleString.Neck, true)]
        Neck = 19,
        [MarketCategory(LocaleString.Body, true)]
        Body = 20,
        [MarketCategory(LocaleString.Head, true)]
        Head = 21,
        [MarketCategory(LocaleString.Arm, true)]
        Arm = 22,
        [MarketCategory(LocaleString.Feet, true)]
        Feet = 23,
        [MarketCategory(LocaleString.Arrow, true)]
        Arrow = 24,
        [MarketCategory(LocaleString.Bullet, true)]
        Bullet = 25,
        [MarketCategory(LocaleString.Potion, true)]
        Potion = 26,
        [MarketCategory(LocaleString.Container, true)]
        Container = 27,

        [MarketCategory(LocaleString.Miscellaneous, true)]
        Miscellaneous = 99
    }

    public class MarketCategoryAttribute : Attribute
    {
        public LocaleString Name { get; }
        public bool IsActive { get; }

        public MarketCategoryAttribute(LocaleString name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
