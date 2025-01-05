using System;

namespace XM.Data.Shared
{
    public interface IDBEntity
    {
        string Id { get; set; }

        DateTime DateCreated { get; set; }

        string EntityType { get; set; }
    }
}
