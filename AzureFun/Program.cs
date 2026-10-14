using AzureFun.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {

        string? connectionString =
            context.Configuration["AzureSqlLocalDatabase"];
        //services.AddApplicationInsightsTelemetryWorkerService();
        //services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
