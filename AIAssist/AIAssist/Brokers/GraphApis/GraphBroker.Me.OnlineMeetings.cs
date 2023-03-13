using Microsoft.Graph.Me.FindMeetingTimes;
using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<OnlineMeeting> PostCurrentUserMeetingAsync(OnlineMeeting body)
        {
            var result = await this.graphServiceClient.Me.OnlineMeetings.PostAsync(body);
            return result;
        }

        public async Task<MeetingTimeSuggestionsResult> GetMeetingAvailability(FindMeetingTimesPostRequestBody body)
        {
            var result = await this.graphServiceClient.Me.FindMeetingTimes.PostAsync(body);
            return result;
        }
    }
}
