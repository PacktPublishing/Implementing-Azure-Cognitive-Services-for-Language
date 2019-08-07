using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TwitterAnalytics.Services;
using TwitterAnalytics.Helpers;

namespace TwitterAnalytics
{   public static class TwitterFunction
    {
        [FunctionName("twitterFunction")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "get", "post", 
            Route = null)] HttpRequest req,ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody))
            {
                return new BadRequestObjectResult("You must supply a request body");

            }

            log.LogInformation($"Original Tweet: { requestBody}");

            TweetService service = new TweetService();

           var result = service.ProcessTweet(requestBody);

           if(result == false)
            {
                return new BadRequestObjectResult($"We we're unable to process the Tweet: {requestBody}");
            }

            return (ActionResult)new OkObjectResult($"The Tweet: {requestBody}, was processed successfully");
        }


    }

}
