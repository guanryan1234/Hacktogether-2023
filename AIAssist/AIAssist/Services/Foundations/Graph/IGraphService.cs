using AIAssist.Models;
using Microsoft.Graph.Models;
using static Microsoft.Graph.Search.SearchRequestBuilder;

namespace AIAssist.Services.Foundations.Graph
{
    public interface IGraphService
    {
        public Task<List<DateTimeOffset>> RetrieveMeetingAvailabilityAsync();

        public Task<List<TodoTaskList>> RetrieveCurrentUserToDoTaskListsAsync();

        public Task<List<TodoTask>> RetrieveCurrentUserToDoTasksAsync(TodoTaskList todoTaskList);

        public Task<Person> RetrieveUserBasedOnTokenSearchAsync(string personIdentifier);

        public Task<Person> RetrieveCurrentUserAsync();

        public Task UpdateCurrentUserToDoTaskAsync(TodoTaskList todoTaskList, TodoTask todoTask);

        public Task<Event> CreateCurrentUserMeetingAsync(Event eve, Attendee attendee);
    }
}
