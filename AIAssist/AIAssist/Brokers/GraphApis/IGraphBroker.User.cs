using Microsoft.Graph.Models;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<SearchEntity> GetUserBasedOnTokenSearchAsync(Action<SearchRequestBuilderGetRequestConfiguration> resquestParams);

        public Task<User> GetCurrentUserAsync();
    }
}
