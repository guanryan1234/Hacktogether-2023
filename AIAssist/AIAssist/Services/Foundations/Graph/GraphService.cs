using AIAssist.Brokers.GraphApis;
using Azure.Identity;
using Microsoft.Graph;
using System.Runtime.CompilerServices;

namespace AIAssist.Services.Foundations.Graph
{
    public class GraphService : IGraphService
    {
        private IGraphBroker graphBroker;

        public GraphService(IGraphBroker graphBroker)
        {
            this.graphBroker = graphBroker;
        }

        public async Task Test()
        {
            await Task.Delay(100);
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "common";

            // Value from app registration
            var clientId = "YOUR_CLIENT_ID";

            // using Azure.Identity;
            var options = new InteractiveBrowserCredentialOptions
            {
                TenantId = tenantId,
                ClientId = clientId,
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                // MUST be http://localhost or http://localhost:PORT
                // See https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core
                RedirectUri = new Uri("http://localhost"),
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.interactivebrowsercredential
            var interactiveCredential = new InteractiveBrowserCredential(options);

            var graphClient = new GraphServiceClient(interactiveCredential, scopes);

            var result = graphClient.Me.Todo.Lists.GetAsync("");
            return;
        } 
    }
}
