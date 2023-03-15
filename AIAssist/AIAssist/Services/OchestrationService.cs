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

        public async Task Test()
        {
            CurrentPerson = await this.graphService.RetrieveCurrentUserAsync();
            var taskLists = await this.graphService.RetrieveCurrentUserToDoTaskListsAsync();

            foreach(var taskList in taskLists)
            {
                var tasks = await this.graphService.RetrieveCurrentUserToDoTasksAsync(taskList);
                foreach(var task in tasks)
                {
                    if (task.Title.ToLower().Contains("schedule")){
                        // STRICKLY TESTING
                        if (IsValidTask(task))
                        {
                            {
                                try
                                {
                                    // Get Ai read meeting details
                                    var meetingDetails = await this.openAIService.RetrieveStreamedCompletionAsync(task.Title);

                                    // get attendee 
                                    var attendee = await GetAttendeeAsync(meetingDetails.Who);

                                    // create the meeting 
                                    var onlineEvent = await MapToOnlineMeetingAsync(meetingDetails);

                                    // schedule meeting online
                                    await ScheduleMeeting(task, taskList, onlineEvent, attendee);

                                    // update task
                                    task.Status = Microsoft.Graph.Models.TaskStatus.Completed;
                                    ToDoTasksAssisted.Add(task.Title);   
                                    await this.graphService.UpdateCurrentUserToDoTaskAsync(taskList, task);

                                    // reset for next test
                                    task.Status = Microsoft.Graph.Models.TaskStatus.NotStarted;
                                    await this.graphService.UpdateCurrentUserToDoTaskAsync(taskList, task);
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
                        else
                        {
                            // reset for next test
                            task.Status = Microsoft.Graph.Models.TaskStatus.NotStarted;
                            await this.graphService.UpdateCurrentUserToDoTaskAsync(taskList, task);
                        }
                    }
                }
            }
        }

        private async Task ScheduleMeeting(TodoTask todoTask, TodoTaskList todoTaskList, Event eve, Attendee attendee)
        {
            try
            {
                // update task 
                todoTask.Status = Microsoft.Graph.Models.TaskStatus.InProgress;
                await this.graphService.UpdateCurrentUserToDoTaskAsync(todoTaskList, todoTask);

                // create meeting
                await this.graphService.CreateCurrentUserMeetingAsync(eve, attendee);
            }
            catch (Exception exception)
            {
                todoTask.Status = Microsoft.Graph.Models.TaskStatus.NotStarted;
                await this.graphService.UpdateCurrentUserToDoTaskAsync(todoTaskList, todoTask);
                throw new OperationCanceledException("YEEEE");
            }
        }

        private async Task<Event> MapToOnlineMeetingAsync(MeetingDetails meetingDetails)
        {
            var suggestedTime = CreateAdjustedStartTimeWindow(meetingDetails.When);

            var eve = new Event()
            {
                Start = suggestedTime.ToDateTimeTimeZone(),
                End = suggestedTime.AddHours(1).ToDateTimeTimeZone(),
                Subject = meetingDetails.What,
                IsOnlineMeeting = true,
            };

            return eve;
        }

        private async Task<Attendee> GetAttendeeAsync(string who)
        {
            var result = await this.graphService.RetrieveUserBasedOnTokenSearchAsync(who);

            var attendee = new Attendee()
            {
                EmailAddress = new EmailAddress
                {
                    Address = result.UserPrincipalName,
                    Name = result.DisplayName
                },
                Type = AttendeeType.Required
            };

            return attendee;
        }

        private DateTimeOffset CreateAdjustedStartTimeWindow(DateTimeOffset startTime)
        {
            if(startTime > DateTimeOffset.Now)
            {
                return DateTimeOffset.UtcNow;
            }

            return startTime;
        }

        private bool IsValidTask(TodoTask toDoTask)
        {
            var isValidTask = false;

            switch (toDoTask.Status)
            {
                case Microsoft.Graph.Models.TaskStatus.NotStarted:
                    isValidTask = true;
                    break;
                default:
                    break;
            }

            return isValidTask;
        }

        //public async Task OchestrateMeetingSchedulingAsync();
    }
}
