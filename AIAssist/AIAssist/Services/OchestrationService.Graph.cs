using AIAssist.Models;
using Microsoft.Graph.Models;

namespace AIAssist.Services
{
    public partial class OchestrationService
    {
        public async Task GetUserToDoTask()
        {
            //var todoTaskListCollectionResponse = await this.graphBroker.Get();
            await Task.Delay(500);
            //var result = await this.graphServiceClient.Me.FindMeetingTimes.PostAsync(body);
            return;
        }

        private bool IsScheduleTask(string task)
        {
            var isScheduleTask = false;

            if (task.ToLower().Contains("schedule"))
                isScheduleTask = true;

            return isScheduleTask;
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

        private async Task<Attendee> TryGetAttendeeAsync(string who)
        {
            try
            {
                return await GetAttendeeAsync(who);
            }
            catch(ArgumentNullException exception)
            {
                return null;
            }
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
            if (startTime > DateTimeOffset.Now)
            {
                return DateTimeOffset.UtcNow;
            }

            return startTime;
        }

        private bool IsValidTaskStatus(TodoTask toDoTask)
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
    }
}
