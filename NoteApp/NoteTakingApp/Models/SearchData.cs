using Azure.Search.Documents.Models;


namespace NoteTakingApp.Models
{
    public class SearchData<T>
    {
        public string searchText { get; set; }

        // The list of results.
        public SearchResults<T> resultList;
    }
}
