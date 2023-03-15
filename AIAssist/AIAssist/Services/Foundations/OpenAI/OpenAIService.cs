using AIAssist.Brokers.OpenAIApis;
using AIAssist.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AIAssist.Services.Foundations.OpenAI
{
    public class OpenAIService : IOpenAIService
    {
        private string MODEL = "davinci:ft-personal-2023-03-14-03-20-49";
        private IOpenAIBroker openAIBroker;

        public OpenAIService(IOpenAIBroker openAIBroker)
        {
            this.openAIBroker = openAIBroker;
        }

        public async Task<MeetingDetails> RetrieveStreamedCompletionAsync(string prompt)
        {
            var dateTimeHandler = new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy"};
            var result = ParseOpenAIResponse(await this.openAIBroker.StreamCompletionAsync(prompt, MODEL));
            var meetingDetails = JsonConvert.DeserializeObject<MeetingDetails>(result, dateTimeHandler);
            ValidateMeetingDetails(meetingDetails);
            return meetingDetails;
        }

        private string ParseOpenAIResponse(string response)
        {
            var subResponse = "";
            int start = response.IndexOf('{');
            int end = response.IndexOf('}');

            if(start != -1 && end != -1 && start < end)
            {
                subResponse = response.Substring(start, end - start + 1);
            }

            return subResponse;
        }

        private void ValidateMeetingDetails(MeetingDetails? meetingDetails)
        {
            if(meetingDetails == null)
                throw new ArgumentOutOfRangeException(nameof(meetingDetails));
        }
    }
}
