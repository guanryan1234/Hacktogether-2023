using AIAssist.Brokers.GraphApis;
using AIAssist.Brokers.OpenAIApis;
using AIAssist.Services;
using AIAssist.Services.Foundations.Graph;
using AIAssist.Services.Foundations.OpenAI;
using Azure.Identity;
using Microsoft.Graph;
using OpenAI;
using System.Net.Http.Headers;

namespace AIAssist
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddLogging();

            services.AddSingleton<IOpenAIBroker, OpenAIBroker>(broker =>
            {
                var apiKey = Environment
                 .GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User);

                var openAIClient = new OpenAIClient(apiKey);
                return new OpenAIBroker(openAIClient);
            });

            services.AddSingleton<IGraphBroker, GraphBroker>(broker =>
            {
                string token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IldhVVNDSGZkS3JrTGRXMW9SV0FqRXBrU3RRdlp5bkhyYWxQelZCblgwU2MiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mZDYzZGMyNC04OGU2LTQxZDYtOWRjNy01MzQ5OTBiYjI2ZDYvIiwiaWF0IjoxNjc4ODUyNDc0LCJuYmYiOjE2Nzg4NTI0NzQsImV4cCI6MTY3ODg1NzYyMywiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkUyWmdZREIzL3NidjRMayt4Tk1qZkpuT3YxZUdMY0ZzRlZaMktxMzFFMktuM0gybjZ2UzgwbE9QcjJRbnE4cHJYWWI0MEtPWEFRPT0iLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkdyYXBoIEV4cGxvcmVyIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6ImNvcnIiLCJnaXZlbl9uYW1lIjoieXVzdWYiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiI2Ny4xNjEuOTYuMTciLCJuYW1lIjoieXVzdWYgY29yciIsIm9pZCI6Ijk0ZTYxNWRkLWMzNmMtNDI0YS1iNDQ5LTZkMzRhYzE0NGE3ZCIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMjdGRTA2NDJEIiwicmgiOiIwLkFYd0FKTnhqX2VhSTFrR2R4MU5Ka0xzbTFnTUFBQUFBQUFBQXdBQUFBQUFBQUFDN0FJdy4iLCJzY3AiOiJBY3JvbnltLlJlYWQuQWxsIEFQSUNvbm5lY3RvcnMuUmVhZC5BbGwgQVBJQ29ubmVjdG9ycy5SZWFkV3JpdGUuQWxsIEJvb2ttYXJrLlJlYWQuQWxsIENhbGVuZGFycy5SZWFkIENhbGVuZGFycy5SZWFkV3JpdGUgQ2FsZW5kYXJzLlJlYWRXcml0ZS5TaGFyZWQgQ2hhbm5lbC5SZWFkQmFzaWMuQWxsIENoYW5uZWxNZXNzYWdlLlJlYWQuQWxsIENoYXQuUmVhZCBFeHRlcm5hbEl0ZW0uUmVhZC5BbGwgRmlsZXMuUmVhZC5BbGwgTWFpbC5SZWFkIE9ubGluZU1lZXRpbmdzLlJlYWRXcml0ZSBvcGVuaWQgUGVvcGxlLlJlYWQgUGVvcGxlLlJlYWQuQWxsIHByb2ZpbGUgU2l0ZXMuUmVhZC5BbGwgVGFza3MuUmVhZCBUYXNrcy5SZWFkV3JpdGUgVXNlci5SZWFkIGVtYWlsIiwic2lnbmluX3N0YXRlIjpbImttc2kiXSwic3ViIjoiX0hnYTlUa3pOX0FnLTg1a3V2eGdkWWhDLWxMZ0N1b0RMMWJLZlVEaHhlbyIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJOQSIsInRpZCI6ImZkNjNkYzI0LTg4ZTYtNDFkNi05ZGM3LTUzNDk5MGJiMjZkNiIsInVuaXF1ZV9uYW1lIjoibXNncmFwaGlzYXNzQHN0aHkwLm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6Im1zZ3JhcGhpc2Fzc0BzdGh5MC5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJkVTJxTU5SZlgwcU1rMkZJX0w1MkFRIiwidmVyIjoiMS4wIiwid2lkcyI6WyI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6Im9hUThoQmg4dnBBTTY2YmlVZ1N1cFZpOURpdGtFbmM0TUt0Xy1vdWFZZk0ifSwieG1zX3RjZHQiOjE2Nzc5MTY5OTV9.qzBQUfWBx0uKh8WBKzlrjghnrjNcYOVzfP58RGJ-qo1k3LEadrjVa8qmtlwpXoSG1uzUfSYdJ8nOGrs6WIhVK25uv-LMwS80MjS0tqzx06-mvoz8r4lGRujUFISzyEmIbDbSWEvDTloO_XuiFY7x8t3sFmYpoCdwG4neeBDFJJv_E8-MzuNdwxU5sT-T4_92BeeCYF-AIgjCYtpUZeLgdvVL31SfYtAOM_4eR0OeniWjMcGXHhYDHXotL6Hi3zUWbkT0VNMOekGDn3zz4VJaX2PPGpYVKPMRiNX1pYANTwP4BFa0ACCMeCHiO5UO-QZdFavpiPaeCVvS2MEreSSeOA";
                var clientHandler = new HttpClientHandler();

                var httpClient = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri("https://graph.microsoft.com/v1.0/")
                };

                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                return new GraphBroker(httpClient);
            });

            services.AddSingleton<IOpenAIService, OpenAIService>();
            services.AddSingleton<IGraphService, GraphService>();
            services.AddSingleton<OchestrationService, OchestrationService>();
            AddRootDirectory(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void AddRootDirectory(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.RootDirectory = "/Pages";
            });
        }
    }
}
