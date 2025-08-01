using System;
using System.Collections.Generic;
using System.Reflection;
using Anvil.Services;
using NLog;
using XM.Shared.Core.Configuration;

namespace XM.Shared.Core.Data
{
    [ServiceBinding(typeof(DBService))]
    public class DBService
    {
        private readonly IntegratedDBService _integratedDbService;

        public DBService(IntegratedDBService integratedDbService)
        {
            _integratedDbService = integratedDbService;
        }

        /// <summary>
        /// Retrieves a specific object in the database by an arbitrary key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve</typeparam>
        /// <param name="id">The arbitrary key the data is stored under</param>
        /// <returns>The object stored in the database under the specified key</returns>
        public T Get<T>(string id) where T : IDBEntity
        {
            return _integratedDbService.Get<T>(id);
        }

        public void Set<T>(T entity) where T : IDBEntity
        {
            _integratedDbService.Set<T>(entity);
        }

        /// <summary>
        /// Searches the Redis DB for records matching the query criteria.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="query">The query to run.</param>
        /// <returns>An enumerable of entities matching the criteria.</returns>
        public IEnumerable<T> Search<T>(DBQuery query) where T : IDBEntity
        {
            return _integratedDbService.Search<T>(query);
        }

        public int SearchCount<T>(DBQuery query) where T : IDBEntity
        {
            return _integratedDbService.SearchCount<T>(query);
        }

        public void Delete<T>(string key) where T : IDBEntity
        {
            _integratedDbService.Delete<T>(key);
        }
    }
}
