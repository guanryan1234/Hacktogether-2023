using AIAssist.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Graph.Models;
using System.Threading.Tasks;

namespace AIAssist.Services
{
    public partial class OchestrationService
    {
        private async Task<MeetingDetails> TryGetRetrieveStreamedCompletionAsync(TodoTask task) 
        {
            MeetingDetails meetingDetails = null;
            try
            {
                meetingDetails = await this.openAIService.RetrieveStreamedCompletionAsync(task.Title);
            }
            catch(ArgumentNullException exception)
            {
                // consume 
            }

            return meetingDetails;
        }

    }
}
