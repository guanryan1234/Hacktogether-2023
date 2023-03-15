using OpenAI;

namespace AIAssist.Brokers.OpenAIApis
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
