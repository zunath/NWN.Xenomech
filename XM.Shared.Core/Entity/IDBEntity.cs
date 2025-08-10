using System;

namespace XM.Shared.Core.Entity
{
    public interface IDBEntity
    {
        string Id { get; set; }

        DateTime DateCreated { get; set; }

        string EntityType { get; set; }
    }
}
