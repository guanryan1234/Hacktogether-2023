using Microsoft.Graph;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        private GraphServiceClient graphServiceClient;

        public GraphBroker(GraphServiceClient graphServiceClient)
        {
            this.graphServiceClient = graphServiceClient;
        }
    }
}
