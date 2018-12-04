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
    public static class getBooksList
    {
        [FunctionName("getBooksList")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "books")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Fired /books");

            var books = new string[] { "Book 1", "Book A", "Book ALA" };
            var booksJson = JsonConvert.SerializeObject(books);
            return (ActionResult)new OkObjectResult(booksJson);
        }
    }
}
