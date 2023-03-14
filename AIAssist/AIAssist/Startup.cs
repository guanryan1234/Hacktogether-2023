using AIAssist.Brokers.GraphApis;
using AIAssist.Services;
using Microsoft.Graph;
using OpenAI;

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
                
                return new GraphServiceClient();
            });
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
