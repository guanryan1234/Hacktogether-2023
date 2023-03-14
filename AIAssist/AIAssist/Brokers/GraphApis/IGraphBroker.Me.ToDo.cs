using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<HttpResponseMessage> GetCurrentUserToDoTaskListsAsync();

        public Task<HttpResponseMessage> GetCurrentUserToDoTasksAsync(string listId);
    }
}
