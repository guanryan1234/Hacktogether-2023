using Microsoft.Graph.Me.FindMeetingTimes;
using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<HttpResponseMessage> PostCurrentUserMeetingAsync(OnlineMeeting body)
        {
            var httpResponseMessage = await this.httpClient.GetAsync("me/onlineMeetings");
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetMeetingAvailabilityAsync(FindMeetingTimesPostRequestBody body)
        {
            await Task.Delay(500);
            //var result = await this.graphServiceClient.Me.FindMeetingTimes.PostAsync(body);
            return null;
        }
    }
}
