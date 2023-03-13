using OpenAI;

namespace AIAssist.Brokers.GraphApis
{
    public partial class OpenAIBroker : IOpenAIBroker
    {
        private OpenAIClient openAIClient;

        public OpenAIBroker(OpenAIClient openAIClient)
        {
            this.openAIClient = openAIClient;            
        }
    }
}
