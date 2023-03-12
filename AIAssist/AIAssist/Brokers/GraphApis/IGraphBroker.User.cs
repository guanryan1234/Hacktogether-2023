using Microsoft.Graph.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IGraphBroker
    {
        public Task<User> GetUserBasedOnTokenAsync(string userIdentityToken);

        public Task<User> GetCurrentUserAsync();
    }
}
