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

            var books = new [] { 
                new { name = "Book F1", date = "2018-10-01" },
                new { name = "Book F2", date = "2018-10-02" },
                new { name = "Book F3", date = "2018-10-03" }
                };
                
            return new JsonResult(books);
        }
    }
}
