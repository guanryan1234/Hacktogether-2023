using Microsoft.Graph.Models;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<HttpResponseMessage> GetCurrentUserAsync()
        {
            var response = await this.httpClient.GetAsync("me");
            return response;
        }

        public async Task<HttpResponseMessage> GetUserBasedOnTokenSearchAsync(Action<SearchRequestBuilderGetRequestConfiguration> resquestParams)
        {
            HttpContent httpContent = null;

            var response = await this.httpClient.PostAsync("search/query", httpContent);
            //return searchEntity;
            await Task.Delay(500);
            //var result = await this.graphServiceClient.Me.OnlineMeetings.PostAsync(body);
            return response;
        }
    }
}
