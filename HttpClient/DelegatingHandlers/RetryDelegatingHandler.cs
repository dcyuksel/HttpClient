using Polly;

namespace HttpClient.DelegatingHandlers;

public class RetryDelegatingHandler : DelegatingHandler
{
    private const int MaxRetries = 3;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var retryPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
        var policyResult = await retryPolicy.ExecuteAndCaptureAsync(() => base.SendAsync(request, cancellationToken));
        if (policyResult.Outcome == OutcomeType.Failure)
        {
            throw new HttpRequestException("Http request is failed.", policyResult.FinalException);
        }

        return policyResult.Result;
    }
}
