using Microsoft.Graph.Models;
using System.Net;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<List<TodoTaskList>> GetCurrentUserToDoTaskListsAsync();

        public Task<List<TodoTask>> GetCurrentUserToDoTasksAsync(string taskListId);

        public Task<HttpStatusCode> PatchCurrentUserToDoTaskAsync(string taskListId, string taskId, string todoTaskData);
        
    }
}
