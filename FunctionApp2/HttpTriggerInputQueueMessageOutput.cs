
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FunctionApp2
{
    public static class HttpTriggerInputQueueMessageOutput
    {
        [FunctionName("HttpTriggerInputQueueMessageOutput")]
        [return: Queue("myqueue-items", Connection = "AzureWebJobsStorage")]
        public static TextContainer Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, 
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return new TextContainer { Texto = $"Hola {name}" };
        }
    }

    public class TextContainer
    {
        public string Texto { get; set; }
    }
}
