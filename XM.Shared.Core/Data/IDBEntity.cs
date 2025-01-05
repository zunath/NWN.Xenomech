using System;

namespace XM.Shared.Core.Data
{
    public interface IDBEntity
    {
        string Id { get; set; }

        DateTime DateCreated { get; set; }

        string EntityType { get; set; }
    }
}
