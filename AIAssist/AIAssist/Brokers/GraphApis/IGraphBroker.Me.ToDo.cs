using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<TodoTaskListCollectionResponse> GetCurrentUserToDoTaskAsync();
    }
}
