using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace GraphPOCBlazor.Data
{
    public class GraphService
    {
        private GraphServiceClient graphClient;

        public GraphService() 
        {
            graphClient = Authorization();

        }

        public async Task<User> GetUserData()
        {
            return await graphClient.Me.GetAsync();
        }

        private GraphServiceClient Authorization()
        {
            //var scopes = new[] { "User.Read" };

            //// Multi-tenant apps can use "common",
            //// single-tenant apps must use the tenant ID from the Azure portal
            //var tenantId = "common";

            //// Value from app registration
            //var clientId = "YOUR_CLIENT_ID";

            //// using Azure.Identity;
            //var options = new TokenCredentialOptions
            //{
            //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            //};

            //// Callback function that receives the user prompt
            //// Prompt contains the generated device code that you must
            //// enter during the auth process in the browser
            //Func<DeviceCodeInfo, CancellationToken, Task> callback = (code, cancellation) => {
            //    Console.WriteLine(code.Message);
            //    return Task.FromResult(0);
            //};

            //// https://learn.microsoft.com/dotnet/api/azure.identity.devicecodecredential
            //var deviceCodeCredential = new DeviceCodeCredential(
            //    callback, tenantId, clientId, options);

            //var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);

            string[] scopes = { "User.Read" };

            InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions()
            {
            };

            InteractiveBrowserCredential interactiveBrowserCredential = new InteractiveBrowserCredential();

            this.graphClient = new GraphServiceClient(interactiveBrowserCredential, scopes); // you can pass the TokenCredential directly to the GraphServiceClient

            return graphClient;
        }
    }
}