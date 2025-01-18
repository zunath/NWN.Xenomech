using Anvil.API;
using Anvil.Services;
using NWN.Native.API;
using XM.Shared.API.Constants;
using SavingThrow = Anvil.API.SavingThrow;

namespace XM.Combat.NativeOverride
{
    [ServiceBinding(typeof(OnFortitudeSavingThrow))]
    internal sealed unsafe class OnFortitudeSavingThrow
    {
        [NativeFunction("_ZN17CNWSCreatureStats18GetFortSavingThrowEi", "")]
        private delegate byte GetFortitudeSavingThrowHook(void* thisPtr, int bExcludeEffectBonus);

        // ReSharper disable once NotAccessedField.Local
        private readonly FunctionHook<GetFortitudeSavingThrowHook> _getFortitudeFunctionHook;

        public OnFortitudeSavingThrow(HookService hook)
        {
            _getFortitudeFunctionHook = hook.RequestHook<GetFortitudeSavingThrowHook>(OnGetFortitudeSavingThrow, HookOrder.Late);
        }

        private byte OnGetFortitudeSavingThrow(void* thisPtr, int bExcludeEffectBonus)
        {
            var stats = CNWSCreatureStats.FromPointer(thisPtr);
            var rules = NWNXLib.Rules();

            var effectBonus = 0;
            byte modifier = 0;

            if (bExcludeEffectBonus == 0)
                effectBonus = stats.m_pBaseCreature
                    .GetTotalEffectBonus(3, // 3 = EFFECT_TYPE_SAVING_THROW
                        null,
                        0,
                        0,
                        (int)SavingThrow.Fortitude);

            if (stats.HasFeat((ushort)FeatType.LuckOfHeroes) == 1)
                modifier += (byte)rules.GetRulesetIntEntry(
                    new CRulesKeyHash("LUCKOFHEROES_SAVE_BONUS"), 1);

            if (stats.HasFeat((ushort)FeatType.PrestigeDarkBlessing) == 1)
                modifier += (byte)stats.m_nCharismaModifier;

            return (byte)(stats.m_nStrengthModifier +
                           stats.GetBaseFortSavingThrow() +
                           stats.m_nFortSavingThrowMisc +
                           effectBonus +
                           modifier);
        }
    }
}
