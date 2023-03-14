using AIAssist.Brokers.GraphApis;

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

        public async Task OchestrateSchedulingMeeting()
        {
            // schedule meeting
        }

       
    }
}
