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
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddLogging();

            services.AddSingleton<IOpenAIBroker, OpenAIBroker>(broker =>
            {
                var apiKey = Configuration.GetSection("AuthTokens")["OPENAI_API_KEY"];

                var openAIClient = new OpenAIClient(apiKey);
                return new OpenAIBroker(openAIClient);
            });

            services.AddSingleton<IGraphBroker, GraphBroker>(broker =>
            {
                string token = Configuration.GetSection("AuthTokens")["MS_GRAPH_BEARER_TOKEN"];
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
