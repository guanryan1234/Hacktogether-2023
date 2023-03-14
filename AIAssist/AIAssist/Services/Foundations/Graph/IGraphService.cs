using Microsoft.Graph.Models;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Services.Foundations.Graph
{
    public interface IGraphService
    {
        public Task CreateCurrentUserMeetingAsync();

        public Task<List<DateTimeOffset>> RetrieveMeetingAvailabilityAsync();

        public Task<List<TodoTaskList>> RetrieveCurrentUserToDoTaskListsAsync();

        public Task<TodoTask> RetrieveCurrentUserToDoTasksAsync(string listId);

        public Task<User> RetrieveUserBasedOnTokenSearchAsync(Action<SearchRequestBuilderGetRequestConfiguration> resquestParams);

        public Task<HttpResponseMessage> RetrieveCurrentUserAsync();
    }
}
