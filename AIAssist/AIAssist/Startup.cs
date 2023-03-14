using AIAssist.Brokers.GraphApis;
using AIAssist.Services;
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
            AddRootDirectory(services);
            services.AddLogging();
            services.AddSingleton<WeatherForecastService, WeatherForecastService>();

            services.AddSingleton<IOpenAIBroker, OpenAIBroker>(broker =>
            {
                var apiKey = Environment
                 .GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User);

                var openAIClient = new OpenAIClient(apiKey);
                return new OpenAIBroker(openAIClient);
            });

            services.AddSingleton<IGraphBroker, GraphBroker>(broker =>
            {
                string token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6ImRnRzhTUzk2QTZzQ0llQ1pZYVBvMHJqSmo0UDRtM2hRUmp2WjZaS19NaEUiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mZDYzZGMyNC04OGU2LTQxZDYtOWRjNy01MzQ5OTBiYjI2ZDYvIiwiaWF0IjoxNjc4ODA4OTQwLCJuYmYiOjE2Nzg4MDg5NDAsImV4cCI6MTY3ODgxNDQyMywiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhUQUFBQUhaTlBzYUIyOWdIYnhia0N3eXRuLzdVQmc0aGVCaWwzN3FIMWEwaVduaEcyZFJ6REI5emlJMHlzVVFnNGovNDciLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkdyYXBoIEV4cGxvcmVyIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6ImNvcnIiLCJnaXZlbl9uYW1lIjoieXVzdWYiLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiI0NS4xNDMuODIuODIiLCJuYW1lIjoieXVzdWYgY29yciIsIm9pZCI6Ijk0ZTYxNWRkLWMzNmMtNDI0YS1iNDQ5LTZkMzRhYzE0NGE3ZCIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMjdGRTA2NDJEIiwicmgiOiIwLkFYd0FKTnhqX2VhSTFrR2R4MU5Ka0xzbTFnTUFBQUFBQUFBQXdBQUFBQUFBQUFDN0FJdy4iLCJzY3AiOiJBUElDb25uZWN0b3JzLlJlYWQuQWxsIEFQSUNvbm5lY3RvcnMuUmVhZFdyaXRlLkFsbCBDYWxlbmRhcnMuUmVhZFdyaXRlIG9wZW5pZCBwcm9maWxlIFRhc2tzLlJlYWQgVGFza3MuUmVhZFdyaXRlIFVzZXIuUmVhZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJrbXNpIl0sInN1YiI6Il9IZ2E5VGt6Tl9BZy04NWt1dnhnZFloQy1sTGdDdW9ETDFiS2ZVRGh4ZW8iLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiTkEiLCJ0aWQiOiJmZDYzZGMyNC04OGU2LTQxZDYtOWRjNy01MzQ5OTBiYjI2ZDYiLCJ1bmlxdWVfbmFtZSI6Im1zZ3JhcGhpc2Fzc0BzdGh5MC5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJtc2dyYXBoaXNhc3NAc3RoeTAub25taWNyb3NvZnQuY29tIiwidXRpIjoid1JpSXFfcjlka2VUdFF0d3JVWWtBUSIsInZlciI6IjEuMCIsIndpZHMiOlsiNjJlOTAzOTQtNjlmNS00MjM3LTkxOTAtMDEyMTc3MTQ1ZTEwIiwiYjc5ZmJmNGQtM2VmOS00Njg5LTgxNDMtNzZiMTk0ZTg1NTA5Il0sInhtc19zdCI6eyJzdWIiOiJvYVE4aEJoOHZwQU02NmJpVWdTdXBWaTlEaXRrRW5jNE1LdF8tb3VhWWZNIn0sInhtc190Y2R0IjoxNjc3OTE2OTk1fQ.nd2TlxDl1MqLkvoDOnkhz9gAi2Y6WGVMPo0TNq3sKOUGYgRd-FyqI9eGVwp9B_o6pIRJ9_gTcDrdSgQrGwO3iWWm01NJ5JxUixkRnDhyk2sLJvikMf8TaOQunMBPdvdQ9LWCor0ituZC3Mw6Xn5YXmFFiG9aTR7F1y06rNyNpvJBMxwtzGU80GnW1I0cKzIONa5XU8x1UecfgMMxYe4YHst3qC5MNGlXWy-Eo45XEs_OpdUSkCq4oBwVCkhbky0Qofu6v5PBzcOJkNTaTQFj7l5G9Xu0fjAtUDfXsH9IiFpDxJZtJsN11Z6yHIwI7n298AohbMfPYqKEAD8zrkGpMg";
                var clientHandler = new HttpClientHandler();

                var httpClient = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri("https://graph.microsoft.com/v1.0/")
                };

                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                return new GraphBroker(httpClient);
            });

            services.AddSingleton<OchestrationService, OchestrationService>();
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
