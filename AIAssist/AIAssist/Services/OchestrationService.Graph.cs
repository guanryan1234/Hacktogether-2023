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
    }
}
