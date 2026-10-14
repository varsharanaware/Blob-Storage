using AzureFun.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureFun
{
    public class OnSalesUploadWriteToQueue
    {
        private readonly ILogger<OnSalesUploadWriteToQueue> _logger;

        public OnSalesUploadWriteToQueue(ILogger<OnSalesUploadWriteToQueue> logger)
        {
            _logger = logger;
        }

        [Function("OnSalesUploadWriteToQueue")]
        [QueueOutput("SalesRequestInBound", Connection = "AzureWebJobsStorage")]
        public async Task<SalesRequestFunctionModel> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            SalesRequestFunctionModel? salesRequestData =   JsonConvert.DeserializeObject<Models.SalesRequestFunctionModel>(requestBody);

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return salesRequestData ?? new SalesRequestFunctionModel();
        }
    }
}
