using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
=       public async Task<TodoTaskListCollectionResponse> GetCurrentUserToDoTaskAsync(Todo body)
        {
            var result = await this.graphServiceClient.Me.Todo.Lists.GetAsync();
            return result;
        }
    }
}
