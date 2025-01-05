using System;

namespace XM.Data.Shared
{
    public class EntityBase: IDBEntity
    {
        [Indexed]
        public string Id { get; set; }

        public DateTime DateCreated { get; set; }

        [Indexed]
        public string EntityType { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            DateCreated = DateTime.UtcNow;
            EntityType = GetType().Name;
        }
    }
}
