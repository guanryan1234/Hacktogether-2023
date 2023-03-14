using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<HttpResponseMessage> GetCurrentUserToDoTaskListsAsync()
        {
            var result = await this.httpClient.GetAsync("me/todo/lists");
            return result;
        }

        public async Task<HttpResponseMessage> GetCurrentUserToDoTasksAsync(string listId)
        {
            var result = await this.httpClient.GetAsync($"me/todo/lists/{listId}/tasks");
            return result;
        }
    }
}
