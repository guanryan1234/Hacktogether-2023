using Microsoft.Graph.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static Microsoft.Graph.Search.SearchRequestBuilder;
using System.Text;
using Microsoft.Graph.Search.Query;

namespace AIAssist.Brokers.GraphApis
{
    public partial class GraphBroker : IGraphBroker
    {
        public async Task<Person> GetCurrentUserAsync()
        {
            var httpResponseMessage = await this.httpClient.GetAsync("me");
            var person = JsonConvert.DeserializeObject<Person>(await httpResponseMessage.Content.ReadAsStringAsync());
            return person;
        }

        public async Task<Person> GetUserBasedOnTokenSearchAsync(string queryJson)
        {
            var content = new StringContent(queryJson, Encoding.UTF8, "application/json");
            var httpResponseMessage = await this.httpClient.PostAsync("search/query", content);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseString);
            return MapJObjectToPerson(responseObject);
        }

        private Person MapJObjectToPerson(JObject repsonseJson)
        {
            var personToken = repsonseJson["value"].First()["hitsContainers"].First()["hits"].First()["resource"];
            var personObject = JsonConvert.DeserializeObject<Person>(personToken.ToString());
            return personObject;
        }
    }
}
