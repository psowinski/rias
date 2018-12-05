using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Rias.Infrastructure
{
    public static class getAllBooks
    {
        [FunctionName("getAllBooks")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "books")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Fired /books");

            var books = new string[] { "Book F1", "Book F2", "Book F3" };
            var booksJson = JsonConvert.SerializeObject(books);
            return (ActionResult)new OkObjectResult(booksJson);
        }
    }
}
