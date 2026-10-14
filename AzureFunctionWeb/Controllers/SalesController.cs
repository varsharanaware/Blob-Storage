using AzureFunctionWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureFunctionWeb.Controllers
{
    public class SalesController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public SalesController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public IActionResult createRequest()
        {
            return View();
        }


        //http://localhost:7090/api/OnSalesUploadWriteToQueue
        [HttpPost]
        public async Task<IActionResult> createRequest(SalesRequest salesRequest)
        {
            var client = _factory.CreateClient();
            salesRequest.Id = Guid.NewGuid().ToString();    
            client.BaseAddress = new Uri("http://localhost:7090/api/");
            using (var content = new StringContent(JsonConvert.SerializeObject(salesRequest),System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage response = await client.PostAsync("OnSalesUploadWriteToQueue", content);
                string returnValue = await response.Content.ReadAsStringAsync();
            }

            return RedirectToAction(nameof(createRequest));
        }
    }
}
