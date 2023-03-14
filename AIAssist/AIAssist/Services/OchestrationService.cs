using AIAssist.Brokers.GraphApis;
using Azure;

namespace AIAssist.Services
{
    public partial class OchestrationService
    {
        private IGraphBroker graphBroker;
        private IOpenAIBroker openAIBroker;

        public readonly List<string> toDoTask;

        private string model = "davinci:ft-personal-2023-03-14-03-20-49";

        public OchestrationService(IGraphBroker graphBroker, IOpenAIBroker openAIBroker)
        {
            this.graphBroker = graphBroker;
            this.openAIBroker = openAIBroker;
            this.toDoTask = new List<string>();
        }

        public async Task Test()
        {
            // get todo task list
            var response = await this.graphBroker.GetCurrentUserToDoTaskListsAsync();
            var stringResponse = await response.Content.ReadAsStringAsync(); // get list ids

            // getting task for specific list id
            var id = "AQMkADRmZgBlYTVhYi03NTFjLTRmYzEtODE3Yy1kMzY0N2EyZmEyYmUALgAAA2A-lPQrrB9IpmsRCIJk-OQBAB1CdU0JVyNNiSGhL84CUJgAAAIBEgAAAA==";
            var taskResponse = await this.graphBroker.GetCurrentUserToDoTasksAsync(id);
            var stringTaskResponse = await taskResponse.Content.ReadAsStringAsync();

            // get openAi response
            var prompt = "Schedule a meeting with James Martinez, discussing financial reporting, on Thursday at 2 pm.";
            var openAiResponse = await this.OchestrateOpenAIResponse(prompt);

            // schedule meeting
            Console.Write("Done.");
        }

        //public async Task OchestrateMeetingSchedulingAsync();
    }
}
