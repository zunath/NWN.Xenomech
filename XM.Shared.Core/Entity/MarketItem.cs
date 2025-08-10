using System;
using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class MarketItem : EntityBase
    {
        [Indexed]
        public string PlayerId { get; set; }
        [Indexed]
        public string SellerName { get; set; }
        [Indexed]
        public int Price { get; set; }
        [Indexed]
        public bool IsListed { get; set; }
        [Indexed]
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Data { get; set; }
        public int Quantity { get; set; }
        public string IconResref { get; set; }
        [Indexed]
        public int CategoryId { get; set; }
        public DateTime? DateListed { get; set; }
    }
}



