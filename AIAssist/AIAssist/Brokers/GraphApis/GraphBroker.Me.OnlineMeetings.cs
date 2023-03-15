using Microsoft.Graph.Me.FindMeetingTimes;
using Microsoft.Graph.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<Event> PostCurrentUserMeetingAsync(string eventJson)
        {
            var content = new StringContent(eventJson, Encoding.UTF8, "application/json");
            var httpResponseMessage = await this.httpClient.PostAsync("me/events", content);
            return JsonConvert.DeserializeObject<Event>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> GetMeetingAvailabilityAsync(FindMeetingTimesPostRequestBody body)
        {
            await Task.Delay(500);
            //var result = await this.graphServiceClient.Me.FindMeetingTimes.PostAsync(body);
            return null;
        }
    }
}
