using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using GraphPOCBlazor.Models;
using Newtonsoft.Json;

namespace GraphPOCBlazor.Models
{
    public class GraphService
    {
        private HttpClient graphClient;

        public GraphService()
        {
            var clientHandler = new HttpClientHandler();

            graphClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://graph.microsoft.com/v1.0/")
            };

            graphClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJub25jZSI6IlVlV2I4SWtXeEhQdldkZWd0OFlpeERvU1RJWFA4ZHYycm9wSm1OQWhmdnciLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mZDYzZGMyNC04OGU2LTQxZDYtOWRjNy01MzQ5OTBiYjI2ZDYvIiwiaWF0IjoxNjc4NTk1NDQxLCJuYmYiOjE2Nzg1OTU0NDEsImV4cCI6MTY3ODYwMDA2NCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhUQUFBQUplQm9pbytPcmwzU2ZwWlhKZS9HRUdsN0ZkOVRhdm91WDVkMndaTXE3NDEzcUlJOEtiYkltOXd3aVFEcjFkNmciLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkdyYXBoIEV4cGxvcmVyIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6ImNvcnIiLCJnaXZlbl9uYW1lIjoieXVzdWYiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiI5OC4yMzcuMTg1LjIzNiIsIm5hbWUiOiJ5dXN1ZiBjb3JyIiwib2lkIjoiOTRlNjE1ZGQtYzM2Yy00MjRhLWI0NDktNmQzNGFjMTQ0YTdkIiwicGxhdGYiOiIzIiwicHVpZCI6IjEwMDMyMDAyN0ZFMDY0MkQiLCJyaCI6IjAuQVh3QUpOeGpfZWFJMWtHZHgxTkprTHNtMWdNQUFBQUFBQUFBd0FBQUFBQUFBQUM3QUl3LiIsInNjcCI6IkFQSUNvbm5lY3RvcnMuUmVhZC5BbGwgQVBJQ29ubmVjdG9ycy5SZWFkV3JpdGUuQWxsIG9wZW5pZCBwcm9maWxlIFVzZXIuUmVhZCBlbWFpbCBDYWxlbmRhcnMuUmVhZFdyaXRlIiwic3ViIjoiX0hnYTlUa3pOX0FnLTg1a3V2eGdkWWhDLWxMZ0N1b0RMMWJLZlVEaHhlbyIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJOQSIsInRpZCI6ImZkNjNkYzI0LTg4ZTYtNDFkNi05ZGM3LTUzNDk5MGJiMjZkNiIsInVuaXF1ZV9uYW1lIjoibXNncmFwaGlzYXNzQHN0aHkwLm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6Im1zZ3JhcGhpc2Fzc0BzdGh5MC5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJuRE9tamw0dV9FbVNmNE1lMVF3akFRIiwidmVyIjoiMS4wIiwid2lkcyI6WyI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6Im9hUThoQmg4dnBBTTY2YmlVZ1N1cFZpOURpdGtFbmM0TUt0Xy1vdWFZZk0ifSwieG1zX3RjZHQiOjE2Nzc5MTY5OTV9.sBNMMoXSQlQKwu0U5r_TVJmIKDSnl0f2VYRUs8qJNtzTOOkrh2LXEoSpSJMeuhh2zy3ooj0KmGPKeL93cnIbuBMxE8sPrMwxPXcBTd2X9f4B19PaN_ldY25USXhaWFhyhi-4oecd4cDY8N8Bw1ObLYRPZQKfo9jr_szsp1sStRpQFmQhLD29wxh-evDAhDgREfgMsPCp1wPdtTmL_TVxYgTJ8KMTHr599sU2rQL5oG3Mq_C9CONz_PBWaA0nLsHEck6TOF66UR2nfs-FPawNJlLIMjpL4EIXREGNKLuYi_oU_d5XuL1ONl5GPTLODKUDbxyEVnh0T8W2yfSPGs5Bog");
        }

        public async Task<User> GetUserData()
        {
            var response = await graphClient.GetAsync("me");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            return user;
        }
    }
}