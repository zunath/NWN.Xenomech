﻿using System;

namespace XM.Inventory.Loot
{
    public class LootTableItem
    {
        public string Resref { get; set; }
        public int MaxQuantity { get; set; }
        public int Weight { get; set; }
        public bool IsRare { get; set; }
        public Action<uint> OnSpawn { get; set; }

        public LootTableItem(
            string resref, 
            int maxQuantity, 
            int weight, 
            bool isRare)
        {
            Resref = resref;
            MaxQuantity = maxQuantity;
            Weight = weight;
            IsRare = isRare;
        }
    }
}
