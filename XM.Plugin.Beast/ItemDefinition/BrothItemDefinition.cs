using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Beast;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Item.ItemDefinition
{
    [ServiceBinding(typeof(IItemListDefinition))]
    internal class BrothItemDefinition: IItemListDefinition
    {
        private const string BeastTypeIdVariable = "BEAST_TYPE_ID";
        private readonly ItemBuilder _builder = new();
        private readonly BeastService _beast;

        public BrothItemDefinition(BeastService beast)
        {
            _beast = beast;
        }

        public Dictionary<string, ItemDetail> BuildItems()
        {
            Broth();

            return _builder.Build();
        }

        private void Broth()
        {
            _builder.Create("BROTH")
                .Delay(2f)
                .ReducesItemCharge()
                .ValidationAction((user, item, target, location, index) =>
                {
                    var beastTypeId = GetLocalInt(item, BeastTypeIdVariable);
                    if (!Enum.IsDefined(typeof(BeastType), beastTypeId))
                        return LocaleString.InvalidBeastTypeIdError.ToLocalizedString();

                    var canSummon = _beast.CanSummonBeast(user, (BeastType)beastTypeId);
                    if (!string.IsNullOrWhiteSpace(canSummon))
                        return canSummon;

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location, index) =>
                {
                    var beastType = (BeastType)GetLocalInt(item, BeastTypeIdVariable);
                    _beast.SummonBeast(user, beastType);
                });
        }
    }
}
