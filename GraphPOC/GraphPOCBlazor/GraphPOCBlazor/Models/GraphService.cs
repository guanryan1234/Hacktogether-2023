using Microsoft.Graph;
using yeehee;
using Yusuf.Corr;

namespace GraphPOCBlazor.Data
{
    public class GraphService
    {
        public GraphService() 
        { 
           
        var user = await graphClient.Me
            .GetAsync();

        }
    }
}