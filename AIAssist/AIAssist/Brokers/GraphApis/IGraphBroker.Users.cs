using Microsoft.Graph.Models;
using Microsoft.Graph.Search.Query;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<Person> GetUserBasedOnTokenSearchAsync(string queryJson);

        public Task<Person> GetCurrentUserAsync();
    }
}
