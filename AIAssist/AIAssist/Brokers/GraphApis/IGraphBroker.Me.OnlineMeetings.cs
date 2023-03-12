namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<> PostCurrentUserMeetingAsync();

        public Task<> GetMeetingAvailability();
    }
}
