namespace AIAssist.Services
{
    public partial class OchestrationService
    {
        public async Task<string> OchestrateOpenAIResponse(string prompt)
        {
            string response = await this.openAIBroker.StreamCompletionAsync(prompt, this.model);
            var result = ParseOpenAIResponse(response);
            return result;
        }

        private string ParseOpenAIResponse(string response)
        {
            int start = response.IndexOf('{');
            int end = response.IndexOf('}');

            string parsedResponse = response.Substring(start, (end - start + 1));
            return parsedResponse;
        }
    }
}
