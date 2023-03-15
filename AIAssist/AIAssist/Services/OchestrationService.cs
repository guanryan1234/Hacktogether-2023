using AIAssist.Brokers.GraphApis;
using AIAssist.Models;
using AIAssist.Services.Foundations.Graph;
using AIAssist.Services.Foundations.OpenAI;
using Azure;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.Today;
using Microsoft.Graph.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AIAssist.Services
{
    public partial class OchestrationService
    {
        private IGraphService graphService;
        private IOpenAIService openAIService;

        public readonly List<string> ToDoTasksAssisted;
        public Person CurrentPerson;

        public OchestrationService(IOpenAIService openAIService, IGraphService graphService)
        {
            this.graphService = graphService;
            this.openAIService = openAIService;
            this.ToDoTasksAssisted = new List<string>();
        }

        public async Task PopulateUserData()
        {
            CurrentPerson = await this.graphService.RetrieveCurrentUserAsync();
        }

        public async Task OchestrateAssistAsync()
        {
            var taskLists = await this.graphService.RetrieveCurrentUserToDoTaskListsAsync();

            foreach(var taskList in taskLists)
            {
                var tasks = await this.graphService.RetrieveCurrentUserToDoTasksAsync(taskList);
                foreach(var task in tasks)
                {
                    if (task.Title.ToLower().Contains("schedule"))
                    {
                        // STRICKLY TESTING
                        if (IsValidTaskStatus(task))
                        {
                            {
                                try
                                {
                                    // Get Ai read meeting details
                                    var meetingDetails = await TryGetRetrieveStreamedCompletionAsync(task);

                                    if (meetingDetails != null)
                                    {
                                        // get attendee 
                                        var attendee = await TryGetAttendeeAsync(meetingDetails.Who);

                                        // create the meeting 
                                        var onlineEvent = await MapToOnlineMeetingAsync(meetingDetails);

                                        // schedule meeting online
                                        await ScheduleMeeting(task, taskList, onlineEvent, attendee);

                                        // update task
                                        task.Status = Microsoft.Graph.Models.TaskStatus.Completed;
                                        ToDoTasksAssisted.Add(task.Title);
                                        await this.graphService.UpdateCurrentUserToDoTaskAsync(taskList, task);
                                    }
                                }
                                catch (ArgumentNullException exception)
                                {
                                    // excpected when meeting details are empty
                                }
                                catch(OperationCanceledException exception)
                                {
                                    // YEEEE
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
