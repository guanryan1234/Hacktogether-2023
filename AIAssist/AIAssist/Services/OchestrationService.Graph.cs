namespace AIAssist.Services
{
    public partial class OchestrationService
    {
        public async Task GetUserToDoTask()
        {
            var todoTaskListCollectionResponse = await this.graphBroker.GetCurrentUserToDoTaskAsync();

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
