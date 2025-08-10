using System.Text.Json.Serialization;
using XM.Shared.Core.Json;
using XM.Shared.Core.Primitives;

namespace XM.Progression.Skill
{
    [JsonConverter(typeof(SmartEnumJsonConverter<SkillType>))]
    [KeyNameDomain("SkillType")]
    public sealed class SkillType : SmartEnum<SkillType>
    {
        public static readonly SkillType Invalid = new(nameof(Invalid), 0);
        public static readonly SkillType Longsword = new(nameof(Longsword), 1);
        public static readonly SkillType GreatSword = new(nameof(GreatSword), 2);
        public static readonly SkillType Pistol = new(nameof(Pistol), 3);
        public static readonly SkillType Dagger = new(nameof(Dagger), 4);
        public static readonly SkillType Throwing = new(nameof(Throwing), 5);
        public static readonly SkillType GreatAxe = new(nameof(GreatAxe), 6);
        public static readonly SkillType HandToHand = new(nameof(HandToHand), 7);
        public static readonly SkillType Rifle = new(nameof(Rifle), 8);
        public static readonly SkillType Club = new(nameof(Club), 9);
        public static readonly SkillType Staff = new(nameof(Staff), 10);
        public static readonly SkillType Axe = new(nameof(Axe), 11);
        public static readonly SkillType ShortSword = new(nameof(ShortSword), 12);
        public static readonly SkillType Polearm = new(nameof(Polearm), 13);
        public static readonly SkillType Bow = new(nameof(Bow), 14);

        public static readonly SkillType Weaponcraft = new(nameof(Weaponcraft), 50);
        public static readonly SkillType Armorcraft = new(nameof(Armorcraft), 51);
        public static readonly SkillType Fabrication = new(nameof(Fabrication), 52);
        public static readonly SkillType Synthesis = new(nameof(Synthesis), 53);
        public static readonly SkillType Engineering = new(nameof(Engineering), 54);
        public static readonly SkillType Extraction = new(nameof(Extraction), 55);
        public static readonly SkillType Harvesting = new(nameof(Harvesting), 56);

        public static readonly SkillType Evasion = new(nameof(Evasion), 100);

        private SkillType(string name, int value) : base(name, value) { }
    }
}
