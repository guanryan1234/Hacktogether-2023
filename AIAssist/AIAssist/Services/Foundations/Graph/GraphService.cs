using AIAssist.Brokers.GraphApis;
using AIAssist.Models;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Search;
using Newtonsoft.Json;
using System.Net;

namespace AIAssist.Services.Foundations.Graph
{
    public class GraphService : IGraphService
    {
        private string CUSTOM_CONTENT = "This meeting was created by AI ASSIST.";
        private IGraphBroker graphBroker;

        public GraphService(IGraphBroker graphBroker)
        {
            this.graphBroker = graphBroker;
        }

        public async Task<Person> RetrieveCurrentUserAsync()
        {
            var person = await this.graphBroker.GetCurrentUserAsync();
            ValidatePerson(person); 
            return person;
        }

        public async Task<List<TodoTaskList>> RetrieveCurrentUserToDoTaskListsAsync()
        {
            var todoTaskLists = await this.graphBroker.GetCurrentUserToDoTaskListsAsync();
            ValidTaskLists(todoTaskLists);
            return todoTaskLists;
        }

        private void ValidTaskLists(List<TodoTaskList>? todoTaskLists)
        {
            if(todoTaskLists == null) 
                throw new ArgumentNullException("No Tasks List Returned");
        }

        public async Task<List<TodoTask>> RetrieveCurrentUserToDoTasksAsync(TodoTaskList todoTaskList)
        {
            ValidateToDoTasksList(todoTaskList);
            var toDoTaskList = await this.graphBroker.GetCurrentUserToDoTasksAsync(todoTaskList.Id);
            
            return toDoTaskList;
        }

        private void ValidateToDoTasksList(TodoTaskList toDoTaskList)
        {
            if (toDoTaskList == null)
                throw new ArgumentNullException("No Tasks Returned");
            if (string.IsNullOrEmpty(toDoTaskList.Id))
                throw new ArgumentNullException("Id for Task List is empty");
        }

        private void ValidateToDoTask(TodoTask toDoTask)
        {
            if (toDoTask == null)
                throw new ArgumentNullException("No To Do Task Returned");
            if (string.IsNullOrEmpty(toDoTask.Id))
                throw new ArgumentNullException("Id for To Do Task List is empty");
        }

        public Task<List<DateTimeOffset>> RetrieveMeetingAvailabilityAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Person> RetrieveUserBasedOnTokenSearchAsync(string personIdentifier)
        {
            var queryJson = $@"{{
                  ""requests"": [
                    {{
                      ""entityTypes"": [
                        ""person""
                      ],
                      ""query"": {{
                        ""queryString"": ""{personIdentifier}""
                      }},
                      ""from"": 0,
                      ""size"": 25
                    }}
                  ]
                }}";

            var person = await this.graphBroker.GetUserBasedOnTokenSearchAsync(queryJson);

            ValidatePerson(person);
            return person;
        }

        private void ValidatePerson(Person person)
        {
            if(person == null || string.IsNullOrWhiteSpace(person.DisplayName) || string.IsNullOrWhiteSpace(person.UserPrincipalName))
                throw new ArgumentNullException(nameof(person));
        }

        public async Task UpdateCurrentUserToDoTaskAsync(TodoTaskList todoTaskList, TodoTask todoTask)
        {
            ValidateToDoTasksList(todoTaskList);
            ValidateToDoTask(todoTask);
            var todoTaskData = JsonConvert.SerializeObject(todoTask);
            var statusCode = await this.graphBroker.PatchCurrentUserToDoTaskAsync(todoTaskList.Id, todoTask.Id, todoTaskData);
            ValidateTaskUpdateStatusCode(statusCode);
        }

        private void ValidateTaskUpdateStatusCode(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
                throw new ClientException("Request Not Complete");
        }

        public async Task<Event> CreateCurrentUserMeetingAsync(Event eve, Attendee attendee)
        {
            ValidateOnlineMeeting(eve);
            var eventJson = MapToEventJson(eve, attendee);
            return await this.graphBroker.PostCurrentUserMeetingAsync(eventJson);
        }

        private string MapToEventJson(Event eve, Attendee attendee)
        {
            var attendeeJson = "";

            if (attendee != null)
            {
                attendeeJson = @$"
                    ""attendees"": [
                        {{
                          ""emailAddress"": {{
                            ""address"":""{attendee.EmailAddress.Address}"",
                            ""name"": ""{attendee.EmailAddress.Name}""
                          }},
                          ""type"": ""required""
                        }}
                      ],";
            }

            var eventJson = @$"{{
                    ""isOnlineMeeting"": true,
                    ""subject"": ""{eve.Subject}"",
                    ""body"": {{
                        ""contentType"": ""HTML"",
                        ""content"": ""{CUSTOM_CONTENT}""
                    }},
                    ""start"": {{
                        ""dateTime"": ""{eve.Start.DateTime.ToString()}"",
                        ""timeZone"": ""Pacific Standard Time""
                    }},
                    ""end"": {{
                        ""dateTime"": ""{eve.End.DateTime.ToString()}"",
                        ""timeZone"": ""Pacific Standard Time""
                    }},
                    {attendeeJson}
                }}";

            return eventJson;
        }

        private void ValidateOnlineMeeting(Event eve)
        {
            if(eve == null)
                throw new ArgumentOutOfRangeException(nameof(eve));
        }
    }
}
