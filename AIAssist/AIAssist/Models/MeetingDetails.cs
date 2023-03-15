using Microsoft.Graph.Models;

namespace AIAssist.Models
{
    public class MeetingDetails
    {
        public string What { get; set; }
        public DateTimeOffset When { get; set; }
        public string Who { get; set;}
    }
}
