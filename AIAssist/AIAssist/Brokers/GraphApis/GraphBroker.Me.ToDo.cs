using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<List<TodoTaskList>> GetCurrentUserToDoTaskListsAsync()
        {
            var httpResponseMessage = await this.httpClient.GetAsync("me/todo/lists");
            var taskListsJson = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync())["value"];
            return JsonConvert.DeserializeObject<List<TodoTaskList>>(taskListsJson.ToString());
        }

        public async Task<List<TodoTask>> GetCurrentUserToDoTasksAsync(string taskListId)
        {
            var httpResponseMessage = await this.httpClient.GetAsync($"me/todo/lists/{taskListId}/tasks");
            var taskListJson = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync())["value"];
            return JsonConvert.DeserializeObject<List<TodoTask>>(taskListJson.ToString());
        }
        public async Task<HttpStatusCode> PatchCurrentUserToDoTaskAsync(string taskListId, string taskId, string todoTaskData)
        {
            var content = new StringContent(todoTaskData, Encoding.UTF8, "application/json");
            var statusCode = (await this.httpClient.PatchAsync($"me/todo/lists/{taskListId}/tasks/{taskId}", content)).StatusCode;
            return statusCode;
        }
    }
}
