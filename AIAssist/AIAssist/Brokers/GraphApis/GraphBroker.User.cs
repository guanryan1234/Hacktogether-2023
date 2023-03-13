using Microsoft.Graph.Models;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<User> GetCurrentUserAsync()
        {
            return await this.graphServiceClient.Me.GetAsync();
        }

        public async Task<SearchEntity> GetUserBasedOnTokenSearchAsync(Action<SearchRequestBuilderGetRequestConfiguration> resquestParams)
        {
            var searchEntity = await this.graphServiceClient.Search.GetAsync(resquestParams);
            return searchEntity;
        }
    }
}
