using Azure.Search.Documents.Indexes;
using Azure.Search.Documents;
using Azure;
using System;
using NoteTakingApp.Models;
using System.Threading.Tasks;
using System.Windows.Documents;
using Azure.Search.Documents.Models;
using System.Collections.Generic;

namespace NoteTakingApp.Services
{
    public class Search<T>
    {
        SearchIndexClient _indexClient;
        SearchClient _searchClient;
        private void InitSearch()
        {
            // Read the values from appsettings.json
            string searchServiceUri = Properties.Settings.Default.SearchServiceUri;
            string queryApiKey = Properties.Settings.Default.SearchServiceQueryApiKey;

            // Create a service and index client.
            _indexClient = new SearchIndexClient(new Uri(searchServiceUri), new AzureKeyCredential(queryApiKey));
            _searchClient = _indexClient.GetSearchClient(Properties.Settings.Default.NoteIndex);
        }

        public async Task<SearchData<T>> RunQueryAsync(SearchData<T> model)
        {
            InitSearch();

            var options = new SearchOptions()
            {
                IncludeTotalCount = true
            };

            // Enter Hotel property names to specify which fields are returned.
            // If Select is empty, all "retrievable" fields are returned.
            options.QueryType = SearchQueryType.Full;
            // For efficiency, the search call should be asynchronous, so use SearchAsync rather than Search.
            model.resultList = await _searchClient.SearchAsync<T>(model.searchText, options).ConfigureAwait(true);

            // Display the results.
            return model;
        }

        public List<T> Results(List<SearchResult<T>> searchResults)
        {
            if(searchResults == null)
            {
                return null;
            }
            else
            {
                List<T> results = new List<T>();
                foreach(var searchResult in searchResults)
                {
                    results.Add(searchResult.Document);

                }
                return results;
            }
        }
    }
}

