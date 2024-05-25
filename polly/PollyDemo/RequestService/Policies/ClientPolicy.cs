using Polly;
using Polly.Retry;

namespace RequestService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateRertry { get;}

        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRertry { get; set; }

        public AsyncRetryPolicy<HttpResponseMessage> ExpHttpRertry { get; set; }

        public ClientPolicy()
        {
            ImmediateRertry = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                                    .RetryAsync(5);

            LinearHttpRertry = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                                    .WaitAndRetryAsync(retryCount: 5, retryAttempt => TimeSpan.FromSeconds(3));

            ExpHttpRertry = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                        .WaitAndRetryAsync(retryCount: 5, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(30,retryAttempt)));
        }

    }
}
