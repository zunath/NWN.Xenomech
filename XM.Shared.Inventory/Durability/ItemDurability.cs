using System;
using XM.Shared.API.Constants;

namespace XM.Inventory.Durability
{
    internal class ItemDurability
    {
        private const int DurabilityCap = 500;

        public uint Item { get; }
        private int _currentDurability;

        public int CurrentDurability
        {
            get => _currentDurability;
            set
            {
                _currentDurability = value;
                if (_currentDurability < 0)
                    _currentDurability = 0;
                else if (_currentDurability > DurabilityCap)
                    _currentDurability = DurabilityCap;
            }
        }
        public int MaxDurability { get; set; }
        public float Condition { get; private set; }

        public ItemDurability(uint item)
        {
            Item = item;
            Condition = 1f;
            LoadProperties();
        }

        private void LoadProperties()
        {
            for (var ip = GetFirstItemProperty(Item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(Item))
            {
                var type = GetItemPropertyType(ip);

                if (type == ItemPropertyType.Durability)
                {
                    CurrentDurability = GetItemPropertyCostTableValue(ip);
                    MaxDurability = GetItemPropertyParam1Value(ip) * 10;
                }
                else if (type == ItemPropertyType.Condition)
                {
                    Condition = 1f - GetItemPropertyCostTableValue(ip) * 0.01f;
                }
            }
        }

        public void SaveProperties()
        {
            var maxDurabilityId = MaxDurability / 10;
            var durabilityId = CurrentDurability;

            var durability = ItemPropertyCustom(ItemPropertyType.Durability, -1, durabilityId, maxDurabilityId);
            BiowareXP2.IPSafeAddItemProperty(Item, durability, 0f, AddItemPropertyPolicy.ReplaceExisting, true, true);

            ApplyCondition();
        }

        private void RemoveCondition()
        {
            for (var ip = GetFirstItemProperty(Item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(Item))
            {
                if (GetItemPropertyType(ip) == ItemPropertyType.Condition)
                {
                    RemoveItemProperty(Item, ip);
                }
            }

            Condition = 1f;
        }

        private void ApplyCondition()
        {
            const float MaxPenalty = -70;
            const float SafeThreshold = 0.75f;

            var threshold = MaxDurability * SafeThreshold;

            if (CurrentDurability >= threshold)
            {
                RemoveCondition();
                return;
            }

            var conditionPenalty = (int)(MaxPenalty * (1 - Math.Pow(CurrentDurability / threshold, 2)));
            var conditionId = Math.Abs(conditionPenalty); // Directly using penalty as ID

            if (conditionId <= 1) // If penalty is -1% or better, remove condition
            {
                RemoveCondition();
                return;
            }

            var condition = ItemPropertyCustom(ItemPropertyType.Condition, -1, conditionId);
            BiowareXP2.IPSafeAddItemProperty(Item, condition, 0f, AddItemPropertyPolicy.ReplaceExisting, true, true);

            Condition = 1f - conditionId * 0.01f;
        }
    }
}
