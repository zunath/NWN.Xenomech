﻿using System;
using System.Numerics;
using XM.Progression.Ability.Telegraph;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Ability
{
    public class AbilityDetail
    {
        public FeatType FeatType { get; set; }
        public LocaleString Name { get; set; }
        public LocaleString Description { get; set; }
        public AbilityCategoryType Category { get; set; }
        public AbilityActivationAction ActivationAction { get; set; }
        public AbilityRetargetActivatorAction RetargetActivatorAction { get; set; }
        public AbilityImpactAction ImpactAction { get; set; }
        public AbilityActivationDelayAction ActivationDelay { get; set; }
        public AbilityRecastDelayAction RecastDelay { get; set; }
        public AbilityCustomValidationAction CustomValidation { get; set; }
        public AbilityEquippedAction AbilityEquippedAction { get; set; }
        public AbilityUnequippedAction AbilityUnequippedAction { get; set; }
        public AbilityIsToggledAction AbilityIsToggledAction { get; set; }
        public AbilityToggleAction AbilityToggleAction { get; set; }
        public int EPRequired { get; set; }
        public int TPRequired { get; set; }
        public VisualEffectType ActivationVisualEffect { get; set; }
        public RecastGroup RecastGroup { get; set; }
        public AbilityActivationType ActivationType { get; set; }
        public AnimationType AnimationType { get; set; }
        public ResistType ResistType { get; set; }
        public float MaxRange { get; set; }
        public bool IsHostileAbility { get; set; }
        public bool DisplaysActivationMessage { get; set; }
        public int ResonanceCost { get; set; }
        public string IconResref { get; set; }
        public StatGroup StatGroup { get; set; }
        public AbilityTelegraphAction TelegraphAction { get; set; }
        public TelegraphType TelegraphType { get; set; }
        public Vector2 TelegraphSize { get; set; }
        public SkillType WeaponSkillType { get; set; }
        public int SkillLevelRequired { get; set; }
        public Type PassiveWeaponSkillStatusEffectType { get; set; }

        public AbilityDetail()
        {
            ActivationType = AbilityActivationType.Casted;
            ActivationVisualEffect = VisualEffectType.None;
            AnimationType = AnimationType.Invalid;
            Category = AbilityCategoryType.Invalid;
            EPRequired = 0;
            TPRequired = 0;
            MaxRange = 5.0f;
            IsHostileAbility = false;
            DisplaysActivationMessage = true;
            StatGroup = new ItemStatGroup();
            TelegraphType = TelegraphType.None;
            WeaponSkillType = SkillType.Invalid;
            SkillLevelRequired = 0;
        }
    }
}
