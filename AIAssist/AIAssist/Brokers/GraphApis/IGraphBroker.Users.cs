using Microsoft.Graph.Models;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<HttpResponseMessage> GetUserBasedOnTokenSearchAsync(Action<SearchRequestBuilderGetRequestConfiguration> resquestParams);

        public Task<HttpResponseMessage> GetCurrentUserAsync();
    }
}
