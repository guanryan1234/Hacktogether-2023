using AIAssist.Brokers.GraphApis;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Search;
using System.Runtime.CompilerServices;

namespace AIAssist.Services.Foundations.Graph
{
    public class GraphService : IGraphService
    {
        private IGraphBroker graphBroker;

        public GraphService(IGraphBroker graphBroker)
        {
            this.graphBroker = graphBroker;
        }

        public async Task CreateCurrentUserMeetingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> RetrieveCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TodoTaskList>> RetrieveCurrentUserToDoTaskListsAsync()
        {
            var httpResponseMessage = await this.graphBroker.GetCurrentUserToDoTaskListsAsync();
            var taskListsJson = httpResponseMessage.Content.ReadAsStringAsync();
            
        }

        public Task<TodoTask> RetrieveCurrentUserToDoTasksAsync(string listId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DateTimeOffset>> RetrieveMeetingAvailabilityAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> RetrieveUserBasedOnTokenSearchAsync(Action<SearchRequestBuilder.SearchRequestBuilderGetRequestConfiguration> resquestParams)
        {
            throw new NotImplementedException();
        }

        public async Task Test()
        {

        } 
    }
}
