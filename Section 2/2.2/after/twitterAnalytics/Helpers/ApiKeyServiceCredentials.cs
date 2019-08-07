using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace TwitterAnalytics.Helpers
{
    public class ApiKeyServiceCredentials: ServiceClientCredentials
    {
        private string _subscriptionKey;

        public ApiKeyServiceCredentials()
        {
        }

        public ApiKeyServiceCredentials(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }



          public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                return base.ProcessHttpRequestAsync(request, cancellationToken);
            }
    }
}
