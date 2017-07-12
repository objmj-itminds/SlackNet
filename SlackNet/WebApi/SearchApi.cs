﻿using System.Threading;
using System.Threading.Tasks;
using SlackNet.WebApi.Responses;
using Args = System.Collections.Generic.Dictionary<string, object>;

namespace SlackNet.WebApi
{
    public class SearchApi
    {
        private readonly WebApiClient _client;
        public SearchApi(WebApiClient client) => _client = client;

        /// <summary>
        /// Allows users and applications to search both messages and files in a single call.
        /// </summary>
        /// <param name="query">Search query. May contains booleans, etc.</param>
        /// <param name="sort">Return matches sorted by either score or timestamp.</param>
        /// <param name="sortDirection">Change sort direction to ascending or descending</param>
        /// <param name="highlight">Pass a value of True to enable query highlight markers.</param>
        /// <param name="count">Number of items to return per page.</param>
        /// <param name="page">Page number of results to return.</param>
        /// <param name="cancellationToken"></param>
        public Task<SearchResponse> All(
            string query,
            SortBy sort = SortBy.Score,
            SortDirection sortDirection = SortDirection.Descending,
            bool highlight = false,
            int count = 20,
            int page = 1,
            CancellationToken? cancellationToken = null
        ) =>
            _client.Get<SearchResponse>("search.all", new Args
                    {
                        { "query", query },
                        { "sort", sort },
                        { "sort_dir", sortDirection },
                        { "highlight", highlight },
                        { "count", count },
                        { "page", page }
                    },
                cancellationToken);

        /// <summary>
        /// Returns files matching a search query.
        /// </summary>
        /// <param name="query">Search query. May contains booleans, etc.</param>
        /// <param name="sort">Return matches sorted by either score or timestamp.</param>
        /// <param name="sortDirection">Change sort direction to ascending or descending</param>
        /// <param name="highlight">Pass a value of True to enable query highlight markers.</param>
        /// <param name="count">Number of items to return per page.</param>
        /// <param name="page">Page number of results to return.</param>
        /// <param name="cancellationToken"></param>
        public Task<FileSearchResponse> Files(
            string query,
            SortBy sort = SortBy.Score,
            SortDirection sortDirection = SortDirection.Descending,
            bool highlight = false,
            int count = 20,
            int page = 1,
            CancellationToken? cancellationToken = null
        ) =>
            _client.Get<FileSearchResponse>("search.files", new Args
                    {
                        { "query", query },
                        { "sort", sort },
                        { "sort_dir", sortDirection },
                        { "highlight", highlight },
                        { "count", count },
                        { "page", page }
                    },
                cancellationToken);

        /// <summary>
        /// Returns messages matching a search query.
        /// </summary>
        /// <param name="query">Search query. May contains booleans, etc.</param>
        /// <param name="sort">Return matches sorted by either score or timestamp.</param>
        /// <param name="sortDirection">Change sort direction to ascending or descending</param>
        /// <param name="highlight">Pass a value of True to enable query highlight markers.</param>
        /// <param name="count">Number of items to return per page.</param>
        /// <param name="page">Page number of results to return.</param>
        /// <param name="cancellationToken"></param>
        public Task<MessageSearchResponse> Messages(
            string query,
            SortBy sort = SortBy.Score,
            SortDirection sortDirection = SortDirection.Descending,
            bool highlight = false,
            int count = 20,
            int page = 1,
            CancellationToken? cancellationToken = null
        ) =>
            _client.Get<MessageSearchResponse>("search.messages", new Args
                    {
                        { "query", query },
                        { "sort", sort },
                        { "sort_dir", sortDirection },
                        { "highlight", highlight },
                        { "count", count },
                        { "page", page }
                    },
                cancellationToken);
    }
}