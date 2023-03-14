using Microsoft.Graph;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        private HttpClient httpClient;

        public GraphBroker(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
    }
}
