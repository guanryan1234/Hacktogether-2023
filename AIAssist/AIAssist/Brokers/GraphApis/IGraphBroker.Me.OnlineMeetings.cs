using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<OnlineMeeting> PostCurrentUserMeetingAsync(OnlineMeeting body);

        public Task<MeetingTimeSuggestionsResult> GetMeetingAvailability(FindMeetingTimesPostRequestBody body);
    }
}
