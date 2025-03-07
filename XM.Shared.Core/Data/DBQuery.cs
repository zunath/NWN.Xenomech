﻿using System;
using System.Collections.Generic;
using System.Text;
using NRediSearch;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Data
{
    public class DBQuery
    {
        public DBQueryData QueryData { get; set; }

        /// <summary>
        /// Adds a filter based on a field's name for the given text.
        /// </summary>
        /// <param name="fieldName">The name of the field to search for</param>
        /// <param name="search">The text to search for</param>
        /// <param name="allowPartialMatches">If true, partial matches are accepted.</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery AddFieldSearch(string fieldName, string search, bool allowPartialMatches)
        {
            if (allowPartialMatches)
                search += "*";

            QueryData.FieldSearches.Add(fieldName, new SearchCriteria(search));

            return this;
        }

        /// <summary>
        /// Adds a filter based on a field's name for the given text.
        /// Will search for any matches in the provided list of integers.
        /// </summary>
        /// <param name="fieldName">The name of the field to search for</param>
        /// <param name="search">The list of Ids to search for</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery AddFieldSearch(string fieldName, IEnumerable<int> search)
        {
            var searchText = string.Join("|", search);
            var criteria = new SearchCriteria(searchText)
            {
                SkipEscaping = true
            };

            QueryData.FieldSearches.Add(fieldName, criteria);

            return this;
        }

        /// <summary>
        /// Adds a filter based on a field's name for the given text.
        /// Will search for any matches in the provided list of strings.
        /// </summary>
        /// <param name="fieldName">The name of the field to search for</param>
        /// <param name="search">The list of values to search for</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery AddFieldSearch(string fieldName, IEnumerable<string> search)
        {
            var list = new List<string>();
            foreach (var s in search)
            {
                list.Add(RedisTokenHelper.EscapeTokens(s));
            }

            var searchText = string.Join("|", list);
            var criteria = new SearchCriteria(searchText)
            {
                SkipEscaping = true
            };

            QueryData.FieldSearches.Add(fieldName, criteria);

            return this;
        }

        /// <summary>
        /// Adds a filter based on a field's name for the given number.
        /// </summary>
        /// <param name="fieldName">The name of the field to search for</param>
        /// <param name="search">The number to search for</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery AddFieldSearch(string fieldName, int search)
        {
            QueryData.FieldSearches.Add(fieldName, new SearchCriteria(search.ToString()));

            return this;
        }

        /// <summary>
        /// Adds a filter based on a field's name for the given boolean.
        /// </summary>
        /// <param name="fieldName">The name of the field to search for</param>
        /// <param name="search">The value to search for</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery AddFieldSearch(string fieldName, bool search)
        {
            QueryData.FieldSearches.Add(fieldName, new SearchCriteria((search ? 1 : 0).ToString()));

            return this;
        }

        /// <summary>
        /// Determines the number of records and the number of records to skip.
        /// </summary>
        /// <param name="limit">The number of records to retrieve.</param>
        /// <param name="offset">The number of records to skip.</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery AddPaging(int limit, int offset)
        {
            QueryData.Limit = limit;
            QueryData.Offset = offset;

            return this;
        }

        /// <summary>
        /// Orders the result set by a given field name.
        /// </summary>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="isAscending">if true, sort will be in ascending order. Otherwise, descending order will be used.</param>
        /// <returns>A configured DBQuery</returns>
        public DBQuery OrderBy(string fieldName, bool isAscending = true)
        {
            QueryData.SortByField = fieldName;
            QueryData.IsAscending = isAscending;

            return this;
        }

        /// <summary>
        /// Builds an NRedisSearch query.
        /// </summary>
        /// <returns>An NRedisSearch query.</returns>
        public Query BuildQuery(string typeName, bool countsOnly = false)
        {
            var sb = new StringBuilder();

            // Exact filter on this type of entity.
            sb.Append($"@EntityType:\"{typeName}\"");

            // Filter by name/searchText
            foreach (var (name, criteria) in QueryData.FieldSearches)
            {
                var search = criteria.SkipEscaping
                    ? criteria.Text
                    : RedisTokenHelper.EscapeTokens(criteria.Text);

                sb.Append($" @{name}:{search}");
            }

            var query = new Query(sb.ToString());

            // If we're only retrieving the number of records this query will return
            // (before pagination is applied), set the limit to zero records.
            if (countsOnly)
            {
                query.Limit(0, 0);
            }
            else
            {
                // Apply pagination
                if (QueryData.Limit > 0)
                {
                    query.Limit(QueryData.Offset, QueryData.Limit);
                }
                // If no limit is specified, default to 50.
                // The default limit is 10 which is too small for our use case.
                else
                {
                    query.Limit(0, 50);
                }
            }

            if (!string.IsNullOrWhiteSpace(QueryData.SortByField))
            {
                query.SetSortBy(QueryData.SortByField, QueryData.IsAscending);
            }

            return query;
        }

        public DBQuery()
        {
            QueryData = new DBQueryData();
        }
    }
}
