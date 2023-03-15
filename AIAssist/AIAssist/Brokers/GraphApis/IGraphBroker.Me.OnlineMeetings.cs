using Microsoft.Graph.Me.FindMeetingTimes;
using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<Event> PostCurrentUserMeetingAsync(string eventJson);

        public Task<HttpResponseMessage> GetMeetingAvailabilityAsync(FindMeetingTimesPostRequestBody body);
    }
}
