using System;
using Azure.Storage.Queues.Models;
using AzureFun.Data;
using AzureFun.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFun;

public class OnQueueTriggerUpdateDatabase
{
    private readonly ILogger<OnQueueTriggerUpdateDatabase> _logger;
    private readonly ApplicationDbContext _applicationDbContext;
    public OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    [Function(nameof(OnQueueTriggerUpdateDatabase))]
    public void Run([QueueTrigger("SalesRequestInBound")] QueueMessage message)  // listen to the queue named "SalesRequestInBound"
    {
        string messageBody = message.Body.ToString();

        SalesRequestFunctionModel? salesRequest = System.Text.Json.JsonSerializer.Deserialize<SalesRequestFunctionModel>(messageBody);
        if (salesRequest != null)
        {
            _applicationDbContext.SalesRequests.Add(salesRequest);
            _applicationDbContext.SaveChanges();
        }
        else
        {
            _logger.LogError("Failed to deserialize message body: {messageBody}", messageBody);
        }

       
    }
}