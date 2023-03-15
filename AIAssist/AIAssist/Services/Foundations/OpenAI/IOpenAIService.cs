using AIAssist.Models;

namespace AIAssist.Services.Foundations.OpenAI
{
    public interface IOpenAIService
    {
        public Task<MeetingDetails> RetrieveStreamedCompletionAsync(string prompt);
    }
}
